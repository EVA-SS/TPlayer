using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using TPlayerSupport;

namespace TPlayer.Helper
{
    public class DownCore
    {
        public object Tag = null;
        public static TaskFactory _task = new TaskFactory();
        public string taskID { get { return _taskID; } }
        string _taskID = null;

        #region 任务事件

        public delegate void StringHandler(DownCore core, string e);
        public delegate void StateHandler(DownCore core, DownState e);
        public delegate void DoubleHandler(DownCore core, double e);
        public delegate void IntHandler(DownCore core, int e);

        double _MaxValue, _Value, _Speed;
        string _Name, _Time;

        #region 文件大小

        public double MaxValue
        {
            get { return _MaxValue; }
        }
        private void SetMaxValue(double value)
        {
            if (_MaxValue != value)
            {
                _MaxValue = value;
                DownTotalCore.ChangeMaxValueTask(_taskID, value);
                if (MaxValueChange != null)
                {
                    MaxValueChange(this, value);
                }
            }
        }
        private void SetMaxValue()
        {
            lock (TotalSizeTemp)
            {
                SetMaxValue(TotalSizeTemp.Sum(ab => ab.Value) * TotalCount / DownCount);
            }
            //try
            //{
            //}
            //catch
            //{ }
        }
        /// <summary>
        /// 文件大小改变
        /// </summary>
        public event DoubleHandler MaxValueChange;

        #endregion

        #region 当前下载值

        public double Value
        {
            get { return _Value; }
        }
        private void SetValue(double value)
        {
            if (_Value != value)
            {
                _Value = value;
                DownTotalCore.ChangeValueTask(_taskID, value);
                if (ValueChange != null)
                {
                    ValueChange(this, value);
                }
            }
        }
        private void SetValue()
        {
            lock (DownSizeTemp)
            {
                SetValue(DownSizeTemp.Sum(ab => ab.Value));
            }
            //try
            //{
            //    SetValue(DownSizeTemp.Sum(ab => ab.Value));
            //}
            //catch { }
        }

        /// <summary>
        /// 当前下载值改变
        /// </summary>
        public event DoubleHandler ValueChange;
        #endregion

        #region 文件下载名称

        public string Name
        {
            get { return _Name; }
        }
        private void SetName(string value)
        {
            if (_Name != value)
            {
                _Name = value;
                if (NameChange != null)
                {
                    NameChange(this, value);
                }
            }
        }

        /// <summary>
        /// 文件下载名称改变
        /// </summary>
        public event StringHandler NameChange;
        #endregion

        #region 文件下载速度

        public double Speed
        {
            get { return _Speed; }
        }
        private void SetSpeed(double value)
        {
            if (_Speed != value)
            {
                _Speed = value;
                if (SpeedChange != null)
                {
                    SpeedChange(this, value);
                }
            }
        }

        /// <summary>
        /// 文件下载速度改变
        /// </summary>
        public event DoubleHandler SpeedChange;
        #endregion

        #region 文件剩余时间

        public string Time
        {
            get { return _Time; }
        }
        private void SetTime(string value)
        {
            if (_Time != value)
            {
                _Time = value;
                if (TimeChange != null)
                {
                    TimeChange(this, value);
                }
            }
        }

        /// <summary>
        /// 文件剩余时间改变
        /// </summary>
        public event StringHandler TimeChange;

        #endregion

        #region 文件状态

        private DownState _State = DownState.Ready;
        public DownState State
        {
            get { return _State; }
        }
        private void SetState(DownState value)
        {
            if (_State != value)
            {
                _State = value;
                if (StateChange != null)
                {
                    StateChange(this, value);
                }
            }
        }

        /// <summary>
        /// 文件剩余时间改变
        /// </summary>
        public event StateHandler StateChange;
        public enum DownState
        {
            /// <summary>
            /// 准备中
            /// </summary>
            Ready,
            /// <summary>
            /// 下载中
            /// </summary>
            Downloading,
            /// <summary>
            /// 已停止
            /// </summary>
            Stop,
            /// <summary>
            /// 完成
            /// </summary>
            Complete,
            /// <summary>
            /// 异常
            /// </summary>
            Fail
        }

        #endregion

        #endregion

        #region 方法

        /// <summary>
        /// 暂停下载
        /// </summary>
        public void Suspend()
        {
            resetEvent.Reset();
            SetState(DownState.Stop);
        }
        /// <summary>
        /// 恢复下载
        /// </summary>
        public void Resume()
        {
            SetState(DownState.Downloading);
            resetEvent.Set();
        }

        private bool isStop = false;
        /// <summary>
        /// 停止下载
        /// </summary>
        public void Stop()
        {
            isStop = true;
            //终止task线程
            tokenSource.Cancel();
            resetEvent.Dispose();
            tokenSource.Dispose();

            //thread.Abort();
            //SetState(DownState.Stopping);
        }
        #endregion

        #region 任务核心

        public bool DownInit(string downUrl, string FName, out string errmsg)
        {
            isStop = false;
            try { uri = new Uri(downUrl); } catch { }
            if (uri == null)
            {
                SetState(DownState.Fail);
                errmsg = "地址错误";
                return false;
            }
            string fileName = Global.WebFileNameLength(uri, out _Length, out canSeek);
            if (!string.IsNullOrEmpty(FName))
            {
                SetName(FName + Path.GetExtension(fileName));
            }
            else
            {
                SetName(fileName);
            }
            errmsg = null;
            return true;
        }
        Uri uri = null;
        public long _Length;
        public bool canSeek;
        public bool DownUrl(string savePath, string workPath, out string errmsg)
        {
            if (uri != null)
            {
                _taskID = (DateTime.Now.Ticks + uri.AbsoluteUri).Md5_16();
                //int maxThreads = Environment.ProcessorCount / 2;
                int threadCount = SystemSettings.DownloadTaskCount;
                bool _result;
                DownTotalCore.AddTask(_taskID);
                if (uri.AbsoluteUri.ToLower().EndsWith("m3u8"))
                {
                    //M3u8下载
                    _result = DownUrlByM3u8(threadCount, savePath, workPath, out errmsg);
                }
                else
                {
                    _result = DownUrlByPlain(threadCount, savePath, workPath, out errmsg);
                }
                DownTotalCore.DelTask(_taskID);
                return _result;
            }
            else
            {
                SetState(DownState.Fail);
                errmsg = "请初始化";
                return false;
            }
        }

        #region 下载普通任务

        /// <summary>
        /// 普通下载
        /// </summary>
        /// <param name="threads">线程数</param>
        /// <param name="savePath">保存文件</param>
        /// <param name="workPath">工作目录</param>
        /// <param name="errmsg"></param>
        /// <returns></returns>
        bool DownUrlByPlain(int threads, string savePath, string workPath, out string errmsg)
        {
            SetState(DownState.Downloading);
            workPath.CreateDirectory(true);//新建文件夹

            if (_Length > 0 || _Length == -1)
            {
                SetMaxValue(_Length);

                List<FilesResult> files = new List<FilesResult>();

                #region 任务分配

                double _down_length = _MaxValue;
                int _taskcount = 1;
                if (canSeek && _MaxValue > 0 && _MaxValue > 2097152)
                {
                    _down_length = 2097152;
                    _taskcount = (int)Math.Ceiling(_MaxValue / _down_length);//任务分块
                }
                lock (DownSizeTemp)
                {
                    for (int i = 0; i < _taskcount; i++)
                    {
                        double _s = _down_length * i;
                        double _e = _down_length;
                        if ((_s + _down_length) > _MaxValue)
                        {
                            _e = _down_length - ((_s + _down_length) - _MaxValue);
                        }

                        string filename_temp = $"{i}_{_s}_{_s + _e}.tsdownloading";
                        files.Add(new FilesResult
                        {
                            index = i,
                            path = workPath + filename_temp,
                            start_position = _s,
                            end_position = _e
                        });

                        DownSizeTemp.Add(i, 0);
                    }
                }

                #endregion

                TestTime(false);
                bool isComplete = false;
                Action _action = () =>
                {
                    foreach (FilesResult item in files)
                    {
                        lock (files)
                        {
                            if (files.Count(ab => ab.run) > threads)
                            {
                                resetEvents.Reset();//暂停
                            }
                        }
                        resetEvents.WaitOne();

                        double _downvalueTemp = 0;
                        item.run = item.runTask = true;
                        Action _actions = () =>
                        {
                            if (tokenSource.Token.IsCancellationRequested)
                            {
                                return;
                            }
                            //阻止当前线程
                            resetEvent.WaitOne();

                            int ErrCount = 0;
                            while (item.runTask)
                            {
                                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
                                request.Host = uri.Host;
                                request.Accept = "*/*";
                                request.UserAgent = "Mozilla/5.0";
                                request.Method = "GET";
                                if (SystemSettings.DownloadTimeOut > 0)
                                {
                                    request.Timeout = SystemSettings.DownloadTimeOut;
                                }
                                request.ReadWriteTimeout = request.Timeout; //重要
                                request.AllowAutoRedirect = true;
                                request.KeepAlive = false;
                                try
                                {

                                    long fileLong = 0;

                                    using (FileStream file = new FileStream(item.path, FileMode.OpenOrCreate))
                                    {
                                        fileLong = file.Length;
                                        if (item.end_position > 0 && file.Length >= item.end_position)
                                        {
                                            _downvalueTemp += fileLong;
                                            //_downvalue += fileLong;
                                            lock (DownSizeTemp)
                                            {
                                                DownSizeTemp[item.index] = fileLong;
                                            }

                                            SetValue();
                                            item.complete = true;
                                            return;
                                        }
                                        else if (!canSeek)
                                        {
                                            file.Close();
                                            fileLong = 0;
                                            File.Delete(item.path);
                                        }
                                    }


                                    using (FileStream file = new FileStream(item.path, FileMode.OpenOrCreate))
                                    {
                                        if (canSeek)
                                        {
                                            request.AddRange((long)(item.start_position + file.Length), (long)(item.start_position + item.end_position));
                                        }
                                        using (HttpWebResponse p = (HttpWebResponse)request.GetResponse())
                                        {
                                            using (Stream stream = p.GetResponseStream())
                                            {
                                                if (fileLong > 0)
                                                {
                                                    file.Seek(fileLong, SeekOrigin.Begin);

                                                    _downvalueTemp += fileLong;
                                                    //_downvalue += fileLong;
                                                    lock (DownSizeTemp)
                                                    {
                                                        DownSizeTemp[item.index] = fileLong;
                                                    }
                                                    SetValue();
                                                }
                                                byte[] _cache = new byte[SystemSettings.DownloadCacheCount];
                                                int osize = stream.Read(_cache, 0, _cache.Length);
                                                bool isRun = true;
                                                while (isRun)
                                                {
                                                    if (osize > 0)
                                                    {
                                                        if (tokenSource.Token.IsCancellationRequested)
                                                        {
                                                            return;
                                                        }
                                                        //阻止当前线程
                                                        resetEvent.WaitOne();


                                                        _downvalueTemp += osize;
                                                        //_downvalue += osize;
                                                        lock (DownSizeTemp)
                                                        {
                                                            DownSizeTemp[item.index] = _downvalueTemp;
                                                        }
                                                        SetValue();

                                                        file.Write(_cache, 0, osize);
                                                        osize = stream.Read(_cache, 0, _cache.Length);
                                                    }
                                                    else { isRun = false; }

                                                }
                                                //file.Seek(0, SeekOrigin.Begin);
                                            }

                                        }
                                    }
                                    item.complete = true;
                                }
                                catch
                                {
                                    request.Abort();
                                    if (isStop)
                                    {
                                        item.runTask = false;
                                        return;
                                    }
                                    ErrCount++;

                                    _downvalueTemp = 0;
                                    lock (DownSizeTemp)
                                    {
                                        DownSizeTemp[item.index] = 0;
                                    }
                                    SetValue();

                                    //System.Diagnostics.Debug.WriteLine("序号：" + item.index + "|次数：" + ErrCount);
                                    if (ErrCount > SystemSettings.DownloadRetryCount)
                                    {
                                        item.runTask = false;
                                    }
                                }
                            }
                        };
                        var task = Task.Run(_actions, tokenSource.Token).ContinueWith(t =>
                        {
                            item.run = false;
                            lock (files)
                            {
                                int tackCount = files.Count(ab => ab.run);
                                if (tackCount < threads)
                                {
                                    resetEvents.Set();//继续
                                }
                            }
                        }, TaskContinuationOptions.OnlyOnRanToCompletion | TaskContinuationOptions.ExecuteSynchronously);
                        ParallelTasks.Add(task);

                        resetEvents.WaitOne();
                    }
                    _task.ContinueWhenAll(ParallelTasks.ToArray(), (action =>
                    {
                        isdown = false;
                        if (tokenSource.Token.IsCancellationRequested)
                        {
                            return;
                        }
                        while (isTimeTask)
                        {
                            Thread.Sleep(500);
                        }
                        List<string> _files = new List<string>();
                        lock (files)
                        {
                            foreach (FilesResult item in files)
                            {
                                if (!item.complete)
                                {
                                    return;
                                }
                                _files.Add(item.path);
                            }
                        }

                        Global.CombineMultipleFilesIntoSingleFile(_files, savePath);
                        Directory.Delete(workPath, true);
                        isComplete = true;
                        SetState(DownState.Complete);
                    })).Wait();
                };
                _task.StartNew(_action).Wait();
                if (isComplete)
                {
                    errmsg = null;
                    return true;
                }
                else
                {
                    SetState(DownState.Fail);
                    errmsg = "文件下载失败";
                    return false;
                }
            }
            else
            {
                SetState(DownState.Fail);
                errmsg = "文件长度异常";
                return false;
            }
        }

        #endregion

        #region 下载M3u8任务
        /// <summary>
        /// M3u8下载
        /// </summary>
        /// <param name="threads">线程数</param>
        /// <param name="savePath">保存文件</param>
        /// <param name="workPath">工作目录</param>
        /// <param name="errmsg"></param>
        /// <returns></returns>
        bool DownUrlByM3u8(int threads, string savePath, string workPath, out string errmsg)
        {
            //解析M3u8
            Parser parser = new Parser
            {
                DownName = _Name,
                M3u8Url = uri.AbsoluteUri,
                DownDir = workPath
            };
            string Err;
            if (parser.Parse(out Err))   //开始解析
            {
                if (File.Exists(parser.jsonSavePath))
                {
                    JObject initJson = JObject.Parse(File.ReadAllText(parser.jsonSavePath));
                    bool isVOD = Convert.ToBoolean(initJson["m3u8Info"]["vod"].ToString());

                    JArray parts = JArray.Parse(initJson["m3u8Info"]["segments"].ToString()); //大分组
                    string segCount = initJson["m3u8Info"]["count"].ToString();
                    string oriCount = initJson["m3u8Info"]["originalCount"].ToString(); //原始分片数量

                    int total = Convert.ToInt32(segCount);
                    int PartsCount = parts.Count;
                    string segsPadZero = string.Empty.PadRight(oriCount.Length, '0');
                    string partsPadZero = string.Empty.PadRight(Convert.ToString(parts.Count).Length, '0');
                    TotalCount = total;
                    //点播
                    if (isVOD)
                    {
                        List<FilesResult> files = new List<FilesResult>();

                        workPath.CreateDirectory(true);//新建文件夹

                        #region 任务分配

                        //构造包含所有分片的新的segments
                        JArray segments = new JArray();
                        for (int i = 0; i < parts.Count; i++)
                        {
                            var tmp = JArray.Parse(parts[i].ToString());
                            for (int j = 0; j < tmp.Count; j++)
                            {
                                JObject t = (JObject)tmp[j];
                                t.Add("part", i);
                                segments.Add(t);
                            }
                        }

                        bool isVTT = false;
                        for (int i = 0; i < segments.Count; i++)
                        {
                            JToken info = segments[i];
                            string TsUrl = info["segUri"].Value<string>();
                            //VTT字幕
                            if (isVTT == false && TsUrl.Trim('\"').EndsWith(".vtt"))
                                isVTT = true;
                            string Method = info["method"].Value<string>();
                            string Iv = null;
                            long StartByte = 0, ExpectByte = 0;
                            if (Method != "NONE")
                            {
                                Iv = info["iv"].Value<string>();
                            }
                            try
                            {
                                ExpectByte = info["expectByte"].Value<long>();
                            }
                            catch { }
                            try
                            {
                                StartByte = info["startByte"].Value<long>();
                            }
                            catch { }

                            string Dir = workPath + "Part_" + info["part"].Value<int>().ToString(partsPadZero) + "\\";

                            Dir.CreateDirectory(true);

                            string filename_temp = info["index"].Value<int>().ToString(segsPadZero) + ".ts";
                            files.Add(new FilesResult
                            {
                                index = i,
                                tsurl = TsUrl,
                                path = Dir + filename_temp,
                                start_position = StartByte,
                                end_position = ExpectByte,
                            });
                            lock (DownSizeTemp)
                            {
                                DownSizeTemp.Add(i, 0);
                            }
                        }
                        #endregion

                        SetState(DownState.Downloading);
                        TestTime(true);
                        bool isComplete = false;


                        Action _action = () =>
                        {
                            foreach (FilesResult item in files)
                            {
                                lock (files)
                                {
                                    //int counts = files.Count(ab => ab.run);
                                    if (files.Count(ab => ab.run) > threads)
                                    {
                                        resetEvents.Reset();//暂停
                                    }
                                }
                                resetEvents.WaitOne();

                                double _downvalueTemp = 0;
                                item.run = item.runTask = true;
                                Action _actions = () =>
                                {
                                    if (tokenSource.Token.IsCancellationRequested)
                                    {
                                        return;
                                    }
                                    //阻止当前线程
                                    resetEvent.WaitOne();


                                    Uri TSuri = new Uri(item.tsurl);

                                    int ErrCount = 0;
                                    while (item.runTask)
                                    {
                                        try
                                        {
                                            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(TSuri);
                                            request.Host = TSuri.Host;
                                            request.Accept = "*/*";
                                            request.UserAgent = "Mozilla/5.0";
                                            request.Method = "GET";
                                            if (SystemSettings.DownloadTimeOut > 0)
                                            {
                                                request.Timeout = SystemSettings.DownloadTimeOut;
                                            }
                                            request.ReadWriteTimeout = request.Timeout; //重要
                                            request.AllowAutoRedirect = true;
                                            request.KeepAlive = false;

                                            using (HttpWebResponse p = (HttpWebResponse)request.GetResponse())
                                            {
                                                lock (TotalSizeTemp)
                                                {
                                                    if (!TotalSizeTemp.ContainsKey(item.index))
                                                    {
                                                        using (FileStream file = new FileStream(item.path, FileMode.OpenOrCreate))
                                                        {
                                                            long fileLong = p.ContentLength;
                                                            if (fileLong > 0 && file.Length >= fileLong)
                                                            {
                                                                _downvalueTemp += fileLong;
                                                                lock (DownSizeTemp)
                                                                {
                                                                    DownSizeTemp[item.index] = fileLong;
                                                                }

                                                                TotalSizeTemp.Add(item.index, fileLong);

                                                                DownCount++;

                                                                SetValue();
                                                                return;
                                                            }
                                                        }
                                                    }
                                                }
                                                using (FileStream file = new FileStream(item.path, FileMode.Create))
                                                {
                                                    if (item.start_position > 0 && item.end_position > 0)
                                                    {
                                                        request.AddRange((long)item.start_position, (long)item.end_position);
                                                    }
                                                    lock (TotalSizeTemp)
                                                    {
                                                        if (!TotalSizeTemp.ContainsKey(item.index))
                                                        {
                                                            TotalSizeTemp.Add(item.index, p.ContentLength);
                                                        }
                                                    }
                                                    using (Stream stream = p.GetResponseStream())
                                                    {
                                                        byte[] _cache = new byte[SystemSettings.DownloadCacheCount];
                                                        int osize = stream.Read(_cache, 0, _cache.Length);


                                                        bool isRun = true;
                                                        while (isRun)
                                                        {
                                                            if (osize > 0)
                                                            {
                                                                if (tokenSource.Token.IsCancellationRequested)
                                                                {
                                                                    return;
                                                                }
                                                                //阻止当前线程
                                                                resetEvent.WaitOne();
                                                                _downvalueTemp += osize;
                                                                lock (DownSizeTemp)
                                                                {
                                                                    DownSizeTemp[item.index] = _downvalueTemp;
                                                                }
                                                                SetValue();
                                                                file.Write(_cache, 0, osize);
                                                                osize = stream.Read(_cache, 0, _cache.Length);
                                                            }
                                                            else { isRun = false; }

                                                        }
                                                    }
                                                }
                                            }
                                            item.complete = true;
                                            DownCount++;
                                            item.runTask = false;
                                        }
                                        catch
                                        {
                                            if (isStop)
                                            {
                                                item.runTask = false;
                                                return;
                                            }
                                            ErrCount++;

                                            _downvalueTemp = 0;
                                            lock (DownSizeTemp)
                                            {
                                                DownSizeTemp[item.index] = 0;
                                            }
                                            SetValue();
                                            //System.Diagnostics.Debug.WriteLine("序号：" + item.index + "|次数：" + ErrCount);

                                            if (ErrCount > SystemSettings.DownloadRetryCount)
                                            {
                                                item.runTask = false;
                                            }
                                        }
                                    }
                                };

                                var task = Task.Run(_actions, tokenSource.Token).ContinueWith(t =>
                                {
                                    item.run = false;
                                    lock (files)
                                    {
                                        int tackCount = files.Count(ab => ab.run);
                                        if (tackCount < threads)
                                        {
                                            resetEvents.Set();//继续
                                        }
                                    }
                                }, TaskContinuationOptions.OnlyOnRanToCompletion | TaskContinuationOptions.ExecuteSynchronously);
                                ParallelTasks.Add(task);

                                resetEvents.WaitOne();
                            }
                            _task.ContinueWhenAll(ParallelTasks.ToArray(), (action =>
                            {
                                isdown = false;
                                if (tokenSource.Token.IsCancellationRequested)
                                {
                                    return;
                                }
                                List<string> _files = new List<string>();
                                lock (files)
                                {
                                    foreach (FilesResult item in files)
                                    {
                                        if (!item.complete) { return; }
                                        _files.Add(item.path);
                                    }
                                }

                                bool externalAudio = false, externalSub = false;
                                string externalAudioUrl = null, externalSubUrl = null;

                                try
                                {
                                    if (initJson["m3u8Info"]["audio"].ToString() != "")
                                        externalAudio = true;
                                    externalAudioUrl = initJson["m3u8Info"]["audio"].ToString();
                                    System.Diagnostics.Debug.WriteLine("识别到外挂音频轨道");
                                }
                                catch { }
                                try
                                {
                                    if (initJson["m3u8Info"]["sub"].ToString() != "")
                                        externalSub = true;
                                    externalSubUrl = initJson["m3u8Info"]["sub"].ToString();
                                    System.Diagnostics.Debug.WriteLine("识别到外挂字幕轨道");
                                }
                                catch { }
                                CombineMultipleFilesByM3u8(savePath, PartsCount, workPath, partsPadZero, externalAudio, externalAudioUrl, externalSub, externalSubUrl);

                                SetMaxValue();
                                while (isTimeTask)
                                {
                                    Thread.Sleep(500);
                                }
                                //Thread.Sleep(500);
                                Directory.Delete(workPath, true);
                                isComplete = true;
                                SetState(DownState.Complete);
                            })).Wait();
                        };
                        _task.StartNew(_action).Wait();

                        if (isComplete)
                        {
                            errmsg = null;
                            return true;
                        }
                        else
                        {
                            SetState(DownState.Fail);
                            errmsg = "文件下载失败";
                            return false;
                        }

                    }
                    else
                    {
                        SetState(DownState.Fail);
                        errmsg = "无法下载直播";
                        return false;
                    }
                }
                else
                {
                    SetState(DownState.Fail);
                    errmsg = "M3u8解析失败:地址无效";
                    return false;
                }
            }
            else
            {
                SetState(DownState.Fail);
                errmsg = "M3u8解析失败";
                return false;
            }
        }

        #endregion

        #region 计算速度

        void TestTime(bool m3u8)
        {
            double oldsize = 0;
            Action _actions_time = () =>
            {
                while (isdown)
                {
                    Thread.Sleep(1000);

                    if (tokenSource.Token.IsCancellationRequested)
                    {
                        return;
                    }
                    //阻止当前线程
                    resetEvent.WaitOne();

                    if (m3u8)
                    {
                        SetMaxValue();
                    }

                    double _downsize = _Value - oldsize;
                    oldsize = _Value;
                    if (_downsize > 0)
                    {
                        int se = (int)((_MaxValue - oldsize) / _downsize);
                        if (se < 1)
                        {
                            SetSpeed(_downsize);
                        }
                        else
                        {
                            TimeSpan timeSpan = new TimeSpan(0, 0, 0, se);
                            string time_txt = $"{timeSpan.Hours.ToString().PadLeft(2, '0')}:{timeSpan.Minutes.ToString().PadLeft(2, '0')}:{timeSpan.Seconds.ToString().PadLeft(2, '0')}";
                            if (time_txt.StartsWith("00:"))
                            {
                                time_txt = time_txt.Substring(3);
                            }
                            SetSpeed(_downsize);
                            SetTime(time_txt);
                        }
                    }
                    else
                    {
                        SetSpeed(0);
                    }

                }
            };
            _task.ContinueWhenAll(new Task[] { _task.StartNew(_actions_time) }, (action =>
            {
                isTimeTask = false;
            }));
        }

        #endregion

        #endregion

        #region 临时

        private CancellationTokenSource tokenSource = new CancellationTokenSource();
        private ManualResetEvent resetEvent = new ManualResetEvent(true), resetEvents = new ManualResetEvent(true);

        /// <summary>
        /// 并行任务集合
        /// </summary>
        private List<Task> ParallelTasks = new List<Task>();
        private bool isdown = true, isTimeTask = true;

        private int DownCount = 0;
        private int TotalCount = 0;
        private Dictionary<int, double> DownSizeTemp = new Dictionary<int, double>();
        private Dictionary<int, double> TotalSizeTemp = new Dictionary<int, double>();

        #endregion

        #region 其他
        bool CombineMultipleFilesByM3u8(string OutPutPath, int PartsCount, string DownDir, string partsPadZero, bool externalAudio, string externalAudioUrl, bool externalSub, string externalSubUrl)
        {
            //System.Diagnostics.Debug.WriteLine("Start Merging");
            //System.Diagnostics.Debug.WriteLine("开始合并分片...");

            //只有一个Part直接 合并
            if (PartsCount == 1)
            {
                if (File.Exists(DownDir + "!MAP.ts"))
                    File.Move(DownDir + "!MAP.ts", DownDir + "Part_0\\!MAP.ts");

                //System.Diagnostics.Debug.WriteLine("二进制合并...请耐心等待");
                Global.CombineMultipleFilesIntoSingleFile(Global.GetFiles(DownDir + "Part_0", ".ts"), OutPutPath);
                //System.Diagnostics.Debug.WriteLine("任务结束");
                return true;
            }
            else
            {
                //合并分段
                //System.Diagnostics.Debug.WriteLine("合并分段中...");
                for (int i = 0; i < PartsCount; i++)
                {
                    string outputFilePath = DownDir + "\\Part_" + i.ToString(partsPadZero) + ".ts";
                    Global.CombineMultipleFilesIntoSingleFile(Global.GetFiles(DownDir + "\\Part_" + i.ToString(partsPadZero), ".ts"), outputFilePath);
                }

                //System.Diagnostics.Debug.WriteLine("二进制合并...请耐心等待");
                Global.CombineMultipleFilesIntoSingleFile(Global.GetFiles(DownDir, ".ts"), OutPutPath);
                //System.Diagnostics.Debug.WriteLine("任务结束");
                return true;
            }

            return false;
        }

        #endregion

        class FilesResult
        {
            public int index { get; set; }
            public string tsurl { get; set; }
            /// <summary>
            /// 文件保存地址
            /// </summary>
            public string path { get; set; }
            /// <summary>
            /// 文件开始位置
            /// </summary>
            public double start_position { get; set; }

            /// <summary>
            /// 文件结束位置
            /// </summary>
            public double end_position { get; set; }
            public bool run { get; set; }

            /// <summary>
            /// 是否运行中
            /// </summary>
            public bool runTask { get; set; }

            /// <summary>
            /// 是否下载完成
            /// </summary>
            public bool complete { get; set; }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TPlayer.Helper
{
    public class DownTotalCore
    {
        public delegate void StringHandler(string e);
        public delegate void StateHandler(DownCore.DownState e);
        public delegate void DoubleHandler(double e);
        public delegate void IntHandler(int e);

        #region 全局事件
        static int _TotalCount = 0;
        static double _TotalMaxValue = 0, _TotalValue = 0, _TotalSpeed = 0;

        #region 总下载中的任务

        public static int TotalCount
        {
            get { return _TotalCount; }
        }

        /// <summary>
        /// 总下载数改变
        /// </summary>
        public static event IntHandler TotalCountChange;

        #endregion

        #region 总进度

        public static double TotalMaxValue
        {
            get { return _TotalMaxValue; }
        }
        /// <summary>
        /// 总文件大小改变
        /// </summary>
        public static event DoubleHandler TotalMaxValueChange;

        #endregion

        #region 当前总下载值

        public static double TotalValue
        {
            get { return _TotalValue; }
        }

        /// <summary>
        /// 当前总下载值改变
        /// </summary>
        public static event DoubleHandler TotalValueChange;
        #endregion

        #region 文件总下载速度

        public static double TotalSpeed
        {
            get { return _TotalSpeed; }
        }

        /// <summary>
        /// 文件下载速度改变
        /// </summary>
        public static event DoubleHandler TotalSpeedChange;
        #endregion

        #endregion

        #region 检测下载速度

        static bool isTimeTask = false;
        static void TestTime()
        {
            if (!isTimeTask)
            {
                isTimeTask = true;
                double oldsize = 0;
                Action _actions_time = () =>
                {
                    while (_TotalCount > 0)
                    {
                        System.Threading.Thread.Sleep(1000);

                        double _downsize = _TotalValue - oldsize;
                        oldsize = _TotalValue;
                        if (_downsize > 0)
                        {
                            if (_TotalSpeed != _downsize)
                            {
                                _TotalSpeed = _downsize;
                                if (TotalSpeedChange != null)
                                {
                                    TotalSpeedChange(_downsize);
                                }
                            }
                        }
                        else
                        {
                            if (_TotalSpeed != 0)
                            {
                                _TotalSpeed = 0;
                                if (TotalSpeedChange != null)
                                {
                                    TotalSpeedChange(0);
                                }
                            }
                        }
                    }
                };
                DownCore._task.ContinueWhenAll(new Task[] { DownCore._task.StartNew(_actions_time) }, (action =>
               {
                   isTimeTask = false;
               }));
            }
        }

        #endregion


        static Dictionary<string, TotalProg> TotalTemp = new Dictionary<string, TotalProg>();
        public static void AddTask(string taskid)
        {
            lock (TotalTemp)
            {
                TotalTemp.Add(taskid, new TotalProg());
                _TotalCount = TotalTemp.Count;
                TestTime();
                if (TotalCountChange != null)
                {
                    TotalCountChange(_TotalCount);
                }
            }
        }
        public static void DelTask(string taskid)
        {
            lock (TotalTemp)
            {
                if (TotalTemp.ContainsKey(taskid))
                {
                    TotalTemp.Remove(taskid);
                    _TotalCount = TotalTemp.Count;
                    if (TotalCountChange != null)
                    {
                        TotalCountChange(_TotalCount);
                    }
                }
            }
        }
        public static void ChangeValueTask(string taskid, double value)
        {
            lock (TotalTemp)
            {
                if (TotalTemp.ContainsKey(taskid))
                {
                    TotalTemp[taskid].Value = value;

                    double _value = TotalTemp.Sum(ab => ab.Value.Value);
                    if (_TotalValue != _value)
                    {
                        _TotalValue = _value;
                        if (TotalValueChange != null)
                        {
                            TotalValueChange(_value);
                        }
                    }
                }
            }
        }

        public static void ChangeMaxValueTask(string taskid, double value)
        {
            lock (TotalTemp)
            {
                if (TotalTemp.ContainsKey(taskid))
                {
                    TotalTemp[taskid].MaxValue = value;

                    double _value = TotalTemp.Sum(ab => ab.Value.MaxValue);
                    if (_TotalMaxValue != _value)
                    {
                        _TotalMaxValue = _value;
                        if (TotalMaxValueChange != null)
                        {
                            TotalMaxValueChange(_value);
                        }
                    }
                }
            }
        }

        public class TotalProg
        {
            public double MaxValue { get; set; }
            public double Value { get; set; }
        }
    }
}

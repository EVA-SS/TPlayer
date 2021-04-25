using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace TPlayer.Helper
{
    class Global
    {
        private volatile static bool shouldStop = false;
        public static long BYTEDOWN = 0;
        public static long STOP_SPEED = 0; //KB 小于此值自动重试
        public static long MAX_SPEED = 0; //KB 速度上限
        public static string VIDEO_TYPE = "";
        public static string AUDIO_TYPE = "";
        public static bool HadReadInfo = false;
        private static bool noProxy = false;


        //参数：
        //  string dir 指定的文件夹
        //  string ext 文件类型的扩展名，如".txt" , “.exe"
        public static int GetFileCount(string dir, string ext)
        {
            if (!Directory.Exists(dir))
                return 0;

            int count = 0;
            DirectoryInfo d = new DirectoryInfo(dir);
            foreach (FileInfo fi in d.GetFiles())
            {
                if (fi.Extension.ToUpper() == ext.ToUpper())
                {
                    count++;
                }
            }
            return count;
        }

        /// <summary>
        /// 寻找指定目录下指定后缀的文件的详细路径 如".txt"
        /// </summary>
        /// <param name="dir"></param>
        /// <param name="ext"></param>
        /// <returns></returns>
        public static string[] GetFiles(string dir, string ext)
        {
            ArrayList al = new ArrayList();
            DirectoryInfo d = new DirectoryInfo(dir);
            foreach (FileInfo fi in d.GetFiles())
            {
                if (fi.Extension.ToUpper() == ext.ToUpper())
                {
                    al.Add(fi.FullName);
                }
            }
            string[] res = (string[])al.ToArray(typeof(string));
            Array.Sort(res); //排序
            return res;
        }


        //大量文件分部分二进制合并
        public static void PartialCombineMultipleFiles(string[] files)
        {
            int div = 0;
            if (files.Length <= 90000)
                div = 100;
            else
                div = 200;

            string outputName = Path.GetDirectoryName(files[0]) + "\\T";
            int index = 0; //序号

            //按照div的容量分割为小数组
            string[][] li = Enumerable.Range(0, files.Count() / div + 1).Select(x => files.Skip(x * div).Take(div).ToArray()).ToArray();
            foreach (var items in li)
            {
                if (items.Count() == 0)
                    continue;
                CombineMultipleFilesIntoSingleFile(items, outputName + index.ToString("0000") + ".ts");
                //合并后删除这些文件
                foreach (var item in items)
                {
                    File.Delete(item);
                }
                index++;
            }
        }


        /// <summary>
        /// 输入一堆已存在的文件，合并到新文件
        /// </summary>
        /// <param name="files"></param>
        /// <param name="outputFilePath"></param>
        public static void CombineMultipleFilesIntoSingleFile(string[] files, string outputFilePath)
        {
            //同名文件已存在的共存策略
            if (File.Exists(outputFilePath))
            {
                outputFilePath = Path.Combine(Path.GetDirectoryName(outputFilePath),
                    Path.GetFileNameWithoutExtension(outputFilePath) + "_" + DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + Path.GetExtension(outputFilePath));
            }
            if (files.Length == 1)
            {
                FileInfo fi = new FileInfo(files[0]);
                fi.MoveTo(outputFilePath);
                return;
            }

            if (!Directory.Exists(Path.GetDirectoryName(outputFilePath)))
                Directory.CreateDirectory(Path.GetDirectoryName(outputFilePath));

            string[] inputFilePaths = files;
            using (var outputStream = File.Create(outputFilePath))
            {
                foreach (var inputFilePath in inputFilePaths)
                {
                    if (inputFilePath == "")
                        continue;
                    using (var inputStream = File.OpenRead(inputFilePath))
                    {
                        // Buffer size can be passed as the second argument.
                        inputStream.CopyTo(outputStream);
                    }
                    //Console.WriteLine("The file {0} has been processed.", inputFilePath);
                }
            }
            //Global.ExplorerFile(outputFilePath);
        }

        /// <summary>
        /// 文件合并函数,
        /// 可将任意个子文件合并为一个,为fileSplit()的逆过程
        /// delet标识是否删除原文件, change对data的首字节进行解密
        /// </summary>
        public static void CombineMultipleFilesIntoSingleFile(List<string> fileIn, string fileOut)
        {
            if (fileIn.Count == 1)
            {
                FileInfo fi = new FileInfo(fileIn[0]);
                fi.MoveTo(fileOut);
                return;
            }
            using (Stream mergeFile = new FileStream(fileOut, FileMode.Create))
            {
                using (BinaryWriter AddWriter = new BinaryWriter(mergeFile))
                {
                    //按序号排序
                    int i = 0;
                    foreach (string file in fileIn)
                    {
                        i++;
                        using (FileStream fs = new FileStream(file, FileMode.Open))
                        {
                            using (BinaryReader TempReader = new BinaryReader(fs))
                            {

                                //由于一个文件拆分成多个文件时，每个文件最后都会拼接上结尾符"\0"，导致总长度多出(n-1)个字符，需要需要针对前面(n-1)个文件去除最后的"\0"。
                                if (i == fileIn.Count)
                                {
                                    AddWriter.Write(TempReader.ReadBytes((int)fs.Length));
                                }
                                else
                                {
                                    AddWriter.Write(TempReader.ReadBytes((int)fs.Length - 1));
                                }
                            }
                        }
                    }
                }
                //删除临时文件夹
                foreach (string file in fileIn)
                {
                    File.Delete(file);
                }
            }
        }


        /// <summary>
        /// 将一个字节流附加至文件流
        /// </summary>
        /// <param name="liveStream"></param>
        /// <param name="file"></param>
        public static void AppendBytesToFileStreamAndDoNotClose(FileStream liveStream, byte[] file)
        {
            FileStream outputStream = liveStream;
            using (var inputStream = new MemoryStream(file))
            {
                inputStream.CopyTo(outputStream);
            }
        }


        //格式化json字符串
        public static string ConvertJsonString(string str)
        {
            //Console.WriteLine(str);
            JsonSerializer serializer = new JsonSerializer();
            TextReader tr = new StringReader(str);
            JsonTextReader jtr = new JsonTextReader(tr);
            object obj = serializer.Deserialize(jtr);
            if (obj != null)
            {
                StringWriter textWriter = new StringWriter();
                JsonTextWriter jsonWriter = new JsonTextWriter(textWriter)
                {
                    Formatting = Newtonsoft.Json.Formatting.Indented,
                    Indentation = 2,
                    IndentChar = ' '
                };  //Indentation 为缩进量
                serializer.Serialize(jsonWriter, obj);
                return textWriter.ToString();
            }
            else
            {
                return str;
            }
        }

        //获取属性
        public static string GetTagAttribute(string attributeList, string key)
        {
            /*#EXT-X-STREAM-INF:PROGRAM-ID=1,RESOLUTION=1056x594,BANDWIDTH=1963351,CODECS="mp4a.40.5,avc1.4d001f",FRAME-RATE=30.000,AUDIO="aac",AVERAGE-BANDWIDTH=1655131*/
            if (attributeList != "")
            {
                try
                {
                    string tmp = attributeList.Trim();
                    if (tmp.Contains(key + "="))
                    {
                        if (tmp[tmp.IndexOf(key + "=") + key.Length + 1] == '\"')
                        {
                            return tmp.Substring(tmp.IndexOf(key + "=") + key.Length + 2, tmp.Remove(0, tmp.IndexOf(key + "=") + key.Length + 2).IndexOf('\"'));
                        }
                        else
                        {
                            if (tmp.Remove(0, tmp.IndexOf(key + "=") + key.Length + 2).Contains(","))
                                return tmp.Substring(tmp.IndexOf(key + "=") + key.Length + 1, tmp.Remove(0, tmp.IndexOf(key + "=") + key.Length + 1).IndexOf(','));
                            else
                                return tmp.Substring(tmp.IndexOf(key + "=") + key.Length + 1);
                        }
                    }
                }
                catch (Exception)
                {
                    return string.Empty;
                }
            }
            return string.Empty;
        }

        //正则表达式
        public static ArrayList RegexFind(string regex, string src, int group = -1)
        {
            ArrayList array = new ArrayList();
            Regex reg = new Regex(@regex);
            MatchCollection result = reg.Matches(src);
            if (result.Count == 0)
                array.Add("NULL");
            foreach (Match m in result)
            {
                if (group == -1)
                    array.Add(m.Value);
                else
                    array.Add(m.Groups[group].Value);
            }
            return array;
        }


        //所给路径中所对应的文件大小
        public static long FileSize(string filePath)
        {
            //定义一个FileInfo对象，是指与filePath所指向的文件相关联，以获取其大小
            FileInfo fileInfo = new FileInfo(filePath);
            return fileInfo.Length;
        }

        //获取文件夹大小
        public static long GetDirectoryLength(string path)
        {
            if (!Directory.Exists(path))
            {
                return 0;
            }
            long size = 0;
            //遍历指定路径下的所有文件
            DirectoryInfo di = new DirectoryInfo(path);
            foreach (FileInfo fi in di.GetFiles())
            {
                size += fi.Length;
            }
            //遍历指定路径下的所有文件夹
            DirectoryInfo[] dis = di.GetDirectories();
            if (dis.Length > 0)
            {
                for (int i = 0; i < dis.Length; i++)
                {
                    size += GetDirectoryLength(dis[i].FullName);
                }
            }
            return size;
        }


        /// <summary>
        /// 获取有效文件名
        /// </summary>
        /// <param name="text"></param>
        /// <param name="replacement"></param>
        /// <returns></returns>
        public static string WebFileNameLength(Uri uri, out long Length, out bool canSeek)
        {
            string fileName = null;
            long _Length = 0;
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
                request.Host = uri.Host;
                request.Accept = "*/*";
                request.UserAgent = "Mozilla/4.0";
                request.Method = "GET";
                request.Timeout = 6000;
                request.ReadWriteTimeout = request.Timeout; //重要
                request.AllowAutoRedirect = true;
                request.KeepAlive = false;
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    string Disposition = response.Headers["Content-Disposition"];
                    if (!string.IsNullOrEmpty(Disposition))
                    {
                        string filename = "";
                        filename = Disposition.Substring(Disposition.IndexOf("filename=") + 9);
                        if (filename.Contains(";"))
                        {
                            filename = filename.Substring(0, filename.IndexOf(";"));
                            if (filename.EndsWith("\""))
                            {
                                filename = filename.Substring(1, filename.Length - 2);
                            }
                        }
                        filename = filename.Trim('"');


                        byte[] byteArray = Encoding.GetEncoding("ISO-8859-1").GetBytes(filename);// System.Text.Encoding.Default.GetBytes(fileName);
                                                                                                 // Encoding.GetEncoding("utf-8").GetBytes(fileName);
                        string urlFilename = Encoding.GetEncoding("utf-8").GetString(byteArray);
                        urlFilename = System.Web.HttpUtility.UrlDecode(urlFilename, Encoding.UTF8);

                        fileName = urlFilename;
                    }
                    else
                    {
                        fileName = response.ResponseUri.Segments[response.ResponseUri.Segments.Length - 1];
                    }

                    _Length = Length = response.ContentLength;
                }
            }
            catch
            {
                fileName = uri.Segments[uri.Segments.Length - 1];
                canSeek = false;
                _Length = Length = 0;
            }
            if (_Length > 0)
            {
                try
                {
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
                    request.Host = uri.Host;
                    request.Accept = "*/*";
                    request.UserAgent = "Mozilla/4.0";
                    request.Method = "GET";
                    request.Timeout = 6000;
                    request.ReadWriteTimeout = request.Timeout; //重要
                    request.AllowAutoRedirect = true;
                    request.KeepAlive = false;
                    request.AddRange(1, _Length - 1);
                    using (HttpWebResponse p = (HttpWebResponse)request.GetResponse())
                    {
                        long length = p.ContentLength;
                        canSeek = length == _Length - 1;
                    }
                }
                catch
                {
                    canSeek = false;
                }
            }
            else
            {
                canSeek = false;
            }
            return fileName;
        }
    }
}

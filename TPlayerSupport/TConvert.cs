using System;
using System.Drawing;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace TPlayerSupport
{
    public static class TConvert
    {
        #region Json

        public static string ToJson(this object obj)
        {
            if (obj == null)
            {
                return null;
            }
            return Newtonsoft.Json.JsonConvert.SerializeObject(obj);
        }
        public static T ToJson<T>(this string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                try
                {
                    return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(value);
                }
                catch
                {
                }
            }
            return default(T);
        }

        public static dynamic ToJson(this string value)
        {
            return Newtonsoft.Json.Linq.JToken.Parse(value) as dynamic;
        }

        #endregion

        #region 图片

        public static string ToBase64ByImg(this byte[] fileData)
        {
            string result;
            if (fileData == null || fileData.Length < 10)
            {
                result = null;
            }
            else
            {
                if (fileData[0] == 71 && fileData[1] == 73 && fileData[2] == 70)
                {
                    result = "data:image/gif;base64," + Convert.ToBase64String(fileData);
                }
                else
                {
                    if (fileData[1] == 80 && fileData[2] == 78 && fileData[3] == 71)
                    {
                        result = "data:image/png;base64," + Convert.ToBase64String(fileData);
                    }
                    else
                    {
                        if (fileData[6] == 74 && fileData[7] == 70 && fileData[8] == 73 && fileData[9] == 70)
                        {
                            result = "data:image/jpeg;base64," + Convert.ToBase64String(fileData);
                        }
                        else
                        {
                            if (fileData[0] == 66 && fileData[1] == 77)
                            {
                                result = "data:image/bmp;base64," + Convert.ToBase64String(fileData);
                            }
                            else
                            {
                                result = "data:image/png;base64," + Convert.ToBase64String(fileData);
                                //result = null;
                            }
                        }
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 将字节转为图片
        /// </summary>
        /// <param name="data">字节</param>
        /// <returns>返回图片</returns>
        public static Image ToImage(this byte[] data)
        {
            if (data != null && data.Length > 0)
            {
                using (MemoryStream ms = new MemoryStream(data))
                {
                    return Image.FromStream(ms);
                }
            }
            return null;
        }
        public static Bitmap ToBitmap(this byte[] data)
        {
            if (data != null && data.Length > 0)
            {
                using (MemoryStream ms = new MemoryStream(data))
                {
                    return new Bitmap(ms);
                }
            }
            return null;
        }
        /// <summary>
        /// 根据图片路径返回图片的字节流byte[]
        /// </summary>
        /// <param name="imagePath">图片路径</param>
        /// <returns>返回的字节流</returns>
        public static byte[] ToByte(this Image img)
        {
            using (Stream ms = new MemoryStream())
            {
                try
                {
                    img.Save(ms, img.RawFormat);
                }
                catch
                {
                    img.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                }
                byte[] imgByte = new byte[ms.Length];
                ms.Read(imgByte, 0, imgByte.Length);
                return imgByte;
            }
        }

        #endregion

        #region 加解密

        #region MD5

        public static string Md5_16(this string str)
        {
            return Md5_16(str, Encoding.UTF8);
        }

        /// <summary>
        /// MD5 16位加密 加密后密码为小写
        /// </summary>
        /// <param name="str"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string Md5_16(this string str, Encoding encoding)
        {
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                return BitConverter.ToString(md5.ComputeHash(encoding.GetBytes(str)), 4, 8).Replace("-", "").ToLower();
            }
        }

        #endregion

        #endregion

        #region 文件大小格式

        public static string CountSize(this double Size, int digits = 2, string nultxt = "0 B")
        {
            if (Size == 0)
            {
                return nultxt;
            }
            string m_strSize = "";
            string _digits = "F" + digits;
            double FactSize = Size;
            if (FactSize < 1024.00)
                m_strSize = Math.Round(FactSize, digits).ToString(_digits) + " B";
            else if (FactSize >= 1024.00 && FactSize < 1048576)
                m_strSize = Math.Round((FactSize / 1024.00), digits).ToString(_digits) + " K";
            else if (FactSize >= 1048576 && FactSize < 1073741824)
                m_strSize = Math.Round((FactSize / 1024.00 / 1024.00), digits).ToString(_digits) + " M";
            else if (FactSize >= 1073741824)
                m_strSize = Math.Round((FactSize / 1024.00 / 1024.00 / 1024.00), digits).ToString(_digits) + " G";
            return m_strSize;
        }

        public static string CountSize(this long Size, int digits = 2, string nultxt = "0 B")
        {
            return CountSize((double)Size, digits, nultxt);
        }
        public static string CountSize(this int Size, int digits = 2, string nultxt = "0 B")
        {
            return CountSize((double)Size, digits, nultxt);
        }
        #endregion

        #region 时间转换

        #region 获取格式化时间
        public static string ToTimeStr(double value, double maxvalue)
        {
            return TimeSpan.FromMilliseconds(value).ToTimeStr() + " / " + TimeSpan.FromMilliseconds(maxvalue).ToTimeStr();
        }
        public static string ToTimeStr(this double value)
        {
            return TimeSpan.FromMilliseconds(value).ToTimeStr();
        }
        public static string ToTimeStr(this int value)
        {
            return TimeSpan.FromMilliseconds(value).ToTimeStr();
        }
        public static string ToTimeStr(this TimeSpan span)
        {
            if (span.Hours == 0)
            {
                return
                span.Minutes.ToString("00") + ":" +
                span.Seconds.ToString("00");
            }
            else if (span.Hours == 0 && span.Minutes == 0)
            {
                return span.Seconds.ToString("00");
            }
            else
            {
                return span.Hours.ToString("00") + ":" +
                span.Minutes.ToString("00") + ":" +
                span.Seconds.ToString("00");
            }
        }

        #endregion

        #region UTC

        public static int ToUtcDateTime(this DateTime time)
        {
            double intResult = 0;
            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            intResult = (time - startTime).TotalSeconds;
            return (int)intResult;
        }

        public static DateTime ToUtcDateTime(this int utc)
        {
            return ToUtcDateTime((double)utc);
        }

        public static DateTime ToUtcDateTime(this double utc)
        {
            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            startTime = startTime.AddSeconds(utc);
            startTime = startTime.AddHours(8);//转化为北京时间(北京时间=UTC时间+8小时 )
            return startTime;
        }

        #endregion

        #region 时间戳
        /// <summary>
        /// 日期转换成unix时间戳
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static long DateTimeToUnixTimestamp(this DateTime dateTime)
        {
            DateTime start = new DateTime(1970, 1, 1, 0, 0, 0, dateTime.Kind);
            return Convert.ToInt64((dateTime - start).TotalSeconds);
        }
        public static long DateTimeToUnixTimestamps(this DateTime dateTime)
        {
            return (dateTime.ToUniversalTime().Ticks - 621355968000000000) / 10000;
        }

        /// <summary>
        /// unix时间戳转换成日期
        /// </summary>
        /// <param name="unixTimeStamp">时间戳（秒）</param>
        /// <returns></returns>
        public static DateTime UnixTimestampToDateTime(this DateTime target, long timestamp)
        {
            DateTime start = new DateTime(1970, 1, 1, 0, 0, 0, target.Kind);
            return start.AddSeconds(timestamp);
        }
        public static DateTime UnixTimestampToDateTime(this long timestamp)
        {
            DateTime start = new DateTime(1970, 1, 1, 0, 0, 0, DateTime.Now.Kind);
            return start.AddSeconds(timestamp);
        }
        /// <summary>
        /// unix时间戳转换成日期
        /// </summary>
        public static DateTime UnixTimestampToDateTimes(long unixTime)
        {
            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1)); // 当地时区
            return startTime.AddMilliseconds(unixTime);
        }

        #endregion

        #endregion

        public static string ToStr(this object value, string nullValue = null)
        {
            if (value == null)
            { return nullValue; }
            else if (value is DBNull)
            { return nullValue; }
            else
            {
                return value.ToString();
            }
        }
        public static int ToInt(this object value, int nullValue = 0)
        {
            if (value == null)
            { return nullValue; }
            else if (value is DBNull)
            { return nullValue; }
            else
            {
                int _value = 0;
                int.TryParse(value.ToString(), out _value);
                return _value;
            }
        }
        public static long ToLong(this object value, long nullValue = 0)
        {
            if (value == null)
            { return nullValue; }
            else if (value is DBNull)
            { return nullValue; }
            else
            {
                long _value = 0;
                long.TryParse(value.ToString(), out _value);
                return _value;
            }
        }
        public static double ToDouble(this object value, double nullValue = 0)
        {
            if (value == null)
            { return nullValue; }
            else if (value is DBNull)
            { return nullValue; }
            else
            {
                double _value = 0;
                double.TryParse(value.ToString(), out _value);
                return _value;
            }
        }

        public static DateTime ToDateTime(this object value, DateTime nullValue = new DateTime())
        {
            if (value == null)
            { return nullValue; }
            else if (value is DBNull)
            { return nullValue; }
            else
            {
                try
                {
                    DateTime _value;
                    DateTime.TryParse(value.ToString(), out _value);
                    return _value;
                }
                catch { return nullValue; }
            }
        }


        public static string ToNumberByFormat(this long value)
        {
            if (value > 10000)
            {
                return (Math.Round(((double)value / 10000), 2) + "W");
            }
            else if (value > 1000)
            {
                return (Math.Round(((double)value / 1000), 2) + "K");
            }
            else
            {
                return value.ToString();
            }
        }



        public static bool CreateDirectory(this string path, bool Hidden = false)
        {
            try
            {
                DirectoryInfo TheFolder = new DirectoryInfo(path);
                if (!TheFolder.Exists)
                {
                    TheFolder.Create();
                    if (Hidden)
                    {
                        TheFolder.Attributes = TheFolder.Attributes | FileAttributes.Hidden;
                    }
                }
                else if (Hidden)
                {
                    if (!TheFolder.Attributes.HasFlag(FileAttributes.Hidden))
                    {
                        TheFolder.Attributes = TheFolder.Attributes | FileAttributes.Hidden;
                    }
                }
                return true;
            }
            catch { }
            return false;
        }
    }
}

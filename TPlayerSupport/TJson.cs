using System.IO;
using System.Runtime.Serialization.Json;

namespace TPlayerSupport
{

    #region DataContract序列化

    public static class TJson
    {

        /// <summary>
        /// 将对象转化为Json字符串
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="instanse">对象本身</param>
        /// <returns>JSON字符串</returns>
        public static string ToJson<T>(this T instanse)
        {
            try
            {
                DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(T));
                using (MemoryStream ms = new MemoryStream())
                {
                    js.WriteObject(ms, instanse);
                    ms.Flush();
                    ms.Seek(0, SeekOrigin.Begin);
                    StreamReader sr = new StreamReader(ms);
                    return sr.ReadToEnd();
                }
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 将字符串转化为JSON对象，如果转换失败，返回default(T)
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="s">字符串</param>
        /// <returns>转换值</returns>
        public static T ToJson<T>(this string s)
        {
            try
            {
                DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(T));
                using (MemoryStream ms = new MemoryStream())
                {
                    StreamWriter sw = new StreamWriter(ms);
                    sw.Write(s);
                    sw.Flush();
                    ms.Seek(0, SeekOrigin.Begin);
                    return (T)js.ReadObject(ms);
                }
            }
            catch
            {
                return default(T);
            }
        }
    }

    #endregion
}

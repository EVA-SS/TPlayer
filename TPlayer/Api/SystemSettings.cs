using IniParser;
using IniParser.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace TPlayer
{
    /// <summary>
    /// 系统设置。保存用户设置的数据。
    /// </summary>
    public class SystemSettings
    {
        #region 常规设置

        #region 程序动画效果

        public static bool AnimationDefault = true;
        public static bool Animation
        {
            get
            {
                return ReadBool("Windows", "Animation", AnimationDefault);
            }
            set
            {
                Write("Windows", "Animation", value);
            }
        }
        #endregion

        #region 最小化到托盘
        public static bool MinimizeToTrayDefault = false;
        public static bool MinimizeToTray
        {
            get
            {
                return ReadBool("Tray", "Minimize", MinimizeToTrayDefault);
            }
            set
            {
                Write("Tray", "Minimize", value);
            }
        }
        #endregion

        #region 多重运行
        public static bool MultipleDefault = false;
        public static bool Multiple
        {
            get
            {
                return ReadBool("Windows", "Multiple", MultipleDefault);
            }
            set
            {
                Write("Windows", "Multiple", value);
            }
        }
        #endregion

        #region 记忆播放位置

        public static bool RememberLocationDefault = true;
        public static bool RememberLocation
        {
            get
            {
                return ReadBool("Play", "RememberLocation", RememberLocationDefault);
            }
            set
            {
                Write("Play", "RememberLocation", value);
            }
        }
        #endregion

        #region 硬件加速
        public static bool SpeedupEnableDefault = true;
        public static bool SpeedupEnable
        {
            get
            {
                return ReadBool("Play", "SpeedupEnable", SpeedupEnableDefault);
            }
            set
            {
                Write("Play", "SpeedupEnable", value);
            }
        }
        #endregion

        #region 视频打开时

        public static int VideoOpenFrameDefault = 0;
        public static int VideoOpenFrame
        {
            get
            {
                return ReadInt("Play", "VideoOpenFrame", VideoOpenFrameDefault);
            }
            set
            {
                Write("Play", "VideoOpenFrame", value);
            }
        }
        #endregion

        #region 网络下载

        #region 视频缓存
        public static bool  CacheVideoDefault = true;
        public static bool CacheVideo
        {
            get
            {
                return ReadBool("Cache", "Video", CacheVideoDefault);
            }
            set
            {
                Write("Cache", "Video", value);
            }
        }
        #endregion

        #region 贪婪下载
        public static bool CacheGreedDefault = true;
        public static bool CacheGreed
        {
            get
            {
                return ReadBool("Cache", "Greed", CacheGreedDefault);
            }
            set
            {
                Write("Cache", "Greed", value);
            }
        }
        #endregion

        #endregion

        #region 关联

        public static string[] GetLink
        {
            get
            {
                string value = ReadString("Windows", "SetLink");
                if (string.IsNullOrEmpty(value)) { return new string[0]; }
                else
                {
                    return value.Split(';');
                }
            }
        }
        public static string SetLink
        {
            set
            {
                Write("Windows", "SetLink", value);
            }
        }

        #endregion

        #endregion

        #region 下载

        #region 下载完成后关闭

        public static bool DownloadCompleteExit
        {
            get
            {
                return ReadBool("Download", "CompleteExit", true);
            }
            set
            {
                Write("Download", "CompleteExit", value);
            }
        }

        #endregion

        #region 同时下载数量

        public static int DownloadCountDefault = 2;
        public static int DownloadCount
        {
            get
            {
                return ReadInt("Download", "Count", DownloadCountDefault);
            }
            set
            {
                Write("Download", "Count", value);
            }
        }

        #endregion

        #region 下载线程数量

        public static int DownloadTaskCountDefault = 2;

        public static int DownloadTaskCount
        {
            get
            {
                return ReadInt("Download", "TaskCount", DownloadTaskCountDefault);
            }
            set
            {
                Write("Download", "TaskCount", value);
            }
        }

        #endregion

        #region 下载缓冲数量

        public static int DownloadCacheCountDefault = 1024;
        public static int DownloadCacheCount
        {
            get
            {
                return ReadInt("Download", "CacheCount", DownloadCacheCountDefault);
            }
            set
            {
                Write("Download", "CacheCount", value);
            }
        }

        #endregion

        #region 下载重试次数

        public static int DownloadRetryCountDefault = 15;
        public static int DownloadRetryCount
        {
            get
            {
                return ReadInt("Download", "RetryCount", DownloadRetryCountDefault);
            }
            set
            {
                Write("Download", "RetryCount", value);
            }
        }

        #endregion

        #region 下载超时（10秒）
        public static int DownloadTimeOutDefault = 10000;

        public static int DownloadTimeOut
        {
            get
            {
                return ReadInt("Download", "TimeOut", DownloadTimeOutDefault);
            }
            set
            {
                Write("Download", "TimeOut", value);
            }
        }

        #endregion

        #endregion

        #region 高级设置

        #region 3D
        public static bool _3DDefault = false;
        public static bool _3D
        {
            get
            {
                return ReadBool("Play", "3D", _3DDefault);
            }
            set
            {
                Write("Play", "3D", value);
            }
        }
        public static int _3DModeDefault = 1;
        public static int _3DMode
        {
            get
            {
                return ReadInt("Play", "3DMode", _3DModeDefault);
            }
            set
            {
                Write("Play", "3DMode", value);
            }
        }

        public static int _3DColorDefault = 1;
        public static int _3DColor
        {
            get
            {
                return ReadInt("Play", "3DColor", _3DColorDefault);
            }
            set
            {
                Write("Play", "3DColor", value);
            }
        }

        #endregion

        #region VR

        public static bool VRDefault = false;
        public static bool VR
        {
            get
            {
                return ReadBool("Play", "VR", VRDefault);
            }
            set
            {
                Write("Play", "VR", value);
            }
        }

        public static int VRModeDefault = 0;
        public static int VRMode
        {
            get
            {
                return ReadInt("Play", "VRMode", VRModeDefault);
            }
            set
            {
                Write("Play", "VRMode", value);
            }
        }

        #endregion

        #endregion

        #region 外观设置

        #region 背景图片

        public static string BackImgUrlDefault = null;
        public static string BackImgUrl
        {
            get
            {
                string url = ReadString("Windows", "BackImgUrl");
                if (!string.IsNullOrEmpty(url))
                {
                    if (!File.Exists(url))
                    {
                        Write("Windows", "BackImgUrl", null);
                    }
                    else
                    {
                        return url;
                    }
                }
                return BackImgUrlDefault;
            }
            set
            {
                Write("Windows", "BackImgUrl", value);
            }
        }

        #endregion

        #region 是否显示Logo

        public static bool ShowLogoDefault = false;
        public static bool ShowLogo
        {
            get
            {
                return ReadBool("Windows", "ShowLogo", ShowLogoDefault);
            }
            set
            {
                Write("Windows", "ShowLogo", value);
            }
        }

        #endregion

        #endregion

        #region 其他

        #region 网络视频来源类型

        /// <summary>
        /// 网络视频来源类型
        /// </summary>
        public static string WebVideoSourceType
        {
            get
            {
                return ReadString("WebVideo", "SourceType");
            }
            set
            {
                Write("WebVideo", "SourceType", value);
            }
        }

        #endregion

        #region 常用播放类型

        /// <summary>
        /// 常用播放类型
        /// </summary>
        public static List<string> WebVideoPlayType
        {
            get
            {
                string value = ReadString("WebVideo", "WebVideoPlayType");
                if (string.IsNullOrEmpty(value)) { return new List<string>(); }
                else
                {
                    return value.Split(';').ToList();
                }
            }
        }
        public static string SetWebVideoPlayType
        {
            set
            {
                Write("WebVideo", "WebVideoPlayType", value);
            }
        }

        #endregion

        #region 电视默认来源

        public static string TvSourceType
        {
            get
            {
                string value = ReadString("Tv2", "SourceType");
                if (string.IsNullOrEmpty(value))
                {
                    return "China";
                }
                else
                {
                    return value;
                }
            }
            set
            {
                Write("Tv2", "SourceType", value);
            }
        }

        #endregion

        #endregion

        #region 更新

        /// <summary>
        /// 检查版本
        /// </summary>
        public static UpdateInfo UpdateInfo
        {
            get
            {
                string value = ReadString("Update", "Info");
                if (!string.IsNullOrEmpty(value))
                {
                    try
                    {
                        return Newtonsoft.Json.JsonConvert.DeserializeObject<UpdateInfo>(value);
                    }
                    catch { }
                }
                return null;
            }
            set
            {
                Write("Update", "Info", Newtonsoft.Json.JsonConvert.SerializeObject(value));
            }
        }
        /// <summary>
        /// 检查时间
        /// </summary>
        public static string UpdateTime
        {
            get
            {
                return ReadString("Update", "Time");
            }
            set
            {
                Write("Update", "Time", value);
            }
        }

        /// <summary>
        /// 获取预览版
        /// </summary>
        public static bool UpdatePie
        {
            get
            {
                return ReadBool("Update", "Pie");
            }
            set
            {
                Write("Update", "Pie", value);
            }
        }
        #endregion

        #region Core

        #region 恢复默认

        public static void Resume()
        {
            if (File.Exists(SystemSettingsFilePath))
            {
                File.WriteAllText(SystemSettingsFilePath, null, Encoding.UTF8);
            }
            data = parser.ReadFile(SystemSettingsFilePath);
        }

        #endregion

        static string SystemSettingsFilePath = Program.BasePath + "setting.ini";

        static FileIniDataParser parser = new FileIniDataParser();
        static IniData data;
        static SystemSettings()
        {
            if (!File.Exists(SystemSettingsFilePath))
            {
                File.WriteAllText(SystemSettingsFilePath, null, Encoding.UTF8);
            }
            try
            {
                data = parser.ReadFile(SystemSettingsFilePath, Encoding.UTF8);
            }
            catch
            {
                File.WriteAllText(SystemSettingsFilePath, null, Encoding.UTF8);
                data = parser.ReadFile(SystemSettingsFilePath, Encoding.UTF8);
            }
        }

        #region 读取
        static string ReadString(string section, string key, string devaue = null, bool isEncrypt = false)
        {
            string value = _ReadString(section, key, isEncrypt);
            if (string.IsNullOrEmpty(value))
            {
                return devaue;
            }
            return value;
        }
        static bool ReadBool(string section, string key, bool devaue = false, bool isEncrypt = false)
        {
            string _value = _ReadString(section, key, isEncrypt);
            if (string.IsNullOrEmpty(_value))
            {
                return devaue;
            }
            else if (_value == "1")
            {
                return true;
            }
            else if (_value == "0")
            {
                return false;
            }
            else
            {
                bool value;
                if (bool.TryParse(_value, out value))
                {
                    return value;
                }
                return devaue;
            }
        }
        static int ReadInt(string section, string key, int devaue = 0, bool isEncrypt = false)
        {
            int value;
            if (int.TryParse(_ReadString(section, key, isEncrypt), out value))
            {
                return value;
            }
            return devaue;
        }
        static float ReadFloat(string section, string key, float devaue = 0, bool isEncrypt = false)
        {
            float value;
            if (float.TryParse(_ReadString(section, key, isEncrypt), out value))
            {
                return value;
            }
            return devaue;
        }
        static double ReadDouble(string section, string key, double devaue = 0, bool isEncrypt = false)
        {
            double value;
            if (double.TryParse(_ReadString(section, key, isEncrypt), out value))
            {
                return value;
            }
            return devaue;
        }
        static long ReadLong(string section, string key, long devaue = 0, bool isEncrypt = false)
        {
            long value;
            if (long.TryParse(_ReadString(section, key, isEncrypt), out value))
            {
                return value;
            }
            return devaue;
        }
        static DateTime ReadDateTime(string section, string key, bool isEncrypt = false)
        {
            DateTime value;
            if (DateTime.TryParse(_ReadString(section, key, isEncrypt), out value))
            {
                return value;
            }
            return new DateTime();
        }
        static string _ReadString(string section, string key, bool isEncrypt)
        {
            string val = data[section][key];
            //if (isEncrypt && !string.IsNullOrEmpty(val))
            //{
            //    return val.Base64UrlDecrypt();
            //}
            return val;
        }
        #endregion

        #region 写入

        static bool Write(string section, string key, string Value, bool isEncrypt = false)
        {
            return _Write(section, key, Value, isEncrypt);
        }
        static bool Write(string section, string key, int Value, bool isEncrypt = false)
        {
            return _Write(section, key, Value.ToString(), isEncrypt);
        }
        static bool Write(string section, string key, long Value, bool isEncrypt = false)
        {
            return _Write(section, key, Value.ToString(), isEncrypt);
        }
        static bool Write(string section, string key, double Value, bool isEncrypt = false)
        {
            return _Write(section, key, Value.ToString(), isEncrypt);
        }
        static bool Write(string section, string key, float Value, bool isEncrypt = false)
        {
            return _Write(section, key, Value.ToString(), isEncrypt);
        }
        static bool Write(string section, string key, bool Value, bool isEncrypt = false)
        {
            return _Write(section, key, Value ? "1" : "0", isEncrypt);
        }
        static bool _Write(string section, string key, object Value, bool isEncrypt)
        {
            try
            {
                if (Value == null)
                {
                    data[section][key] = null;
                }
                else
                {
                    string val = Value.ToString();
                    if (string.IsNullOrEmpty(val))
                    {
                        data[section][key] = null;
                    }
                    else
                    {
                        data[section][key] = val;
                        //data[section][key] = isEncrypt ? val.Base64UrlEncrypt() : val;
                    }
                }
                parser.WriteFile(SystemSettingsFilePath, data, Encoding.UTF8);
                return true;
            }
            catch { }
            return false;
        }

        #endregion

        #endregion
    }


}

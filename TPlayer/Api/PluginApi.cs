using PluginHelper;
using System;
using System.Collections.Generic;
using System.IO;

namespace TPlayer
{
    public class PluginApi
    {
        public static List<string> PluginTypes = new List<string> { "VideoPlugin", "SubtitleWebPlugin" };
        public static List<PluginItem> Plugins = new List<PluginItem>();
        public static void LoadPlugins()
        {
            string[] PluginFiles = Directory.GetFiles(Program.PluginPath);
            foreach (string file in PluginFiles)
            {
                //判断文件格式是否为.dll格式
                if (Path.GetExtension(file).ToLower() == ".dll" && Plugins.Find(ab => ab.FullPath == file) == null)
                {
                    PluginDomain pluginDomain = file.GetPluginObject(PluginTypes);
                    if (pluginDomain != null && pluginDomain.isSuccess)
                    {
                        PluginItem Pinfo = new PluginItem { FullPath = file };
                        Pinfo.PluginVersion = pluginDomain.GetPropertyValue<Version>("PluginVersion");
                        if (Pinfo.PluginVersion != null)
                        {
                            Pinfo.PluginName = pluginDomain.GetPropertyValue<string>("PluginName");

                            Pinfo.Domain = pluginDomain;
                            Plugins.Add(Pinfo);
                            System.Diagnostics.Debug.WriteLine("加载插件：" + Pinfo.PluginName + " | 版本：" + Pinfo.PluginVersion.ToString());
                        }
                        else
                        {
                            //不匹配卸载
                            pluginDomain.Dispose();
                        }
                    }
                }
            }
        }

        #region 实现所有插件调用

        public static List<SubtitleWebList> SubtitleWebSearch(string search)
        {
            List<PluginItem> _Plugins = Plugins.FindAll(ab => ab.Domain.PluginType == "SubtitleWebPlugin");
            if (_Plugins != null && _Plugins.Count > 0)
            {
                foreach (PluginItem item in _Plugins)
                {
                    List<SubtitleWebList> val = item.Domain.GetMethodValue<List<SubtitleWebList>>("Search", search);
                    if (val != null && val.Count > 0)
                    {
                        return val;
                    }
                }
            }
            return null;
        }

        #endregion
    }


    /// <summary>
    /// 插件信息
    /// </summary>
    public class PluginItem
    {
        public string FullPath { get; set; }

        /// <summary>
        /// 插件名称
        /// </summary>
        public string PluginName { get; set; }

        /// <summary>
        /// 插件内部版本
        /// </summary>
        public Version PluginVersion { get; set; }
        public PluginDomain Domain { get; set; }

        public void Unload()
        {
            if (Domain != null)
            {
                Domain.Unload();
                //Domain = null;
            }
        }
        public void Load()
        {
            if (Domain != null)
            {
                Domain.Load();
            }
        }
        public void Dispose()
        {
            if (Domain != null)
            {
                Domain.Dispose();
                Domain = null;
            }
        }
    }
}

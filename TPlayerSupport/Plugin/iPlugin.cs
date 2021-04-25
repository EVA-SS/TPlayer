using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace PluginHelper
{
    public interface VideoPlugin : iPlugin
    {
        List<ListItem> GetList(string search, int count);
        ListItem GetOne(string search);
    }

    #region 字幕插件

    /// <summary>
    /// 字幕插件
    /// </summary>
    public interface SubtitleWebPlugin : iPlugin
    {
        List<SubtitleWebList> Search(string search);
    }
    public class SubtitleWebList
    {
        public string title { get; set; }
        public string intro { get; set; }
        public string url { get; set; }
        public string img { get; set; }

        public List<SubtitleWebItem> data { get; set; }
    }
    public class SubtitleWebItem
    {
        public string title { get; set; }
        public int type { get; set; }
        public string imgsrc { get; set; }
        public string imgalt { get; set; }
        public string down { get; set; }
        public string url { get; set; }
    }

    #endregion

    public class ListItem
    {
        public string name { get; set; }
    }
    public interface iPlugin
    {
        /// <summary>
        /// 插件名称
        /// </summary>
        string PluginName { get; }

        /// <summary>
        /// 插件内部版本
        /// </summary>
        Version PluginVersion { get; }
    }
}

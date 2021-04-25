using System.Collections.Generic;
using System.Drawing;

namespace TPlayer.Frm.Web
{
    /// <summary>
    /// 影片类型  http://www.okzy.me/?m=vod-detail-id-24562.html
    /// </summary>
    public class TitleName
    {
        /// <summary>
        /// ID
        /// </summary>
        public string titleId { get; set; }
        /// <summary>
        /// 分类
        /// </summary>
        public string titleType { get; set; }
        /// <summary>
        /// 页数
        /// </summary>
        public string pageCount { get; set; }
        /// <summary>
        /// 影片主页
        /// </summary>
        public List<Video> titleVideos { get; set; }
    }
    public class MainTitle
    {
        /// <summary>
        /// ID
        /// </summary>
        public string mainId { get; set; }
        /// <summary>
        /// 分类
        /// </summary>
        public string mainType { get; set; }
    }

    /// <summary>
    /// 影片信息
    /// </summary>
    public class Video
    {
        /// <summary>
        /// 影片标识
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 影片名
        /// </summary>
        public string videoName { get; set; }
        /// <summary>
        /// 影片封面地址
        /// </summary>
        public string videoImgUrl { get; set; }
        ///// <summary>
        ///// 影片图片
        ///// </summary>
        //public byte[] videoImg { get; set; }
        public Image videoImg { get; set; }
        /// <summary>
        /// 影片类型
        /// </summary>
        public string videoType { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public string videoUpdateTime { get; set; }
        /// <summary>
        /// 影片相关详情
        /// </summary>
        public List<VideoInfo> videoTotalInfo { get; set; }
        public string videoTotalInfoTxt { get; set; }
        /// <summary>
        /// 剧情介绍
        /// </summary>
        public string videoSynopsis { get; set; }
        /// <summary>
        /// 影片质量
        /// </summary>
        public string videoQuality { get; set; }
        /// <summary>
        /// 影片别名
        /// </summary>
        public string videoAlias { get; set; }
        /// <summary>
        /// 影片导演
        /// </summary>
        public string videoDirector { get; set; }
        /// <summary>
        /// 影片主演
        /// </summary>
        public string videoProtagonist { get; set; }
        /// <summary>
        /// 影片地区
        /// </summary>
        public string videoRegion { get; set; }
        /// <summary>
        /// 影片语言
        /// </summary>
        public string videoLanguage { get; set; }
        /// <summary>
        /// 影片上映时间
        /// </summary>
        public string videoReleaseTime { get; set; }
        /// <summary>
        /// 影片总播放量
        /// </summary>
        public string videoTotalAmountPlay { get; set; }
        /// <summary>
        /// 影片今日播放量
        /// </summary>
        public string videoTodayPlay { get; set; }
        /// <summary>
        /// 影片播放地址
        /// </summary>
        public List<PlayAddress> playInformation { get; set; }
        public bool isRun { get; set; }
    }
    public class VideoInfo
    {
        public string key { get; set; }
        public string url { get; set; }
        public string value { get; set; }
    }
    /// <summary>
    /// 播放信息
    /// </summary>
    public class PlayAddress
    {
        /// <summary>
        /// 链接类型
        /// </summary>
        public string playType { get; set; }

        public List<VideoUrl> videoUrl { get; set; }
    }
    /// <summary>
    /// 播放地址
    /// </summary>
    public class VideoUrl
    {
        /// <summary>
        /// 第几集
        /// </summary>
        public string playName { get; set; }
        /// <summary>
        /// 影片地址
        /// </summary>
        public string playURL { get; set; }
    }

}

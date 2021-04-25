namespace TPlayer.Frm.Web
{
    public class PicCacheData
    {
        /// <summary>
        /// id
        /// </summary>		
        public int id { get; set; }
        /// <summary>
        /// pictureID
        /// </summary>		
        public int pictureID { get; set; }
        /// <summary>
        /// pictureURL
        /// </summary>		
        public byte[] pictureImage { get; set; }
        /// <summary>
        /// pictureDatatime
        /// </summary>		
        public string pictureDatatime { get; set; }
        /// <summary>
        /// 图片渠道
        /// </summary>
        public string picType { get; set; }

    }
    /// <summary>
    /// 图片控件关联信息
    /// </summary>
    public class PicInfo
    {
        public string ImageUrl { get; set; }
        public int ImageId { get; set; }

        public string BarValue { get; set; }
    }

}

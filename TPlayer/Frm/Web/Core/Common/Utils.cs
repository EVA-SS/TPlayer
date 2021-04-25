using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;

namespace TPlayer.Frm.Web
{
    public static class Utils
    {
        [DllImport("wininet.dll")]
        private extern static bool InternetGetConnectedState(out int Description, int ReservedValue);

        ///<summary>
        /// 查看网络是否连接
        ///</summary>
        ///<returns>返回Ture：可以连接到Internet，False则连接不上</returns>
        public static bool IsConnectedToInternet()
        {
            int Desc;
            return InternetGetConnectedState(out Desc, 0);
        }

        // <summary> 
        /// 字节流转换成图片 
        /// </summary> 
        /// <param name="byt">要转换的字节流</param> 
        /// <returns>转换得到的Image对象</returns> 
        public static Image BytToImg(byte[] byt)
        {
            try
            {
                using (MemoryStream ms = new MemoryStream(byt))
                {
                    Image img = Image.FromStream(ms);
                    ms.Dispose();
                    return img;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }
        /// <summary>
        ///  图片转换成字节流 
        /// </summary>
        /// <param name="img"></param>
        /// <returns></returns>
        public static byte[] ImageToByteArray(Image img)
        {
            ImageConverter imgconv = new ImageConverter();
            byte[] b = (byte[])imgconv.ConvertTo(img, typeof(byte[]));
            return b;
        }
        /// <summary>
        /// 图片转换成字节流
        /// </summary>
        /// <param name="img"></param>
        /// <returns></returns>
        public static byte[] Image2Byte(Image img)
        {
            MemoryStream ms = new MemoryStream();
            img.Save(ms, ImageFormat.Jpeg);
            byte[] byData = new byte[ms.Length];
            ms.Position = 0;
            ms.Read(byData, 0, byData.Length);
            ms.Close();
            return byData;
        }
        ///// <summary>
        ///// 加载动画
        ///// </summary>
        ///// <returns></returns>
        //public static Image GetLoadingImg()
        //{
        //   return Resources.loading;
        //}

    }
}

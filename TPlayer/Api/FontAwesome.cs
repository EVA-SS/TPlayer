using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.IO;
using System.Runtime.InteropServices;

namespace TPlayer
{
    public class FontAwesome
    {
        #region Fileds
        /// <summary>
        /// FontCollection object
        /// </summary>
        private static readonly PrivateFontCollection FontCollection = new PrivateFontCollection();
        #endregion

        #region //ctor with no params

        /// <summary>
        /// ctor with no params
        /// </summary>
        static FontAwesome()
        {
            //if (File.Exists(path))
            //{
            //}
            //else
            //{
            //    throw new FileNotFoundException("FontAwesome font file not found", path);
            //}

            IntPtr MeAdd = Marshal.AllocHGlobal(Properties.Resources.FontUI.Length);
            Marshal.Copy(Properties.Resources.FontUI, 0, MeAdd, Properties.Resources.FontUI.Length);
            FontCollection.AddMemoryFont(MeAdd, Properties.Resources.FontUI.Length);

            //FontCollection.AddFontFile(path);
            //FontCollection.AddFontFile(FontAwesomeLocation + FontAwesomeName);
        }
        ~FontAwesome()
        {
            FontCollection.Dispose();
        }

        #endregion

        #region //get FontAwesome icon

        /// <summary>
        /// get FontAwesome icon
        /// </summary>
        /// <param name="iconText">FontAwesome icon hex code</param>
        /// <returns></returns>
        public static Icon GetIcon(int iconText, int IconSize = 128)
        {
            Bitmap bmp = GetImage(iconText, IconSize);
            if (bmp != null)
            {
                return ToIcon(bmp, IconSize);
            }
            return null;
        }
        public static Icon GetIcon(string iconText16, int IconSize = 128)
        {
            Bitmap bmp = GetImage(iconText16, IconSize);
            if (bmp != null)
            {
                return ToIcon(bmp, IconSize);
            }
            return null;
        }

        #endregion

        #region //get FontAwesome image

        /// <summary>
        /// get FontAwesome image
        /// </summary>
        /// <param name="iconText">FontAwesome icon hex code</param>
        /// <returns></returns>
        public static Bitmap GetImage(int iconText, int IconSize = 128)
        {
            return GetImage(iconText, IconSize, Color.White);
        }
        public static Bitmap GetImage(string iconText16, int IconSize = 128)
        {
            return GetImage(iconText16, IconSize, Color.White);
        }
        static StringFormat _StringFormat = new StringFormat { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center };

        public static Bitmap GetImage(int iconText, int IconSize, Color ForeColer)
        {
            return GetImageCore(GetUnicode(iconText), IconSize, ForeColer);
        }
        public static Bitmap GetImage(string iconText16, int IconSize, Color ForeColer)
        {
            return GetImageCore(GetUnicode(iconText16), IconSize, ForeColer);
        }
        static Bitmap GetImageCore(string unicode, int IconSize, Color ForeColer)
        {
            //get icon really size
            //convert font code 
            using (Font font = GetFont(IconSize))// new Font("FontAwesome", IconSize * (3f / 4f), FontStyle.Regular, GraphicsUnit.Point);
            {
                Size size = GetIconSize(font, unicode);
                var bmp = new Bitmap(size.Width, size.Height);
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    //setting graphics
                    g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
                    g.InterpolationMode = InterpolationMode.HighQualityBilinear;
                    g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    g.SmoothingMode = SmoothingMode.HighQuality;

                    Rectangle rect = new Rectangle(new Point(0, 0), size);

                    //draw icon
                    using (Brush iconBrush = new SolidBrush(ForeColer))
                    {
                        g.DrawString(unicode, font, iconBrush, rect, _StringFormat);
                    }
                }
                //resizer image
                bmp = Resizer(bmp, IconSize);

                return bmp;
            }
        }


        public static Bitmap GetImage(Font font, int iconText, int IconSize, Color ForeColer)
        {
            return GetImageCore(font, GetUnicode(iconText), IconSize, ForeColer);
        }
        public static Bitmap GetImage(Font font, string iconText16, int IconSize, Color ForeColer)
        {
            return GetImageCore(font, GetUnicode(iconText16), IconSize, ForeColer);
        }
        static Bitmap GetImageCore(Font font, string unicode, int IconSize, Color ForeColer)
        {
            //get icon really size
            //convert font code 

            Size size = GetIconSize(font, unicode);
            var bmp = new Bitmap(size.Width, size.Height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                //setting graphics
                g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
                g.InterpolationMode = InterpolationMode.HighQualityBilinear;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                g.SmoothingMode = SmoothingMode.HighQuality;

                Rectangle rect = new Rectangle(new Point(0, 0), size);

                //draw icon
                using (Brush iconBrush = new SolidBrush(ForeColer))
                {
                    g.DrawString(unicode, font, iconBrush, rect, _StringFormat);
                }
            }
            //resizer image
            bmp = Resizer(bmp, IconSize);

            return bmp;

        }

        #endregion

        #region //get icon really size

        /// <summary>
        /// get icon really size
        /// </summary>
        /// <param name="iconText">FontAwesome icon hex code</param>
        /// <returns></returns>
        public static Size GetIconSize(Font font, string unicode, int IconSize = 128)
        {
            using (var bmp = new Bitmap(IconSize, IconSize))
            {
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    return g.MeasureString(unicode, font).ToSize();
                }
            }
        }
        public static Size GetIconSize(Font font, int iconText, int IconSize = 128)
        {
            return GetIconSize(font, char.ConvertFromUtf32(iconText), IconSize);
        }
        public static string GetUnicode(string iconText16)
        {
            return GetUnicode(int.Parse(iconText16, System.Globalization.NumberStyles.HexNumber));
        }
        public static string GetUnicode(int iconText)
        {
            return char.ConvertFromUtf32(iconText);
        }

        #endregion

        #region //图像大小调整功能

        /// <summary>
        /// 图像大小调整功能
        /// </summary>
        /// <param name="srcImage">原图</param>
        /// <param name="destSize">目标图片大小</param>
        /// <param name="offset">目标图像偏移点</param>
        /// <returns></returns>
        private static Bitmap Resizer(Bitmap srcImage, int destSize)
        {
            using (srcImage)
            {
                Rectangle srcRect = GetTransparentBounds(srcImage);

                var bmp = new Bitmap(destSize, destSize);
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
                    g.InterpolationMode = InterpolationMode.HighQualityBilinear;
                    g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    g.SmoothingMode = SmoothingMode.HighQuality;

                    //Size osize = new Size(srcRect.Width - srcRect.X, srcRect.Height - srcRect.Y);

                    double dbl = (srcRect.Width * 1.0) / (srcRect.Height * 1.0);
                    int width = srcRect.Width, height = srcRect.Height;
                    int swidth = width, sheight = height;
                    if (width == height)
                    {
                        //正方形
                        if (width > destSize)
                        {
                            //图片过大
                            swidth = sheight = destSize;
                        }
                        else
                        {
                            swidth = sheight = width;
                        }
                    }
                    else if (width > height)
                    {
                        //横向图
                        if (width > destSize)
                        {
                            //width = destWidth;
                            //height = (destWidth * sourHeight) / sourWidth;
                            //图片过大
                            swidth = destSize;
                            sheight = (int)((double)destSize / dbl);
                        }
                    }
                    else
                    {
                        //纵向图
                        if (height > destSize)
                        {
                            //图片过大
                            sheight = destSize;
                            swidth = (int)((double)destSize * dbl);
                        }
                    }


                    int x = (destSize - swidth) / 2;
                    int y = (destSize - sheight) / 2;
                    //get dest & src image's draw rectangle
                    g.DrawImage(srcImage, new Rectangle(x, y, swidth, sheight), srcRect, GraphicsUnit.Pixel);

                    return bmp;
                }
            }
        }
        public static Rectangle GetTransparentBounds(Bitmap source)
        {
            var width = source.Width;
            var height = source.Height;

            int? ax1 = null, ax2 = null, ay1 = null, ay2 = null;

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    //for (int i = 0; i < srcImage.Height; i++)
                    //{
                    //    Color color = srcImage.GetPixel(i, j);
                    //}
                    Color color = source.GetPixel(x, y);
                    if (color.A > 0)
                    {
                        if (ax1.HasValue)
                        {
                            ax1 = Math.Min(ax1.Value, x);
                            ax2 = Math.Max(ax2.Value, x);
                        }
                        else
                        {
                            ax1 = x;
                            ax2 = x;
                        }

                        if (ay1.HasValue)
                        {
                            ay1 = Math.Min(ay1.Value, y);
                            ay2 = Math.Max(ay2.Value, y);
                        }
                        else
                        {
                            ay1 = y;
                            ay2 = y;
                        }

                        //System.Diagnostics.Debug.WriteLine(ax1.Value + "：" + ax2.Value);
                        //System.Diagnostics.Debug.WriteLine(ay1.Value + "：" + ay2.Value);
                    }
                };
            }

            if (ax1.HasValue && ay1.HasValue)
            {
                return new Rectangle(ax1.Value, ay1.Value, (ax2.Value + 1) - ax1.Value, (ay2.Value + 1) - ay1.Value);
            }
            return new Rectangle(0, 0, source.Width, source.Height);
        }
        #endregion

        #region //convert image to icon

        /// <summary>
        /// convert image to icon
        /// </summary>
        /// <param name="srcBitmap">The input stream</param>
        /// <param name="size">The size (16x16 px by default)</param>
        /// <returns>Icon</returns>
        private static Icon ToIcon(Bitmap srcBitmap, int size)
        {
            if (srcBitmap == null)
            {
                throw new ArgumentNullException("srcBitmap");
            }
            Icon icon = null;

            Bitmap bmp = new Bitmap(srcBitmap, new Size(size, size));

            // save the resized png into a memory stream for future use
            using (MemoryStream tmpStream = new MemoryStream())
            {
                bmp.Save(tmpStream, ImageFormat.Png);

                Stream outStraem = new MemoryStream();
                BinaryWriter writer = new BinaryWriter(outStraem);
                if (outStraem.Length <= 0)
                {
                    return null;
                }
                // 0-1 reserved, 0
                writer.Write((byte)0);
                writer.Write((byte)0);

                // 2-3 image type, 1 = icon, 2 = cursor
                writer.Write((short)1);

                // 4-5 number of images
                writer.Write((short)1);

                // image entry 1
                // 0 image width
                writer.Write((byte)size);
                // 1 image height
                writer.Write((byte)size);

                // 2 number of colors
                writer.Write((byte)0);

                // 3 reserved
                writer.Write((byte)0);

                // 4-5 color planes
                writer.Write((short)0);

                // 6-7 bits per pixel
                writer.Write((short)32);

                // 8-11 size of image data
                writer.Write((int)tmpStream.Length);

                // 12-15 offset of image data
                writer.Write((int)(6 + 16));

                // write image data
                // png data must contain the whole png data file
                writer.Write(tmpStream.ToArray());

                writer.Flush();
                writer.Seek(0, SeekOrigin.Begin);
                icon = new Icon(outStraem);
                outStraem.Dispose();
            }
            return icon;
        }

        #endregion

        #region //get font function
        public static Font GetFont(int size)
        {
            return new Font(FontCollection.Families[0], size * (3f / 4f), FontStyle.Regular, GraphicsUnit.Point);
        }
        #endregion
    }
}
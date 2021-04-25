using System.Drawing;
using System.Drawing.Imaging;

namespace TSkin
{
    public class Api
    {
        public static Image GetImgH(Image _Img, float matrix3 = 0.5f)
        {
            Rectangle rect = new Rectangle(0, 0, _Img.Width, _Img.Height);

            using (ImageAttributes attributes = new ImageAttributes())
            {
                // 建立5*5阶RGBA颜色矩阵
                ColorMatrix matrix = new ColorMatrix();
                matrix.Matrix33 = matrix3;
                // 为ImageAttributes对象指定颜色调整矩阵
                // ColorMatrixFlag.Default表示使用矩阵调整所有颜色；ColorAdjustType.Bitmap表示调整位图的颜色
                attributes.SetColorMatrix(matrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
                //dc.Clear(Color.FromKnownColor(KnownColor.ButtonFace)); // 清空DC，否则每次会将不同的透明度图像叠加显示
                Bitmap bmp = new Bitmap(rect.Width, rect.Height);
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    g.DrawImage(_Img, rect, 0, 0, rect.Width, rect.Height, GraphicsUnit.Pixel, attributes);
                }
                return bmp;
            }
        }
        public static Image GetImgHDispose(Image _Img, float matrix3 = 0.5f)
        {
            using (_Img)
            {
                Rectangle rect = new Rectangle(0, 0, _Img.Width, _Img.Height);

                using (ImageAttributes attributes = new ImageAttributes())
                {
                    // 建立5*5阶RGBA颜色矩阵
                    ColorMatrix matrix = new ColorMatrix();
                    matrix.Matrix33 = matrix3;
                    // 为ImageAttributes对象指定颜色调整矩阵
                    // ColorMatrixFlag.Default表示使用矩阵调整所有颜色；ColorAdjustType.Bitmap表示调整位图的颜色
                    attributes.SetColorMatrix(matrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
                    //dc.Clear(Color.FromKnownColor(KnownColor.ButtonFace)); // 清空DC，否则每次会将不同的透明度图像叠加显示
                    Bitmap bmp = new Bitmap(rect.Width, rect.Height);
                    using (Graphics g = Graphics.FromImage(bmp))
                    {
                        g.DrawImage(_Img, rect, 0, 0, rect.Width, rect.Height, GraphicsUnit.Pixel, attributes);
                    }
                    return bmp;
                }
            }
        }
    }
}

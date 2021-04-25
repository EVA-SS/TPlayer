using System.Drawing;
using System.Drawing.Drawing2D;

namespace TSkin
{
    public static class DrawHelper
    {
        #region RendererBackground 渲染背景图片，使背景图片不失真

        /// <summary>
        /// 渲染背景图片,使背景图片不失真
        /// </summary>
        /// <param name="g"></param>
        /// <param name="rect"></param>
        /// <param name="backgroundImage"></param>
        /// <param name="method"></param>
        public static void RendererBackground(Graphics g, Rectangle rect, Image backgroundImage, bool method)
        {
            if (!method)
            {
                g.DrawImage(backgroundImage, new Rectangle(rect.X + 0, rect.Y, 5, rect.Height), 0, 0, 5, backgroundImage.Height, GraphicsUnit.Pixel);
                g.DrawImage(backgroundImage, new Rectangle(rect.X + 5, rect.Y, rect.Width - 10, rect.Height), 5, 0, backgroundImage.Width - 10, backgroundImage.Height, GraphicsUnit.Pixel);
                g.DrawImage(backgroundImage, new Rectangle(rect.X + rect.Width - 5, rect.Y, 5, rect.Height), backgroundImage.Width - 5, 0, 5, backgroundImage.Height, GraphicsUnit.Pixel);
            }
            else
            {
                DrawHelper.RendererBackground(g, rect, 5, backgroundImage);
            }
        }

        /// <summary>
        /// 渲染背景图片,使背景图片不失真
        /// </summary>
        /// <param name="g"></param>
        /// <param name="rect"></param>
        /// <param name="cut"></param>
        /// <param name="backgroundImage"></param>
        public static void RendererBackground(Graphics g, Rectangle rect, int cut, Image backgroundImage)
        {
            //左上角
            g.DrawImage(backgroundImage, new Rectangle(rect.X, rect.Y, cut, cut), 0, 0, cut, cut, GraphicsUnit.Pixel);
            //上边
            g.DrawImage(backgroundImage, new Rectangle(rect.X + cut, rect.Y, rect.Width - cut * 2, cut), cut, 0, backgroundImage.Width - cut * 2, cut, GraphicsUnit.Pixel);
            //右上角
            g.DrawImage(backgroundImage, new Rectangle(rect.X + rect.Width - cut, rect.Y, cut, cut), backgroundImage.Width - cut, 0, cut, cut, GraphicsUnit.Pixel);
            //左边
            g.DrawImage(backgroundImage, new Rectangle(rect.X, rect.Y + cut, cut, rect.Height - cut * 2), 0, cut, cut, backgroundImage.Height - cut * 2, GraphicsUnit.Pixel);
            //左下角
            g.DrawImage(backgroundImage, new Rectangle(rect.X, rect.Y + rect.Height - cut, cut, cut), 0, backgroundImage.Height - cut, cut, cut, GraphicsUnit.Pixel);
            //右边
            g.DrawImage(backgroundImage, new Rectangle(rect.X + rect.Width - cut, rect.Y + cut, cut, rect.Height - cut * 2), backgroundImage.Width - cut, cut, cut, backgroundImage.Height - cut * 2, GraphicsUnit.Pixel);
            //右下角
            g.DrawImage(backgroundImage, new Rectangle(rect.X + rect.Width - cut, rect.Y + rect.Height - cut, cut, cut), backgroundImage.Width - cut, backgroundImage.Height - cut, cut, cut, GraphicsUnit.Pixel);
            //下边
            g.DrawImage(backgroundImage, new Rectangle(rect.X + cut, rect.Y + rect.Height - cut, rect.Width - cut * 2, cut), cut, backgroundImage.Height - cut, backgroundImage.Width - cut * 2, cut, GraphicsUnit.Pixel);
            //平铺中间
            g.DrawImage(backgroundImage, new Rectangle(rect.X + cut, rect.Y + cut, rect.Width - cut * 2, rect.Height - cut * 2), cut, cut, backgroundImage.Width - cut * 2, backgroundImage.Height - cut * 2, GraphicsUnit.Pixel);
        }

        #endregion
        public static GraphicsPath CreateRoundRect(float x, float y, float width, float height, float radius)
        {
            GraphicsPath gp = new GraphicsPath();
            gp.AddLine(x + radius, y, x + width - (radius * 2), y);
            gp.AddArc(x + width - (radius * 2), y, radius * 2, radius * 2, 270, 90);
            gp.AddLine(x + width, y + radius, x + width, y + height - (radius * 2));
            gp.AddArc(x + width - (radius * 2), y + height - (radius * 2), radius * 2, radius * 2, 0, 90);
            gp.AddLine(x + width - (radius * 2), y + height, x + radius, y + height);
            gp.AddArc(x, y + height - (radius * 2), radius * 2, radius * 2, 90, 90);
            gp.AddLine(x, y + height - (radius * 2), x, y + radius);
            gp.AddArc(x, y, radius * 2, radius * 2, 180, 90);
            gp.CloseFigure();
            return gp;
        }

        public static GraphicsPath CreateRoundRect(Rectangle rect, float radius)
        {
            return CreateRoundRect(rect.X, rect.Y, rect.Width, rect.Height, radius);
        }

        public static Color BlendColor(Color backgroundColor, Color frontColor, double blend)
        {
            double ratio = blend / 255d;
            double invRatio = 1d - ratio;
            int r = (int)((backgroundColor.R * invRatio) + (frontColor.R * ratio));
            int g = (int)((backgroundColor.G * invRatio) + (frontColor.G * ratio));
            int b = (int)((backgroundColor.B * invRatio) + (frontColor.B * ratio));
            return Color.FromArgb(r, g, b);
        }

        public static Color BlendColor(Color backgroundColor, Color frontColor)
        {
            return BlendColor(backgroundColor, frontColor, frontColor.A);
        }
    }
}

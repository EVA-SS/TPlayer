using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace TSkin
{
    public class SlideButton : Control
    {
        public delegate void SliderChangedEventHandler(object sender, EventArgs e);
        public event SliderChangedEventHandler SliderValueChanged;

        #region Variables
        float diameter;
        RoundedRectangleF rect;
        RectangleF circle;
        private bool isOn;
        float artis;
        Timer paintTicker = new Timer();
        #endregion

        #region Properties
        public bool IsOn
        {
            get { return isOn; }
            set
            {
                paintTicker.Stop();
                isOn = value;
                paintTicker.Start();
                if (SliderValueChanged != null)
                    SliderValueChanged(this, EventArgs.Empty);
            }
        }
        #endregion

        public SlideButton()
        {
            this.SetStyle(
                 ControlStyles.AllPaintingInWmPaint |
                 ControlStyles.OptimizedDoubleBuffer |
                 ControlStyles.ResizeRedraw |
                 ControlStyles.Selectable |
                 ControlStyles.DoubleBuffer | ControlStyles.Selectable |
                 ControlStyles.SupportsTransparentBackColor |
                 ControlStyles.ContainerControl |
                 ControlStyles.UserPaint, true);
            this.UpdateStyles();

            Cursor = Cursors.Hand;

            artis = 4; //increment for sliding animation
            diameter = 30;
            rect = new RoundedRectangleF(2 * diameter, diameter + 2, diameter / 2, 1, 1);
            circle = new RectangleF(1, 1, diameter, diameter);

            paintTicker.Tick += paintTicker_Tick;
            paintTicker.Interval = 1;
        }

        protected override void OnEnabledChanged(EventArgs e)
        {
            Invalidate();
            base.OnEnabledChanged(e);
        }
        protected override void OnResize(EventArgs e)
        {
            Width = (Height - 2) * 2;
            diameter = Width / 2;
            artis = 4 * diameter / 30;
            rect = new RoundedRectangleF(2 * diameter, diameter + 2, diameter / 2, 1, 1);
            circle = new RectangleF(!isOn ? 1 : Width - diameter - 1, 1, diameter, diameter);
            base.OnResize(e);
        }
        //creates slide animation
        void paintTicker_Tick(object sender, EventArgs e)
        {
            float x = circle.X;

            if (isOn)           //switch the circle to the left
            {
                if (x + artis <= Width - diameter - 1)
                {
                    x += artis;
                    circle = new RectangleF(x, 1, diameter, diameter);

                    Invalidate();
                }
                else
                {
                    x = Width - diameter - 1;
                    circle = new RectangleF(x, 1, diameter, diameter);

                    Invalidate();
                    paintTicker.Stop();
                }

            }
            else //switch the circle to the left with animation
            {
                if (x - artis >= 1)
                {
                    x -= artis;
                    circle = new RectangleF(x, 1, diameter, diameter);

                    Invalidate();
                }
                else
                {
                    x = 1;
                    circle = new RectangleF(x, 1, diameter, diameter);

                    Invalidate();
                    paintTicker.Stop();

                }
            }
        }

        protected override Size DefaultSize
        {
            get
            {
                return new Size(60, 35);
            }
        }
        Pen pen = new Pen(Color.LightGray, 2f);
        SolidBrush brush = new SolidBrush(Color.FromArgb(75, 237, 144));
        SolidBrush WhiteBrush = new SolidBrush(Color.White);
        Pen circlepen = new Pen(Color.LightGray, 1.2f);
        SolidBrush disableBrush = new SolidBrush(Color.FromArgb(207, 207, 207));
        SolidBrush ellBrush = new SolidBrush(Color.FromArgb(179, 179, 179));

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.HighQuality;
            if (Enabled)
            {
                g.FillPath(isOn ? brush : WhiteBrush, rect.Path);
                if (!isOn)
                {
                    g.DrawPath(pen, rect.Path);
                }
                g.FillEllipse(WhiteBrush, circle);
                g.DrawEllipse(circlepen, circle);
            }
            else
            {
                g.FillPath(disableBrush, rect.Path);
                g.FillEllipse(ellBrush, circle);
                g.DrawEllipse(Pens.DarkGray, circle);
            }

            base.OnPaint(e);

        }
        public event CEventHandler OnClick;
        public delegate void CEventHandler(bool isOn);
        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
                return;
            isOn = !isOn;
            IsOn = isOn;
            if (OnClick != null)
            { OnClick(isOn); }
        }
    }


    public class RoundedRectangleF
    {

        Point location;
        float radius;
        GraphicsPath grPath;
        float x, y;
        float width, height;


        public RoundedRectangleF(float width, float height, float radius, float x = 0, float y = 0)
        {

            location = new Point(0, 0);
            this.radius = radius;
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
            grPath = new GraphicsPath();
            if (radius <= 0)
            {
                grPath.AddRectangle(new RectangleF(x, y, width, height));
                return;
            }
            RectangleF upperLeftRect = new RectangleF(x, y, 2 * radius, 2 * radius);
            RectangleF upperRightRect = new RectangleF(width - 2 * radius - 1, x, 2 * radius, 2 * radius);
            RectangleF lowerLeftRect = new RectangleF(x, height - 2 * radius - 1, 2 * radius, 2 * radius);
            RectangleF lowerRightRect = new RectangleF(width - 2 * radius - 1, height - 2 * radius - 1, 2 * radius, 2 * radius);

            grPath.AddArc(upperLeftRect, 180, 90);
            grPath.AddArc(upperRightRect, 270, 90);
            grPath.AddArc(lowerRightRect, 0, 90);
            grPath.AddArc(lowerLeftRect, 90, 90);
            grPath.CloseAllFigures();

        }
        public RoundedRectangleF()
        {
        }
        public GraphicsPath Path
        {
            get
            {
                return grPath;
            }
        }
        public RectangleF Rect
        {
            get
            {
                return new RectangleF(x, y, width, height);
            }
        }
        public float Radius
        {
            get
            {
                return radius;
            }
            set
            {
                radius = value;
            }
        }

    }

}

using Animations;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

namespace TSkin
{
    public class TRadioButton : RadioButton
    {
        Point MouseLocation = new Point(-1, -1);


        // animation managers
        private readonly AnimationManager animationManager;
        private readonly AnimationManager rippleAnimationManager;

        // size related variables which should be recalculated onsizechanged
        private Rectangle radioButtonBounds;
        private int boxOffset;

        // size constants
        private const int RADIOBUTTON_SIZE = 19;
        private const int RADIOBUTTON_SIZE_HALF = RADIOBUTTON_SIZE / 2;
        private const int RADIOBUTTON_OUTER_CIRCLE_WIDTH = 2;
        private const int RADIOBUTTON_INNER_CIRCLE_SIZE = RADIOBUTTON_SIZE - (2 * RADIOBUTTON_OUTER_CIRCLE_WIDTH);

        public TRadioButton()
        {
            SetStyle(
                 ControlStyles.UserPaint |
                 ControlStyles.AllPaintingInWmPaint |
                 ControlStyles.OptimizedDoubleBuffer |
                 ControlStyles.ResizeRedraw |
                 ControlStyles.SupportsTransparentBackColor | ControlStyles.DoubleBuffer, true);
            UpdateStyles();

            Margin = new Padding(0);
            animationManager = new AnimationManager
            {
                AnimationType = AnimationType.EaseInOut,
                Increment = 0.06
            };
            rippleAnimationManager = new AnimationManager(false)
            {
                AnimationType = AnimationType.Linear,
                Increment = 0.10,
                SecondaryIncrement = 0.08
            };
            animationManager.OnAnimationProgress += sender => Invalidate();
            rippleAnimationManager.OnAnimationProgress += sender => Invalidate();

            CheckedChanged += (sender, args) => animationManager.StartNewAnimation(Checked ? AnimationDirection.In : AnimationDirection.Out);

        }
        protected override void OnSizeChanged(EventArgs e)
        {
            boxOffset = Height / 2 - (int)Math.Ceiling(RADIOBUTTON_SIZE / 2d);
            radioButtonBounds = new Rectangle(boxOffset, boxOffset, RADIOBUTTON_SIZE, RADIOBUTTON_SIZE);

            base.OnSizeChanged(e);
        }

        public override Size GetPreferredSize(Size proposedSize)
        {
            int width = boxOffset + 20 + (int)CreateGraphics().MeasureString(Text, this.Font).Width;
            return new Size(width, 30);
            //return Ripple ? new Size(width, 30) : new Size(width, 20);
        }


        SolidBrush downbrush = new SolidBrush(Color.DimGray);
        [Description("设置单选框边框颜色"), Category("TSkin样式"), DefaultValue(typeof(Color), "DimGray")]
        public Color DownColor
        {
            get => downbrush.Color;
            set
            {
                if (downbrush.Color != value)
                {
                    downbrush.Color = value;
                    this.Invalidate();
                }
            }
        }
        // Checkbox colors
        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g2 = e.Graphics;
            g2.Clear(this.BackColor);
            Rectangle rect = ClientRectangle;
            Bitmap bitmap = new Bitmap(rect.Width, rect.Height);
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.TextRenderingHint = TextRenderingHint.AntiAlias;


                var RADIOBUTTON_CENTER = boxOffset + RADIOBUTTON_SIZE_HALF;

                var animationProgress = animationManager.GetProgress();
                int colorAlpha = Enabled ? (int)(animationProgress * 255.0) : this.ForeColor.A;
                int backgroundAlpha = Enabled ? (int)(this.ForeColor.A * (1.0 - animationProgress)) : this.ForeColor.A;
                float animationSize = (float)(animationProgress * 8f);
                float animationSizeHalf = animationSize / 2;
                animationSize = (float)(animationProgress * 9f);

                //var brush = new SolidBrush(Color.Black);
                using (SolidBrush brush = new SolidBrush(Color.FromArgb(colorAlpha, Enabled ? downbrush.Color : this.ForeColor)))
                {
                    using (Pen pen = new Pen(brush.Color))
                    {
                        // draw ripple animation
                        if (rippleAnimationManager.IsAnimating())
                        {
                            for (int i = 0; i < rippleAnimationManager.GetAnimationCount(); i++)
                            {
                                var animationValue = rippleAnimationManager.GetProgress(i);
                                var animationSource = new Point(RADIOBUTTON_CENTER, RADIOBUTTON_CENTER);
                                using (SolidBrush rippleBrush = new SolidBrush(Color.FromArgb((int)((animationValue * 40)), ((bool)rippleAnimationManager.GetData(i)[0]) ? Color.Black : brush.Color)))
                                {
                                    var rippleHeight = (Height % 2 == 0) ? Height - 3 : Height - 2;
                                    var rippleSize = (rippleAnimationManager.GetDirection(i) == AnimationDirection.InOutIn) ? (int)(rippleHeight * (0.8d + (0.2d * animationValue))) : rippleHeight;
                                    using (var path = DrawHelper.CreateRoundRect(animationSource.X - rippleSize / 2, animationSource.Y - rippleSize / 2, rippleSize, rippleSize, rippleSize / 2))
                                    {
                                        g.FillPath(rippleBrush, path);
                                    }
                                }
                            }
                        }

                        // draw radiobutton circle
                        Color uncheckedColor = DrawHelper.BlendColor(this.BackColor, Enabled ? this.ForeColor : this.ForeColor, backgroundAlpha);
                        using (var path = DrawHelper.CreateRoundRect(boxOffset, boxOffset, RADIOBUTTON_SIZE, RADIOBUTTON_SIZE, 9f))
                        {
                            g.FillPath(new SolidBrush(uncheckedColor), path);

                            if (Enabled)
                            {
                                g.FillPath(brush, path);
                            }
                        }

                        g.FillEllipse(
                            new SolidBrush(this.BackColor),
                            RADIOBUTTON_OUTER_CIRCLE_WIDTH + boxOffset,
                            RADIOBUTTON_OUTER_CIRCLE_WIDTH + boxOffset,
                            RADIOBUTTON_INNER_CIRCLE_SIZE,
                            RADIOBUTTON_INNER_CIRCLE_SIZE);

                        if (Checked)
                        {
                            using (var path = DrawHelper.CreateRoundRect(RADIOBUTTON_CENTER - animationSizeHalf, RADIOBUTTON_CENTER - animationSizeHalf, animationSize, animationSize, 4f))
                            {
                                g.FillPath(brush, path);
                            }
                        }
                        Brush tom = new SolidBrush(this.ForeColor);
                        SizeF stringSize = g.MeasureString(Text, this.Font);
                        g.DrawString(Text, this.Font, tom, boxOffset + 22, Height / 2 - stringSize.Height / 2);

                    }
                }
            }
            if (base.Enabled)
            {
                g2.DrawImage(bitmap, rect);
            }
            else
            {
                g2.DrawImage(Api.GetImgHDispose(bitmap, 0.4f), rect);
            }
        }

        private bool IsMouseInCheckArea()
        {
            return radioButtonBounds.Contains(MouseLocation);
        }
        protected override void OnMouseLeave(EventArgs eventargs)
        {
            MouseLocation = new Point(-1, -1);
            base.OnMouseLeave(eventargs);
        }
        protected override void OnMouseDown(MouseEventArgs args)
        {
            if (args.Button == MouseButtons.Left && IsMouseInCheckArea())
            {
                rippleAnimationManager.SecondaryIncrement = 0;
                rippleAnimationManager.StartNewAnimation(AnimationDirection.InOutIn, new object[] { Checked });
            }
            base.OnMouseDown(args);
        }
        protected override void OnMouseUp(MouseEventArgs mevent)
        {
            rippleAnimationManager.SecondaryIncrement = 0.08;
            base.OnMouseUp(mevent);
        }
        protected override void OnMouseMove(MouseEventArgs args)
        {
            MouseLocation = args.Location;
            Cursor = IsMouseInCheckArea() ? Cursors.Hand : Cursors.Default;
            base.OnMouseMove(args);
        }
    }
}

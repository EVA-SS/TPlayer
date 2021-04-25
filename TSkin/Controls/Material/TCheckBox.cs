using Animations;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

namespace TSkin
{
    public class TCheckBox : CheckBox
    {
        [Browsable(false)]
        public Point MouseLocation { get; set; }

        private readonly AnimationManager animationManager;
        private readonly AnimationManager rippleAnimationManager;

        private const int CHECKBOX_SIZE = 18;
        private const int CHECKBOX_SIZE_HALF = CHECKBOX_SIZE / 2;
        private const int CHECKBOX_INNER_BOX_SIZE = CHECKBOX_SIZE - 4;

        private int boxOffset;
        private Rectangle boxRectangle;

        public TCheckBox()
        {
            SetStyle(
                 ControlStyles.UserPaint |
                 ControlStyles.AllPaintingInWmPaint |
                 ControlStyles.OptimizedDoubleBuffer |
                 ControlStyles.ResizeRedraw |
                 ControlStyles.DoubleBuffer, true);
            //强制分配样式重新应用到控件上
            UpdateStyles();
            animationManager = new AnimationManager
            {
                AnimationType = AnimationType.EaseInOut,
                Increment = 0.05
            };
            rippleAnimationManager = new AnimationManager(false)
            {
                AnimationType = AnimationType.Linear,
                Increment = 0.10,
                SecondaryIncrement = 0.08
            };
            animationManager.OnAnimationProgress += sender => Invalidate();
            rippleAnimationManager.OnAnimationProgress += sender => Invalidate();

            CheckedChanged += (sender, args) =>
            {
                animationManager.StartNewAnimation(Checked ? AnimationDirection.In : AnimationDirection.Out);
            };

            MouseLocation = new Point(-1, -1);
        }

        SolidBrush downbrush = new SolidBrush(Color.DimGray);
        [Description("设置复选框边框颜色"), Category("TSkin样式"), DefaultValue(typeof(Color), "DimGray")]
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


        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);

            boxOffset = Height / 2 - 9;
            boxRectangle = new Rectangle(boxOffset, boxOffset, CHECKBOX_SIZE - 1, CHECKBOX_SIZE - 1);
        }

        public override Size GetPreferredSize(Size proposedSize)
        {
            int w = boxOffset + CHECKBOX_SIZE + 2 + (int)CreateGraphics().MeasureString(Text, this.Font).Width;
            return new Size(w, 30);
        }

        private static readonly Point[] CHECKMARK_LINE = { new Point(3, 8), new Point(7, 12), new Point(14, 5) };
        private const int TEXT_OFFSET = 22;

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
                // clear the control
                //g.Clear(this.BackColor);
                var CHECKBOX_CENTER = boxOffset + CHECKBOX_SIZE_HALF - 1;
                double animationProgress = animationManager.GetProgress();
                int colorAlpha = Enabled ? (int)(animationProgress * 255.0) : this.ForeColor.A;
                int backgroundAlpha = Enabled ? (int)(this.ForeColor.A * (1.0 - animationProgress)) : this.ForeColor.A;
                //var brush = new SolidBrush(BcColor);
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
                                var animationSource = new Point(CHECKBOX_CENTER, CHECKBOX_CENTER);
                                //var rippleBrush = new SolidBrush(Color.ForestGreen);
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

                        var checkMarkLineFill = new Rectangle(boxOffset, boxOffset, (int)(17.0 * animationProgress), 17);
                        using (var checkmarkPath = DrawHelper.CreateRoundRect(boxOffset, boxOffset, 17, 17, 1f))
                        {
                            //SolidBrush brush2 = new SolidBrush(DrawHelper.BlendColor(this.BackColor, Enabled ? SkinManager.GetCheckboxOffColor() : this.ForeColor, backgroundAlpha));
                            using (Pen pen2 = new Pen(downbrush.Color))
                            {
                                g.FillPath(downbrush, checkmarkPath);
                                g.DrawPath(pen2, checkmarkPath);

                                g.FillRectangle(new SolidBrush(this.BackColor), boxOffset + 2, boxOffset + 2, CHECKBOX_INNER_BOX_SIZE - 1, CHECKBOX_INNER_BOX_SIZE - 1);
                                g.DrawRectangle(new Pen(this.BackColor), boxOffset + 2, boxOffset + 2, CHECKBOX_INNER_BOX_SIZE - 1, CHECKBOX_INNER_BOX_SIZE - 1);
                            }

                            if (Enabled)
                            {
                                g.FillPath(brush, checkmarkPath);
                                g.DrawPath(pen, checkmarkPath);
                            }
                            else if (Checked)
                            {
                                g.SmoothingMode = SmoothingMode.None;
                                g.FillRectangle(brush, boxOffset + 2, boxOffset + 2, CHECKBOX_INNER_BOX_SIZE, CHECKBOX_INNER_BOX_SIZE);
                                g.SmoothingMode = SmoothingMode.AntiAlias;
                            }

                            g.DrawImageUnscaledAndClipped(DrawCheckMarkBitmap(), checkMarkLineFill);
                        }

                        // draw checkbox text
                        Brush tom = new SolidBrush(this.ForeColor);
                        SizeF stringSize = g.MeasureString(Text, this.Font);
                        g.DrawString(Text, this.Font, tom, boxOffset + TEXT_OFFSET, Height / 2 - stringSize.Height / 2);

                        // dispose used paint objects
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

        private Bitmap DrawCheckMarkBitmap()
        {
            var checkMark = new Bitmap(CHECKBOX_SIZE, CHECKBOX_SIZE);
            var g = Graphics.FromImage(checkMark);

            // clear everything, transparant
            g.Clear(Color.Transparent);

            // draw the checkmark lines
            using (var pen = new Pen(this.BackColor, 2))
            {
                g.DrawLines(pen, CHECKMARK_LINE);
            }

            return checkMark;
        }

        public override bool AutoSize
        {
            get { return base.AutoSize; }
            set
            {
                base.AutoSize = value;
                if (value)
                {
                    Size = new Size(10, 10);
                }
            }
        }

        private bool IsMouseInCheckArea()
        {
            return boxRectangle.Contains(MouseLocation);
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();

            if (DesignMode) return;
            MouseLeave += (sender, args) =>
            {
                MouseLocation = new Point(-1, -1);
            };
            MouseDown += (sender, args) =>
            {
                if (args.Button == MouseButtons.Left && IsMouseInCheckArea())
                {
                    rippleAnimationManager.SecondaryIncrement = 0;
                    rippleAnimationManager.StartNewAnimation(AnimationDirection.InOutIn, new object[] { Checked });
                }
            };
            MouseUp += (sender, args) =>
            {
                rippleAnimationManager.SecondaryIncrement = 0.08;
            };
            MouseMove += (sender, args) =>
            {
                MouseLocation = args.Location;
                Cursor = IsMouseInCheckArea() ? Cursors.Hand : Cursors.Default;
            };
        }

    }
}

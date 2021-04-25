using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TPlayer.Frm
{
    public partial class Message : Form
    {
        public ControlAnimation controlAnimation = new ControlAnimation();
        [DllImport("user32")]
        private static extern bool AnimateWindow(IntPtr hwnd, int dwTime, int dwFlags);

        public static List<Message> messages = new List<Message>();
        Form form;
        public Message(Form form, MessageType type, string txt)
        {
            this.form = form;
            InitializeComponent();
            base.SetStyle(
              ControlStyles.UserPaint |
              ControlStyles.AllPaintingInWmPaint |
              ControlStyles.OptimizedDoubleBuffer |
              ControlStyles.ResizeRedraw |
              ControlStyles.DoubleBuffer, true);
            base.UpdateStyles();

            int wi = TextRenderer.MeasureText(txt, label_txt.Font).Width + 80;
            //System.Diagnostics.Debug.WriteLine("字体宽度：" + wi);
            if (wi < 470)
            {
                this.Width = wi;
            }
            label_txt.Text = txt;
            switch (type)
            {
                case MessageType.Good:
                    this.BackgroundImage = Properties.Resources.bg_ribbon;
                    pic.Image = Properties.Resources.icon_good;
                    this.BackColor = Color.FromArgb(253, 246, 236);
                    this.ForeColor = Color.FromArgb(255, 202, 100);
                    break;
                case MessageType.Info:
                    pic.Image = Properties.Resources.icon_info;
                    this.BackColor = Color.FromArgb(235, 238, 245);
                    this.ForeColor = Color.FromArgb(144, 147, 153);
                    break;
                case MessageType.Success:
                    pic.Image = Properties.Resources.icon_success;
                    this.BackColor = Color.FromArgb(240, 249, 235);
                    this.ForeColor = Color.FromArgb(103, 194, 58);
                    break;
                case MessageType.Warn:
                    pic.Image = Properties.Resources.icon_warn;
                    this.BackColor = Color.FromArgb(253, 246, 236);
                    this.ForeColor = Color.FromArgb(230, 162, 60); break;
                case MessageType.Error:
                    pic.Image = Properties.Resources.icon_error;
                    this.BackColor = Color.FromArgb(254, 240, 240);
                    this.ForeColor = Color.FromArgb(245, 108, 108); break;
            }

        }

        #region 无焦点窗体
        protected override CreateParams CreateParams
        {
            get
            {
                const int WS_EX_NOACTIVATE = 0x08000000;
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= WS_EX_NOACTIVATE;
                return cp;
            }
        }

        #endregion

        private void SetReion()
        {
            if (base.Region != null)
            {
                base.Region.Dispose();
            }
            TSkin.MainApi.MainApi.CreateRegion(this, this.ClientRectangle, 6, TSkin.UICornerRadiusSides.All);
        }

        protected override void OnLoad(EventArgs e)
        {
            int _top = 0;
            if (messages.Count == 0)
            {
                if (form != null)
                {
                    _top = form.Top + 10;
                    this.Location = new Point(form.Left + ((form.Width - this.Width) / 2), -this.Height);
                }
                else
                {
                    this.Top = -this.Height;
                }
            }
            else
            {
                if (form != null)
                {
                    _top = (messages[messages.Count - 1].Top + messages[messages.Count - 1].Height) + 10;
                    this.Location = new Point(form.Left + ((form.Width - this.Width) / 2), messages[messages.Count - 1].Top - this.Height);
                }
                else
                {
                    this.Top = messages[messages.Count - 1].Top - this.Height;
                }
            }
            messages.Add(this);
            base.OnLoad(e);
            SetReion();
            label_txt.Click += Close_Click;
            pic.Click += Close_Click;
            this.Click += Close_Click;

            controlAnimation.TopMove(this, _top, 200, AnimationType.Ball);
            TaskFactory _task = new TaskFactory();
            Action _action = () =>
            {
                System.Threading.Thread.Sleep(2000);
            };
            _task.ContinueWhenAll(new Task[] { _task.StartNew(_action) }, (action =>
            {
                if (!isClose)
                {
                    this.Invoke(new Action(() =>
                    {
                        this.Close();
                    }));
                }
            }));

        }
        bool isClose = false;
        private void Close_Click(object sender, EventArgs e)
        {
            isClose = true;
            this.Close();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            messages.Remove(this);
            int top;
            if (form != null)
            { top = form.Top + 10; }
            else
            {
                top = 10;
            }
            foreach (Message item in messages)
            {
                controlAnimation.TopMove(item, top, 100, AnimationType.Ball);
                top += item.Height + 10;
            }
            base.OnClosing(e);
            AnimateWindow(this.Handle, 200, 0x00080000 | 0x00010000);

        }
    }
    public enum MessageType
    {
        Good,
        Info,
        Success,
        Warn,
        Error
    }
}

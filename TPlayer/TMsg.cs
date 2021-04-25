using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TPlayer
{
    public partial class TMsg : NetDimension.WinForm.ModernUIForm
    {
        TaskFactory _task = new TaskFactory();
        public TMsg()
        {
            SetStyle(
                  ControlStyles.UserPaint |
                  ControlStyles.AllPaintingInWmPaint |
                  ControlStyles.OptimizedDoubleBuffer |
                  ControlStyles.ResizeRedraw |
                  ControlStyles.DoubleBuffer, true);
            UpdateStyles();
            InitializeComponent();
        }
        ControlAnimation controlAnimation = new ControlAnimation();
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

        }

        [DllImport("user32")]
        public static extern bool AnimateWindow(IntPtr whnd, int dwtime, int dwflag);
        protected override void OnClosing(CancelEventArgs e)
        {
            this.CloseShadow();
            AnimateWindow(this.Handle, 300, 0x0001 | 0x10000);
            base.OnClosing(e);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Dispose();
            panel1.Size = this.Size;
            panel1.Left = this.Width;
            Action task = () =>
            {
                Invoke(new Action(() =>
                {
                    controlAnimation.LeftMove(panel1, 0, 300, AnimationType.Ball);
                }));
                Thread.Sleep(320);


                Invoke(new Action(() =>
                {
                    label1.Visible = true;
                    controlAnimation.WidthMove(panel1, 5, 300, AnimationType.Ball);
                }));
                Thread.Sleep(2000);


                Invoke(new Action(() =>
                {
                    controlAnimation.WidthMove(panel1, this.Width, 300, AnimationType.Ball);
                }));
                Thread.Sleep(320);

                Invoke(new Action(() =>
                {
                    label1.Visible = false; panel1.Visible = false;
                    this.BackColor = panel1.BackColor;
                    this.Close();
                    //controlAnimation.LeftWidthMove(this,0, this.Left + this.Width, 300, AnimationType.Ball);
                }));
                //Invoke(new Action(() =>
                //{
                //    controlAnimation.LeftMove(panel1, this.Width, 300, AnimationType.Ball);
                //}));
            };
            _task.StartNew(task);
        }
    }
}

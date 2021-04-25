using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TPlayer.Frm
{
    public partial class Update : TSkin.Main
    {
        #region 窗口基本操作

        #region 调用非托管的动态链接库来让窗体可以拖动
        [DllImport("User32.DLL")]
        public static extern int SendMessage(IntPtr hWnd, uint Msg, int wParam, int lParam);
        [DllImport("User32.DLL")]
        public static extern bool ReleaseCapture();
        public void Frm_Move(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, 0x0112, 61456 | 2, 0);
            }
        }
        #endregion

        #endregion

        UpdateInfo updateInfo;
        TaskFactory _task = new TaskFactory();
        public Update(UpdateInfo updateInfo)
        {
            this.updateInfo = updateInfo;
            InitializeComponent();
            label2.Text += $" “{updateInfo.verson}” " + updateInfo.time.ToString("yyyy年MM月dd日 HH:mm");
            icon_pie.Visible = updateInfo.pre;
            label4.Text = updateInfo.title;
            label5.Text = updateInfo.descTxt;

            //label1.Text = verson;
        }
        protected override void OnLoad(EventArgs e)
        {
            this.Height = label5.Top + label5.PreferredSize.Height + 100;
            this.MinimumSize = this.Size;
            base.OnLoad(e);
        }
        private void btn_close_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
        }

    }
}

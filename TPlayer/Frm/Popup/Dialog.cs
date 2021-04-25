using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Popup
{
    public partial class Dialog : TSkin.Main
    {
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


        /// <summary>
        /// 确认框
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="text">内容</param>
        public Dialog(string title, string text) : this(title, text, null, "确认", false)
        {
        }
        /// <summary>
        /// 确认框
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="text">内容</param>
        /// <param name="mini">是否迷你模式</param>
        public Dialog(string title, string text, bool mini) : this(title, text, null, "确认", mini)
        {
        }
        /// <summary>
        /// 确认框
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="text">内容</param>
        /// <param name="btn">按钮文字</param>
        public Dialog(string title, string text, string btn) : this(title, text, null, btn, false)
        {
        }

        /// <summary>
        /// 确认框
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="text">内容</param>
        /// <param name="value">默认值提示</param>
        /// <param name="mini">是否迷你模式</param>
        public Dialog(string title, string text, string value, bool mini) : this(title, text, value, "确认", mini)
        {
        }

        /// <summary>
        /// 确认框
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="text">内容</param>
        /// <param name="value">默认值提示</param>
        /// <param name="btn">按钮文字</param>
        /// <param name="mini">是否迷你模式</param>
        public Dialog(string title, string text, string value, string btn, bool mini)
        {
            InitializeComponent();
            btn_ok.Text = btn;
            this.panel_value.Text = text;
            this.Text = label_title.Text = title;
            if (value != null)
            {
                this.label_value.Text = value;
            }
            if (mini)
            {
                this.panel_value.TextAlign = System.Drawing.ContentAlignment.TopCenter;
                this.Width = this.MaximumSize.Width;
            }
        }

        PopupCore.ActionBoolNo _before = null;
        public Dialog Before(PopupCore.ActionBoolNo action)
        {
            _before = action;
            return this;
        }
        private void btn_ok_Click(object sender, EventArgs e)
        {
            if (_before != null)
            {
                btn_ok.Enabled = false;
                btn_ok.State = true;

                IAsyncResult iresult = _before.BeginInvoke(null, null);
                bool result = _before.EndInvoke(iresult);
                btn_ok.State = false;
                btn_ok.Enabled = true;
                if (result)
                {
                    this.DialogResult = DialogResult.OK;
                }
            }
            else
            {
                this.DialogResult = DialogResult.OK;
            }
        }

        private void btn_no_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
        }
    }

    public class PopupCore
    {

        public delegate bool ActionBoolNo();
    }
}

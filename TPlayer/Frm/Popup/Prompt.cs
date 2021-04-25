using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Popup
{
    public partial class Prompt : TSkin.Main
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
        /// 输入框
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="value">默认值</param>
        /// <param name="btn">按钮文字</param>
        public Prompt(string title, string value = null, string btn = "确认")
        {
            InitializeComponent();
            btn_ok.Text = btn;
            panel_value.Click += (object _s, EventArgs _e) =>
            {
                txt_value.Focus();
            };
            this.Text = label_title.Text = title;
            if (value != null)
            {
                this.label_value.Text = txt_value.Text = value;
            }
        }
        public string Val = null;

        PopupCore.ActionBoolNo _before = null;
        public Prompt Before(PopupCore.ActionBoolNo action)
        {
            _before = action;
            return this;
        }
        private void btn_ok_Click(object sender, EventArgs e)
        {
            this.panel_value.IsActive = false;
            Val = this.txt_value.Text;
            if (string.IsNullOrEmpty(Val) && string.IsNullOrEmpty(label_value.Text))
            {
                this.panel_value.IsActive = true;
                this.txt_value.Focus();
                return;
            }
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
                else
                {
                    this.panel_value.IsActive = true;
                    this.txt_value.Focus();
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
}

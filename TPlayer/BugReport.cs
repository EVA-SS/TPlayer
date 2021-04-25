using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TPlayer
{
    public partial class BugReport : NetDimension.WinForm.ModernUIForm
    {
        #region 调用非托管的动态链接库来让窗体可以拖动
        [DllImport("User32.DLL")]
        public static extern int SendMessage(IntPtr hWnd, uint Msg, int wParam, int lParam);
        [DllImport("User32.DLL")]
        public static extern bool ReleaseCapture();
        public void FrmMove(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(Handle, 0x0112, 61456 | 2, 0);
        }
        #endregion

        Exception _bugInfo;
        public BugReport(Exception _bugInfo)
        {
            this._bugInfo = _bugInfo;
            InitializeComponent();
            this.label4.Text += $"  （{_bugInfo.GetType().Name}）";
            this.label5.Text = _bugInfo.Message;
            btn_close.Image = FontAwesome.GetImage("4FA4", 30, Color.White);
            //sb.Append("【异常类型】：" + ex.GetType().Name + "<br>");
            //sb.Append("【异常信息】：" + ex.Message + "<br>");
            //sb.Append("【异常方法】：" + ex.TargetSite + "<br>");
            //sb.Append("【堆栈调用】：" + ex.StackTrace + "<br>");
        }

        TaskFactory _task = new TaskFactory();
        private void btn_ok_Click(object sender, EventArgs e)
        {
            loading.State = true;
            btn_close.Enabled = btn_ok.Enabled = false;
            bool isok = false;
            Action _action = () =>
            {
                isok = SendEmail();
            };
            _task.ContinueWhenAll(new Task[] { _task.StartNew(_action) }, (action =>
            {
                loading.State = false;
                if (!isok)
                {
                    Api.OpenMessage(this, Frm.MessageType.Error, "发送失败");
                    this.Invoke(new Action(() =>
                    {
                        btn_close.Enabled = btn_ok.Enabled = true;
                    }));
                }
                else
                {
                    this.BeginInvoke(new Action(() =>
                    {
                        this.DialogResult = DialogResult.OK;
                    }));
                }
            }));
        }

        private bool SendEmail()
        {
            PlatformHelper plat = new PlatformHelper();
            StringBuilder errTxt = new StringBuilder();
            errTxt.Append(string.Format("用户名 - {0}<br>操作系统版本 - {1} {2}<br>机器名 - {3}<br>", Environment.UserName, plat.FullName, plat.Build, Environment.MachineName));
            errTxt.Append("【异常类型】：" + _bugInfo.GetType().Name + "<br>");
            errTxt.Append("【异常信息】：" + _bugInfo.Message + "<br>");
            errTxt.Append("【异常方法】：" + _bugInfo.TargetSite + "<br>");
            errTxt.Append("【堆栈调用】：" + _bugInfo.StackTrace + "<br>");
            try
            {
                string[] filesok = null;
                //图像附件
                using (System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient("smtp.163.com"))
                {
                    client.UseDefaultCredentials = false;
                    client.Credentials = new System.Net.NetworkCredential("tomknow@163.com", "你的密码");
                    client.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                    using (System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage("tomknow@163.com", "17379620@qq.com", "TPlayer错误报告" + Application.ProductVersion.ToString(), errTxt.ToString()))
                    {
                        try
                        {
                            string[] files = Helper.Global.GetFiles(Program.BasePath + "log\\", ".log");
                            foreach (string item in files)
                            {
                                message.Attachments.Add(new System.Net.Mail.Attachment(item));
                            }
                            filesok = files;
                        }
                        catch { }
                        message.BodyEncoding = Encoding.UTF8;
                        message.IsBodyHtml = true;
                        client.Send(message);
                    }
                }
                if (filesok != null)
                {
                    Log.LogHelper.ExitThread();
                    System.Threading.Thread.Sleep(500);

                    foreach (string item in filesok)
                    {
                        try
                        {
                            File.Delete(item);
                        }
                        catch
                        { }
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        private void btn_no_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
        }
        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            //Application.Exit(); 
            Environment.Exit(0);
        }
    }
}

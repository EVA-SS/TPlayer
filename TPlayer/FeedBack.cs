using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TPlayerSupport;

namespace TPlayer
{
    public partial class FeedBack : Form
    {
        public FeedBack()
        {
            InitializeComponent();
        }
        protected override void OnLoad(EventArgs e)
        {
            //SetupSystemMenu();
            base.OnLoad(e);
        }
        #region 安装系统菜单

        [DllImport("user32.dll")]
        static extern IntPtr GetSystemMenu(IntPtr hwnd, int bRevert);

        [DllImport("user32.dll")]
        static extern int AppendMenu(IntPtr hMenu, int Flagsw, int IDNewItem, string lpNewItem);
        void SetupSystemMenu()
        {
            IntPtr menu = GetSystemMenu(this.Handle, 0);
            //   add   a   separator 
            AppendMenu(menu, 0xA00, 0, null);
            //   add   an   item   with   a   unique   ID 
            AppendMenu(menu, 0, 1234, "聚合视频");
            AppendMenu(menu, 0, 1235, "打开本地");
            AppendMenu(menu, 0, 1236, "打开URL");
        }
        #endregion

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd_file = "imgs".OpenFile())
            {
                if (ofd_file.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        pictureBox1.BackgroundImage = Image.FromFile(ofd_file.FileName);
                    }
                    catch { }
                }
            }
            //pictureBox1
        }

        TaskFactory _task = new TaskFactory();
        private void btn_Click(object sender, EventArgs e)
        {
            string title = textBox1.Text, desc = textBox2.Text;
            if (string.IsNullOrEmpty(title))
            {
                textBox1.Focus();
                return;
            }
            btn.Enabled = false;
            bool isok = false;
            Image img = pictureBox1.BackgroundImage;
            Action _action = () =>
            {
                isok = SendEmail(title, desc, img);
            };
            _task.ContinueWhenAll(new Task[] { _task.StartNew(_action) }, (action =>
            {
                if (!isok)
                {
                    Api.OpenMessage(this, Frm.MessageType.Error, "发送失败");
                    this.Invoke(new Action(() =>
                    {
                        btn.Enabled = true;
                    }));
                }
                else
                {
                    Api.OpenMessage(Program._Main, Frm.MessageType.Good, "发送成功！谢谢您的反馈我们一定再接再厉");
                    this.BeginInvoke(new Action(() =>
                    {
                        this.Close();
                    }));
                }
            }));
        }
        private bool SendEmail(string title, string desc, Image img)
        {
            PlatformHelper plat = new PlatformHelper();
            StringBuilder errTxt = new StringBuilder();
            errTxt.Append(string.Format("用户名 - {0}<br>操作系统版本 - {1} {2}<br>机器名 - {3}<br>", Environment.UserName, plat.FullName, plat.Build, Environment.MachineName));

            errTxt.Append("<h2>" + title + "</h2>");
            if (string.IsNullOrEmpty(desc))
            {
                errTxt.Append("<br>");
            }
            else
            {
                foreach (string item in desc.Split('\n'))
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        errTxt.Append(item.Trim() + "<br>");
                    }
                }
            }
            if (img != null)
            {
                errTxt.Append("<img src=\"" + img.ToByte().ToBase64ByImg() + "\"/>");
            }
            try
            {
                using (System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient("smtp.163.com"))
                {
                    client.UseDefaultCredentials = false;
                    client.Credentials = new System.Net.NetworkCredential("tomknow@163.com", "你的密码");
                    client.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                    using (System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage("tomknow@163.com", "17379620@qq.com", "TPlayer" + ftype + " " + Application.ProductVersion.ToString(), errTxt.ToString()))
                    {
                        message.BodyEncoding = Encoding.UTF8;
                        message.IsBodyHtml = true;
                        client.Send(message);
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }


        string ftype = "";

        private void btn_type1_Click(object sender, EventArgs e)
        {
            ftype = "问题反馈";
            panel1.Visible = true;
            this.Text += " -" + ftype;
        }

        private void btn_type2_Click(object sender, EventArgs e)
        {
            ftype = "功能反馈";
            panel1.Visible = true;
            this.Text += " -" + ftype;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                Image image = Clipboard.GetImage();
                if (image != null)
                {
                    pictureBox1.BackgroundImage = image;
                }
                else
                {
                    bool isok = false;
                    string str = Clipboard.GetText();
                    if (!string.IsNullOrEmpty(str) && File.Exists(str))
                    {
                        try
                        {
                            pictureBox1.BackgroundImage = Image.FromFile(str); isok = true;
                        }
                        catch { }
                    }
                    if (!isok)
                    {
                        object data = Clipboard.GetData(DataFormats.FileDrop);
                        if (data is string[])
                        {
                            string[] myFiles = data as string[];
                            if (myFiles.Length > 0)
                            {
                                try
                                {
                                    pictureBox1.BackgroundImage = Image.FromFile(myFiles[0]); isok = true;
                                }
                                catch { }
                            }
                        }
                    }
                }
            }
            catch { }
        }
    }
}

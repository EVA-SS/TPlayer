using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace TPlayer.Frm
{
    public partial class WebBrowser : NetDimension.WinForm.ModernUIForm
    {
        #region 窗口基本操作

        #region 调用非托管的动态链接库来让窗体可以拖动
        [DllImport("User32.DLL")]
        public static extern int SendMessage(IntPtr hWnd, uint Msg, int wParam, int lParam);
        [DllImport("User32.DLL")]
        public static extern bool ReleaseCapture();
        public void Frm_Move(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && !isMax)
            {
                ReleaseCapture();
                SendMessage(Handle, 0x0112, 61456 | 2, 0);
            }
        }
        public void Frm_MaxMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (e.Clicks == 2)
                {
                    Max();
                }
                else if (!isMax)
                {
                    Frm_Move(sender, e);
                }
            }
        }
        #endregion

        public bool isMax = false;//记录是否最大化

        /// <summary>
        /// 最大化还原
        /// </summary>
        public void Max()
        {

            if (WindowState == FormWindowState.Maximized)
            {
                btn_max.Image = FontAwesome.GetImage("4FB2", 30, Color.Black);
                isMax = false;
                WindowState = FormWindowState.Normal;
            }
            else
            {
                btn_max.Image = FontAwesome.GetImage("4FB1", 30, Color.Black);
                isMax = true;
                WindowState = FormWindowState.Maximized;
            }

        }

        private void Min_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void Max_Click(object sender, EventArgs e)
        {
            Max();
        }

        private void Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        TPlayer player;
        public WebBrowser(TPlayer player)
        {
            this.player = player;
            InitializeComponent();
            btn_min.Image = FontAwesome.GetImage("4FA9", 30, Color.Black);
            btn_max.Image = FontAwesome.GetImage("4FB2", 30, Color.Black);
            btn_close.Image = FontAwesome.GetImage("4FA3", 30, Color.Black);
            btn_close.ImageHove = FontAwesome.GetImage("4FA3", 30, Color.White);

            btn_list.Image = FontAwesome.GetImage("4F8F", 30, Color.Black);
            btn_next.Image = FontAwesome.GetImage("50D9", 18, Color.Black);
        }
        public WebBrowser(TPlayer player, PlayerItem playerItem) : this(player)
        {
            this.Text = label1.Text = playerItem.name;
            try
            {
                webPlay.Navigate(playerItem.url);
            }
            catch
            {
                webPlay = new System.Windows.Forms.WebBrowser
                {
                    Dock = DockStyle.Fill,
                    Name = "webPlay",
                    //ScriptErrorsSuppressed = true,
                    TabIndex = 0
                };
                this.Controls.Add(webPlay);
                webPlay.BringToFront();
                webPlay.DocumentCompleted += webPlay_DocumentCompleted;
                webPlay.Navigate(playerItem.url);
            }
            pictureBox2.Visible = true;
            pictureBox2.Start();
        }


        private void btn_list_Click(object sender, EventArgs e)
        {
        }

        private void btn_next_Click(object sender, EventArgs e)
        {
        }

        private void webPlay_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            pictureBox2.Stop();
            pictureBox2.Visible = false;
        }
        private void webPlay_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
        }

        private void webPlay_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            //webPlay.Visible = false;
        }
    }
}

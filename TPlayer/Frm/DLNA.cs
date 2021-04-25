using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace TPlayer
{
    public partial class DLNA : NetDimension.WinForm.ModernUIForm
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

        TPlayer player;
        public DLNA(TPlayer player)
        {
            this.player = player;
            InitializeComponent();

            btn_refresh.Image = FontAwesome.GetImage("4FA6", 30, Color.Black);
            btn_min.Image = FontAwesome.GetImage("4FA9", 30, Color.Black);
            btn_close.Image = FontAwesome.GetImage("4FA3", 30, Color.Black);
            btn_close.ImageHove = FontAwesome.GetImage("4FA3", 30, Color.White);
        }
        protected override void OnClosing(CancelEventArgs e)
        {
            player._DLNA = null;
            base.OnClosing(e);
        }
        protected override void OnLoad(EventArgs e)
        {
            if (player.player.GetConfig(1801) == "1")
            {
                btn_refresh.Enabled = slideButton1.IsOn = true;
                deviceList();
            }
            slideButton1.OnClick += (bool ison) =>
            {
                btn_refresh.Enabled = ison;
                player.player.SetConfig(1801, ison ? "1" : "0");
                if (ison)
                {
                    player.player.SetConfig(1804, "");
                    deviceList();
                }
            };

            base.OnLoad(e);
        }

        public void deviceList()
        {
            metroLoading.State = true;
            if (player.device_list == null)
            {
                player.device_list = player.player.GetConfig(1802);
            }
            if (!string.IsNullOrEmpty(player.device_list))
            {
                panel1.SuspendLayout();
                panel1.Controls.Clear();
                string dlna = player.player.GetConfig(1803);


                if (!string.IsNullOrEmpty(dlna))
                {
                    if (panel_desk != null)
                    {
                        panel_desk.Enabled = false;
                    }
                    player.isDLNA = true;
                }
                else
                {
                    if (panel_desk != null)
                    {
                        panel_desk.Enabled = true;
                    }
                    player.isDLNA = false;
                }

                string[] device_lists = player.device_list.Split('\n');
                foreach (string item in device_lists)
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        string[] items = item.Split(';');

                        panel1.Controls.Add(AddControl(dlna, items[0], items[2], items[1]));
                    }
                }

                if (player.isDLNA)
                {
                    panel1.Controls.Add(AddControl(dlna, "返回本机", "退出投屏功能", ""));
                }

                panel1.ResumeLayout();
            }
            metroLoading.State = false;
        }

        Control AddControl(string sel, string name, string demo, string tag)
        {
            TSkin.TButDLNA butDLNA = new TSkin.TButDLNA
            {
                Dock = DockStyle.Top,
                ActiveColor = Color.White,
                ActiveColor2 = Color.FromArgb(240, 240, 240),
                DefaultColor = Color.Transparent,
                DefaultColor2 = Color.Transparent,
                Font = new Font("微软雅黑", 14F),
                Image = string.IsNullOrEmpty(tag) ? Properties.Resources.icon_tv_sel : Properties.Resources.icon_tv,
                IsActive = sel == tag,
                Size = new Size(0, 80),
                Text = name,
                Text2 = demo
            };
            butDLNA.Click += (object _s, EventArgs _e) =>
            {
                Panel_Click(butDLNA, name, tag);
            };
            return butDLNA;
        }

        private void Panel_Click(TSkin.TButDLNA sender, string name, string tag)
        {
            foreach (Control item in panel1.Controls)
            {
                if (item is TSkin.TButDLNA)
                {
                    (item as TSkin.TButDLNA).IsActive = false;
                }
            }
            player.player.SetConfig(1803, tag);
            if (string.IsNullOrEmpty(tag))
            {
                player.isDLNA = false;
                panel_desk.Enabled = true;
                player.ShowPrompt("DLNA设备", "本机");
            }
            else
            {
                player.isDLNA = true;
                panel_desk.Enabled = false;
                player.ShowPrompt("DLNA设备", name);
            }
            sender.IsActive = true;
            //deviceList();
        }
        private void PanelZM_Click(object sender, EventArgs e)
        {
            if (!player.isDLNA)
            {
                player.isDeskTop = !player.isDeskTop;
                panel_desk.IsActive = player.isDeskTop;
            }
        }

        private void refresh_Click(object sender, EventArgs e)
        {
            player.player.SetConfig(1804, "");
            player.device_list = player.player.GetConfig(1802);
            //deviceList();
        }


        private void btn_close_Click(object sender, EventArgs e)
        {
            //player.Activate();
            this.Close();
        }

        private void btn_min_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
    }
}

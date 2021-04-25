using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TPlayer.Frm
{
    public partial class Setting : NetDimension.WinForm.ModernUIForm
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
        public TPlayer player;
        public Setting(TPlayer player, string index = "1")
        {
            this.player = player;
            InitializeComponent();
            this._index = index;
            btn_refresh.Image = FontAwesome.GetImage("4FA6", 30, Color.Black);
            btn_min.Image = FontAwesome.GetImage("4FA9", 30, Color.Black);
            btn_close.Image = FontAwesome.GetImage("4FA3", 30, Color.Black);
            btn_close.ImageHove = FontAwesome.GetImage("4FA3", 30, Color.White);
        }
        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            player._setting = null;
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Action _action = () =>
            {
                System.Threading.Thread.Sleep(500);
            };
            player._task.ContinueWhenAll(new Task[] { player._task.StartNew(_action) }, (action =>
            {
                this.BeginInvoke(new Action(() =>
                {
                    if (this._index != "1")
                    {
                        foreach (Control item in panel1.Controls)
                        {
                            if (item.Name.StartsWith("b_") && item is TSkin.TBut)
                            {
                                TSkin.TBut f = item as TSkin.TBut;
                                if (item.Tag.ToString() == this._index)
                                {
                                    f.IsActive = true;
                                }
                                else
                                {
                                    f.IsActive = false;
                                }
                            }
                        }
                    }
                    menu_typeCore();
                }));
            }));
        }
        private void btn_min_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public SettingP1 p1 = null;
        public SettingP2 p2 = null;
        public SettingP3 p3 = null;
        public SettingP4 p4 = null;
        public SettingP5 p5 = null;
        public SettingP6 p6 = null;
        public SettingP7 p7 = null;
        public string _index = "1";
        private void menu_type(object sender, EventArgs e)
        {
            TSkin.TBut f = sender as TSkin.TBut;
            if (_index != f.Tag.ToString())
            {
                foreach (Control item in panel1.Controls)
                {
                    if (item.Name.StartsWith("b_") && item is TSkin.TBut)
                    {
                        TSkin.TBut fs = item as TSkin.TBut;
                        if (fs != f)
                        {
                            fs.IsActive = false;
                        }
                    }
                }
                _index = f.Tag.ToString();
                f.IsActive = true;
                this.BeginInvoke(new Action(() =>
                {
                    //panel3.SuspendLayout();
                    menu_typeCore();
                    //panel3.ResumeLayout();
                }));
            }
        }
        void menu_typeCore()
        {
            bool isCanRefresh = false;
            switch (_index)
            {
                case "1":
                    isCanRefresh = true;
                    if (p1 == null)
                    {
                        p1 = new SettingP1(this)
                        {
                            Dock = DockStyle.Fill
                        };
                        panel3.Controls.Add(p1);
                    }
                    p1.BringToFront();
                    break;
                case "2":
                    isCanRefresh = true;
                    if (p2 == null)
                    {
                        p2 = new SettingP2(this)
                        {
                            Dock = DockStyle.Fill
                        };
                        panel3.Controls.Add(p2);
                    }
                    p2.BringToFront();
                    break;
                case "3":
                    isCanRefresh = true;
                    if (p3 == null)
                    {
                        p3 = new SettingP3(this)
                        {
                            Dock = DockStyle.Fill
                        };
                        panel3.Controls.Add(p3);
                    }
                    p3.BringToFront();
                    break;
                case "4":
                    if (p4 == null)
                    {
                        p4 = new SettingP4(this)
                        {
                            Dock = DockStyle.Fill
                        };
                        panel3.Controls.Add(p4);
                    }
                    p4.BringToFront();
                    break;
                case "5":
                    if (p5 == null)
                    {
                        p5 = new SettingP5(this)
                        {
                            Dock = DockStyle.Fill
                        };
                        panel3.Controls.Add(p5);
                    }
                    p5.BringToFront();
                    break;
                case "6":
                    isCanRefresh = true;
                    if (p6 == null)
                    {
                        p6 = new SettingP6(this)
                        {
                            Dock = DockStyle.Fill
                        };
                        panel3.Controls.Add(p6);
                    }
                    p6.BringToFront();
                    break;
                case "7":
                    isCanRefresh = true;
                    if (p7 == null)
                    {
                        p7 = new SettingP7(this)
                        {
                            Dock = DockStyle.Fill
                        };
                        panel3.Controls.Add(p7);
                    }
                    p7.BringToFront();
                    break;
            }
            btn_refresh.Enabled = isCanRefresh;
        }

        private void btn_resume_Click(object sender, EventArgs e)
        {
            using (Popup.Dialog dialog = new Popup.Dialog("恢复默认设置", "您确定这样做嘛，这将会移除您所有设置",true))
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    btn_refresh.Enabled = false;
                    panel3.SuspendLayout();

                    bool isOk = false;
                    Action _action = () =>
                    {
                        if (SystemSettings.GetLink.Length > 0)
                        {
                            AdminAppMainSetLink adminAppMainSetLink = new AdminAppMainSetLink(null, AppLink.GetAllLinkList);
                            isOk = adminAppMainSetLink.OpenAssistExe("SetLink");
                        }
                        else { isOk = true; }
                    };
                    player._task.ContinueWhenAll(new Task[] { player._task.StartNew(_action) }, (action =>
                    {
                        string[] link = SystemSettings.GetLink;
                        this.Invoke(new Action(() =>
                        {
                            SystemSettings.Resume();
                            if (isOk)
                            {
                                SystemSettings.SetLink = null;
                            }
                            else if (link.Length > 0)
                            {
                                SystemSettings.SetLink = string.Join(";", link);
                            }

                            if (p1 != null)
                            {
                                p1.Dispose();
                                p1 = null;
                            }
                            if (p2 != null)
                            {
                                p2.Dispose();
                                p2 = null;
                            }
                            if (p3 != null)
                            {
                                p3.Dispose();
                                p3 = null;
                            }
                            if (p4 != null)
                            {
                                p4.Dispose();
                                p4 = null;
                            }
                            if (p5 != null)
                            {
                                p5.Dispose();
                                p5 = null;
                            }
                            if (p6 != null)
                            {
                                p6.Dispose();
                                p6 = null;
                            }
                            if (p7 != null)
                            {
                                p7.Dispose();
                                p7 = null;
                            }
                            player.pictureBox1.Visible = player.label2.Visible = true;
                            player.backImage.BackgroundImage = null;

                            menu_typeCore();
                            panel3.ResumeLayout();
                        }));
                    }));
                }
            }
        }

        private void btn_refresh_Click(object sender, EventArgs e)
        {
            btn_refresh.Enabled = false;
            panel3.SuspendLayout();
            switch (_index)
            {
                case "1":
                    if (p1 != null)
                    {
                        p1.Resume();
                    }
                    break;
                case "2":
                    if (p2 != null)
                    {
                        p2.Resume();
                    }
                    break;
                case "3":
                    if (p3 != null)
                    {
                        p3.Resume();
                    }
                    break;
                case "4":

                    break;
                case "5":
                    break;
                case "6":

                    if (p6 != null)
                    {
                        p6.Resume();
                    }
                    break;
                case "7":

                    if (p7 != null)
                    {
                        p7.Resume();
                    }
                    break;
            }

            if (p1 != null)
            {
                p1.Dispose();
                p1 = null;
            }
            if (p2 != null)
            {
                p2.Dispose();
                p2 = null;
            }
            if (p3 != null)
            {
                p3.Dispose();
                p3 = null;
            }
            if (p4 != null)
            {
                p4.Dispose();
                p4 = null;
            }
            if (p5 != null)
            {
                p5.Dispose();
                p5 = null;
            }
            if (p6 != null)
            {
                p6.Dispose();
                p6 = null;
            }
            if (p7 != null)
            {
                p7.Dispose();
                p7 = null;
            }
            menu_typeCore();
            panel3.ResumeLayout();
        }

    }
}

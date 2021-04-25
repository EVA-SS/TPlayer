using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using TPlayerSupport;

namespace TPlayer
{
    public partial class DownList : NetDimension.WinForm.ModernUIForm
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


        private void Min_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        public TPlayer player;
        public DownList(TPlayer player)
        {
            this.player = player;
            InitializeComponent();
            btn_play.Image = FontAwesome.GetImage("4FF9", 30, Color.FromArgb(80, 227, 194));
            btn_pause.Image = FontAwesome.GetImage("4FF8", 30, Color.OrangeRed);
            btn_min.Image = FontAwesome.GetImage("4FA9", 30, Color.Black);
            btn_close.Image = FontAwesome.GetImage("4FA3", 30, Color.Black);
            btn_close.ImageHove = FontAwesome.GetImage("4FA3", 30, Color.White);

            if (player.DowningCount > 0)
            {
                label2.Text = "（" + player.DowningCount + "）";
            }
        }
        public void ListRefresh()
        {
            int count = player.DowningCount;
            if (count > 0)
            {
                label2.Invoke(new Action(() =>
                {
                    label2.Text = "（" + count + "）";
                }));
            }
            else
            {
                label2.Invoke(new Action(() =>
                {
                    label2.Text = null;
                }));
            }
        }
        public void AddRefresh(List<DownItem> tasks)
        {
            panel2.SuspendLayout();
            lock (player.DownList)
            {
                foreach (DownItem item in tasks)
                {
                    AddCom(item);
                }
            }
            panel2.ResumeLayout();
        }
        protected override void OnClosing(CancelEventArgs e)
        {
            lock (player.DownList)
            {
                foreach (DownItem downIt in player.DownList)
                {
                    if (downIt.core != null && downIt.core.Tag != null)
                    {
                        downIt.core.StateChange -= Core_StateChange;
                        downIt.core.TimeChange -= Core_TimeChange;
                        downIt.core.SpeedChange -= Core_SpeedChange;
                        downIt.core.MaxValueChange -= Core_MaxValueChange;
                        downIt.core.ValueChange -= Core_ValueChange;
                        downIt.core.Tag = null;
                    }
                }
            }
            player._downList = null;
            player.Activate();
            base.OnClosing(e);
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            lock (player.DownList)
            {
                foreach (DownItem item in player.DownList)
                {
                    AddCom(item);
                }
            }
        }



        void AddCom(DownItem downIt)
        {
            Frm.Downitem item = new Frm.Downitem
            {
                Dock = DockStyle.Top,
                Size = new Size(0, 68)
            };
            item.logo.Image = downIt.img;
            item.title.Text = downIt.name;
            panel2.Controls.Add(item);

            if (downIt.core != null)
            {
                downIt.core.Tag = item;

                switch (downIt.core.State)
                {
                    case Helper.DownCore.DownState.Complete:
                        item.state.ForeColor = Color.FromArgb(80, 227, 194);
                        item.state.Text = "下载完成";

                        item.link_retry.Visible = item.link_del.Visible = false;
                        item.link_open.Visible = true;
                        break;
                    case Helper.DownCore.DownState.Downloading:
                        item.state.ForeColor = Color.Black;
                        item.state.Text = "下载中";
                        item.link_retry.Visible = item.link_open.Visible = false;
                        item.link_del.Visible = true;
                        break;
                    case Helper.DownCore.DownState.Fail:
                        item.state.ForeColor = Color.FromArgb(235, 17, 35);
                        item.state.Text = "下载失败";
                        item.link_open.Visible = false;
                        item.link_retry.Visible = item.link_del.Visible = true;
                        break;
                    case Helper.DownCore.DownState.Stop:
                        item.state.ForeColor = Color.OrangeRed;
                        item.state.Text = "已停止";
                        item.link_retry.Visible = item.link_open.Visible = false;
                        item.link_del.Visible = true;
                        break;
                }
                item.LinkClick += (a, b) =>
                {
                    switch (b)
                    {
                        case "retry":
                            player.DelDownList(downIt);
                            panel2.Controls.Remove(item);
                            player.AddDownList(new List<DownItem> { downIt });
                            break;
                        case "open":
                            if (downIt.core.State == Helper.DownCore.DownState.Complete)
                            {
                                downIt.savepath.OpenExplorer();
                            }
                            break;
                        case "del":
                            player.DelDownList(downIt);
                            panel2.Controls.Remove(item);
                            break;
                    }
                };
                item.prog.MaxValue = downIt.core.MaxValue;
                item.prog.Value = downIt.core.Value;
                //downIt.core.NameChange += (e) => { label4.Text = e; };
                downIt.core.SpeedChange += Core_SpeedChange;
                downIt.core.MaxValueChange += Core_MaxValueChange;
                downIt.core.ValueChange += Core_ValueChange;
                downIt.core.StateChange += Core_StateChange;
                downIt.core.TimeChange += Core_TimeChange;
            }

            item.BringToFront();

        }

        private void Core_TimeChange(Helper.DownCore core, string e)
        {
            if (core.Tag != null)
            {
                lock (core.Tag)
                {
                    Frm.Downitem item = core.Tag as Frm.Downitem;
                    if (item != null)
                    {
                        item.Invoke(new Action(() =>
                        {
                            item.time.Text = "预计还需 " + e;
                        }));
                    }
                }
            }
        }

        private void Core_StateChange(Helper.DownCore core, Helper.DownCore.DownState e)
        {
            if (core.Tag != null)
            {
                lock (core.Tag)
                {
                    Frm.Downitem item = core.Tag as Frm.Downitem;

                    if (item != null)
                    {
                        switch (e)
                        {
                            case Helper.DownCore.DownState.Complete:
                                item.Invoke(new Action(() =>
                                {
                                    item.state.ForeColor = Color.FromArgb(80, 227, 194);
                                    item.state.Text = "下载完成";
                                    item.time.Text = null;
                                    item.size.Text = core.MaxValue.CountSize();
                                    item.link_retry.Visible = item.link_del.Visible = false;

                                    item.link_open.Visible = true;
                                }));
                                break;
                            case Helper.DownCore.DownState.Downloading:
                                item.Invoke(new Action(() =>
                                {
                                    item.state.ForeColor = Color.Black;
                                    item.state.Text = "下载中";

                                    item.link_retry.Visible = item.link_open.Visible = false;
                                    item.link_del.Visible = true;
                                }));
                                break;
                            case Helper.DownCore.DownState.Fail:
                                item.Invoke(new Action(() =>
                                {
                                    item.state.ForeColor = Color.FromArgb(235, 17, 35);
                                    item.state.Text = "下载失败";
                                    item.time.Text = item.size.Text = null;

                                    item.link_open.Visible = false;
                                    item.link_retry.Visible = item.link_del.Visible = true;
                                }));
                                break;
                            case Helper.DownCore.DownState.Ready:
                                item.Invoke(new Action(() =>
                                {
                                    item.state.ForeColor = Color.DimGray;
                                    item.state.Text = "准备中";

                                    item.link_del.Visible = item.link_retry.Visible = item.link_open.Visible = false;
                                }));
                                break;
                            case Helper.DownCore.DownState.Stop:
                                item.Invoke(new Action(() =>
                                {
                                    item.state.ForeColor = Color.OrangeRed;
                                    item.state.Text = "已停止";
                                    item.time.Text = null;

                                    item.link_retry.Visible = item.link_open.Visible = false;
                                    item.link_del.Visible = true;
                                }));
                                break;
                        }
                    }
                }
            }
        }

        private void Core_ValueChange(Helper.DownCore core, double e)
        {
            if (core.Tag != null)
            {
                lock (core.Tag)
                {
                    Frm.Downitem item = core.Tag as Frm.Downitem;
                    if (item != null)
                    { item.prog.Value = e; }
                }
            }
        }

        private void Core_MaxValueChange(Helper.DownCore core, double e)
        {
            if (core.Tag != null)
            {
                lock (core.Tag)
                {
                    Frm.Downitem item = core.Tag as Frm.Downitem;
                    if (item != null)
                    { item.prog.MaxValue = e; }
                }
            }
        }

        private void Core_SpeedChange(Helper.DownCore core, double e)
        {
            if (core.Tag != null)
            {
                lock (core.Tag)
                {
                    Frm.Downitem item = core.Tag as Frm.Downitem;
                    if (item != null)
                    {
                        item.Invoke(new Action(() =>
                        {
                            item.size.Text = core.Value.CountSize() + "/" + core.MaxValue.CountSize() + "   " + e.CountSize() + " /s";
                        }));
                    }
                }
            }
        }

        private void btn_play_Click(object sender, EventArgs e)
        {
            lock (player.DownList)
            {
                foreach (DownItem item in player.DownList)
                {
                    if (item.core != null)
                    {
                        item.core.Resume();
                    }
                }
            }
        }

        private void btn_pause_Click(object sender, EventArgs e)
        {
            lock (player.DownList)
            {
                foreach (DownItem item in player.DownList)
                {
                    if (item.core != null)
                    {
                        item.core.Suspend();
                    }
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using TPlayerSupport;

namespace TPlayer
{
    public partial class ControllerMini : Form
    {
        TPlayer player;
        public ControllerMini(TPlayer player)
        {
            this.player = player;
            InitializeComponent();
            if (player.isMusic)
            {
                btn_exit.Visible = btn_tv.Visible = false;
            }
            this.Text = label1.Text = player.Text;
        }
        void Ce()
        {
            Rectangle rect = Screen.PrimaryScreen.WorkingArea;
            this.Location = new Point(rect.X + ((rect.Width - this.Width) / 2), rect.Y);
        }
        protected override void OnLoad(EventArgs e)
        {
            Ce();
            base.OnLoad(e);
            Action _action = () =>
            {
                System.Threading.Thread.Sleep(500);
            };
            player._task.ContinueWhenAll(new Task[] { player._task.StartNew(_action) }, (action =>
            {
                this.Invoke(new Action(() =>
                {
                    this.Focus();
                    this.Deactivate += ControllerMini_Deactivate;
                    progFull.MouseEnter += ProgFull_MouseEnter;
                }));
            }));
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            Ce();
            base.OnSizeChanged(e);
        }
        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            player._controllerMini = null;
        }
        bool _IsPlay = false;
        public bool IsPlay
        {
            get { return _IsPlay; }
            set
            {
                if (_IsPlay != value)
                {
                    _IsPlay = value;
                    btn_play.Image = value ? Properties.Resources.material_pause_h : Properties.Resources.material_play_h;
                }
            }
        }

        string maxtime = "00";
        private void progFull_MaxTimeChange(double value)
        {
            maxtime = TConvert.ToTimeStr(value);
        }

        private void progFull_TimeChange(double value)
        {
            label2.Text = TConvert.ToTimeStr(value) + "/" + maxtime;
        }

        private void btn_play_Click(object sender, EventArgs e)
        {
            player.PlayorPause();
        }

        private void btn_next_Click(object sender, EventArgs e)
        {
            player.PlayDown();
        }
        bool isEx = false;
        private void btn_tv_Click(object sender, EventArgs e)
        {
            if (player._DLNA == null)
            {
                isEx = true;
                player._DLNA = new DLNA(player);
                player._DLNA.Show();
                player._DLNA.FormClosing += (a, b) =>
                {
                    isEx = false;
                    this.Focus();
                    this.Activate();
                };
            }
            else { player._DLNA.Activate(); }
        }

        private void btn_exit_Click(object sender, EventArgs e)
        {
            if (player.isMusic)
            {
                player.isMusic = false;
                player.ShowPrompt("退出音乐模式");
            }
            else if (player.isDLNA)
            {
                player.player.SetConfig(1803, "");
                player.isDLNA = false;
                player.ShowPrompt("DLNA设备", "本机");
            }
            else if (player.isDeskTop)
            {
                player.isDeskTop = false;
            }
        }

        #region 鼠标事件
        ControlAnimation ControlAnimation = new ControlAnimation();
        bool isZk = true;
        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            if (!isZk)
            {
                isZk = true;
                Ce();
                progFull.Dock = DockStyle.Top;
                if (Program.settings.Animation)
                {
                    ControlAnimation.TopMove(this, Screen.PrimaryScreen.WorkingArea.Top, 300, AnimationType.Ball);
                }
                else
                {
                    this.Top = Screen.PrimaryScreen.WorkingArea.Top;
                }
                this.Activate();
            }
        }
        private void ProgFull_MouseEnter(object sender, EventArgs e)
        {
            OnMouseEnter(e);
        }
        private void ControllerMini_Deactivate(object sender, EventArgs e)
        {
            if (isZk && !isEx)
            {
                isZk = false;
                Ce();
                if (Program.settings.Animation)
                {
                    ControlAnimation.TopMove(this, Screen.PrimaryScreen.WorkingArea.Top - (this.Height - progFull.Height), 300, AnimationType.Ball);
                }
                else
                {
                    this.Top = Screen.PrimaryScreen.WorkingArea.Top - (this.Height - progFull.Height);
                }
                progFull.Dock = DockStyle.Bottom;
            }
        }
        #endregion

        #region 拖拽文件进入窗体时
        protected override void OnDragEnter(DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                if (!isZk)
                {
                    isZk = true;
                    Ce();
                    progFull.Dock = DockStyle.Top;
                    this.Top = 0;
                    this.Activate();
                }
                e.Effect = DragDropEffects.All;
            }
            else
                e.Effect = DragDropEffects.None;
            base.OnDragEnter(e);
        }
        protected override void OnDragDrop(DragEventArgs e)
        {
            object data = e.Data.GetData(DataFormats.FileDrop);
            if (data is string[])
            {
                string[] myFiles = data as string[];

                List<PlayerItem> PlayList_Temp = new List<PlayerItem>();
                foreach (string url in myFiles)
                {
                    PlayList_Temp.Add(new PlayerItem("file", url));
                }
                if (PlayList_Temp.Count > 0)
                {
                    player.isTvModel = false;
                    //int index = player.PlayList.Count;
                    player.SetPlayList(PlayList_Temp);
                    player.OpenFileWeb(0);

                }
            }
            base.OnDragDrop(e);
        }
        #endregion
    }
}

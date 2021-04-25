using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TPlayer.Com
{
    public partial class Sidebar : Form
    {
        TPlayer player = null;

        #region 初始化

        public Sidebar(TPlayer player)
        {
            this.player = player;
            SetStyle(
                ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw |
                ControlStyles.DoubleBuffer, true);
            UpdateStyles();
            InitializeComponent();
            btn_search.Image = FontAwesome.GetImage("5128", 26, Color.White);
        }
        protected override void OnClosing(CancelEventArgs e)
        {
            player.LocationChanged -= Player_LocationSizeChanged;
            player.SizeChanged -= Player_LocationSizeChanged;
            if (SystemSettings.Animation)
            {
                if (player.IsFullScreen || player.isMax)
                {
                    new ControlAnimation().LeftMove(player._sidebarBtn, player._sidebarBtn.Left + this.Width, 40, AnimationType.Ball);
                    AnimateWindow(this.Handle, 80, 0x00040000 | 0x00000001 | 0x10000);
                }
                else
                {
                    AnimateWindow(this.Handle, 80, 0x00040000 | 0x00000002 | 0x10000);
                }
            }
            else if (player.IsFullScreen || player.isMax)
            {
                player._sidebarBtn.Left = player._sidebarBtn.Left + this.Width;
            }
            player._sidebar = null;
            if (player._sidebarBtn != null)
            {
                player._sidebarBtn.IsLeft = false;
            }
            base.OnClosing(e);
        }

        [DllImport("user32")]
        public static extern bool AnimateWindow(IntPtr whnd, int dwtime, int dwflag);
        protected override void OnLoad(EventArgs e)
        {
            playList.PlayIndex = player.PlayIndex;
            playList.Playing = player.IsPlayPause;
            Player_LocationSizeChanged(this, e);
            videoList();
            base.OnLoad(e);
            player.LocationChanged += Player_LocationSizeChanged;
            player.SizeChanged += Player_LocationSizeChanged;
            btn_del.Image = FontAwesome.GetImage("4F8C", 38);
            btn_more.Image = FontAwesome.GetImage("4F88", 38);

            bool isDownDel = false, isDownMore = false;

            #region 功能菜单
            btn_more.MouseDown += (a, b) =>
            {
                isDownMore = true;
                if (player.isTvModel)
                {
                    Action _action = () =>
                    {
                        System.Threading.Thread.Sleep(600);
                    };
                    player._task.ContinueWhenAll(new Task[] { player._task.StartNew(_action) }, (action =>
                    {
                        if (isDownMore)
                        {
                            isDownMore = false;
                            Frm.UIListSel frm = new Frm.UIListSel(player.CountrysTemp, SystemSettings.TvSourceType);
                            if (frm.ShowDialog() == DialogResult.OK)
                            {
                                try
                                {
                                    SystemSettings.TvSourceType = frm.SelValue;
                                    List<Frm.TvData> Channel = player.tvDatas.FindAll(ab => ab.country.name == SystemSettings.TvSourceType);

                                    List<PlayerItem> PlayList_Temp = new List<PlayerItem>();

                                    foreach (Frm.TvData item in Channel)
                                    {
                                        PlayList_Temp.Add(new PlayerItem("web", item.url, item.name));
                                    }
                                    player.PlayIndex = -1;
                                    player.SetPlayList(PlayList_Temp);
                                }
                                catch (Exception ez) { Api.OpenMessage(player, Frm.MessageType.Error, "切换失败:" + ez.Message); }
                            }
                            this.Invoke(new Action(() =>
                            {
                                playList.Invalidate();
                            }));
                        }
                    }));
                }
            };
            btn_more.MouseUp += (a, b) =>
            {
                if (isDownMore)
                {
                    isDownMore = false;
                    btn_del.Visible = playList.IsCheck = !playList.IsCheck;
                }
            };
            btn_more.Leave += (a, b) =>
            {
                isDownMore = false;
            };
            #endregion

            #region 删除

            btn_del.MouseDown += (a, b) =>
            {
                isDownDel = true;
                Action _action = () =>
                {
                    System.Threading.Thread.Sleep(600);
                };
                player._task.ContinueWhenAll(new Task[] { player._task.StartNew(_action) }, (action =>
                {
                    if (isDownDel)
                    {
                        isDownDel = false;
                        foreach (var item in playList.Items)
                        {
                            if (item.Visible)
                            {
                                item.Check = !item.Check;
                                if (item.Check)
                                {
                                    DelTemp.Add(item);
                                }
                                else
                                {
                                    DelTemp.Remove(item);
                                }
                            }
                        }
                        this.Invoke(new Action(() =>
                        {
                            playList.Invalidate();
                        }));
                    }
                }));
            };
            btn_del.MouseUp += (a, b) =>
            {
                if (isDownDel)
                {
                    isDownDel = false;
                    btn_del.Visible = playList.IsCheck = false;
                    if (DelTemp.Count > 0)
                    {
                        bool isStop = false;
                        List<PlayerItem> PlayListTemp = new List<PlayerItem>();
                        foreach (TPlayerList.VideoListItem item in DelTemp)
                        {
                            if (player.PlayIndex == item.Index)
                            {
                                isStop = true;
                            }
                            PlayListTemp.Add(player.PlayList[item.Index]);
                        }
                        player.PlayList.RemoveAll(ab => PlayListTemp.Contains(ab));
                        DelTemp.Clear();
                        if (isStop && player.IsPlaying)
                        {
                            if (player.PlayList.Count == 0)
                            {
                                player.player.Close();
                            }
                            else
                            {
                                player.PlayUp();
                            }
                        }
                        else
                        {
                            if (player.PlayIndex > player.PlayList.Count - 1)
                            {
                                player.PlayIndex--;
                            }
                        }

                        //playList.chatVScroll.Value = 0;
                        videoList();
                    }
                }
            };
            btn_del.Leave += (a, b) =>
            {
                isDownDel = false;
            };

            #endregion

            text_search.GotFocus += (a, b) =>
            {
                text_search.BackColor = panel1.BackColor = btn_search.BorderColor = Color.FromArgb(64, 64, 73);
            };
            text_search.LostFocus += (a, b) =>
            {
                text_search.BackColor = panel1.BackColor = btn_search.BorderColor = this.BackColor;
            };

            if (SystemSettings.Animation)
            {
                if (player.IsFullScreen || player.isMax)
                {
                    Rectangle WorkingAreaRect = Screen.PrimaryScreen.WorkingArea;
                    new ControlAnimation().LeftMove(player._sidebarBtn, (WorkingAreaRect.Left + player.ClientRectangle.Width - player._sidebarBtn.Width) - this.Width, 100, AnimationType.Ball);
                    AnimateWindow(this.Handle, 80, 0x00040000 | 0x00000002 | 0x20000);
                }
                else
                {
                    AnimateWindow(this.Handle, 80, 0x00040000 | 0x00000001 | 0x20000);
                }
            }
            else if (player.IsFullScreen || player.isMax)
            {
                Rectangle WorkingAreaRect = Screen.PrimaryScreen.WorkingArea;
                player._sidebarBtn.Left = (WorkingAreaRect.Left + player.ClientRectangle.Width - player._sidebarBtn.Width) - this.Width;
            }

            OnSizeChanged(null);
        }
        protected override void OnDeactivate(EventArgs e)
        {
            if (player.IsFullScreen || player.isMax)
            {
                this.Close();
            }
            base.OnDeactivate(e);
        }


        private void Player_LocationSizeChanged(object sender, EventArgs e)
        {
            this.Height = player.ClientRectangle.Height;
            if (player.IsFullScreen)
            {
                this.Location = new Point(player.ClientRectangle.Width - this.Width, player.Top);
            }
            else if (player.isMax)
            {
                Rectangle WorkingAreaRect = Screen.PrimaryScreen.WorkingArea;
                this.Location = new Point(WorkingAreaRect.Left + player.ClientRectangle.Width - this.Width, WorkingAreaRect.Top);
            }
            else
            {
                this.Location = new Point(player.Left + player.Width, player.Top);
            }
        }

        #endregion
        public void videoList()
        {
            if (player.PlayList.Count > 0)
            {
                panel1.Visible = player.PlayList.Count > 10;
                label2.Text = player.PlayList.Count + "条视频";
                playList.Items.Clear();
                TPlayerList.VideoListItem Sitem = null;
                for (int i = 0; i < player.PlayList.Count; i++)
                {
                    PlayerItem item = player.PlayList[i];
                    TPlayerList.VideoListItem videoListItem = new TPlayerList.VideoListItem
                    {
                        Index = i,
                        Enabled = true,
                        Visible = true,
                        Name = item.name,
                        Desc = item.time,
                        Tag = item,
                    };
                    playList.Items.Add(videoListItem);
                    if (i == player.PlayIndex)
                    {
                        Sitem = videoListItem;
                    }
                }
                playList.InPaint();
                playList.Invalidate();
                if (Sitem != null)
                {
                    playList.chatVScroll.MoveSliderToLocation(Sitem.Bounds.Y);
                }
            }
            else
            {
                panel1.Visible = false;
                label2.Text = "空";
                playList.Items.Clear();
                //titleCheckControl1.InPaint();
                playList.Invalidate();
            }
        }

        List<TPlayerList.VideoListItem> DelTemp = new List<TPlayerList.VideoListItem>();
        private void playList_DownClick(TPlayerList.VideoListItem Item)
        {
            if (playList.IsCheck)
            {
                Item.Check = !Item.Check;
                if (Item.Check) { DelTemp.Add(Item); }
                else
                {
                    DelTemp.Remove(Item);
                }
                playList.Tom(Item);
            }
            else
            {
                //PlayerItem playerItem = Item.Tag as PlayerItem;
                player.OpenFileWeb(Item.Index);
            }
        }
        private void playList_PlayClick(TPlayerList.VideoListItem Item)
        {
            if (player.IsPlaying && Item.Index == player.PlayIndex)
            {
                player.PlayorPause();
            }
            else
            {
                //PlayerItem playerItem = Item.Tag as PlayerItem;
                player.OpenFileWeb(Item.Index);
            }
        }

        private void playList_MoreClick(TPlayerList.VideoListItem Item)
        {

        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            string _text = text_search.Text.Trim();
            if (string.IsNullOrEmpty(_text))
            {
                foreach (var item in playList.Items)
                {
                    item.Visible = true;
                }
            }
            else
            {
                string[] _texts = _text.Split(' ');
                foreach (var item in playList.Items)
                {
                    bool isvi = false;
                    foreach (string items in _texts)
                    {
                        if (item.Name.Contains(items))
                        {
                            isvi = true; break;
                        }
                    }
                    item.Visible = isvi;
                }
            }
            playList.InPaint();
            playList.Invalidate();
        }

        private void text_search_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                e.Handled = true;
                btn_search_Click(sender, null);
            }
        }
    }
}

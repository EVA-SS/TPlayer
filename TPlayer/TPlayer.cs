using HttpLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using TPlayerSupport;

namespace TPlayer
{
    public partial class TPlayer : NetDimension.WinForm.ModernUIForm, IPrompt, IPromptByBuffer
    {
        public AxAPlayer3Lib.AxPlayer player = null;
        public TaskFactory _task = new TaskFactory();


        #region APlayer

        #region 控制器

        /// <summary>
        /// 播放-暂停
        /// </summary>
        public void PlayorPause()
        {
            int st = player.GetState();
            if (st == 5 || st == 4) //暂停
            {
                player.Pause();
            }
            else if (st == 3 || st == 2) //播放
            {
                player.Play();
            }
        }

        #region 控制播放器属性

        #region 播放器

        bool _IsPlayPause = false;
        bool _IsPlaying = false;

        /// <summary>
        /// 暂停/播放
        /// </summary>
        public bool IsPlayPause
        {
            get { return _IsPlayPause; }
            set
            {
                if (isInvented) { return; }
                if (_IsPlayPause != value)
                {
                    _IsPlayPause = value;
                    ToolItem1.Visible = !value;
                    ToolItem2.Visible = value;
                    //notify.Text = value ? this.Text : this.Text + "（已暂停）";
                    if (value)
                    {
                        tProgress.State = TSkin.ProgressBarState.Normal;
                    }
                    else
                    {
                        PlayMaxValue = player.GetDuration();
                        PlayValue = player.GetPosition();
                        tProgress.State = TSkin.ProgressBarState.Pause;
                    }
                    if (_controller != null)
                    {
                        try
                        {
                            _controller.IsPlay = value;
                        }
                        catch { }
                    }
                    if (_sidebar != null)
                    {
                        try
                        {
                            _sidebar.playList.Playing = _IsPlayPause;
                        }
                        catch { }
                    }
                }
            }
        }

        /// <summary>
        /// 是否正在播放
        /// </summary>
        public bool IsPlaying
        {
            get { return _IsPlaying; }
            set
            {
                if (_IsPlaying != value)
                {
                    if (value && _setting != null && _setting.p5 != null)
                    {
                        _setting.p5._Load();
                    }
                    _IsPlaying = value;
                    if (_controller != null)
                    {
                        try
                        {
                            _controller.SetSize();
                        }
                        catch { }
                    }

                    SetLoadIng(false);
                    if (value)
                    {
                        TaskState = true;

                        SystemSleepManagement.PreventSleep(true);
                        tProgress.State = TSkin.ProgressBarState.Normal;
                        tProgress.Style = ProgressBarStyle.Blocks;
                        tProgress.ShowInTaskbar = true;

                        if (property != null)
                        {
                            property.LoadVideoInfo();
                        }
                    }
                    else
                    {
                        uil = false;
                        TaskState = false;

                        PlaySpeed = 100;
                        player.Visible = false;

                        if (_controller != null)
                        {
                            _controller._buff = null;
                            _controller.Invoke(new Action(() =>
                            {
                                _controller.SetBufferValue(null);
                            }));
                        }
                        CursorVisible = true;
                        if (property != null)
                        {
                            property.CloseVideoInfo();
                        }

                        画面模式ToolStripMenuItem.Visible = false;
                    }

                    音轨列表无ToolStripMenuItem.Visible = 保存截图ToolStripMenuItem.Visible = gif图截取ToolStripMenuItem.Visible = value;
                }
            }
        }

        double _PlayValue = 0;
        double _PlayMaxValue = 0;
        public double PlayValue
        {
            get { return _PlayValue; }
            set
            {
                if (_PlayValue != value)
                {
                    _PlayValue = value;
                    try
                    {
                        tProgress.Value = (int)value;
                    }
                    catch { }

                    if (_controller != null)
                    {
                        try
                        {
                            _controller.Value = value;
                        }
                        catch { }
                    }
                }
            }
        }
        public double PlayMaxValue
        {
            get { return _PlayMaxValue; }
            set
            {
                if (_PlayMaxValue != value)
                {
                    _PlayMaxValue = value;

                    tProgress.Maximum = (int)value;
                    if (_controller != null)
                    {
                        try
                        {
                            _controller.MaxValue = value;
                        }
                        catch
                        {
                        }
                    }
                }
            }
        }

        #endregion

        #endregion

        #endregion

        #region 播放器事件

        #region 播放器状态

        public bool uil = false;
        string oldUrl = null;
        private void Player_OnStateChanged(object sender, AxAPlayer3Lib._IPlayerEvents_OnStateChangedEvent e)
        {
            //try
            //{
            int State = e.nNewState;
            Debug.WriteLine("State：" + State);
            if (State == 0)//停止播放
            {
                if (IsPlaying)
                {
                    IsPlaying = IsPlayPause = false;
                }
                else
                {
                    SetLoadIng(false);
                }
                if (isclose)
                {
                    this.Close();
                }
                else
                {
                    string err = player.GetConfig(7);
                    CachePath(PlayIndex, err == "0");
                    if (err != "0" && err != "1")
                    {
                        tProgress.Value = tProgress.Maximum = 100;
                        tProgress.State = TSkin.ProgressBarState.Error;
                        //MessageBox.Show(err);
                        using (PlayErr er = new PlayErr(this, err))
                        {
                            if (er.ShowDialog(this) == DialogResult.OK)
                            {
                                player.SetConfig(4, "");
                                OpenFileWeb(PlayIndex);
                            }
                        }
                    }

                    SystemSleepManagement.ResotreSleep();
                    SetNameVideo("TPlayer");

                    if (_header != null)
                    {
                        _header.Explain = null;
                    }

                    PlayMaxValue = PlayValue = 0;
                    tProgress.State = TSkin.ProgressBarState.Normal;
                    tProgress.Style = ProgressBarStyle.Blocks;
                    tProgress.ShowInTaskbar = false;
                    isMusic = false;
                    if (!isDeskTop)
                    {
                        Controller(true);
                    }
                    if (!isDLNA && backImage.Image != null)
                    {
                        backImage.Image = null;
                        backImage.Blur = 0;
                        string backImgUrl = SystemSettings.BackImgUrl;
                        if (!string.IsNullOrEmpty(backImgUrl))
                        {
                            backImage.BackgroundImage = Image.FromFile(backImgUrl);
                        }
                    }
                    panel1.Visible = true;
                }
            }
            else
            {
                if (State == 4)//正在开始播放
                {
                    if (!uil)//打开媒体成功/正在开始播放
                    {
                        //IsPlaying = false;
                        uil = IsPlaying = true;


                        PlayerItem playerItem = PlayList[PlayIndex];

                        if (playerItem != null)
                        {
                            playerItem.videoSize = new Size(player.GetVideoWidth(), player.GetVideoHeight());
                            oldUrl = playerItem.url;
                            if (playerItem.type == "web")
                            {
                                playerItem.url = player.GetConfig(4);
                            }
                            else if (playerItem.type == "file")
                            {
                                LoadVideoSubs(playerItem.url);//打开成功后自动匹配加载本地字幕
                            }
                            int max = player.GetDuration();
                            if (max > 0)
                            {
                                playerItem.time = max.ToTimeStr();
                            }
                            else
                            {
                                playerItem.time = "直播";
                            }

                            if (playerItem.videoSize.Width > 0 && playerItem.videoSize.Height > 0)
                            {
                                画面模式ToolStripMenuItem.Visible = true;
                                isMusic = false;
                                if (isDLNA)
                                {
                                    backImage.BlurColor = Color.FromArgb(60, 0, 0, 0);
                                    backImage.Image = Properties.Resources.icon_DLNA;
                                    backImage.Blur = 40;
                                }
                                else
                                {
                                    player.Size = this.ClientRectangle.Size;
                                    player.Visible = true;
                                }

                                if (this.isMini)
                                {
                                    if (playerItem.videoSize.Width > 0 && playerItem.videoSize.Height > 0)
                                    {
                                        if (playerItem.videoSize.Width == playerItem.videoSize.Height)
                                        {
                                            this.Size = new Size(300, 300);
                                        }
                                        else if (playerItem.videoSize.Width > playerItem.videoSize.Height)
                                        {
                                            this.Size = new Size((playerItem.videoSize.Width * 300) / playerItem.videoSize.Height, 300);
                                        }
                                        else
                                        {
                                            this.Size = new Size(300, (playerItem.videoSize.Height * 300) / playerItem.videoSize.Width);
                                        }
                                    }
                                }
                                else
                                {
                                    switch (SystemSettings.VideoOpenFrame)
                                    {
                                        case 1:
                                            this.Size = playerItem.videoSize;
                                            break;
                                        case 2:
                                            if (!IsFullScreen)
                                            {
                                                FullScreen();
                                            }
                                            break;
                                    }
                                    UseFrameStyle();
                                }
                            }
                            else
                            {
                                isMusic = true;
                                if (playerItem != null)
                                {
                                    _task.StartNew(() =>
                                    {
                                        Image image = playerItem.url.GetMusicPicture();
                                        if (image != null)
                                        {
                                            backImage.Invoke(new Action(() =>
                                            {
                                                backImage.BlurColor = Color.FromArgb(60, 255, 255, 255);
                                                backImage.Image = image;
                                                backImage.Blur = 40;
                                            }));
                                        }
                                    });
                                }
                            }
                        }
                        else
                        {
                            oldUrl = null;
                        }


                        RefreshMeun();


                        if (_sidebar != null)
                        {
                            try
                            {
                                TPlayerList.VideoListItem videoListItem = _sidebar.playList.Items[_playIndex];
                                if (videoListItem != null)
                                {
                                    videoListItem.Name = playerItem.name;
                                    videoListItem.Desc = playerItem.time;
                                    _sidebar.playList.InPaint();
                                }
                            }
                            catch { }
                            _sidebar.playList.Invalidate();
                        }
                        if (SystemSettings.VR)
                        {
                            player.SetConfig(2402, SystemSettings.VRMode.ToString());
                            isVr = SystemSettings.VRMode > 0;
                        }
                        player.SetConfig(42, "1");//避免某些URL出现循环播放
                    }
                }
                else if (State == 1)//准备播放
                {
                }
                else if (State == 5)//播放中
                {
                    IsPlayPause = true;
                }
                else//暂停
                {
                    IsPlayPause = false;
                    //if (State == 6)//正在关闭
                    //{
                    //    CachePath(PlayIndex);
                    //}
                }
            }
            //}
            //catch 
            //{ }
        }

        #endregion

        /// <summary>
        /// 屏幕铺满
        /// </summary>
        PlayFrameStyle _FrameStyle = PlayFrameStyle.None;
        public PlayFrameStyle FrameStyle
        {
            get { return _FrameStyle; }
            set
            {
                if (_FrameStyle != value)
                {
                    _FrameStyle = value;
                    UseFrameStyle();
                }
            }
        }
        public void UseFrameStyle()
        {
            switch (_FrameStyle)
            {
                case PlayFrameStyle.None:
                    player.SetConfig(204, "");
                    break;
                case PlayFrameStyle._4_3:
                    player.SetConfig(204, "4;3");
                    break;
                case PlayFrameStyle._16_9:
                    player.SetConfig(204, "16;9");
                    break;
                case PlayFrameStyle.Paved:
                    player.SetConfig(204, player.Width + ";" + player.Height);
                    break;
            }
        }

        #region 鼠标操作

        private void Player_OnMessage(object sender, AxAPlayer3Lib._IPlayerEvents_OnMessageEvent e)
        {
            switch (e.nMessage)
            {
                case 0x201://点击事件
                    player.Focus();
                    if (isVr)
                    {
                        string on = player.GetConfig(2403);
                        if (!string.IsNullOrEmpty(on))
                        {
                            nDrag = new Point(Convert.ToInt32(e.lParam) & 0xffff, Convert.ToInt32(e.lParam) >> 16);
                            string[] os = on.Split(';');
                            g_fVRBaseH = Convert.ToDouble(os[0]);
                            g_fVRBaseV = Convert.ToDouble(os[1]);
                            g_fVRBaseD = Convert.ToDouble(os[2]);
                            isMoveDown = true;
                        }
                    }
                    else
                    {
                        if (IsFullScreen && IsPlaying)
                        {
                            int _maxvalue = player.GetDuration();
                            if (_maxvalue > 0)
                            {
                                isplayTemp = false;
                                nDrag = new Point(Convert.ToInt32(e.lParam) & 0xffff, Convert.ToInt32(e.lParam) >> 16);
                                isMoveDown = true;
                            }
                            //PlayorPause();
                        }
                        else
                        {
                            FrmMove(null, null);
                        }
                    }
                    break;
                case 0x0200://鼠标移动
                    int xPos = Convert.ToInt32(e.lParam) & 0xffff;
                    int yPos = Convert.ToInt32(e.lParam) >> 16;
                    top_Pos = new Point(xPos, yPos);

                    if (old_Pos.X != top_Pos.X && old_Pos.Y != top_Pos.Y)
                    {
                        CursorVisible = true;
                    }
                    old_Pos = top_Pos;
                    if (isMoveDown)
                    {
                        if (isVr)
                        {
                            PointF pointF = new PointF(xPos - nDrag.X, yPos - nDrag.Y);

                            double h = g_fVRBaseH + pointF.X / 200;
                            double v = g_fVRBaseV + pointF.Y / 500;
                            string sz = h + ";" + v + ";" + g_fVRBaseD;
                            player.SetConfig(2403, sz);
                        }
                        else
                        {
                            int diff = xPos - nDrag.X;
                            if (diff > 10 || diff < -10)
                            {
                                isInvented = true;
                                if (!isplayTemp && IsPlayPause)
                                {
                                    isplayTemp = true;
                                    player.Pause();
                                }
                                int _value = player.GetPosition() + (diff * 10);

                                player.SetPosition(_value);
                                if (_controller != null)
                                {
                                    try
                                    {
                                        _controller.Value = _value;
                                    }
                                    catch { }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (isMini || IsFullScreen || isDLNA)
                        {
                            if (isInPlayRect(top_Pos))
                            {
                                Controller(true);
                            }
                            else if (MoveBtnrect.Contains(top_Pos))
                            {
                                Show_sidebarBtn();
                            }
                            else
                            {
                                if (_sidebarBtn != null)
                                {
                                    if (!IsFullScreen || _sidebar == null)
                                        _sidebarBtn.Close();
                                }
                                Controller(false);
                            }
                        }
                        else if (!IsFullScreen && !isDeskTop)
                        {
                            Controller(true);
                        }
                    }
                    break;
                case 0x203://双击全屏事件
                    FullScreen();
                    break;
                case 0x202://鼠标松开
                    if (isMoveDown)
                    {
                        isInvented = false;
                        isMoveDown = false;
                        if (isplayTemp)
                        {
                            isplayTemp = false;
                            player.Play();
                        }
                    }
                    break;
                case 517://鼠标右键
                    Menu.Show(player, player.PointToClient(Cursor.Position));
                    break;
            }
        }

        bool isplayTemp = false;

        #region VR

        public bool isVr = false;
        bool isInvented = false;
        bool isMoveDown = false;
        Point nDrag;
        double g_fVRBaseH = 0.0;
        double g_fVRBaseV = 0.0;
        double g_fVRBaseD = 0.0;

        #endregion

        #endregion

        #region 键盘操作

        int PlaySpeed = 100;
        public void Player_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyData == Keys.Escape)
            {
                if (!_isDeskTop)
                {
                    FullScreen();
                }
            }
            else if (e.KeyData == Keys.Menu)
            {
                //菜单
            }
            else if (e.KeyCode == Keys.D0 && e.Control)
            {
                if (PlaySpeed != 100)
                {
                    PlaySpeed = 100;
                    player.SetConfig(104, PlaySpeed.ToString());
                    string tips = ((PlaySpeed * 1.0) / 100).ToString();
                    if (!tips.Contains(".")) { tips += ".0"; }
                    ShowPrompt("已恢复", tips + "x", "倍数播放，键盘 Ctrl + ← → 快速调节");
                }
            }
            #region 已被APlayer屏蔽

            //else if (e.KeyValue == 219)
            //{
            //    if (IsPlaying)
            //    {
            //        //快退

            //        if (PlaySpeed <= 50)
            //        {
            //            string tips = ((PlaySpeed * 1.0) / 100).ToString();
            //            if (!tips.Contains(".")) { tips += ".0"; }
            //            ShowPrompt("正以", tips + "x", "倍数播放，已经够慢了");
            //        }
            //        else
            //        {
            //            PlaySpeed -= 25;
            //            player.SetConfig(104, PlaySpeed.ToString());
            //            string tips = ((PlaySpeed * 1.0) / 100).ToString();
            //            if (!tips.Contains(".")) { tips += ".0"; }
            //            ShowPrompt("正以", tips + "x", "倍数播放，键盘 [ ] 快速调节");
            //        }
            //    }
            //}
            //else if (e.KeyValue == 221)
            //{
            //    if (IsPlaying)
            //    {
            //        //快进
            //        if (PlaySpeed >= 300)
            //        {
            //            string tips = ((PlaySpeed * 1.0) / 100).ToString();
            //            if (!tips.Contains(".")) { tips += ".0"; }
            //            ShowPrompt("正以", tips + "x", "倍数播放，已经够快了");
            //        }
            //        else
            //        {
            //            PlaySpeed += 25;
            //            player.SetConfig(104, PlaySpeed.ToString());
            //            string tips = ((PlaySpeed * 1.0) / 100).ToString();
            //            if (!tips.Contains(".")) { tips += ".0"; }
            //            ShowPrompt("正以", tips + "x", "倍数播放，键盘 [ ] 快速调节");
            //        }
            //    }
            //}

            #endregion
            else if (e.KeyCode == Keys.Left)
            {
                if (IsPlaying)
                {
                    if (e.Control)
                    {
                        //快退

                        if (PlaySpeed <= 50)
                        {
                            string tips = ((PlaySpeed * 1.0) / 100).ToString();
                            if (!tips.Contains(".")) { tips += ".0"; }
                            ShowPrompt("正以", tips + "x", "倍数播放，已经够慢了");
                        }
                        else
                        {
                            PlaySpeed -= 25;
                            player.SetConfig(104, PlaySpeed.ToString());
                            string tips = ((PlaySpeed * 1.0) / 100).ToString();
                            if (!tips.Contains(".")) { tips += ".0"; }
                            ShowPrompt("正以", tips + "x", "倍数播放，键盘 Ctrl + ← → 快速调节");
                        }
                    }
                    else
                    {
                        player.SetPosition(player.GetPosition() - 1000);
                    }
                }
            }
            else if (e.KeyCode == Keys.Right)
            {
                if (IsPlaying)
                {
                    if (e.Control)
                    {
                        //快进
                        if (PlaySpeed >= 300)
                        {
                            string tips = ((PlaySpeed * 1.0) / 100).ToString();
                            if (!tips.Contains(".")) { tips += ".0"; }
                            ShowPrompt("正以", tips + "x", "倍数播放，已经够快了");
                        }
                        else
                        {
                            PlaySpeed += 25;
                            player.SetConfig(104, PlaySpeed.ToString());
                            string tips = ((PlaySpeed * 1.0) / 100).ToString();
                            if (!tips.Contains(".")) { tips += ".0"; }
                            ShowPrompt("正以", tips + "x", "倍数播放，键盘 Ctrl + ← → 快速调节");
                        }
                    }
                    else
                    {
                        player.SetPosition(player.GetPosition() + 1000);
                    }
                }
            }

            #region 播放暂停

            else if (e.KeyData == Keys.Space || e.KeyData == Keys.MediaPlayPause)
            {
                if (IsPlaying)
                {
                    PlayorPause();
                }
            }
            else if (e.KeyData == Keys.Play)
            {
                if (IsPlaying)
                {
                    int st = player.GetState();
                    if (st == 3 || st == 2) //播放
                    {
                        player.Play();
                    }
                }
            }
            else if (e.KeyData == Keys.Pause)
            {
                if (IsPlaying)
                {
                    int st = player.GetState();
                    if (st == 5 || st == 4) //暂停
                    {
                        player.Pause();
                    }
                }
            }
            else if (e.KeyData == Keys.Next)
            {
                //下一首
                PlayDown();
            }
            else if (e.KeyData == Keys.MediaStop)
            {
                if (IsPlaying)
                {
                    player.Close();
                }
            }
            #endregion

            #region 系统已处理

            //else if (e.KeyData == Keys.VolumeMute)
            //{
            //    if (player.GetConfig(12) == "0")
            //    {
            //        player.SetConfig(12, "1");
            //        ShowPrompt("当前音量", "静音");
            //    }
            //    else
            //    {
            //        player.SetConfig(12, "0");
            //        ShowPrompt("当前音量", player.GetVolume().ToString());
            //    }
            //}
            //else if (e.KeyData == Keys.VolumeUp)
            //{
            //    ShowCov(player.GetVolume() + 10);
            //}
            //else if (e.KeyData == Keys.VolumeDown)
            //{
            //    ShowCov(player.GetVolume() - 10);
            //}

            #endregion
            else if (e.KeyCode == Keys.Up)
            {
                //if (Vr)
                //{
                //    Vb += 0.05;
                //    PlayerSetConfig(2403, Va + ";" + Vb + ";" + Vc);
                //}
                //else
                //{
                if (e.Control)
                {
                    ShowCov(player.GetVolume() + 50);
                }
                else
                {
                    ShowCov(player.GetVolume() + 10);
                }
                //}
            }
            else if (e.KeyCode == Keys.Down)
            {
                //if (Vr)
                //{
                //    Vb -= 0.05;
                //    PlayerSetConfig(2403, Va + ";" + Vb + ";" + Vc);
                //}
                //else
                //{
                if (e.Control)
                {
                    ShowCov(player.GetVolume() - 50);
                }
                else
                {
                    ShowCov(player.GetVolume() - 10);
                }
                //}
            }
        }

        protected override void OnPreviewKeyDown(PreviewKeyDownEventArgs e)
        {
            if (!player.Visible)
            {
                Player_PreviewKeyDown(this, e);
            }
            base.OnPreviewKeyDown(e);
        }

        private void GoMouseWheel(object sender, MouseEventArgs e)
        {
            if (isVr)
            {
                int y = (e.Delta);

                string on = player.GetConfig(2403);
                if (!string.IsNullOrEmpty(on))
                {
                    string[] os = on.Split(';');
                    g_fVRBaseH = Convert.ToDouble(os[0]);
                    g_fVRBaseV = Convert.ToDouble(os[1]);
                    g_fVRBaseD = Convert.ToDouble(os[2]);
                    g_fVRBaseD += y;
                    string sz = g_fVRBaseH + ";" + g_fVRBaseV + ";" + g_fVRBaseD;
                    player.SetConfig(2403, sz);
                }
            }
        }

        #endregion

        #region 播放事件OnEvent

        private void Player_OnEvent(object sender, AxAPlayer3Lib._IPlayerEvents_OnEventEvent e)
        {
            switch (e.nEventCode)
            {
                case 10010://播放完毕
                    CachePath(PlayIndex, true);
                    PlayDown();
                    break;
                case 10004://DLNA
                    device_list = player.GetConfig(1802);
                    break;
                case 10005:///GIT完毕
                    break;
                case 10009://M3u8

                    #region 为了让M3U8更流畅

                    bool isRun = true;
                    resetM3u8.Set();//继续
                    Action _action = () =>
                    {
                        Thread.Sleep(6000);
                    };
                    _task.ContinueWhenAll(new Task[] { _task.StartNew(_action) }, (action =>
                    {
                        if (isRun)
                        {
                            resetM3u8.Set();//继续
                        }
                    }));
                    resetM3u8.WaitOne();//阻止当前线程
                    resetM3u8.Reset();//停止
                    isRun = false;

                    #endregion

                    break;
            }
        }
        ManualResetEvent resetM3u8 = new ManualResetEvent(true);

        #region 刷新DLNA设备

        string _device_list = "";
        public string device_list
        {
            get { return _device_list; }
            set
            {
                if (_device_list != value)
                {
                    _device_list = value;
                    if (_DLNA != null)
                    {
                        _DLNA.deviceList();
                    }
                    ShowPrompt("检测到新设备", "DLNA");
                }
            }
        }

        #endregion

        #endregion

        #region 缓冲

        private void Player_OnBuffer(object sender, AxAPlayer3Lib._IPlayerEvents_OnBufferEvent e)
        {
            int jiz = e.nPercent;
            //Debug.WriteLine(jiz);
            if (jiz > 0)
            {
                if (jiz > 99)
                {
                    if (promptBuffer != null)
                    {
                        promptBuffer.Close();
                    }
                }
                else if (jiz > 96)
                {
                    ShowPromptBuffer("正在缓冲", jiz + "%");
                }
                else
                {
                    ShowPromptBuffer("正在缓冲", jiz + "%");
                    //_header.Explain = string.Format("正在缓冲...({0}%)", jiz);
                }
            }
            else
            {
                ShowPromptBuffer("正在缓冲");
            }
        }

        #endregion

        #endregion

        #region 核心操作

        public int playerSetConfig(int a, string value)
        {
            int _value = 0;
            this.Invoke(new Action(() =>
            {
                _value = player.SetConfig(a, value);
            }));
            return _value;
        }
        public string playerGetConfig(int a)
        {
            string value = null;
            this.Invoke(new Action(() =>
            {
                value = player.GetConfig(a);
            }));
            return value;
        }

        #endregion

        #endregion

        #region 系统事件
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case 13://网络连接
                    base.WndProc(ref m);
                    break;
                case 537://设备变化
                    RefreshMeunPlayerBySoundDevice();
                    base.WndProc(ref m);
                    break;
                case 0x0312://快捷键
                    switch (m.WParam.ToInt32())
                    {
                        case 1010://桌面退出
                            isDeskTop = false;
                            break;
                        case 1003://上一首
                            PlayUp();
                            break;
                        case 1004://下一首
                            PlayDown();
                            break;
                    }
                    break;
                case 0x112://系统菜单
                    switch (m.WParam.ToInt32())
                    {
                        case 1234://聚合视频
                            break;
                        case 1235://打开本地
                            break;
                        case 1236://打开URL
                            break;
                    }
                    base.WndProc(ref m);
                    break;
                case 0x004A://消息通知
                    try
                    {
                        Program.CopyDataStruct cds = (Program.CopyDataStruct)m.GetLParam(typeof(Program.CopyDataStruct));

                        string[] files = cds.lpData.Split('|');
                        List<PlayerItem> PlayList_Temp = new List<PlayerItem>();
                        foreach (string url in files)
                        {
                            if (PlayList.Find(ab => ab.url == url) == null)
                            {
                                PlayList_Temp.Add(new PlayerItem("file", url));
                            }
                        }
                        if (PlayList_Temp.Count > 0)
                        {
                            isTvModel = false;
                            OpenFileWeb(PlayList_Temp, 0);
                        }

                    }
                    catch (Exception ez)
                    {
                        Api.OpenMessage(this, Frm.MessageType.Warn, ez.Message);
                    }
                    base.WndProc(ref m);
                    break;
                default:
                    base.WndProc(ref m);
                    break;
            }
        }

        #endregion

        #region 控制器

        #region 显示隐藏

        bool ControllerShow = false;
        /// <summary>
        /// 显示/隐藏控制器 与鼠标交互
        /// </summary>
        public void Controller(bool IsShow)
        {
            if (ControllerShow != IsShow)
            {
                //if (!IsShow)
                //{
                //    if (isDeskTop || isDLNA)
                //    {
                //        return;
                //    }
                //    else if (!IsFullScreen)
                //    {
                //        return;
                //    }
                //}
                ControllerShow = IsShow;
                if (IsShow)
                {
                    Show_header();
                    Show_controller();
                    Show_sidebarBtn();
                }
                else
                {
                    if (_sidebarBtn != null)
                    {
                        if (!IsFullScreen || _sidebar == null)
                            _sidebarBtn.Close();
                    }
                    if (_volume != null)
                    {
                        _volume.Close();
                    }
                    if (_header != null)
                    {
                        _header.Close();
                    }
                    if (_controller != null)
                    {
                        _controller.Close();
                    }
                    HideCursor();
                    this.player.Focus();
                }
            }
        }
        void Show_header()
        {
            if (_header == null)
            {
                _header = new Com.Header(this);
                lock (_header)
                {
                    _header.Show(this);
                    _header.MouseDown += FrmMaxMove;
                    _header.MClick += _header_MClick;
                }
            }
            else
            {
                _header.NoClose();
            }
        }

        void Show_controller()
        {
            if (_controller == null)
            {
                _controller = new Com.Controller(this)
                {
                    _maxvalue = _PlayMaxValue,
                    _value = _PlayValue,
                    _IsNext = PlayList.Count > 1,
                };
                lock (_controller)
                {

                    if (IsPlaying && IsPlayPause)
                    {
                        _controller.LoadState = false;
                        _controller.IsPlay = true;
                        _controller.LoadState = true;
                        TaskState = true;
                    }
                    _controller.Show(this);

                    _controller.MouseDown += FrmMove;
                    _controller.ValueChange += (double value) =>
                    {
                        player.SetPosition(Convert.ToInt32(value));
                        PlayValue = value;
                    };
                    _controller.MClick += _com_MClick;
                }
            }
            else
            {
                _controller.NoClose();
            }
        }
        void Show_sidebarBtn()
        {
            if (PlayList.Count > 0 && _sidebarBtn == null)
            {
                _sidebarBtn = new Com.SidebarBtn(this);
                _sidebarBtn.Show(this);
                _sidebarBtn.Click += (object _s, EventArgs _e) =>
                {
                    if (_sidebar == null)
                    {
                        _sidebar = new Com.Sidebar(this);
                        if (_sidebar != null)
                        {
                            _sidebarBtn.IsLeft = true;
                        }
                        _sidebar.Show(this);
                    }
                    else
                    {
                        //if (_sidebar != null)
                        //{
                        //    _sidebarBtn.IsLeft = false;
                        //}
                        _sidebar.Close();
                    }
                };
            }
        }

        #endregion


        /// <summary>
        /// 是否显示控制器
        /// </summary>
        public bool IsShowController = true;
        public Com.Header _header = null;
        public Com.Controller _controller = null;
        public Com.Sidebar _sidebar = null;
        public Com.SidebarBtn _sidebarBtn = null;
        public bool is_volume = false;
        public Com.Volume _volume = null;

        #region 控制器操作

        Rectangle oldrect;
        private void _header_MClick(int value)
        {
            switch (value)
            {
                case 0://菜单
                    //Api.OpenMessage(Program._Main, Frm.MessageType.Good, "发送成功！谢谢您的反馈我们一定再接再厉");
                    //return;
                    Frm.UIListSel frm = new Frm.UIListSel(new string[] { "通道1（聚合）", "通道2（API）", "通道3（电视）" });
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        switch (frm.SelValue)
                        {
                            case "通道1（聚合）":
                                new Frm.WebVideo(this).Show();
                                break;
                            case "通道2（API）":
                                new Frm.WebApiVideo(this).Show();
                                break;
                            case "通道3（电视）":
                                Frm.Web.ConfigDAL config = new Frm.Web.ConfigDAL();
                                bool is_update = false;
                                Action _action = () =>
                                {
                                    if (config.Exists("TvDataV2"))
                                    {
                                        tvDatas = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Frm.TvData>>(config.GetPicCacheData("TvDataV2"));
                                        is_update = true;
                                    }
                                    else
                                    {
                                        Api.OpenMessage(this, Frm.MessageType.Info, "请稍后正在网络拉取...");
                                        List<HttpLib.Val> _header = new List<HttpLib.Val> {
                                            new HttpLib.Val("Accept","text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9"),
                                            new HttpLib.Val("Accept-Encoding","gzip, deflate, br"),
                                            new HttpLib.Val("Accept-Language","zh-CN,zh;q=0.9,en;q=0.8,en-GB;q=0.7,en-US;q=0.6"),
                                            new HttpLib.Val("User-Agent","Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/83.0.4103.61 Safari/537.36 Edg/83.0.478.44"),
                                        };
                                        tvDatas = HttpLib.Http.Get("https://iptv-org.github.io/iptv/channels.json").header(_header).redirect(true).request().ToJson<List<Frm.TvData>>();

                                        if (tvDatas != null && tvDatas.Count > 0)
                                        {
                                            config.AddPicCacheData("TvDataV2", Newtonsoft.Json.JsonConvert.SerializeObject(tvDatas));
                                        }
                                    }
                                    if (tvDatas != null && tvDatas.Count > 0)
                                    {
                                        isTvModel = true;
                                        List<string> countrys = new List<string>();
                                        foreach (Frm.TvData item in tvDatas)
                                        {
                                            if (!countrys.Contains(item.country.name))
                                            {
                                                countrys.Add(item.country.name);
                                            }
                                        }
                                        CountrysTemp = countrys;
                                    }
                                    else
                                    {
                                        Api.OpenMessage(this, Frm.MessageType.Warn, "加载失败");
                                    }
                                };
                                _task.ContinueWhenAll(new Task[] { _task.StartNew(_action) }, (action =>
                                {
                                    if (tvDatas != null && tvDatas.Count > 0)
                                    {
                                        List<Frm.TvData> Channel = tvDatas.FindAll(ab => ab.country.name == SystemSettings.TvSourceType);

                                        List<PlayerItem> PlayList_Temp = new List<PlayerItem>();

                                        foreach (Frm.TvData item in Channel)
                                        {
                                            PlayList_Temp.Add(new PlayerItem("web", item.url, item.name));
                                        }

                                        this.Invoke(new Action(() =>
                                        {
                                            SetPlayList(PlayList_Temp);
                                            PlayIndex = -1;
                                            if (_sidebar == null)
                                            {
                                                Show_sidebarBtn();
                                                if (_sidebar == null)
                                                {
                                                    _sidebar = new Com.Sidebar(this);
                                                    if (_sidebar != null)
                                                    {
                                                        _sidebarBtn.IsLeft = true;
                                                    }
                                                    _sidebar.Show(this);
                                                }
                                            }
                                        }));
#if DEBUG
#else
                                        if (is_update)
                                        {
                                            try
                                            {
                                                List<Val> _header = new List<Val> {
                                                    new Val("Accept","text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9"),
                                                    new Val("Accept-Encoding","gzip, deflate, br"),
                                                    new Val("Accept-Language","zh-CN,zh;q=0.9,en;q=0.8,en-GB;q=0.7,en-US;q=0.6"),
                                                    new Val("User-Agent","Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/83.0.4103.61 Safari/537.36 Edg/83.0.478.44"),
                                                };
                                                tvDatas = Http.Get("https://iptv-org.github.io/iptv/channels.json").redirect(true).header(_header).request().ToJson<List<Frm.TvData>>();

                                                if (tvDatas != null && tvDatas.Count > 0)
                                                {
                                                    config.DeletePicCacheDataPicCacheData("TvDataV2");
                                                    config.AddPicCacheData("TvDataV2", Newtonsoft.Json.JsonConvert.SerializeObject(tvDatas));
                                                }

                                            }
                                            catch { }
                                        }
#endif
                                    }
                                }));

                                //return;
                                //new Frm.WebTv(this).Show();
                                break;
                        }
                    }
                    break;
                case 1://最小化
                    if (SystemSettings.MinimizeToTray)
                    {
                        Controller(false);
                        this.Hide();
                        notify.Visible = true;
                        notify.ShowBalloonTip(3000, "还在运行中", "TPlayer隐藏到托盘", ToolTipIcon.Info);
                    }
                    else
                    {
                        WindowState = FormWindowState.Minimized;
                    }
                    break;
                case 2://最大化/还原
                    Max();
                    break;
                case 3://关闭
                    this.Close();
                    break;
                case 4://置顶
                    if (!this.isTop)
                    {
                        ShowPrompt("总在最前");
                    }
                    else
                    {
                        ShowPrompt("取消在最前");
                    }
                    this.isTop = this.TopMost = !this.isTop;
                    if (_header != null)
                    {
                        _header.Print();
                    }
                    break;
                case 5://小窗口
                    if (IsPlaying)
                    {
                        this.isMini = !this.isMini;
                        if (this.isMini)
                        {
                            if (isMax)
                            {
                                Max();
                            }
                            PlayerItem playerItem = PlayList[PlayIndex];

                            if (playerItem != null)
                            {
                                if (playerItem.videoSize.Width > 0 && playerItem.videoSize.Height > 0)
                                {
                                    oldrect = new Rectangle(this.Location, this.Size);
                                    ShowPrompt("已切换小窗口");
                                    if (playerItem.videoSize.Width == playerItem.videoSize.Height)
                                    {
                                        this.Size = new Size(300, 300);
                                    }
                                    else if (playerItem.videoSize.Width > playerItem.videoSize.Height)
                                    {
                                        this.Size = new Size((playerItem.videoSize.Width * 300) / playerItem.videoSize.Height, 300);
                                    }
                                    else
                                    {
                                        this.Size = new Size(300, (playerItem.videoSize.Height * 300) / playerItem.videoSize.Width);
                                    }
                                }
                                else
                                {
                                    this.isMini = false;
                                }
                            }
                        }
                        else
                        {
                            this.Size = oldrect.Size;
                            this.Location = oldrect.Location;
                            ShowPrompt("已切换正常模式");
                        }
                        if (_header != null)
                        {
                            _header.Print();
                        }
                    }
                    break;
            }
        }

        public Frm.Setting _setting = null;
        public EffectSetting effectSetting = null;
        public DownList _downList = null;
        private void _com_MClick(int value)
        {
            switch (value)
            {
                case 0://播放暂停
                    if (IsPlaying)
                        PlayorPause();
                    break;
                case 1:
                    player.Close();
                    //暂停
                    //LiveWallpaperEngine.LWECore.RestoreParent();
                    //player.desk.SetDesk(player, false);
                    //Rectangle rectangle = player.GetPlayerSize();
                    //player.Location = rectangle.Location;
                    //player.Size = rectangle.Size;
                    break;
                case 2:
                    //下一首
                    PlayDown();
                    break;
                case 3:
                    //音量
                    if (player.GetConfig(12) == "0")
                    {
                        player.SetConfig(12, "1");
                        ShowPrompt("当前音量", "静音");
                    }
                    else
                    {
                        player.SetConfig(12, "0");
                        ShowPrompt("当前音量", player.GetVolume().ToString());
                    }
                    if (_controller != null)
                    {
                        _controller.Print();
                    }
                    break;
                case 4:
                    if (_IsPlaying)
                    {
                        //设置
                        if (effectSetting != null)
                        {
                            effectSetting.Activate();
                        }
                        else
                        {
                            effectSetting = new EffectSetting(this);
                            effectSetting.Show();
                        }
                    }
                    else
                    {
                        if (_setting != null)
                        {
                            _setting.Activate();
                        }
                        else
                        {
                            _setting = new Frm.Setting(this);
                            _setting.Show();
                        }
                    }
                    break;
                case 5:
                    //全屏
                    FullScreen();
                    break;
                case 6:
                    //截图
                    Screenshot();
                    break;
                case 7:
                    //TV投屏
                    if (_DLNA == null)
                    {
                        _DLNA = new DLNA(this);
                        _DLNA.Show();
                    }
                    else { _DLNA.Activate(); }
                    break;
                case 8:
                    //录制
                    if (player.GetConfig(2601) == "1")
                    {
                        PlayerItem playerItem = PlayList[PlayIndex];
                        if (playerItem != null)
                        {
                            if (!isRecord)
                            {

                                bool isPlayP = false;

                                if (IsPlayPause)
                                {
                                    isPlayP = true;
                                    player.Pause();
                                }

                                string[] videos = player.GetConfig(802).Split(';');//wmv;mp4;rmvb
                                if (videos.Length > 0)
                                {
                                    string videoFormat = videos[0];
                                    if (playerItem.videoSize.Width > 0 && playerItem.videoSize.Height > 0)
                                    {
                                        if (videos.Contains("mp4"))
                                        {
                                            videoFormat = "mp4";
                                        }
                                    }
                                    else
                                    {
                                        if (videos.Contains("mp3"))
                                        {
                                            videoFormat = "mp3";
                                        }
                                    }
                                    player.SetConfig(2606, videoFormat);
                                    int values = player.GetPosition();

                                    string recordbasepath = Program.BasePath + "record\\" + playerItem.fileHash + "\\";

                                    recordbasepath.CreateDirectory();

                                    playerItem.recordPath = recordbasepath + playerItem.fileNameNo + "_" + values + "." + videoFormat;
#if DEBUG
                                    Debug.WriteLine("录制地址：" + playerItem.recordPath);
#endif
                                    player.SetConfig(2607, playerItem.recordPath);

                                    player.SetConfig(2608, "");//在当前播放的位置开始录制，调用后开始写入录制文件。

                                    isRecord = true;
                                }
                                else
                                {
                                    ShowPrompt("录制不可用", "缺失解码器");
                                }

                                if (isPlayP)
                                {
                                    player.Play();
                                }

                            }
                            else
                            {
                                player.SetConfig(2609, "");//在当前播放的位置停止录制，调用后录制文件写入完毕，即可以取用了。

                                isRecord = false;

                                if (File.Exists(playerItem.recordPath))
                                {
                                    ShowPromptFile("录制保存至", Path.GetFileName(playerItem.recordPath), playerItem.recordPath);
                                }
                            }
                        }
                        else
                        {
                            ShowPrompt("录制不可用", "请播放视频");
                        }
                    }
                    else
                    {
                        ShowPrompt("录制不可用");
                    }
                    break;
                case 10:
                    //下载
                    if (_downList != null)
                    {
                        _downList.Show();
                        _downList.Activate();
                    }
                    else
                    {
                        _downList = new DownList(this);
                        _downList.Show();
                    }
                    break;
            }
        }

        #region 截图

        void Screenshot()
        {
            if (IsPlaying)
            {
                bool isPlayP = false;

                if (IsPlayPause)
                {
                    isPlayP = true;
                    player.Pause();
                }
                DateTime dateTime = DateTime.Now;
                //string courl = Program.CodecsPath + "ijl15.dll";
                //ijl15.dll
                if (player.GetConfig(701).ToInt() == 1)
                {
                    if (!Screenshot(dateTime))
                    {
                        RepairCode();
                    }
                    if (isPlayP)
                    {
                        Invoke(new Action(() =>
                        {
                            player.Play();
                        }));
                    }
                }
                else
                {
                    RepairCode();
                }
            }
        }

        bool Screenshot(DateTime dateTime)
        {
            string path = Program.ScreenshotPath + dateTime.ToString("yyyy-MM-dd") + "\\";
            path.CreateDirectory();
            string timetxt = dateTime.ToString("HHmmss");
            string pathurl = path + timetxt + ".jpg";
            if (File.Exists(pathurl))
            {
                int i = 1;
                while (File.Exists(path + timetxt + "（" + i + "）" + ".jpg"))
                {
                    i++;
                }
                pathurl = path + timetxt + "（" + i + "）" + ".jpg";
            }

            playerSetConfig(707, "2");
            playerSetConfig(702, pathurl);
            if (File.Exists(pathurl))
            {
                try
                {
                    Invoke(new Action(() =>
                    {
                        Clipboard.SetDataObject(Image.FromFile(pathurl));
                    }));
                }
                catch { }
                ShowPromptFile("截图保存至", timetxt + ".jpg", pathurl);
                return true;
            }

            return false;
        }


        #endregion

        #endregion

        #region 鼠标隐藏交互

        bool _cursorVisible = true;

        public bool CursorVisible
        {
            get { return _cursorVisible; }
            set
            {
                if (_cursorVisible != value)
                {
                    if (Api.ShowCursor(this, value))
                    {
                        _cursorVisible = value;
                        if (_cursorVisible)
                        {
                            HideCursor();
                        }
                    }
                }
            }
        }


        bool isRunHideCursor = false;
        void HideCursor()
        {
            if (_cursorVisible && IsFullScreen && IsPlaying && !isRunHideCursor)
            {
                isRunHideCursor = true;
                //Debug.WriteLine("HideCursor Start");
                Action _action = () =>
                {
                    while (_cursorVisible && IsFullScreen && IsPlaying)
                    {
                        //Debug.WriteLine("HideCursor While");
                        Thread.Sleep(2000);
                        if (_sidebar == null && !ControllerShow && (old_Pos.X == top_Pos.X && old_Pos.Y == top_Pos.Y))
                        {
                            CursorVisible = false;
                            return;
                        }
                        old_Pos = top_Pos;
                    }
                };
                _task.ContinueWhenAll(new Task[] { _task.StartNew(_action) }, (action =>
                {
                    //Debug.WriteLine("HideCursor End");
                    isRunHideCursor = false;
                }));
            }
        }

        #endregion

        #endregion

        #region 播放状态样式

        #region 录制模式

        bool _isRecord = false;
        public bool isRecord
        {
            get { return _isRecord; }
            set
            {
                if (_isRecord != value)
                {
                    _isRecord = value;
                    if (value)
                    {
                        RecordTime = DateTime.Now;
                    }
                    if (_header != null)
                    {
                        _header.Print();
                    }
                    if (_controller != null)
                    {
                        _controller.Print();
                    }
                }
            }
        }

        #endregion

        #region 音乐模式

        bool _isMusic = false;
        public bool isMusic
        {
            get { return _isMusic; }
            set
            {
                if (_isMusic != value)
                {
                    _isMusic = value;
                    Controller(true);
                    //if (value)
                    //{
                    //    notify.Visible = true;
                    //    notify.ShowBalloonTip(3000, "还在运行中", "TPlayer隐藏到托盘", ToolTipIcon.Info);
                    //}
                    //else
                    //{
                    //    notify.Visible = false;
                    //    this.Activate();
                    //}
                }
            }
        }

        #endregion

        #region DLNA模式

        bool _isDLNA = false;
        public bool isDLNA
        {
            get { return _isDLNA; }
            set
            {
                if (_isDLNA != value)
                {
                    _isDLNA = value;
                    Controller(true);
                    if (value)
                    {
                        //if (_DLNA != null)
                        //{
                        //    _DLNA.Close();
                        //}
                        if (IsPlaying)
                        {
                            player.Visible = false;
                            backImage.BlurColor = Color.FromArgb(60, 0, 0, 0);
                            backImage.Image = Properties.Resources.icon_DLNA;
                            backImage.Blur = 40;
                        }
                    }
                    else
                    {
                        Controller(true);
                        if (IsPlaying)
                        {
                            player.Size = this.ClientRectangle.Size;
                            player.Visible = true;
                        }
                    }
                }
            }
        }
        public DLNA _DLNA = null;

        #endregion

        #region 桌面模式

        bool _isDeskTop = false;
        public bool isDeskTop
        {
            get { return _isDeskTop; }
            set
            {
                //_isDeskTop = !_isDeskTop;
                //   bool resulst = SetProcessDpiAwarenessContext(_isDeskTop ? DPI_AWARENESS_CONTEXT.DPI_AWARENESS_CONTEXT_UNAWARE_GDISCALED : DPI_AWARENESS_CONTEXT.DPI_AWARENESS_CONTEXT_UNAWARE);
                //Debug.WriteLine(resulst.ToString());
                //return;


                //if (_isDeskTop != value)
                //{
                //    _isDeskTop = value;
                //    //if (_controller != null)
                //    //{
                //    //    _controller.SetSize();
                //    //}
                //    if (value)
                //    {
                //        //WallpaperApi.Initlize(this);
                //        //WallpaperApi.ShowWallpaper(new WallpaperModel() { Path = @"C:\Users\ttgx\Documents\Tencent Files\17379620\FileRecv\AduMusic_bate2.0.0.exe" });
                //        //try
                //        //{
                //        //    PROCESS_DPI_Result result = SetProcessDpiAwareness(PROCESS_DPI_AWARENESS.PROCESS_SYSTEM_DPI_AWARE);
                //        //}
                //        //catch { }
                //        Controller(false);
                //        this.Hide();
                //        Rectangle rect = Screen.PrimaryScreen.Bounds;
                //        player.Dock = DockStyle.None;
                //        player.Location = rect.Location;
                //        player.Size = rect.Size;

                //        wallpaperHelper = new WallpaperEngine.WallpaperHelper(rect);
                //        bool isOk = wallpaperHelper.SendToBackground(this.Handle);
                //        if (isOk)
                //        {
                //            //if (dpixRatio > 1)
                //            //{
                //            //    Api.OpenMessage(this, Frm.MessageType.Info, "程序以及被DPI缩放，大小有所问题");
                //            //}
                //            notify.Visible = true;
                //            notify.ShowBalloonTip(3000, "还在运行中", "TPlayer隐藏到托盘", ToolTipIcon.Info);
                //            //Show_controllerMini();
                //            IsFullScreen = true;


                //            //this.Show();
                //            //DesktopMouseEventReciver.AddHandle(item.ReceiveMouseEventHandle, item.Screen);
                //        }
                //        else
                //        {
                //            //DesktopMouseEventReciver.RemoveHandle(item.ReceiveMouseEventHandle, item.Screen);

                //            isDeskTop = false;
                //            Api.OpenMessage(this, Frm.MessageType.Warn, "桌面嵌入失败");
                //        }
                //    }
                //    else
                //    {
                //        notify.Visible = false;
                //        //if (_controllerMini != null)
                //        //{
                //        //    _controllerMini.Close();
                //        //}
                //        wallpaperHelper.RestoreParent();
                //        //player.Dock = DockStyle.Fill;
                //        //this.Font = new Font("微软雅黑", 10F);
                //        //WallpaperEngine.WallpaperHelper.RestoreAllHandles();
                //        this.Show(); this.Activate();
                //        //try
                //        //{
                //        //    PROCESS_DPI_Result result = SetProcessDpiAwareness(PROCESS_DPI_AWARENESS.PROCESS_DPI_UNAWARE);
                //        //}
                //        //catch { }
                //        Controller(true);
                //    }


                //}
            }
        }

        #endregion

        //void Show_controllerMini()
        //{
        //    if (_controllerMini == null)
        //    {
        //        _controllerMini = new ControllerMini(this)
        //        {
        //            IsPlay = IsPlayPause
        //        };
        //        _controllerMini.progFull.Value = _PlayValue;
        //        _controllerMini.progFull.MaxValue = _PlayMaxValue;
        //        _controllerMini.Show();
        //        _controllerMini.progFull.ValueChange += (double _value) =>
        //        {
        //            player.SetPosition(Convert.ToInt32(_value));
        //            PlayValue = _value;
        //        };
        //        //_controllerMini.MClick += _com_MClick;
        //    }
        //    else { _controllerMini.Activate(); }
        //}

        #endregion

        #region 打开播放

        #region 其他参数

        public int WebplayIndex = -1;
        public bool isTvModel = false;
        public List<Frm.TvData> tvDatas = null;
        public List<string> CountrysTemp = new List<string>();

        #endregion

        int _playIndex = -1;

        public int PlayIndex
        {
            get
            {
                return _playIndex;
            }
            set
            {
                if (_playIndex != value)
                {
                    _playIndex = value;
                    if (_sidebar != null)
                    {
                        _sidebar.playList.PlayIndex = _playIndex;
                        //_sidebar.videoList();
                    }
                }
            }
        }

        public List<PlayerItem> PlayList = new List<PlayerItem>();//播放列表

        bool isCacheModel = false;
        public void SetPlayList(List<PlayerItem> PlayList)
        {
            this.PlayList = PlayList;
            if (_controller != null)
            {
                _controller.IsNext = PlayList.Count > 1;
            }
            if (!IsFullScreen && !isDeskTop && !isMusic)
            {
                Show_sidebarBtn();
            }

            if (_sidebar != null)
            {
                _sidebar.Invoke(new Action(() =>
                {
                    _sidebar.videoList();
                }));
            }
        }

        #region 调用播放

        public void OpenFileWeb()
        {
            OpenFileWeb(this.PlayIndex);
        }
        public void OpenFileWeb(int index)
        {
            PlayerItem playerItem = null;
            try { playerItem = this.PlayList[index]; } catch { }
            if (playerItem != null)
            {
                OpenFileWeb(playerItem, index);
            }
        }
        public void OpenFileWeb(List<PlayerItem> PlayList, int index)
        {
            CachePath(this.PlayIndex);

            SetPlayList(PlayList);
            PlayerItem playerItem = this.PlayList[index];
            if (playerItem != null)
            {
                OpenFileWeb(playerItem, index);
            }
        }
        void OpenFileWeb(PlayerItem playerItem, int index)
        {
            tProgress.State = TSkin.ProgressBarState.Normal;
            tProgress.Style = ProgressBarStyle.Marquee;
            tProgress.ShowInTaskbar = true;

            if (playerItem.type == "web_net1")
            {

                Action _action = () =>
                {
                    string weburl = Jx1(playerItem.url);
                    if (weburl != null)
                    {
                        playerItem.type = "web";
                        playerItem.url = weburl;
                    }
                };
                _task.ContinueWhenAll(new Task[] { _task.StartNew(_action) }, (action =>
                {
                    this.Invoke(new Action(() =>
                    {
                        if (playerItem.type == "web")
                        {
                            OpenFileWeb(index);
                        }
                        else
                        {
                            SetLoadIng(false);
                            SetNameVideo("TPlayer");
                            tProgress.State = TSkin.ProgressBarState.Normal;
                            tProgress.Style = ProgressBarStyle.Blocks;
                            tProgress.ShowInTaskbar = false;
                            new Frm.WebBrowser(this, playerItem).Show();
                        }
                    }));
                }));
            }
            else
            {
                if (IsPlaying)
                {
                    CachePath(playerItem);
                    if (playerItem.url == oldUrl)
                    {
                        _IsPlaying = uil = false;
                        //TaskState = false;
                        PlaySpeed = 100;
                    }
                    else
                    {
                        IsPlaying = false;
                    }
                }
                SetForegroundWindow(this.Handle);
                this.Activate();


                SetLoadIng(true);
                SetNameVideo(playerItem.name);
                //IsPlayPause = IsPlaying = false;
                panel1.Visible = false;

                if (playerItem.colltag != null)
                {
                    string pathcoll = Program.CachePath + "coll\\" + playerItem.colltag;
                    try
                    {
                        if (index == 0 || !SystemSettings.RememberLocation)
                        {
                            if (File.Exists(pathcoll))
                            {
                                File.Delete(pathcoll);
                            }
                        }
                        else
                        {
                            (Program.CachePath + "coll\\").CreateDirectory();
                            File.WriteAllText(pathcoll, index.ToString());
                        }
                    }
                    catch { }
                }
                PlayIndex = index;


                //if (_controller != null)
                //{
                //    _controller.IsNext = PlayList.Count > 1;
                //}
                playerItem.fileHash = playerItem.url.Md5_16();
                playerItem.fileName = playerItem.url.ToVideoName();
                playerItem.fileNameNo = Path.GetFileNameWithoutExtension(playerItem.fileName);

                string basepath = Program.CachePath + "video\\" + playerItem.fileHash + "\\";

                playerItem.cachePath = basepath + "cache";
                playerItem.videoPath = basepath + playerItem.fileName;

                basepath.CreateDirectory(true);

                PlayerInfoJson playerInfoJson = new PlayerInfoJson
                {
                    PlayType = playerItem.type,
                    Name = playerItem.name,
                    FileName = playerItem.fileName,
                    Hash = playerItem.fileHash,
                    FileCache = playerItem.cachePath,
                    FilePath = playerItem.videoPath,
                    Url = playerItem.url
                };

                File.WriteAllText(basepath + "config.json", playerInfoJson.ToJson());


#if DEBUG
                Debug.WriteLine("cache地址：" + playerItem.cachePath);
                Debug.WriteLine("video地址：" + playerItem.fileName);
#endif
                //if (playerItem.type == "web" && !playerItem.url.EndsWith("m3u8"))
                if (playerItem.type == "web" && !isTvModel)
                {
                    if (File.Exists(playerItem.videoPath))
                    {
                        isCacheModel = false;
                        playerItem.type = "file";
                        playerItem.url = playerItem.videoPath;
                        player.SetConfig(2201, "");
                    }
                    else if (SystemSettings.CacheVideo)
                    {
                        isCacheModel = true;
                        player.SetConfig(2201, playerItem.cachePath);
                    }
                    else
                    {
                        isCacheModel = false;
                        player.SetConfig(2201, "");
                    }
                }
                else
                {
                    isCacheModel = false;
                    player.SetConfig(2201, "");
                }

                if (SystemSettings.RememberLocation && File.Exists(playerItem.cachePath + "_ini"))
                {
                    int val = File.ReadAllText(playerItem.cachePath + "_ini").ToInt();
                    if (val > 0)
                    {
                        player.SetConfig(102, val.ToString());
                    }
                    else
                    {
                        player.SetConfig(102, "");
                    }
                }
                else
                {
                    player.SetConfig(102, "");
                }
                player.Focus();
                player.Open(playerItem.url);
            }
        }

        [DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);
        #endregion

        /// <summary>
        /// 播放上一首
        /// </summary>
        public void PlayUp()
        {
            if (PlayList.Count > 1)
            {
                if (PlayIndex < 1)
                {
                    OpenFileWeb(PlayList.Count - 1);
                }
                else
                {
                    PlayIndex--;
                    OpenFileWeb(PlayIndex);
                }
            }
        }

        /// <summary>
        /// 播放下一首
        /// </summary>
        public void PlayDown()
        {
            if (PlayList.Count > 1)
            {
                if (PlayIndex >= PlayList.Count - 1)
                {
                    //OpenFileWeb(0);
                }
                else
                {
                    PlayIndex++;
                    OpenFileWeb(PlayIndex);
                }
            }
        }

        #region UI调用播放

        private void btn_open_file_Click(object sender, EventArgs e)
        {
            //int dsa = player.SetConfig(24, Program.BasePath + "PluginDemo.dll");

            //APlayerPlugin.CallOpenMedia(OpenMedia);
            //APlayerPlugin.CallVideoFrame(VideoFrame);

            //return;
            using (OpenFileDialog ofd_file = "video".OpenFile(true))
            {
                if (ofd_file.ShowDialog(this) == DialogResult.OK)
                {
                    isTvModel = false;
                    List<PlayerItem> PlayList_Temp = new List<PlayerItem>();
                    foreach (string item in ofd_file.FileNames)
                    {
                        PlayList_Temp.Add(new PlayerItem("file", item));
                    }
                    OpenFileWeb(PlayList_Temp, 0);
                }
            }
        }

        private void btn_open_web_Click(object sender, EventArgs e)
        {
            //return;
            //https://videocdn.taobao.com/oss/taobao-ugc/c72056b75bc44a87bbfed1187b814012/1472640214/video.mp4
            using (OpenUrl openUrl = new OpenUrl())
            {
                if (openUrl.ShowDialog(this) == DialogResult.OK)
                {
                    PlayerItem FplayerItem = PlayList.Find(ab => ab.url == openUrl.Url);
                    if (FplayerItem == null)
                    {
                        isTvModel = false;
                        List<PlayerItem> PlayList_Temp = new List<PlayerItem> {
                            new PlayerItem("web", openUrl.Url)
                        };

                        OpenFileWeb(PlayList_Temp, 0);
                    }
                    else
                    {
                        int index = PlayList.IndexOf(FplayerItem);
                        OpenFileWeb(index);
                    }
                }
            }
        }


        #endregion

        #region 缓存视频

        /// <summary>
        /// 缓存视频/视频录制
        /// </summary>
        void CachePath(PlayerItem playerItem, bool del = false)
        {
            if (playerItem != null)
            {
                if (isRecord)
                {
                    player.SetConfig(2609, "");//在当前播放的位置停止录制=
                    isRecord = false;
                    if (File.Exists(playerItem.recordPath))
                    {
                        ShowPromptFile("录制保存至", Path.GetFileName(playerItem.recordPath), playerItem.recordPath);
                    }
                }
                if (!string.IsNullOrEmpty(playerItem.cachePath))
                {
                    if (del)
                    {
                        if (File.Exists(playerItem.cachePath + "_ini"))
                        {
                            File.Delete(playerItem.cachePath + "_ini");
                        }
                    }
                    else
                    {
                        int value = player.GetPosition();
                        int max = player.GetDuration();
                        if (SystemSettings.RememberLocation && value > 0 && (max - 3000) > value)
                        {
                            File.WriteAllText(playerItem.cachePath + "_ini", value.ToString());
                        }
                        else
                        {
                            if (File.Exists(playerItem.cachePath + "_ini"))
                            {
                                File.Delete(playerItem.cachePath + "_ini");
                            }
                        }
                    }
                    if (playerItem.type == "web")
                    {
                        if (playerSetConfig(2204, playerItem.cachePath) == 1)
                        {
                            playerSetConfig(2205, playerItem.cachePath + ";" + playerItem.videoPath);
                            Debug.WriteLine("缓存完成");
                            Debug.WriteLine(playerItem.cachePath);
                            Debug.WriteLine(playerItem.videoPath);
                            try
                            {
                                File.Delete(playerItem.cachePath);
                                Debug.WriteLine("缓存删除成功！");
                            }
                            catch { Debug.WriteLine("缓存删除失败！"); }
                        }
                    }
                }
            }
        }
        void CachePath(int index, bool del = false)
        {
            int _Count = this.PlayList.Count;
            if (_Count > 0 && index < _Count)
            {
                PlayerItem playerItem = null;
                try { playerItem = this.PlayList[index]; } catch { }
                if (playerItem != null)
                {
                    CachePath(playerItem, del);
                }
            }
        }

        #endregion

        #region 加载字幕

        /// <summary>
        /// 加载视频本地匹配字幕
        /// </summary>
        void LoadVideoSubs(string videoPath)
        {
            string subsPath = Path.GetDirectoryName(videoPath).TrimEnd('\\') + "\\" + Path.GetFileNameWithoutExtension(videoPath);
            if (File.Exists(subsPath + ".ass"))
            {
                LoadZimu(subsPath + ".ass");
            }
            else if (File.Exists(subsPath + ".ssa"))
            {
                LoadZimu(subsPath + ".ssa");
            }
            else if (File.Exists(subsPath + ".srt"))
            {
                LoadZimu(subsPath + ".srt");
            }
            else if (File.Exists(subsPath + ".idx"))
            {
                LoadZimu(subsPath + ".idx");
            }
            //player.SetConfig(507, "1;50;90");//字幕显示位置：1表示设置生效，50表示设置在水平位置 50%，90垂直位置 90%
            //player.SetConfig(508, "微软雅黑;16;16777215;1");//设置默认字幕字体;大小;颜色;阴影

        }

        #endregion

        #region 拖拽文件进入窗体时
        protected override void OnDragEnter(DragEventArgs e)
        {
            //e.Effect = DragDropEffects.All;
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.All;
            else
                e.Effect = DragDropEffects.None;
            base.OnDragEnter(e);
        }
        protected override void OnDragDrop(DragEventArgs e)
        {
            object data = e.Data.GetData(DataFormats.FileDrop);
            if (data is string[])
            {
                List<TFileInfo> myFiles = (data as string[]).ScanPath();
                if (myFiles.Count > 0)
                {
                    List<PlayerItem> PlayList_Temp = new List<PlayerItem>();
                    foreach (TFileInfo file in myFiles)
                    {
                        if (PlayList.Find(ab => ab.url == file.FilePath) == null)
                        {
                            PlayList_Temp.Add(new PlayerItem("file", file.FilePath));
                        }
                    }
                    if (PlayList_Temp.Count > 0)
                    {
                        isTvModel = false;
                        OpenFileWeb(PlayList_Temp, 0);
                    }
                    else if (!IsPlaying)
                    {
                        OpenFileWeb();
                    }
                }
            }
            base.OnDragDrop(e);
        }
        #endregion

        #region 解析

        public string Jx1(string url = "https://youku.com-iqiyi.net/share/RlXi0CztsJgjnZ8P")
        {
            //text/html; charset=UTF-8
            try
            {
                //List<HttpLib.Val> _header = new List<HttpLib.Val> {
                //    new HttpLib.Val("Accept","text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9"),
                //    new HttpLib.Val("Accept-Language","zh-CN,zh;q=0.9,en;q=0.8,en-GB;q=0.7,en-US;q=0.6"),
                //    new HttpLib.Val("Accept-Encoding","gzip, deflate, br"),
                //    new HttpLib.Val("User-Agent","Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/83.0.4103.116 Safari/537.36 Edg/83.0.478.61"),
                //};
                //WebList webList = HttpHelper.HttpGet(url, _header, null, true);
                //string html = webList.ToStringX();
                //if (!string.IsNullOrEmpty(html))
                //{
                //    html = html.Substring(html.IndexOf("<script type=\"text/javascript\">") + 31);
                //    html = html.Substring(html.IndexOf("var main = \"") + 12);

                //    string url_html = new Uri(new Uri(url), html.Substring(0, html.IndexOf("\""))).ToString();

                //    return JxUrlCore(url_html);
                //}
            }
            catch { }
            return null;
        }

        public string JxUrlCore(string url)
        {
            WebResult webList = Http.Get(url).requestNone();
            if (webList == null) { return null; }
            Web.VideoFormat videoFormat = Web.VideoFormatUtil.detectVideoFormat(url, webList.Type);

            if (videoFormat == null)
            {
                //检测成功，不是视频
                return null;
            }
            //Web.VideoInfo videoInfo = new Web.VideoInfo();
            if ("m3u8".Equals(videoFormat.Name))
            {
                double duration = Web.M3U8Util.figureM3U8Duration(ref url);
                if (duration <= 0)
                {
                    //检测成功，不是m3u8的视频
                    return null;
                }
                return url;
                //videoInfo.setDuration(duration);
            }
            else
            {
                return url;
            }
        }

        #endregion

        #endregion

        #region 其他公共功能

        public Frm.WebZimu webZimu = null;
        public void LoadZimu(string path)
        {
            this.Invoke(new Action(() =>
            {
                player.SetConfig(503, path);
                if (player.GetConfig(504) == "0")
                {
                    player.SetConfig(504, "1");
                }
                if (effectSetting != null && effectSetting.p5 != null)
                {
                    effectSetting.p5.change_subtitle();
                }
                RefreshMeunBySubtilte();
            }));
        }

        #endregion

        #region 下载

        public delegate void DownCompleteEventHandler(DownItem Item);
        public delegate void DownProgCompleteEventHandler(DownItem Item, double val, double maxval);
        public event DownCompleteEventHandler DownComplete;
        public event DownProgCompleteEventHandler DownProgChange;


        private int _DownTotalCount = 0;
        public int DownTotalCount
        {
            get
            {
                return _DownTotalCount;
            }
            set
            {
                if (_DownTotalCount != value)
                {
                    _DownTotalCount = value;
                    if (_controller != null)
                    {
                        _controller.Invoke(new Action(() =>
                        {
                            _controller.SetSize();
                        }));
                    }
                    if (_downList != null)
                    {
                        _downList.BeginInvoke(new Action(() =>
                        {
                            _downList.ListRefresh();
                        }));
                    }
                    if (value == 0)
                    {
                        SaveDownConfig();
                        if (isDownClose > -1)
                        {
                            if (isDownClose == 1)
                            {
                                this.BeginInvoke(new Action(() =>
                                {
                                    this.Close();
                                }));
                            }
                            else
                            {
                                isDownClose = -1;
                                this.BeginInvoke(new Action(() =>
                                {
                                    notifyDown.Visible = false;
                                    this.Show();
                                    this.Activate();
                                    Controller(true);
                                }));
                            }
                        }
                    }
                }
            }
        }
        void RefreshDown()
        {
            if (DownList != null)
            {
                int _DownTotalCount = 0;
                lock (DownList)
                {
                    _DownTotalCount = DownList.FindAll(ab => !ab.isComplete).Count;
                }
                DownTotalCount = _DownTotalCount;
            }
            else
            {
                DownTotalCount = 0;
            }
        }
        public void DelDownList(DownItem task)
        {
            lock (DownList)
            {
                task.core.Stop();
                DownList.Remove(task);
                RefreshDown();
            }
        }

        public List<DownItem> DownList = new List<DownItem>();
        public bool AddDownList(List<DownItem> tasks)
        {
            foreach (DownItem downItem in tasks)
            {
                downItem.core = new Helper.DownCore();
            }
            lock (DownList)
            {
                DownList.AddRange(tasks);
            }

            if (_downList != null)
            {
                _downList.BeginInvoke(new Action(() =>
                {
                    _downList.AddRefresh(tasks);
                }));
            }
            return DownTask();
        }
        bool DownTask()
        {
            bool isDownOK = false;
            bool isCanDownOK = true;
            while (isCanDownOK)
            {
                if (DowningCount < SystemSettings.DownloadCount)
                {
                    DownItem downItem = null;
                    lock (DownList)
                    {
                        downItem = DownList.Find(ab => !ab.isRun && !ab.isComplete);
                    }
                    if (downItem != null)
                    {
                        downItem.isRun = true;
                        isDownOK = true;
                        DowningCount++;
                        //downItem.core = new Helper.DownCore();
                        downItem.core.MaxValueChange += (core, e) =>
                        {
                            downItem.FileSize = e;
                        };
                        downItem.core.ValueChange += (core, e) =>
                        {
                            if (DownProgChange != null)
                            {
                                DownProgChange(downItem, e, downItem.FileSize);
                            }
                        };
                        //downItem.core.SpeedChange += (e) =>
                        //{
                        //};

                        Action _action = () =>
                        {
                            string InitErr;
                            if (downItem.core.DownInit(downItem.url, downItem.fileName, out InitErr))
                            {
                                string Err;
                                if (downItem.core.DownUrl(downItem.savepath, downItem.basepath, out Err))
                                {
                                    downItem.isComplete = true;
                                    lock (DownList)
                                    {
                                        DownList.Remove(downItem);
                                    }
                                    if (DownComplete != null)
                                    {
                                        DownComplete(downItem);
                                    }

                                    //下载完成
                                    //downItem.core = null;
                                }
                                else
                                {
                                    //下载失败
                                    downItem.isComplete = true;
                                    downItem.ErrMsg = Err;
                                    downItem.core = null;

                                    Api.OpenMessage(this, Frm.MessageType.Error, downItem.fileName + "下载失败：" + Err);
                                }
                            }
                            else
                            {
                                downItem.isComplete = true;
                                downItem.ErrMsg = InitErr;
                                downItem.core = null;

                                Api.OpenMessage(this, Frm.MessageType.Warn, downItem.fileName + "下载失败：" + InitErr);
                            }
                        };

                        _task.ContinueWhenAll(new Task[] { _task.StartNew(_action) }, (action =>
                        {
                            downItem.isRun = false;
                            DowningCount--;
                            DownTask();
                        }));
                        RefreshDown();
                    }
                    //if (DownList.Count == 0)
                    //{
                    //}
                    isCanDownOK = false;
                }
                else
                {
                    isCanDownOK = false;
                }
            }
            RefreshDown();
            return isDownOK;
        }

        public int DowningCount = 0;

        #endregion

        #region 持续任务

        public DateTime RecordTime = DateTime.Now;
        DateTime change_header_time;

        CancellationTokenSource tokenSource = new CancellationTokenSource();
        ManualResetEvent resetEvent = new ManualResetEvent(false);

        void LongTask()
        {
            //阻止当前线程
            resetEvent.WaitOne();
            while (true)
            {
                if (tokenSource.Token.IsCancellationRequested)
                {
                    return;
                }
                resetEvent.WaitOne(); //阻止当前线程

                try
                {
                    if (_IsFullScreen && _header != null && (DateTime.Now - change_header_time).TotalSeconds > 15)
                    {
                        change_header_time = DateTime.Now;
                        _header.Invoke(new Action(() =>
                        {
                            _header.Print();
                        }));
                    }
                    if (_isRecord && _header != null)
                    {
                        _header.Invoke(new Action(() =>
                        {
                            _header.Print();
                        }));
                    }
                    if (_IsPlaying)
                    {
                        this.Invoke(new Action(() =>
                        {
                            PlayMaxValue = player.GetDuration();
                            PlayValue = player.GetPosition();
                        }));
                        try
                        {

                            if (IsPlaying && _controller != null)
                            {
                                try
                                {
                                    if (_PlayValue != 0 && isCacheModel)
                                    {
                                        //string _buffvalue2 = playerGetConfig(116);//比关键帧信息更详细的关键帧-文件偏移列表信息，显示网络缓冲数据段状态时能用到该信息
                                        //Debug.WriteLine(_buffvalue2);
                                        SetCacheProg(playerGetConfig(2203));//大小640KB
                                    }
                                    else
                                    {
                                        SetCacheProg(null);
                                    }
                                }
                                catch
                                {
                                    continue;
                                }

                            }
                            else
                            {
                                continue;
                            }
                        }
                        catch { }
                    }
                }
                catch
                {
                }

                Thread.Sleep(500);


                resetEvent.WaitOne();//阻止当前线程
            }
        }

        double _cacheProg = -1;
        public double CacheProg
        {
            get { return _cacheProg; }
            //set
            //{
            //    if (_cacheProg != value)
            //    {
            //        _cacheProg = value;
            //    }
            //}
        }
        bool SetCacheProg(double buff)
        {
            if (_cacheProg != buff)
            {
                _cacheProg = buff;
                //Debug.WriteLine("缓存进度：" + buff);
                if (property != null)
                {
                    property.LoadVideoProg();
                }
                return true;
            }
            return false;
        }
        void SetCacheProg(string _buffvalue)
        {
            if (string.IsNullOrEmpty(_buffvalue))
            {
                if (SetCacheProg(-1))
                {
                    if (_controller._buff != null)
                    {
                        _controller._buff = null;
                        _controller.Invoke(new Action(() =>
                        {
                            _controller.SetBufferValue(null);
                        }));
                    }
                }
            }
            else
            {
                //Debug.WriteLine(_buffvalue);
                int tl = _buffvalue.Length;
                _controller.Leng = tl;
                if (_buffvalue.Contains("1"))
                {
                    if (_buffvalue.Contains("0"))
                    {
                        int val = System.Text.RegularExpressions.Regex.Matches(_buffvalue, "1").Count;
                        double cval = Math.Round((val * 1.0) / (tl * 1.0) * 100.0, 2);
                        SetCacheProg(cval);
                        if (_controller._buff != _buffvalue)
                        {
                            List<_Buffe> ilist = new List<_Buffe>();
                            char _old = _buffvalue[0];
                            int s_i = 0;
                            for (int i = 1; i < _buffvalue.Length; i++)
                            {
                                char _new = _buffvalue[i];
                                if (_new != _old)
                                {
                                    if (_new == '0')
                                    {
                                        if (_old == '1')
                                        {
                                            ilist.Add(new _Buffe(s_i, i - s_i));
                                            s_i = i - 1;
                                        }
                                    }
                                    else
                                    {
                                        s_i = i;
                                    }
                                }
                                _old = _new;
                            }
                            if (_old == '1')
                            {
                                ilist.Add(new _Buffe(s_i, _buffvalue.Length - s_i));
                            }

                            _controller._buff = _buffvalue;
                            _controller.Invoke(new Action(() =>
                            {
                                _controller.SetBufferValue(ilist);
                            }));
                        }
                    }
                    else
                    {
                        //全部缓存完毕
                        if (SetCacheProg(100))
                        {
                            _controller.Invoke(new Action(() =>
                            {
                                _controller.SetBufferValue(new List<_Buffe> {
                                    new _Buffe(-10)
                                });
                            }));

                            PlayerItem playerItem = PlayList[PlayIndex];
                            if (playerItem != null)
                            {
                                if (playerSetConfig(2204, playerItem.cachePath) == 1)
                                {
                                    isCacheModel = false;
                                    playerSetConfig(2205, playerItem.cachePath + ";" + playerItem.videoPath);

                                    ShowPrompt("视频缓存完成");
                                }
                            }

                        }
                    }
                }
                else
                {
                    //0缓存
                    if (SetCacheProg(0))
                    {
                        if (_controller._buff != null)
                        {
                            _controller._buff = null;
                            _controller.Invoke(new Action(() =>
                            {
                                _controller.SetBufferValue(null);
                            }));
                        }
                    }
                }

            }
        }



        bool _TaskState = false;
        public bool TaskState
        {
            get { return _TaskState; }
            set
            {
                if (_TaskState != value)
                {
                    if (value)
                    {
                        _TaskState = true;
                        resetEvent.Set();//继续
                    }
                    else
                    {
                        if (!IsPlaying && !_IsFullScreen && !_isRecord)
                        {
                            _TaskState = false;
                            resetEvent.Reset();//停止
                        }
                    }
                }
            }
        }

        #endregion

        #region 托盘

        private void notify_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            notify.Visible = false;
            this.Show();
            this.Activate();
            Controller(true);
        }

        private void notifyDown_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (_downList != null)
            {
                _downList.Show();
                _downList.Activate();
            }
            else
            {
                _downList = new DownList(this);
                _downList.Show();
            }
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            isDownClose = -1;
            this.Invoke(new Action(() =>
            {
                notifyDown.Visible = false;
                this.Show();
                this.Activate();
                Controller(true);
            }));
        }

        private void ToolItem1_Click(object sender, EventArgs e)
        {
            PlayorPause();
        }

        private void ToolItem2_Click(object sender, EventArgs e)
        {
            PlayorPause();
        }

        private void ToolItem3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        #region 右键菜单

        #region 刷新播放器功能的菜单

        void RefreshMeunPlayer()
        {
            #region 声道映射

            RefreshMeunPlayerByAudioChannel(player.GetConfig(404).ToInt());

            #endregion

            #region 音频输出

            RefreshMeunPlayerBySoundDevice();

            #endregion
        }
        string old_SoundDevice = null;
        public void RefreshMeunPlayerBySoundDevice()
        {
            string SoundDeviceList = player.GetConfig(10);
            if (SoundDeviceList != old_SoundDevice)
            {
                old_SoundDevice = SoundDeviceList;
                输出设备ToolStripMenuItem.DropDownItems.Clear();
                if (!string.IsNullOrEmpty(SoundDeviceList))
                {
                    string SoundDeviceCurrent = player.GetConfig(11);
                    foreach (string itemSoundDevice in SoundDeviceList.Split(';'))
                    {
                        ToolStripMenuItem SoundDeviceItem = new ToolStripMenuItem(itemSoundDevice, SoundDeviceCurrent == itemSoundDevice ? Properties.Resources.cover_pressed2 : null);
                        输出设备ToolStripMenuItem.DropDownItems.Add(SoundDeviceItem);
                        SoundDeviceItem.Click += (a, b) =>
                        {
                            SoundDeviceItem.Image = Properties.Resources.cover_pressed2;
                            foreach (ToolStripMenuItem item in 输出设备ToolStripMenuItem.DropDownItems)
                            {
                                if (item != SoundDeviceItem)
                                {
                                    item.Image = null;
                                }
                            }
                            ShowPrompt("音频输出设备", SoundDeviceItem.Text);
                            player.SetConfig(11, SoundDeviceItem.Text);
                            OpenFileWeb();
                        };
                    }
                }
            }
        }
        /// <summary>
        /// 声道映射
        /// </summary>
        public void RefreshMeunPlayerByAudioChannel(int index)
        {
            foreach (ToolStripMenuItem item in 音频模式ToolStripMenuItem.DropDownItems)
            {
                if (item.Name.StartsWith("AudioChannel_"))
                {
                    if (item.Tag.ToInt() == index)
                    {
                        item.Image = Properties.Resources.cover_pressed2;
                    }
                    else
                    {
                        item.Image = null;
                    }
                }
            }
        }

        #endregion

        #region 刷新随播放状态变动的菜单

        public void RefreshMeun()
        {
            RefreshMeunByAudioTrack();
            RefreshMeunBySubtilteEnable();
            RefreshMeunBySubtilte();
        }

        #region 音轨
        public void RefreshMeunByAudioTrack()
        {
            #region 音轨列表

            音轨列表无ToolStripMenuItem.DropDownItems.Clear();
            string AudioTrackList = player.GetConfig(402);
            if (!string.IsNullOrEmpty(AudioTrackList))
            {
                音轨列表无ToolStripMenuItem.Text = "音轨列表";
                int AudioTrackListCurrent = player.GetConfig(403).ToInt();
                string[] splitAudioTrackList = AudioTrackList.Split(';');
                for (int i = 0; i < splitAudioTrackList.Length; i++)
                {
                    int index = i;
                    ToolStripMenuItem AudioTrackItem = new ToolStripMenuItem(splitAudioTrackList[i], i == AudioTrackListCurrent ? Properties.Resources.cover_pressed2 : null);
                    音轨列表无ToolStripMenuItem.DropDownItems.Add(AudioTrackItem);

                    AudioTrackItem.Click += (a, b) =>
                    {
                        AudioTrackItem.Image = Properties.Resources.cover_pressed2;
                        for (int i_2 = 0; i_2 < 音轨列表无ToolStripMenuItem.DropDownItems.Count - 1; i_2++)
                        {
                            var item = 音轨列表无ToolStripMenuItem.DropDownItems[i_2];
                            if (item != AudioTrackItem)
                            {
                                item.Image = null;
                            }
                        }
                        ShowPrompt("当前音轨", AudioTrackItem.Text);
                        player.SetConfig(403, index.ToString());
                    };
                }
            }
            else
            {
                音轨列表无ToolStripMenuItem.Text = "音轨列表(无)";
            }

            ToolStripMenuItem AudioTrackAddItem = new ToolStripMenuItem("添加音轨");
            音轨列表无ToolStripMenuItem.DropDownItems.Add(AudioTrackAddItem);
            AudioTrackAddItem.Click += (a, b) =>
            {
                using (OpenFileDialog ofd = "video".OpenFile())
                {
                    if (ofd.ShowDialog(this) == DialogResult.OK)
                    {
                        player.SetConfig(409, ofd.FileName);
                        RefreshMeun();
                    }
                }
            };

            #endregion
        }
        public void RefreshMeunByAudioTrack(int index)
        {
            RefreshMeunCore(音轨列表无ToolStripMenuItem, index);
        }

        /// <summary>
        /// 设置声道映射
        /// </summary>
        private void AudioChannelChange_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem f = (ToolStripMenuItem)sender;
            f.Image = Properties.Resources.cover_pressed2;
            foreach (ToolStripMenuItem item in 音频模式ToolStripMenuItem.DropDownItems)
            {
                if (item.Name.StartsWith("AudioChannel_") && item != f)
                {
                    item.Image = null;
                }
            }
            ShowPrompt("音道模式", f.Text);
            player.SetConfig(404, f.Tag.ToString());
        }

        #endregion

        #region 字幕
        public void RefreshMeunBySubtilteEnable()
        {
            显示字幕ToolStripMenuItem.Text = player.GetConfig(504).ToInt() == 1 ? "隐藏字幕" : "显示字幕";
        }
        public void RefreshMeunBySubtilte()
        {
            #region 字幕列表

            选择字幕无ToolStripMenuItem.DropDownItems.Clear();
            string SubtilteLanguageList = player.GetConfig(505);
            if (!string.IsNullOrEmpty(SubtilteLanguageList))
            {
                选择字幕无ToolStripMenuItem.Text = "选择字幕";
                int SubtilteLanguageCurrent = player.GetConfig(506).ToInt();
                string[] splitSubtilteLanguageList = SubtilteLanguageList.Split(';');
                for (int i = 0; i < splitSubtilteLanguageList.Length; i++)
                {
                    int index = i;
                    ToolStripMenuItem SubtilteItem = new ToolStripMenuItem(splitSubtilteLanguageList[i], i == SubtilteLanguageCurrent ? Properties.Resources.cover_pressed2 : null);
                    选择字幕无ToolStripMenuItem.DropDownItems.Add(SubtilteItem);

                    SubtilteItem.Click += (a, b) =>
                    {
                        SubtilteItem.Image = Properties.Resources.cover_pressed2;
                        for (int i_2 = 0; i_2 < 选择字幕无ToolStripMenuItem.DropDownItems.Count - 1; i_2++)
                        {
                            var item = 选择字幕无ToolStripMenuItem.DropDownItems[i_2];
                            if (item != SubtilteItem)
                            {
                                item.Image = null;
                            }
                        }
                        ShowPrompt("当前字幕", SubtilteItem.Text);
                        player.SetConfig(506, index.ToString());
                    };
                }

            }
            else
            {
                选择字幕无ToolStripMenuItem.Text = "选择字幕(无)";
            }

            ToolStripMenuItem SubtilteAddItem = new ToolStripMenuItem("添加字幕");
            选择字幕无ToolStripMenuItem.DropDownItems.Add(SubtilteAddItem);
            SubtilteAddItem.Click += (a, b) =>
            {
                string filter = player.GetConfig(502);
                if (!string.IsNullOrEmpty(filter))
                {
                    string[] filters = filter.Split(';');
                    string _filter = "支持的字幕格式|";
                    foreach (string item in filters)
                    {
                        _filter += $"*.{item};";
                    }
                    using (OpenFileDialog ofd = (_filter + "|所有文件|*.*").OpenFile())
                    {
                        if (ofd.ShowDialog(this) == DialogResult.OK)
                        {
                            LoadZimu(ofd.FileName);
                        }
                    }
                }
            };
            #endregion
        }

        public void RefreshMeunBySubtilte(int index)
        {
            RefreshMeunCore(选择字幕无ToolStripMenuItem, index);
        }


        private void 在线匹配ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (IsPlaying)
            {
                string videoName = this.Text;

                if (webZimu == null)
                {
                    string _videoName = videoName;
                    if (_videoName.Contains(" - "))
                    {
                        _videoName = _videoName.Substring(0, _videoName.LastIndexOf(" - "));
                    }
                    else if (_videoName.Contains("-HD"))
                    {
                        _videoName = _videoName.Substring(0, _videoName.LastIndexOf("-HD"));
                    }
                    webZimu = new Frm.WebZimu(this, _videoName);
                    webZimu.Show();
                }
                else
                {
                    webZimu._Load(videoName);
                    webZimu.Activate();
                }
            }
            else
            {
                if (webZimu == null)
                {
                    webZimu = new Frm.WebZimu(this, null);
                    webZimu.Show();
                }
                else
                {
                    webZimu.Activate();
                }
            }
        }

        private void 显示字幕ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            player.SetConfig(504, 显示字幕ToolStripMenuItem.Text == "显示字幕" ? "1" : "0");
            RefreshMeunBySubtilteEnable();
        }


        #endregion

        public void RefreshMeunCore(ToolStripMenuItem items, int index)
        {
            items.DropDownItems[index].Image = Properties.Resources.cover_pressed2;
            for (int i = 0; i < items.DropDownItems.Count - 1; i++)
            {
                var item = items.DropDownItems[i];
                if (i != index)
                {
                    item.Image = null;
                }
            }
        }



        #endregion

        /// <summary>
        /// 全屏/退出全屏
        /// </summary>
        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            FullScreen();
        }

        private void 保存截图ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Screenshot();
        }

        private void gif图截取ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        public Frm.Property property = null;
        private void 视频属性ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (property == null)
            {
                property = new Frm.Property(this);
                //Api.MaskForm(this, property, Color.White);
                property.Show();
            }
            else
            {
                property.Activate();
            }
        }


        private void 窗口适应视频ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (IsPlaying)
            {
                PlayerItem playerItem = PlayList[PlayIndex];

                if (playerItem != null)
                {
                    if (playerItem.videoSize.Width > 0 && playerItem.videoSize.Height > 0)
                    {

                        int destWidth = this.Width, destHeight = this.Height;
                        int sH, sW;
                        if ((playerItem.videoSize.Width * destHeight) > (playerItem.videoSize.Height * destWidth))
                        {
                            sH = destHeight;
                            sW = (playerItem.videoSize.Width * destHeight) / playerItem.videoSize.Height;
                        }
                        else
                        {
                            sW = destWidth;
                            sH = (destWidth * playerItem.videoSize.Height) / playerItem.videoSize.Width;
                        }

                        this.Size = new Size(sW, sH);
                    }
                }
            }
        }

        private void 使用视频大小ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (IsPlaying)
            {
                PlayerItem playerItem = PlayList[PlayIndex];

                if (playerItem != null)
                {
                    if (playerItem.videoSize.Width > 0 && playerItem.videoSize.Height > 0)
                    {
                        this.Size = playerItem.videoSize;
                    }
                }
            }
        }

        #endregion

        #region 生命周期 / 初始化关闭

        #region 窗口基本操作

        #region 调用非托管的动态链接库来让窗体可以拖动
        [DllImport("User32.DLL")]
        public static extern int SendMessage(IntPtr hWnd, uint Msg, int wParam, int lParam);
        [DllImport("User32.DLL")]
        public static extern bool ReleaseCapture();
        private void FrmFullMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (e.Clicks == 2)
                {
                    FullScreen();
                }
                else if (!isMax && !IsFullScreen)
                {
                    FrmMove(sender, e);
                }
            }
        }
        private void FrmMaxMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (e.Clicks == 2)
                {
                    Max();
                }
                else if (!isMax && !IsFullScreen)
                {
                    FrmMove(sender, e);
                }
            }
        }
        public bool isMoveIn = false;
        public void FrmMove(object sender, MouseEventArgs e)
        {
            if (!isMax && !IsFullScreen)
            {
                isMoveIn = true;
                ReleaseCapture();
                SendMessage(Handle, 0x0112, 61456 | 2, 0);
                isMoveIn = false;
                player.SetConfig(21, "");
            }
        }
        #endregion

        public bool isTop = false;
        public bool isMini = false;

        public bool isMax = false;//记录是否最大化
        /// <summary>
        /// 最大化还原
        /// </summary>
        public void Max()
        {
            if (IsFullScreen)
            {
                FullScreen();
            }
            else
            {
                if (WindowState == FormWindowState.Maximized)
                {
                    isMax = false;
                    WindowState = FormWindowState.Normal;
                    Controller(true);
                }
                else
                {
                    isMax = true;
                    WindowState = FormWindowState.Maximized;

                    if (_sidebar != null)
                    {
                        _sidebar.Close();
                    }
                }
            }
        }

        bool _IsFullScreen = false;
        public bool IsFullScreen
        {
            get { return _IsFullScreen; }
            set
            {
                if (_IsFullScreen != value)
                {
                    if (value)
                    {
                        if (IsPlaying)
                        {
                            _IsFullScreen = true;
                            change_header_time = DateTime.Now;

                            if (isMax)
                            {
                                this.WindowState = FormWindowState.Normal;
                            }

                            if (isTop)
                            {
                                this.TopMost = false;
                            }
                            this.FormBorderStyle = FormBorderStyle.None;
                            this.WindowState = FormWindowState.Maximized;
                            player.SetConfig(21, "");

                            IsShowController = false;
                            if (_header != null)
                            {
                                _header.Print();
                            }
                            if (_sidebar != null)
                            {
                                _sidebar.Close();
                            }
                            HideCursor();

                            TaskState = true;

                        }
                        else { Max(); }

                    }
                    else
                    {
                        isMax = false;
                        _IsFullScreen = false;
                        IsShowController = true;
                        this.FormBorderStyle = FormBorderStyle.Sizable;
                        this.WindowState = FormWindowState.Normal;
                        Controller(true);
                        if (_header != null)
                        {
                            _header.Print();
                        }
                        //退出全屏
                        if (isTop)
                        {
                            this.TopMost = true;
                        }

                        TaskState = false;
                    }
                    if (_controller != null)
                    {
                        _controller.Print();
                    }
                }
            }
        }

        /// <summary>
        /// 全屏
        /// </summary>
        void FullScreen()
        {
            IsFullScreen = !IsFullScreen;
        }

        #endregion

        public TPlayer(int mainid, List<string> files)
        {
            this.mainid = mainid;
            InitializeComponent();
            pictureBox1.Visible = label2.Visible = !SystemSettings.ShowLogo;
            SetFromId(this.Handle);
            if (files.Count > 0)
            {
                Action _action = () =>
                {
                    Thread.Sleep(1000);
                };
                _task.ContinueWhenAll(new Task[] { _task.StartNew(_action) }, (action =>
                {
                    this.BeginInvoke(new Action(() =>
                    {
                        List<PlayerItem> PlayList_Temp = new List<PlayerItem>();
                        foreach (string url in files)
                        {
                            if (PlayList.Find(ab => ab.url == url) == null)
                            {
                                PlayList_Temp.Add(new PlayerItem("file", url));
                            }
                        }
                        if (PlayList_Temp.Count > 0)
                        {
                            isTvModel = false;
                            OpenFileWeb(PlayList_Temp, 0);
                        }
                    }));
                }));
            }
        }


        public bool LoadState = true;

        #region DPI

        public enum PROCESS_DPI_AWARENESS
        {
            /// <summary>
            /// DPI 不知道。此应用程序不缩放 DPI 更改，并且始终假定其比例因子为 100%（96 DPI）。系统将在任何其他 DPI 设置上自动缩放它。
            /// </summary>
            PROCESS_DPI_UNAWARE,
            /// <summary>
            /// 系统 DPI 感知。此应用程序不会为 DPI 更改缩放。它将查询 DPI 一次，并在应用的生存期内使用该值。如果 DPI 发生更改，应用将不会调整为新的 DPI 值。当 DPI 从系统值更改时，系统会自动将其放大或缩小。
            /// </summary>
            PROCESS_SYSTEM_DPI_AWARE,
            /// <summary>
            /// 每个监视器 DPI 感知。此应用程序在创建 DPI 时检查 DPI，并在 DPI 更改时调整比例因子。这些应用程序不会由系统自动缩放。
            /// </summary>
            PROCESS_PER_MONITOR_DPI_AWARE
        };

        public enum PROCESS_DPI_Result
        {
            /// <summary>
            /// 成功
            /// </summary>
            S_OK,
            /// <summary>
            /// 传入的值无效。
            /// </summary>
            E_INVALIDARG,
            /// <summary>
            /// DPI感知已经设置，可以通过以前调用此API，也可以通过应用程序（.exe）清单。
            /// </summary>
            E_ACCESSDENIED
        };
        [DllImport("Shcore.dll")]
        internal static extern PROCESS_DPI_Result SetProcessDpiAwareness(PROCESS_DPI_AWARENESS flags);

        public enum DPI_AWARENESS_CONTEXT
        {
            /// <summary>
            /// DPI 不知道。此窗口不缩放 DPI 更改，并且始终假定其比例因子为 100%（96 DPI）。系统将在任何其他 DPI 设置上自动缩放它。
            /// </summary>
            DPI_AWARENESS_CONTEXT_UNAWARE = -1,
            /// <summary>
            /// 系统 DPI 感知。此窗口不缩放 DPI 更改。它将查询 DPI 一次，并在进程的生存期内使用该值。如果 DPI 发生更改，进程将不会调整到新的 DPI 值。当 DPI 从系统值更改时，系统会自动将其放大或缩小。
            /// </summary>
            DPI_AWARENESS_CONTEXT_SYSTEM_AWARE = -2,
            /// <summary>
            /// 每个监视器 DPI 感知。此窗口在创建 DPI 时检查该窗口，并在 DPI 更改时调整比例因子。这些进程不会由系统自动缩放。
            /// </summary>

            DPI_AWARENESS_CONTEXT_PER_MONITOR_AWARE = -3,
            DPI_AWARENESS_CONTEXT_PER_MONITOR_AWARE_V2 = -4,
            /// <summary>
            /// DPI 不知道基于 GDI 的内容的质量会提高。此模式的行为类似于DPI_AWARENESS_CONTEXT_UNAWARE，但也使系统能够在高 DPI 监视器上显示窗口时自动提高文本和其他基于 GDI 基元的呈现质量。
            /// </summary>
            DPI_AWARENESS_CONTEXT_UNAWARE_GDISCALED = -5
        }
        [DllImport("User32.dll")]
        internal static extern bool SetProcessDpiAwarenessContext(DPI_AWARENESS_CONTEXT flags);
        #endregion

        protected override void OnLoad(EventArgs e)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
            ServicePointManager.DefaultConnectionLimit = 10000;
            player = new AxAPlayer3Lib.AxPlayer
            {
                Dock = DockStyle.Fill
            };
            #region 绑定事件

            player.OnBuffer += Player_OnBuffer;
            player.OnMessage += Player_OnMessage;
            player.PreviewKeyDown += Player_PreviewKeyDown;
            player.OnStateChanged += Player_OnStateChanged;
            player.OnDownloadCodec += Player_OnDownloadCodec;
            player.OnEvent += Player_OnEvent;
            MouseWheel += GoMouseWheel;

            backImage.PreviewKeyDown += Player_PreviewKeyDown;

            #endregion

            #region 注册快捷键
            HotKey.RegisterHotKey(Handle, 1010, HotKey.KeyModifiers.Shift, Keys.Escape);//桌面退出
            //HotKey.RegisterHotKey(Handle, 1001, HotKey.KeyModifiers.None, Keys.Oem4);//快退
            //HotKey.RegisterHotKey(Handle, 1002, HotKey.KeyModifiers.None, Keys.Oem6);//快进
            HotKey.RegisterHotKey(Handle, 1003, HotKey.KeyModifiers.None, Keys.PageUp);//上一首
            HotKey.RegisterHotKey(Handle, 1004, HotKey.KeyModifiers.None, Keys.PageDown);//下一首
            #endregion

            this.Controls.Add(player);

            #region 设置初始属性
            string backImgUrl = SystemSettings.BackImgUrl;
            if (!string.IsNullOrEmpty(backImgUrl))
            {
                backImage.BackgroundImage = Image.FromFile(backImgUrl);

                //backImage.Image = Image.FromFile(@"C:\Users\t9713\OneDrive\文档\Tom Files\代码\NET\Me\Win10\Win10\bin\Debug\b84b41993baf16c2b0cdecea7dd3d840219cf4179c9fed8edc1ed853d036daed.jpg");
            }

            player.SetCustomLogo(-1);
            Program.CodecsPath.CreateDirectory(true);
            player.SetConfig(2, Program.CodecsPath);
            if (SystemSettings.CacheGreed)
            {
                player.SetConfig(2207, "1");
            }

            player.SetConfig(209, SystemSettings.SpeedupEnable ? "1" : "0");

            if (SystemSettings._3D)
            {
                player.SetConfig(308, "1");
                player.SetConfig(311, SystemSettings._3DMode.ToString());
                player.SetConfig(312, SystemSettings._3DColor.ToString());
            }
            if (SystemSettings.VR)
            {
                player.SetConfig(2401, "1");
            }

            ToolItem1.Image = FontAwesome.GetImage("5063", 30, Color.Black);
            ToolItem2.Image = FontAwesome.GetImage("5057", 30, Color.Black);
            ToolItem3.Image = FontAwesome.GetImage("50C2", 30, Color.Red);

            #endregion

            player.BringToFront();
            LoadState = false;
            player.Visible = false;
            RefreshMeunPlayer();
            RefreshMeun();
            //SetupSystemMenu();
            base.OnLoad(e);
            //Helper.DownTotalCore.TotalCountChange += DownTotalCore_TotalCountChange;

            //Cabinink.Windows.Energy.EAcPowerStatus eAcPowerStatus = Cabinink.Windows.Energy.BatteryUsageInformation.GetAcPowerStatus();
            //Cabinink.Windows.Energy.EBatteryStatus eBatteryStatus = Cabinink.Windows.Energy.BatteryUsageInformation.GetBatteryStatus();
            List<DownItem> _DownList = null;
            Action _action = () =>
            {
                PluginApi.LoadPlugins();
                Thread.Sleep(200);
                if (File.Exists(Program.BasePath + "down.json"))
                {
                    try
                    {
                        _DownList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DownItem>>(File.ReadAllText(Program.BasePath + "down.json"));
                    }
                    catch { }
                }
            };
            _task.ContinueWhenAll(new Task[] { _task.StartNew(_action) }, (action =>
            {
                this.Invoke(new Action(() =>
                {
                    panel1.Visible = true;
                    Controller(true);
                    //player.Dispose();
                    //player = null;
                }));
                if (_DownList != null && _DownList.Count > 0)
                {
                    foreach (DownItem item in _DownList)
                    {
                        DownItem downItem = _DownList.Find(ab => ab.imgUrl == item.imgUrl && ab.imgOk);
                        if (downItem != null)
                        {
                            item.img = downItem.img;
                        }
                        else
                        {
                            item.imgOk = true;
                            try
                            {
                                item.img = Http.Get(item.imgUrl).redirect(true).requestData().ToImage();
                            }
                            catch { }
                        }
                    }
                    Api.OpenMessage(this, Frm.MessageType.Info, _DownList.Count + "个视频继续下载");
                    AddDownList(_DownList);
                }
            }));
            new Thread(() => LongTask())
            {
                IsBackground = true
            }.Start();
        }


        #region 软件唯一

        int mainid;
        MemoryMappedFile onlyID = null;
        void SetFromId(IntPtr handler)
        {
            onlyID = MemoryMappedFile.CreateOrOpen(mainid.ToString(), 1024000);
            using (MemoryMappedViewStream stream = onlyID.CreateViewStream()) //注意这里的偏移量  
            {
                using (MemoryMappedViewAccessor accessor = onlyID.CreateViewAccessor())
                {
                    accessor.Write(0, ref handler);//这里的handler就是我们窗口句柄  
                }
            }
        }

        #endregion

        #region 安装系统菜单

        [DllImport("user32.dll")]
        static extern int GetSystemMenu(int hwnd, int bRevert);

        [DllImport("user32.dll")]
        static extern int AppendMenu(int hMenu, int Flagsw, int IDNewItem, string lpNewItem);
        void SetupSystemMenu()
        {
            int menu = GetSystemMenu(this.Handle.ToInt32(), 0);
            //   add   a   separator 
            AppendMenu(menu, 0xA00, 0, null);
            //   add   an   item   with   a   unique   ID 
            AppendMenu(menu, 0, 1234, "聚合视频");
            AppendMenu(menu, 0, 1235, "打开本地");
            AppendMenu(menu, 0, 1236, "打开URL");
        }
        #endregion


        bool isclose = false;
        int isDownClose = -1;
        protected override void OnClosing(CancelEventArgs e)
        {
            if (DownList != null)
            {
                lock (DownList)
                {
                    int count = DownTotalCount;
                    if (count > 0)
                    {
                        using (Frm.HasDownLoad form = new Frm.HasDownLoad("还有 " + count + "个 任务正在下载"))
                        {
                            DialogResult dialogResult = form.ShowDialog();
                            if (dialogResult == DialogResult.OK)
                            {
                                e.Cancel = true;
                                base.OnClosing(e);
                                return;
                            }
                            else if (dialogResult == DialogResult.Retry)
                            {
                                isDownClose = 1;
                                Controller(false);
                                this.Hide();
                                player.Close();
                                e.Cancel = true;
                                notifyDown.Visible = true;
                                base.OnClosing(e);
                                return;
                            }
                            else if (dialogResult == DialogResult.Ignore)
                            {
                                isDownClose = 0;
                                Controller(false);
                                this.Hide();
                                player.Close();
                                e.Cancel = true;
                                notifyDown.Visible = true;
                                base.OnClosing(e);
                                return;
                            }
                        }
                    }
                }
            }
            tokenSource.Cancel();
            if (onlyID != null) { onlyID.Dispose(); onlyID = null; }

            notify.Visible = notifyDown.Visible = false;
            Controller(false);
            if (uil && !isclose)
            {
                this.Hide();
                isclose = true;
                //this.Enabled = false;
                player.Close();
                e.Cancel = true;
                base.OnClosing(e);
                return;
            }
            SaveDownConfig();
            notify.Dispose();
            notifyDown.Dispose();

            base.OnClosing(e);
        }
        void SaveDownConfig()
        {
            lock (DownList)
            {
                foreach (DownItem item in DownList)
                {
                    if (item.core != null)
                    {
                        item.core.Stop();
                    }
                }
                List<DownItem> downItems = this.DownList.FindAll(ab => !ab.isComplete);
                if (downItems != null && downItems.Count > 0)
                {
                    File.WriteAllText(Program.BasePath + "down.json", Newtonsoft.Json.JsonConvert.SerializeObject(downItems));
                }
                else if (File.Exists(Program.BasePath + "down.json"))
                {
                    File.Delete(Program.BasePath + "down.json");
                }
            }
        }

        #endregion

        #region 区域与位置

        void SetNameVideo(string txt)
        {
            if (this.Text != txt)
            {
                this.Text = txt;
                if (txt.Length > 64)
                {
                    notify.Text = txt.Substring(0, 60);
                }
                else
                {
                    notify.Text = txt;
                }
                if (_header != null)
                {
                    _header.Print();
                }
            }
        }

        public bool isInPlayRect(Point point)
        {
            if (this.ClientRectangle.Contains(point) && !Moverect.Contains(point))
            {
                return true;
            }
            return false;
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (!player.Visible)
            {
                top_Pos = e.Location;
                if (old_Pos.X != top_Pos.X && old_Pos.Y != top_Pos.Y)
                {
                    CursorVisible = true;
                }
                old_Pos = top_Pos;
                //Controller(true);
                if (isMini || IsFullScreen)
                {
                    if (isInPlayRect(top_Pos))
                    {
                        Controller(true);
                    }
                    else if (MoveBtnrect.Contains(top_Pos))
                    {
                        Show_sidebarBtn();
                    }
                    else
                    {
                        if (_sidebarBtn != null)
                        {
                            if (!IsFullScreen || _sidebar == null)
                                _sidebarBtn.Close();
                        }
                        Controller(false);
                    }
                }
            }
            base.OnMouseMove(e);
        }
        Point top_Pos, old_Pos;

        Rectangle Moverect;

        Rectangle MoveBtnrect = new Rectangle(0, 0, 78, 128);
        protected override void OnSizeChanged(EventArgs e)
        {
            Size size = this.Size;
            int _Height = size.Height / 4;
            if (_Height < 100)
            {
                _Height = 100;
            }
            Moverect = new Rectangle(0, _Height, size.Width, size.Height - (_Height * 2));
            MoveBtnrect.Location = new Point(size.Width - MoveBtnrect.Width, ((size.Height - MoveBtnrect.Height) / 2));

            if (IsPlaying)
            {
                if (_FrameStyle == PlayFrameStyle.Paved)
                {
                    UseFrameStyle();
                }
                player.SetConfig(21, "");
            }
            base.OnSizeChanged(e);
        }

        #endregion

        #region APlayer插件

        #region 打开媒体回调

        private APlayerPlugin.OpenMedia OpenMedia = new APlayerPlugin.OpenMedia(OpenMediaCallBack);

        private static bool OpenMediaCallBack(string pcszUrl, long nFileSize, int nDuration)
        {
            try
            {
                Program._Main.Invoke(new Action(() =>
                {
                    Program._Main.BackgroundImageLayout = ImageLayout.Zoom;
                    Program._Main.Visible = false;
                }));
            }
            catch { }
            return true;
        }

        #endregion

        #region 每帧画面回调

        #region BITMAPINFO_FLAT
        [StructLayout(LayoutKind.Sequential)]
        public struct BITMAPINFO_FLAT
        {
            public int bmiHeader_biSize;
            public int bmiHeader_biWidth;
            public int bmiHeader_biHeight;
            public short bmiHeader_biPlanes;
            public short bmiHeader_biBitCount;
            public int bmiHeader_biCompression;
            public int bmiHeader_biSizeImage;
            public int bmiHeader_biXPelsPerMeter;
            public int bmiHeader_biYPelsPerMeter;
            public int bmiHeader_biClrUsed;
            public int bmiHeader_biClrImportant;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1024)]
            public byte[] bmiColors;
        }
        #endregion

        [DllImport("gdi32")]
        public static extern IntPtr CreateDIBSection(IntPtr hdc, ref BITMAPINFO_FLAT bmi, int iUsage, ref int ppvBits, IntPtr hSection, int dwOffset);

        [DllImport("gdi32.dll")]
        public static extern IntPtr CreateCompatibleDC(IntPtr hDC);


        [DllImport("gdi32.dll")]
        public static extern IntPtr SelectObject(IntPtr hDC, IntPtr hObject);


        private APlayerPlugin.VIDEO_FRAME_CALLBACK VideoFrame = new APlayerPlugin.VIDEO_FRAME_CALLBACK(VideoFrame_CallBack);


        private static void VideoFrame_CallBack(IntPtr pFrame)
        {
            try
            {
                VIDEO_FRAME_INFO frame = Marshal.PtrToStructure<VIDEO_FRAME_INFO>(pFrame);
                byte[] _data = new byte[frame.length];
                Marshal.Copy(frame.frame, _data, 0, _data.Length);
                string filePath = @"C:\Users\ttgx\Desktop\test1.mp4";

                BITMAPINFO_FLAT bi = new BITMAPINFO_FLAT
                {
                    bmiHeader_biBitCount = 32,
                    bmiHeader_biPlanes = 1,
                    bmiHeader_biWidth = frame.width,
                    bmiHeader_biHeight = -frame.height,
                    bmiHeader_biCompression = 0,
                    bmiHeader_biSizeImage = frame.width * frame.height * 4
                };

                int pBits = 0;
                IntPtr hBmp = CreateDIBSection(Program._Main.Handle, ref bi, 0, ref pBits, IntPtr.Zero, 0);
                IntPtr hMemDC = CreateCompatibleDC(Program._Main.Handle);
                IntPtr hOldBmp = SelectObject(hMemDC, hBmp);

                if (File.Exists(filePath))
                {
                    //BinaryWriter binaryWriter =new BinaryWriter()
                    using (FileStream fileStream = new FileStream(filePath, FileMode.Append, FileAccess.Write))
                    {
                        fileStream.Write(_data, 0, _data.Length);
                    }
                }
                else
                {
                    using (FileStream fileStream = new FileStream(filePath, FileMode.CreateNew, FileAccess.Write))
                    {
                        fileStream.Write(_data, 0, _data.Length);
                    }
                }
                //var img = _data.ToImage();
                //Program._Main.Invoke(new Action(() =>
                //{
                //    Program._Main.BackgroundImage = img;
                //}));
            }
            catch { }
        }

        #endregion

        #endregion

        #region 解码器

        public bool isDownloadCodec = false;
        private void Player_OnDownloadCodec(object sender, AxAPlayer3Lib._IPlayerEvents_OnDownloadCodecEvent e)
        {
            isDownloadCodec = true;
            SetLoadIng(true);
            this.AllowDrop = false;
            Action _action = () =>
            {
                string url = e.strCodecPath;
                string name = Path.GetFileName(url);
                if (_loading != null)
                {
                    _loading.Titite = "解码器：" + name;
                }
                string basecodeurl = url.Remove((url.Length - name.Length) - 1, name.Length + 1);

                string codes_url = "http://xmp.down.sandai.net/kankan/codecs3/" + url.Substring(Program.CodecsPath.Length).Replace("\\", "/").TrimStart('/');

                basecodeurl.CreateDirectory();

                Helper.DownCore core = new Helper.DownCore();
                double maxVal = 0;
                double downVal = 0;
                core.MaxValueChange += (s, val) =>
                {
                    maxVal = val;
                };
                core.ValueChange += (s, val) =>
                {
                    double _downVal = Math.Round(val / maxVal * 100.0, 1);
                    if (_downVal != downVal)
                    {
                        downVal = _downVal;
                        if (_loading != null)
                        {
                            _loading.Titite = string.Format("解码器：{0} {1}%", name, downVal);
                        }
                    }
                };
                string InitErr;
                if (core.DownInit(codes_url, name, out InitErr))
                {
                    string Err;
                    if (!core.DownUrl(url, Program.TempPath + name + "\\", out Err))
                    {
                        if (_loading != null)
                        {
                            _loading.Titite = name + " " + Err;
                            Thread.Sleep(2000);
                        }
                    }
                }
                else
                {
                    if (_loading != null)
                    {
                        _loading.Titite = name + " " + InitErr;
                        Thread.Sleep(2000);
                    }
                }
            };
            _task.ContinueWhenAll(new Task[] { _task.StartNew(_action) }, (action =>
            {
                isDownloadCodec = false;
                if (_loading != null)
                {
                    _loading.Titite = "即将播放";
                }
                //SetLoadIng(false);
                this.Invoke(new Action(() =>
                {
                    this.AllowDrop = true;
                    player.SetConfig(19, "1");
                }));
            }));
        }

        public bool RepairCode()
        {
            bool isXifu = false;
            if (!IsPlaying)
            {
                isXifu = true;
            }
            else
            {
                using (Popup.Dialog dialog = new Popup.Dialog("修复解码器", "在修复之前，需要关闭播放器，请确认无误后确认关闭并修复", "修复", true))
                {
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        isXifu = true;
                    }
                }
            }
            if (isXifu)
            {
                this.Close();
                new Process
                {
                    StartInfo = new ProcessStartInfo(Program.ExePath, "-r codecs")
                    { UseShellExecute = false },
                }.Start();
            }
            return isXifu;
        }

        #endregion

        #region UI操作

        #region 加载状态UI

        public Loading _loading = null;
        public void SetLoadIng(bool state)
        {
            if (state)
            {
                if (Visible)
                {
                    if (_loading == null)
                    {
                        BeginInvoke(new Action(() =>
                        {
                            _loading = new Loading(this);
                            _loading.Show(this);
                        }));
                    }
                }
            }
            else if (_loading != null)
            {
                _loading.BeginInvoke(new Action(() =>
                {
                    _loading.Close();
                }));
            }
            //try
            //{

            //}
            //catch { }
        }

        #endregion

        #region 弹出层

        public PromptByBuffer promptBuffer = null;
        public Prompt prompt = null;
        public void ShowPromptBuffer(string left, string middle = null)
        {
            this.Invoke(new Action(() =>
            {
                if (promptBuffer == null)
                {
                    promptBuffer = new PromptByBuffer(this, left, middle);
                    promptBuffer.Show(this);
                }
                else
                {
                    promptBuffer.Change(left, middle);
                }
            }));
        }
        public void ShowPrompt(string left, string middle = null, string right = null, bool autoCloes = true)
        {
            this.Invoke(new Action(() =>
            {
                if (prompt == null)
                {
                    prompt = new Prompt(this, left, middle, right, null, autoCloes);
                    prompt.Show(this);
                }
                else
                {
                    prompt.autoCloes = autoCloes;
                    prompt.Change(left, middle, right);
                }
            }));
        }
        public void ShowPromptFile(string left, string middle, string file, bool autoCloes = true)
        {
            this.Invoke(new Action(() =>
            {
                if (prompt == null)
                {
                    prompt = new Prompt(this, left, middle, null, file, autoCloes);
                    prompt.Show(this);
                }
                else
                {
                    prompt.autoCloes = autoCloes;
                    prompt.Change(left, middle, null);
                }
            }));
        }

        bool maxVolume = false;
        public void ShowCov(int p)
        {
            bool isShowMax = false;
            if (p > 99 && !maxVolume)
            {
                p = 100;
                maxVolume = isShowMax = true;
            }
            else if (p < 100 && maxVolume)
            {
                maxVolume = false;
            }
            player.SetVolume(p);
            int value = player.GetVolume();
            if (_volume != null)
            {
                _volume.Value = value;
            }

            ShowPrompt("当前音量", value.ToString(), isShowMax ? "键盘 ↑ 继续增加音量" : null);
            if (_controller != null)
            {
                _controller.Print();
            }
        }


        Prompt IPrompt.iPrompt { get => prompt; set => prompt = value; }
        public PromptByBuffer iPromptBuffer { get => promptBuffer; set => promptBuffer = value; }


        public Form iForm => this;

        #endregion

        #endregion
    }
}

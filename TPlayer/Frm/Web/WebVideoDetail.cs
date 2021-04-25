using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using TPlayerSupport;
using TSkin;

namespace TPlayer.Frm.Web
{
    public partial class WebVideoDetail : Form
    {
        string videotype;
        WebVideo webVideo; Video video;
        public WebVideoDetail(WebVideo webVideo, Video video, string videotype)
        {
            this.webVideo = webVideo;
            this.video = video;
            this.videotype = videotype;
            SetStyle(
                ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw |
                ControlStyles.Selectable | ControlStyles.DoubleBuffer, true);
            //强制分配样式重新应用到控件上
            UpdateStyles();
            InitializeComponent();
            btn_close.Image = FontAwesome.GetImage("4FA4", 30, Color.Black);
            if (video.videoImg != null)
            {
                Rectangle Rect = new Rectangle(0, 0, video.videoImg.Width, video.videoImg.Height);
                Bitmap bitmap = new Bitmap(video.videoImg);
                using (Graphics gTop = Graphics.FromImage(bitmap))
                {
                    gTop.FillRectangle(new SolidBrush(Color.FromArgb(200, 255, 255, 255)), Rect);
                }
                bitmap.GaussianBlur(ref Rect, 80, false);
                this.BackgroundImage = bitmap;
            }
            pictureBox1.Image = video.videoImg;
            label1.Text = video.videoName;
            label3.Text = video.videoQuality;
            foreach (VideoInfo item in video.videoTotalInfo)
            {
                Label label = new Label
                {
                    BackColor = Color.Transparent,
                    Text = item.key + "：" + item.value + "    ",
                    AutoSize = true
                };
                panel1.Controls.Add(label);
                label.MouseDown += Frn_Move;
            }
            foreach (PlayAddress item in video.playInformation)
            {
                menuXList1.Items.Add(new TPlayerList.TopItem
                {
                    Visible = true,
                    Enabled = true,
                    Name = item.playType
                });
            }
            menuXList1.InPaint();
            menuXList1.Invalidate();
            label4.Text = video.videoSynopsis;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            webVideo.player.DownProgChange -= Player_DownProgChange;
            webVideo.player.DownComplete -= Player_DownComplete;
            webVideo.webVideoDetail = null;
            webVideo.LocationChanged -= Player_LocationSizeChanged;
            webVideo.SizeChanged -= Player_LocationSizeChanged;
            if (SystemSettings.Animation)
            {
                AnimateWindow(this.Handle, 80, 0x00040000 | 0x00000001 | 0x10000);
                //AnimateWindow(this.Handle, 80, 0x00040000 | 0x00000002 | 0x10000);
            }
            base.OnClosing(e);
        }

        [DllImport("user32")]
        public static extern bool AnimateWindow(IntPtr whnd, int dwtime, int dwflag);
        protected override void OnLoad(EventArgs e)
        {
            Player_LocationSizeChanged(this, e);
            base.OnLoad(e);
            webVideo.LocationChanged += Player_LocationSizeChanged;
            webVideo.SizeChanged += Player_LocationSizeChanged;

            AnimateWindow(this.Handle, 80, 0x00040000 | 0x00000002 | 0x20000);
            //AnimateWindow(this.Handle, 80, 0x00040000 | 0x00000001 | 0x20000);
            bool isok = false;
            List<string> _WebVideoPlayType = SystemSettings.WebVideoPlayType;
            if (_WebVideoPlayType.Count > 0)
            {
                for (int i = 0; i < _WebVideoPlayType.Count; i++)
                {
                    TPlayerList.TopItem FtopItem = menuXList1.Items.Find(ab => ab.Name == _WebVideoPlayType[i]);
                    if (FtopItem != null)
                    {
                        isok = true;
                        menuXList1_DownClick(FtopItem);
                        break;
                    }
                }
            }
            if (!isok)
            {
                menuXList1_DownClick(menuXList1.Items[0]);
            }
            OnSizeChanged(null);
            webVideo.player.DownComplete += Player_DownComplete;
            webVideo.player.DownProgChange += Player_DownProgChange;
        }

        private void Player_LocationSizeChanged(object sender, EventArgs e)
        {
            if (webVideo.isMax)
            {
                Rectangle WorkingAreaRect = Screen.PrimaryScreen.WorkingArea;
                this.Height = webVideo.ClientRectangle.Height;
                this.Location = new Point(WorkingAreaRect.Left + webVideo.ClientRectangle.Width - this.Width, WorkingAreaRect.Top);
            }
            else
            {
                this.Height = webVideo.Height;
                this.Location = new Point(webVideo.Left + webVideo.Width - this.Width, webVideo.Top);
            }
        }
        int playIndex = -1;
        PlayAddress playAddress;
        bool isdownModel = false;
        private void menuXList1_DownClick(TPlayerList.TopItem Item)
        {
            panel7.Visible = isdownModel = false;
            btn_play.Text = "播放全部";
            btn_down.Text = "下载";
            playIndex = Item.Index;
            menuXList1.SelectItemIndex = Item.Index;
            menuXList1.Tom(Item);
            panel_videos.Visible = false;
            panel_videos.Controls.Clear();
            playAddress = video.playInformation.Find(ab => ab.playType == Item.Name);


            List<string> _WebVideoPlayType = SystemSettings.WebVideoPlayType;
            if (_WebVideoPlayType.Contains(Item.Name))
            {
                _WebVideoPlayType.Remove(Item.Name);
            }
            _WebVideoPlayType.Insert(0, Item.Name);
            SystemSettings.SetWebVideoPlayType = string.Join(";", _WebVideoPlayType);

            string _colltag = "webvideo[" + this.videotype + "]_" + playIndex + "_" + video.id;
            int coll = 0;
            if (SystemSettings.RememberLocation && File.Exists(Program.CachePath + "coll\\" + _colltag))
            {
                int.TryParse(File.ReadAllText(Program.CachePath + "coll\\" + _colltag), out coll);
            }
            if (coll > 0)
            {
                if (playAddress.videoUrl.Count - 1 < coll)
                {
                    coll = 0;
                    try
                    {
                        File.Delete(Program.CachePath + "coll\\" + _colltag);
                    }
                    catch { }
                }
                btn_play.Text = "继续 " + playAddress.videoUrl[coll].playName;
            }
            TSkin.TBut slabel = null;
            for (int i = 0; i < playAddress.videoUrl.Count; i++)
            {
                int _index = i;
                VideoUrl item = playAddress.videoUrl[i];

                TSkin.TBut but = new TSkin.TBut
                {
                    TabIndex = _index,
                    Name = video.id + "_" + menuXList1.SelectItemIndex + item.playName,
                    ASize = true,
                    Radius = 6,
                    Text = item.playName,
                    ImageSize = new Size(20,20),
                    ImageMargin = new Padding(6, 0, 0, 0),
                    TextAlign = ContentAlignment.MiddleRight,
                    TextMargin = new Padding(6, 4, 6, 4),
                    ValueColor = Color.FromArgb(140, Color.OrangeRed),
                    BorderColorActive = Color.OrangeRed,
                    BorderColor = Color.OrangeRed,

                    BackColor = Color.FromArgb(20, 0, 0, 0),
                    BackColor2 = Color.FromArgb(20, 0, 0, 0),
                    BackColorActive = Color.DodgerBlue,
                    BackColorActive2 = Color.DodgerBlue,
                    ForeColorActive = Color.White,
                    Tag = item
                };

                if (coll > 0 && _index == coll)
                {
                    slabel = but;
                    but.IsActive = true;
                    //file.Icon = Properties.Resources.icon_success;
                }
                if (File.Exists(Program.DownLoadPath + video.videoName + "\\" + item.playName + Path.GetExtension(item.playURL)))
                {
                    but.Image = Properties.Resources.icon_success;
                }
                panel_videos.Controls.Add(but);
                but.Click += (a, b) =>
                {
                    if (isdownModel)
                    {
                        if (!File.Exists(Program.DownLoadPath + video.videoName + "\\" + item.playName + Path.GetExtension(item.playURL)))
                        {
                            if (but.BorderWidth == 0)
                            {
                                but.BorderWidth = 1;
                            }
                            else
                            {
                                but.BorderWidth = 0;
                            }
                        }
                    }
                    else
                    {
                        string pathcoll = Program.CachePath + "coll\\" + _colltag;
                        (Program.CachePath + "coll\\").CreateDirectory(true);
                        File.WriteAllText(pathcoll, _index.ToString());

                        _play(_index, _colltag);
                    }
                };
            }
            panel_videos.Visible = true;
            if (slabel != null)
            {
                slabel.Focus();
            }

        }

        private void btn_play_Click(object sender, EventArgs e)
        {
            if (playAddress != null)
            {
                string _colltag = "webvideo[" + this.videotype + "]_" + playIndex + "_" + video.id;

                int coll = 0;
                if (SystemSettings.RememberLocation && File.Exists(Program.CachePath + "coll\\" + _colltag))
                {
                    int.TryParse(File.ReadAllText(Program.CachePath + "coll\\" + _colltag), out coll);
                }

                _play(coll, _colltag);
            }
        }
        void _play(int index, string colltag)
        {
            if (menuXList1.SelectItemIndex > -1)
            {
                foreach (Control item in panel_videos.Controls)
                {
                    if (item is TSkin.TBut)
                    {
                        if (item.TabIndex == index)
                        {
                            (item as TSkin.TBut).IsActive = true;
                        }
                        else
                        {
                            (item as TSkin.TBut).IsActive = false;
                        }
                    }
                }
                //iswrite = false;
                //menuXList1_DownClick(menuXList1.Items[menuXList1.SelectItemIndex]);
                //iswrite = true;
            }

            webVideo.player.PlayIndex = 0;
            List<PlayerItem> PlayList_Temp = new List<PlayerItem>();
            //if (playAddress.videoUrl.Find(ab => ab.playURL.EndsWith(".mp4") || ab.playURL.EndsWith(".m3u8")) != null)
            //{

            //}
            foreach (VideoUrl item in playAddress.videoUrl)
            {
                string path = Program.DownLoadPath + video.videoName + "\\" + item.playName + Path.GetExtension(item.playURL);
                if (File.Exists(path))
                {
                    PlayList_Temp.Add(new PlayerItem("file", path, video.videoName + " - " + item.playName, colltag));
                }
                else
                {
                    PlayList_Temp.Add(new PlayerItem("web", item.playURL, video.videoName + " - " + item.playName, colltag));
                }
            }
            webVideo.player.isTvModel = false;
            webVideo.player.OpenFileWeb(PlayList_Temp, index);
        }
        private void btn_down_Click(object sender, EventArgs e)
        {
            if (playAddress != null)
            {
                if (!isdownModel)
                {
                    panel7.Visible = true;
                    isdownModel = true;
                    btn_down.Text = "下载选中";
                    foreach (TSkin.TBut file in panel_videos.Controls)
                    {
                        VideoUrl videoUrl = file.Tag as VideoUrl;
                        if (!File.Exists(Program.DownLoadPath + video.videoName + "\\" + videoUrl.playName + Path.GetExtension(videoUrl.playURL)))
                        {
                            file.BorderWidth = 1;
                        }
                    }
                }
                else
                {
                    //bool isDownok = true;
                    //if (SystemSettings.AduDownload)
                    //{
                    //    TPlayerDownLib.VideoTask videoTask = new TPlayerDownLib.VideoTask
                    //    {
                    //        name = video.videoName,
                    //        cover = video.videoImgUrl,
                    //        intro = video.videoSynopsis,
                    //        list = new List<TPlayerDownLib.VideoTaskList>()
                    //    };

                    //    foreach (TSkin.TBut file in panel3.Controls)
                    //    {
                    //        if (file.BorderWidth == 1)
                    //        {

                    //            VideoUrl videoUrl = file.Tag as VideoUrl;

                    //            TPlayerDownLib.VideoTaskList videoTaskList = new TPlayerDownLib.VideoTaskList
                    //            {
                    //                name = videoUrl.playName,
                    //                url = videoUrl.playURL,
                    //                fileName = videoUrl.playName + Path.GetExtension(videoUrl.playURL),
                    //                tempPath = Program.DownLoadPath + video.videoName + "\\" + videoUrl.playName + "\\"
                    //            };
                    //            videoTaskList.savePath = Program.DownLoadPath + video.videoName + "\\" + videoTaskList.fileName;

                    //            videoTask.list.Add(videoTaskList);
                    //        }
                    //    }
                    //    Action _action = () =>
                    //    {
                    //        if (!TPlayerDownLib.DownLib.RunDownLoad)
                    //        {
                    //            bool isRun = false;
                    //            LL2:
                    //            if (string.IsNullOrEmpty(SystemSettings.AduDownloadPath))
                    //            {
                    //                if (File.Exists("AduDownload.exe"))
                    //                {
                    //                    Api.OpenMessage(this, MessageType.Info, "正在启动 AduDownload");
                    //                    System.Diagnostics.Process.Start("AduDownload.exe");
                    //                    isRun = true;
                    //                }
                    //                else
                    //                {
                    //                    using (HasDownLoad noAduDownLoad = new HasDownLoad("AduDownLoad 下载器未找到"))
                    //                    {
                    //                        DialogResult dialogResult = DialogResult.No;
                    //                        this.Invoke(new Action(() =>
                    //                        {
                    //                            dialogResult = noAduDownLoad.ShowDialog();
                    //                        }));
                    //                        switch (dialogResult)
                    //                        {
                    //                            case DialogResult.OK:
                    //                                goto LL2;
                    //                                break;
                    //                            case DialogResult.Retry:
                    //                                SystemSettings.AduDownload = false;
                    //                                isDownok = false;
                    //                                break;
                    //                        }
                    //                    }
                    //                }
                    //            }
                    //            else
                    //            {
                    //                Api.OpenMessage(this, MessageType.Info, "正在启动 AduDownload");
                    //                System.Diagnostics.Process.Start(SystemSettings.AduDownloadPath);
                    //                isRun = true;
                    //            }
                    //            int ErrCount = 0;
                    //            while (isRun)
                    //            {
                    //                System.Threading.Thread.Sleep(500);
                    //                if (!TPlayerDownLib.DownLib.RunDownLoad)
                    //                {
                    //                    ErrCount++;
                    //                    if (ErrCount > 10)
                    //                    {
                    //                        isRun = false; break;
                    //                    }
                    //                }
                    //                else
                    //                {
                    //                    isRun = false;
                    //                }
                    //            }
                    //        }
                    //        if (TPlayerDownLib.DownLib.SendDownLoad(videoTask))
                    //        {
                    //            Api.OpenMessage(webVideo, MessageType.Success, "下载任务已发送成功");
                    //        }
                    //        else
                    //        {
                    //            using (HasDownLoad noAduDownLoad = new HasDownLoad("AduDownLoad 下载器无法使用"))
                    //            {
                    //                DialogResult dialogResult = DialogResult.No;
                    //                this.Invoke(new Action(() =>
                    //                {
                    //                    dialogResult = noAduDownLoad.ShowDialog();
                    //                }));
                    //                switch (dialogResult)
                    //                {
                    //                    case DialogResult.OK:
                    //                        isDownok = false;
                    //                        break;
                    //                    case DialogResult.Retry:
                    //                        SystemSettings.AduDownload = false;
                    //                        isDownok = false;
                    //                        break;
                    //                }
                    //            }

                    //        }

                    //    };
                    //    _task.ContinueWhenAll(new Task[] { _task.StartNew(_action) }, (action =>
                    //    {
                    //    }));
                    //}
                    //else
                    //{
                    List<DownItem> downItems = new List<DownItem>();
                    foreach (TBut file in panel_videos.Controls)
                    {
                        if (file.BorderWidth == 1)
                        {
                            VideoUrl videoUrl = file.Tag as VideoUrl;

                            DownItem downItem = new DownItem
                            {
                                url = videoUrl.playURL,
                                imgUrl = video.videoImgUrl,
                                img = video.videoImg,
                                ID = video.id + "_" + menuXList1.SelectItemIndex + videoUrl.playName,
                                basepath = Program.DownLoadPath + video.videoName + "\\" + videoUrl.playName + "\\",
                                name = video.videoName + " - " + videoUrl.playName,
                                fileName = videoUrl.playName + Path.GetExtension(videoUrl.playURL),
                            };
                            downItem.savepath = Program.DownLoadPath + video.videoName + "\\" + downItem.fileName;
                            downItems.Add(downItem);
                        }
                    }

                    if (downItems.Count > 0)
                    {
                        bool isok = false;
                        Action _action = () =>
                        {
                            isok = webVideo.player.AddDownList(downItems);
                        };
                        webVideo.player._task.ContinueWhenAll(new Task[] { webVideo.player._task.StartNew(_action) }, (action =>
                        {
                            this.BeginInvoke(new Action(() =>
                            {
                                panel7.Visible = isdownModel = false;
                                btn_down.Text = "下载";
                            }));
                        }));
                    }
                    else
                    {
                        foreach (TSkin.TBut file in panel_videos.Controls)
                        {
                            if (file.BorderWidth == 1)
                            {
                                file.BorderWidth = 0;
                            }
                        }
                        panel7.Visible = isdownModel = false;
                        btn_down.Text = "下载";
                    }
                }

            }
        }

        TaskFactory _task = new TaskFactory();
        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Frn_Move(object sender, MouseEventArgs e)
        {
            webVideo.Frm_MaxMove(sender, e);
        }

        private void btn_sel_all_Click(object sender, EventArgs e)
        {
            int selcount = 0;
            foreach (TSkin.TBut file in panel_videos.Controls)
            {
                if (file.BorderWidth == 1)
                {
                    selcount++;
                }
            }
            if (selcount == panel_videos.Controls.Count)
            {
                foreach (TSkin.TBut file in panel_videos.Controls)
                {
                    if (file.BorderWidth == 1)
                    {
                        file.BorderWidth = 0;
                    }
                }
            }
            else
            {
                foreach (TSkin.TBut file in panel_videos.Controls)
                {
                    if (file.BorderWidth == 0)
                    {
                        VideoUrl videoUrl = file.Tag as VideoUrl;

                        if (!File.Exists(Program.DownLoadPath + video.videoName + "\\" + videoUrl.playName + Path.GetExtension(videoUrl.playURL)))
                        {
                            file.BorderWidth = 1;
                        }
                    }
                }
            }
        }

        private void btn_sel_no_Click(object sender, EventArgs e)
        {
            foreach (TSkin.TBut file in panel_videos.Controls)
            {
                VideoUrl videoUrl = file.Tag as VideoUrl;
                if (!File.Exists(Program.DownLoadPath + video.videoName + "\\" + videoUrl.playName + Path.GetExtension(videoUrl.playURL)))
                {
                    if (file.BorderWidth == 1)
                    {
                        file.BorderWidth = 0;
                    }
                    else
                    {
                        file.BorderWidth = 1;
                    }
                }
            }
        }

        private void Player_DownProgChange(DownItem Item, double val, double maxval)
        {
            if (menuXList1.SelectItemIndex > -1)
            {
                Control[] Controls = panel_videos.Controls.Find(Item.ID, false);
                if (Controls != null && Controls.Length > 0)
                {
                    TSkin.TBut but = Controls[0] as TSkin.TBut;
                    but.MaxValue = maxval;
                    but.Value = val;
                }
            }
        }

        private void Player_DownComplete(DownItem Item)
        {
            if (menuXList1.SelectItemIndex > -1)
            {
                Control[] Controls = panel_videos.Controls.Find(Item.ID, false);
                if (Controls != null && Controls.Length > 0)
                {
                    TSkin.TBut but = Controls[0] as TSkin.TBut;
                    but.MaxValue = but.Value = 0;
                    but.Image = Properties.Resources.icon_success;
                }
            }
        }
    }
}

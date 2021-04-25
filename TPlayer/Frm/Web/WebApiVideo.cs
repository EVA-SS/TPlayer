using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using TPlayer.Frm.Web;
using TPlayerSupport;

namespace TPlayer.Frm
{
    public partial class WebApiVideo : NetDimension.WinForm.ModernUIForm
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
                WindowState = FormWindowState.Normal;
            }
            else
            {
                btn_max.Image = FontAwesome.GetImage("4FB1", 30, Color.Black);
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
        Font fontlogo = FontAwesome.GetFont(38);
        public WebApiVideo(TPlayer player)
        {
            this.player = player;
            InitializeComponent();

            pictureBox2.Image = FontAwesome.GetImage(fontlogo, "4FBB", 38, Color.Black);
            btn_min.Image = FontAwesome.GetImage("4FA9", 30, Color.Black);
            btn_max.Image = FontAwesome.GetImage("4FB2", 30, Color.Black);
            btn_close.Image = FontAwesome.GetImage("4FA3", 30, Color.Black);
            btn_close.ImageHove = FontAwesome.GetImage("4FA3", 30, Color.White);
            webVideoList1.chatVScroll.ScrollChange += (value) =>
            {
                //System.Diagnostics.Debug.WriteLine("当前：" + value);
                if (isdownok && value > 0.88)
                {
                    thisPage++;
                    int page = thisPage;
                    Action _action = () =>
                    {
                        if (Regex.IsMatch(thisURL, @"pg=([\d])"))
                        {
                            string result = Regex.Replace(thisURL, @"pg=([\d]*)", "pg=" + page);
                            if (!LoadingCover(result, false))
                            {
                                isdownok = false;
                            }
                        }
                    };
                    _task.ContinueWhenAll(new Task[] { _task.StartNew(_action) }, (action =>
                    {
                        webVideoList1.Invoke(new Action(() =>
                        {
                            webVideoList1.Invalidate();
                        }));
                    }));
                }
            };

            webVideoList1.TopChange += (bmp) =>
            {
                panel1.BackgroundImage = bmp;
            };
            //菜单 4FA1
        }

        public List<TitleName> titleNames;
        ConfigDAL config = new ConfigDAL();

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            string key = "OK_ApiTitle";
            if (config.Exists(key))
            {
                titleNames = JsonConvert.DeserializeObject<List<TitleName>>(config.GetPicCacheData(key));
            }
            else
            {
                var jsonData = OK_DataAPI.GetData("https://api.okzy.tv/api.php/provide/vod/at/json/?ac=list&t=1");
                titleNames = OK_DataAPI.GetVideoType(jsonData);
                config.AddPicCacheData(key, JsonConvert.SerializeObject(titleNames));
            }
            foreach (TitleName item in titleNames)
            {
                menuXList1.Items.Add(new TPlayerList.TopItem
                {
                    Enabled = true,
                    Visible = true,
                    Tag = item.titleId,
                    Name = item.titleType,
                });
            }
            menuXList1.InPaint();
            menuXList1.Invalidate();
        }
        bool isdownok = true;
        private void menuXList1_DownClick(TPlayerList.TopItem Item)
        {
            thisPage = 1;
            isdownok = true;
            menuXList1.SelectItemIndex = Item.Index;
            menuXList1.Tom(Item);
            webVideoList1.Items.Clear();
            webVideoList1.chatVScroll.MoveSliderToLocation(0);
            string typeUrl = $"?ac=detail&t={Item.Tag}&pg={thisPage}";

            timer1.Enabled = true;
            Action _action = () =>
            {
                LoadingCover(typeUrl);
            };
            _task.ContinueWhenAll(new Task[] { _task.StartNew(_action) }, (action =>
            {
                this.Invoke(new Action(() =>
                {
                    webVideoList1.chatVScroll.OnScrollChange(100);
                    timer1.Enabled = false;
                    if (isgo != -1)
                    {
                        isgo = -1;
                        //isgo = true;
                        pictureBox2.Image = FontAwesome.GetImage(fontlogo, "4FBB", 38, Color.Black);
                    }
                }));
            }));
        }

        private int thisPage = 1;
        private string thisURL = "";

        #region 加载封面

        private PicCacheDataDAL picCacheDataDAL = new PicCacheDataDAL();
        /// <summary>
        /// 图片加载线程
        /// </summary>
        private void SaveThumbnail(int start, int end)
        {
            Action _action = () =>
            {
                for (int i = start; i < end; i++)
                {
                    TPlayerList.WebVideoListItem itemsv = webVideoList1.Items[i];
                    if (itemsv.Img == null)
                    {
                        Video video = itemsv.Tag as Video;

                        HttpLib.WebResult webResult;
                        byte[] data = HttpLib.Http.Get(video.videoImgUrl).redirect(true).requestData(out webResult);

                        if (webResult != null)
                        {
                            itemsv.Img = data.ToImage();
                            PicCacheData picdata = new PicCacheData();//保存封面缩略图
                            picdata.pictureID = video.id;
                            picdata.pictureImage = data;
                            picdata.picType = "OK_API";
                            picCacheDataDAL.InsertPicUrl(picdata);
                        }

                    }
                }
            };
            _task.ContinueWhenAll(new Task[] { _task.StartNew(_action) }, (action =>
            {
                webVideoList1.InPaint();
                this.Invoke(new Action(() =>
                {
                    webVideoList1.Invalidate();
                }));
            }));
        }
        TaskFactory _task = new TaskFactory();

        private bool LoadingCover(string homeUrl, bool isshow = true)
        {
            try
            {
                TitleName CurrentVideos;
                var jsonData = OK_DataAPI.GetData(homeUrl);
                if (jsonData != null)
                {
                    CurrentVideos = OK_DataAPI.GetVideoList(jsonData);
                }
                else
                {
                    CurrentVideos = new TitleName { titleVideos = new List<Video>() };
                }

                if (CurrentVideos == null || CurrentVideos.titleVideos == null || CurrentVideos.titleVideos.Count == 0)
                {
                    if (isshow)
                        Api.OpenMessage(this, MessageType.Warn, "没有获取到数据,请重试！");
                    return false;
                }
                thisURL = homeUrl;
                int index = webVideoList1.Items.Count;
                lock (webVideoList1.Items)
                {
                    //Label_test.Text = CurrentVideos.pageCount + "页";
                    for (int i = 0; i < CurrentVideos.titleVideos.Count; i++)
                    {
                        Video video = CurrentVideos.titleVideos[i];
                        TPlayerList.WebVideoListItem webVideoListItem = new TPlayerList.WebVideoListItem
                        {
                            Name = video.videoName,
                            Tag = video,
                            //Img= video.videoImgUrl
                        };

                        if (picCacheDataDAL.ExistsPic(video.id, "OK_API"))
                        {
                            using (DataTable dataTable = picCacheDataDAL.GetPicUrl(video.id.ToString(), "OK_API"))
                            {
                                foreach (DataRow item in dataTable.Rows)
                                {
                                    webVideoListItem.Img = ((byte[])item["pictureImage"]).ToImage();
                                    continue;
                                }
                            }
                        }

                        webVideoList1.Items.Add(webVideoListItem);
                    }
                }
                webVideoList1.InPaint();
                SaveThumbnail(index, index + CurrentVideos.titleVideos.Count);
                return true;
            }
            //catch (AggregateException ex1)
            //{
            //    if (isshow)
            //        MessageBox.Show(ex1.ToString());
            //    return false;
            //}
            catch (Exception ex)
            {
                if (isshow)
                    Api.OpenMessage(this, MessageType.Error, ex.Message);
                return false;
            }
        }


        #endregion

        private void text_search_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                e.Handled = true;
                string str = text_search.Text.Trim();
                if (!string.IsNullOrEmpty(str))
                {
                    isdownok = true;
                    webVideoList1.Items.Clear();
                    webVideoList1.chatVScroll.MoveSliderToLocation(0);

                    timer1.Enabled = true;
                    string strUrl = "?ac=detail&wd=" + Uri.EscapeUriString(str) + "&pg=1";

                    Action _action = () =>
                    {
                        if (LoadingCover(strUrl))
                        {
                            thisPage = 1;
                            //txt_PageIndex.Text = thisPage.ToString();
                        }
                    };
                    _task.ContinueWhenAll(new Task[] { _task.StartNew(_action) }, (action =>
                    {
                        webVideoList1.Invoke(new Action(() =>
                        {
                            webVideoList1.Invalidate();
                        }));
                        this.Invoke(new Action(() =>
                        {
                            timer1.Enabled = false;
                            if (isgo != -1)
                            {
                                isgo = -1;
                                pictureBox2.Image = FontAwesome.GetImage(fontlogo, "4FBB", 38, Color.Black);
                            }
                        }));
                    }));
                }
            }
        }
        int isgo = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            isgo++;
            Color color = Color.Black;
            switch (isgo)
            {
                case 0:
                    color = Color.FromArgb(255, 63, 63);//红
                    break;
                case 1:
                    color = Color.FromArgb(255, 189, 63);//橙
                    break;
                case 2:
                    color = Color.FromArgb(255, 248, 63);//黄
                    break;
                case 3:
                    color = Color.FromArgb(96, 255, 63);//绿
                    break;
                case 4:
                    color = Color.FromArgb(63, 161, 255);//青
                    break;
                case 5:
                    color = Color.FromArgb(51, 153, 204);//蓝
                    break;
                case 6:
                    color = Color.FromArgb(204, 204, 255);//紫
                    break;
                default:
                    isgo = -1;
                    break;
            }
            pictureBox2.Image = FontAwesome.GetImage(fontlogo, "4FBB", 38, color);
        }

        private void webVideoList1_DownClick(TPlayerList.WebVideoListItem Item)
        {
            Video video = Item.Tag as Video;
            if (video.playInformation != null)
            {
                int selplayType = -1;
                if (video.playInformation.Count > 1)
                {
                    List<string> seldata = new List<string>();
                    foreach (PlayAddress item in video.playInformation)
                    {
                        seldata.Add(item.playType);
                    }
                    UIListSel frm = new UIListSel(seldata);
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        selplayType = frm.SelIndex;
                    }
                }
                else
                {
                    selplayType = 0;
                }
                if (selplayType > -1)
                {
                    string _colltag = "webvideo[okzy]_" + selplayType + "_" + video.id;
                    List<PlayerItem> PlayList_Temp = new List<PlayerItem>();
                    if (video.playInformation[selplayType].videoUrl.Find(ab => ab.playURL.EndsWith(".mp4")) != null || video.playInformation[selplayType].videoUrl.Find(ab => ab.playURL.EndsWith(".m3u8")) != null)
                    {
                        foreach (VideoUrl item in video.playInformation[selplayType].videoUrl)
                        {
                            PlayList_Temp.Add(new PlayerItem("web", item.playURL, video.videoName + " - " + item.playName, _colltag));
                        }
                    }
                    else
                    {
                        foreach (VideoUrl item in video.playInformation[selplayType].videoUrl)
                        {
                            PlayList_Temp.Add(new PlayerItem("web_net1", item.playURL, video.videoName + " - " + item.playName, _colltag));
                        }
                    }
                    player.isTvModel = false;
                    int coll = 0;
                    if (SystemSettings.RememberLocation && File.Exists(Program.CachePath + "coll\\" + PlayList_Temp[0].colltag))
                    {
                        int.TryParse(File.ReadAllText(Program.CachePath + "coll\\" + PlayList_Temp[0].colltag), out coll);
                    }
                    player.OpenFileWeb(PlayList_Temp, coll);
                }

            }
            else if (video.isRun)
            {
                Api.OpenMessage(this, MessageType.Info, "请稍后，正在获取地址");
            }
        }
    }
}

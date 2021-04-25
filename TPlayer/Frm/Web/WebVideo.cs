using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using TPlayer.Frm.Web;
using TPlayerSupport;

namespace TPlayer.Frm
{
    public partial class WebVideo : NetDimension.WinForm.ModernUIForm
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
                isMax = false;
                btn_max.Image = FontAwesome.GetImage("4FB2", 30, Color.Black);
                WindowState = FormWindowState.Normal;
            }
            else
            {
                isMax = true;
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

        public TPlayer player;
        Font fontlogo = FontAwesome.GetFont(38);
        public WebVideo(TPlayer player)
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
                if (isdownok && !isrun && value > 0.88)
                {
                    isrun = true;
                    thisPage++;
                    int page = thisPage;
                    Action _action = () =>
                    {
                        if (Regex.IsMatch(thisURL, @"pg-([\d])"))
                        {
                            string result = Regex.Replace(thisURL, @"pg-([\d]*)", "pg-" + thisPage);
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
                        isrun = false;
                    }));
                }
            };

            webVideoList1.TopChange += (bmp) =>
            {
                panel1.BackgroundImage = bmp;
            };
            //菜单 4FA1
        }

        ConfigDAL config = new ConfigDAL();

        OK_DataHtml dataClass;
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            switch (SystemSettings.WebVideoSourceType)
            {
                case "OK资源网":
                    oK资源网ToolStripMenuItem.Checked = true;
                    dataClass = new OK_DataHtml(DataType.OK资源网);
                    break;
                case "速播资源站":
                    速播资源站ToolStripMenuItem.Checked = true;
                    dataClass = new OK_DataHtml(DataType.速播资源站);
                    break;
                case "麻花资源":
                    麻花资源ToolStripMenuItem.Checked = true;
                    dataClass = new OK_DataHtml(DataType.麻花资源);
                    break;
                case "最新资源网":
                    最新资源网ToolStripMenuItem.Checked = true;
                    dataClass = new OK_DataHtml(DataType.最新资源网);
                    break;
                case "123资源网":
                    _123资源网ToolStripMenuItem.Checked = true;
                    dataClass = new OK_DataHtml(DataType._123资源网);
                    break;
                case "超快资源网":
                    超快资源网ToolStripMenuItem.Checked = true;
                    dataClass = new OK_DataHtml(DataType.超快资源网);
                    break;
                case "卧龙资源网":
                    卧龙资源网ToolStripMenuItem.Checked = true;
                    dataClass = new OK_DataHtml(DataType.卧龙资源网);
                    break;
                case "高清资源网":
                    高清资源网ToolStripMenuItem.Checked = true;
                    dataClass = new OK_DataHtml(DataType.高清资源网);
                    break;
                default:
                    最大资源网ToolStripMenuItem.Checked = true;
                    dataClass = new OK_DataHtml(DataType.最大资源网);
                    break;
            }
            _LoadType();
        }

        void _LoadType()
        {
            timer1.Enabled = true;
            string key = dataClass.baseType + "Title";
            menuXList1.Items.Clear();
            Action _action = () =>
            {
                List<TitleName> titleNames;
                if (config.Exists(key))
                {
                    titleNames = JsonConvert.DeserializeObject<List<TitleName>>(config.GetPicCacheData(key));
                }
                else
                {
                    titleNames = dataClass.GetVideoType();
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
            };
            _task.ContinueWhenAll(new Task[] { _task.StartNew(_action) }, (action =>
            {
                this.Invoke(new Action(() =>
                {
                    menuXList1.InPaint();
                    menuXList1.Invalidate();
                    if (menuXList1.Items.Count > 0)
                    {
                        menuXList1_DownClick(menuXList1.Items[0]);
                    }
                    else
                    {
                        timer1.Enabled = false;
                        if (isgo != -1)
                        {
                            isgo = -1;
                            //isgo = true;
                            pictureBox2.Image = FontAwesome.GetImage(fontlogo, "4FBB", 38, Color.Black);
                        }
                    }
                }));
            }));
        }

        bool isdownok = true;
        bool isrun = false;
        private void menuXList1_DownClick(TPlayerList.TopItem Item)
        {
            thisPage = 1;
            isdownok = true;
            menuXList1.SelectItemIndex = Item.Index;
            menuXList1.Tom(Item);
            webVideoList1.Items.Clear();
            webVideoList1.chatVScroll.MoveSliderToLocation(0);
            string typeUrl;

            typeUrl = "?m=vod-index-pg-1.html";
            if (!string.IsNullOrWhiteSpace(Item.Tag.ToString()))
            {
                typeUrl = $"?m=vod-type-id-{Item.Tag}-pg-" + thisPage + ".html";
            }

            isrun = timer1.Enabled = true;
            Action _action = () =>
            {
                LoadingCover(typeUrl);
            };
            _task.ContinueWhenAll(new Task[] { _task.StartNew(_action) }, (action =>
            {
                isrun = false;
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
        public TitleName CurrentVideos = new TitleName();
        private int thisPage = 1;
        private string thisURL = "";

        #region 加载封面

        private PicCacheDataDAL picCacheDataDAL = new PicCacheDataDAL();
        /// <summary>
        /// 图片加载线程
        /// </summary>
        private void SaveThumbnail(string type, int start, int end)
        {
            Action _action = () =>
            {
                for (int i = start; i < end; i++)
                {
                    TPlayerList.WebVideoListItem itemsv = webVideoList1.Items[i];
                    if (itemsv.Img == null)
                    {
                        Video video = itemsv.Tag as Video;
                        Video vs = dataClass.GetVideoInfo(video.id);
                        int errCount = 0;
                        while (vs == null)
                        {
                            System.Threading.Thread.Sleep(500);
                            vs = dataClass.GetVideoInfo(video.id);
                            errCount++;
                            if (errCount > 5)
                            {
                                break;
                            }
                        }

                        if (vs != null)
                        {
                            video.videoName = vs.videoName;
                            video.videoImgUrl = vs.videoImgUrl;
                            video.videoType = vs.videoType;
                            video.videoQuality = vs.videoQuality;
                            video.videoTotalInfo = vs.videoTotalInfo;
                            video.videoSynopsis = vs.videoSynopsis;

                            video.playInformation = vs.playInformation;

                            HttpLib.WebResult webResult;
                            byte[] data = HttpLib.Http.Get(vs.videoImgUrl).redirect(true).requestData(out webResult);
                            if (webResult != null)
                            {
                                video.videoImg = itemsv.Img = data.ToImage();
                                PicCacheData picdata = new PicCacheData
                                {
                                    pictureID = video.id,
                                    pictureImage = data,
                                    picType = type,
                                };//保存封面缩略图
                                picCacheDataDAL.InsertPicUrl(picdata);
                            }
                        }
                        video.isRun = false;
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
            string type = dataClass.baseType;
            try
            {

                CurrentVideos = dataClass.GetVideoList(homeUrl);//主页


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

                        if (picCacheDataDAL.ExistsPic(video.id, type))
                        {
                            using (DataTable dataTable = picCacheDataDAL.GetPicUrl(video.id.ToString(), type))
                            {
                                foreach (DataRow item in dataTable.Rows)
                                {
                                    var videoImg = (byte[])item["pictureImage"];

                                    //int videoId = Convert.ToInt32(item["pictureID"]);
                                    video.videoImg = webVideoListItem.Img = videoImg.ToImage();
                                    continue;
                                }
                            }
                        }
                        else { video.isRun = true; }

                        webVideoList1.Items.Add(webVideoListItem);
                    }
                }
                webVideoList1.InPaint();
                SaveThumbnail(type, index, index + CurrentVideos.titleVideos.Count);
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

                    isrun = timer1.Enabled = true;
                    string strUrl = "index.php?m=vod-search-pg-1-wd-" + Uri.EscapeUriString(str) + ".html";

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
                        isrun = false;
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
        public WebVideoDetail webVideoDetail = null;
        private void webVideoList1_DownClick(TPlayerList.WebVideoListItem Item)
        {
            Video video = Item.Tag as Video;
            if (video.playInformation != null)
            {
                if (timer1.Enabled)
                {
                    timer1.Enabled = false;
                    if (isgo != -1)
                    {
                        isgo = -1;
                        //isgo = true;
                        pictureBox2.Image = FontAwesome.GetImage(fontlogo, "4FBB", 38, Color.Black);
                    }
                }
                if (webVideoDetail != null)
                {
                    webVideoDetail.Close();
                }
                webVideoDetail = new WebVideoDetail(this, video, dataClass.baseType);
                webVideoDetail.Show(this);
            }
            else if (video.isRun)
            {
                Api.OpenMessage(this, MessageType.Info, "请稍后，正在获取地址");
            }
            else
            {
                timer1.Enabled = true;
                video.isRun = true;
                bool isOk = false;
                Action _action = () =>
                {
                    Video vs = dataClass.GetVideoInfo(video.id);
                    int errCount = 0;
                    while (vs == null)
                    {
                        System.Threading.Thread.Sleep(500);
                        vs = dataClass.GetVideoInfo(video.id);
                        errCount++;
                        if (errCount > 5)
                        {
                            break;
                        }
                    }

                    if (vs != null)
                    {
                        isOk = true;
                        video.videoName = vs.videoName;
                        video.videoImgUrl = vs.videoImgUrl;
                        video.videoType = vs.videoType;
                        video.videoQuality = vs.videoQuality;
                        video.videoTotalInfo = vs.videoTotalInfo;
                        video.videoSynopsis = vs.videoSynopsis;
                        video.playInformation = vs.playInformation;
                        if (Item.Img == null)
                        {
                            HttpLib.WebResult webResult;
                            byte[] data = HttpLib.Http.Get(vs.videoImgUrl).redirect(true).requestData(out webResult);

                            if (webResult != null)
                            {
                                video.videoImg = Item.Img = data.ToImage();
                                PicCacheData picdata = new PicCacheData
                                {
                                    pictureID = video.id,
                                    pictureImage = data,
                                    picType = dataClass.baseType,
                                };//保存封面缩略图
                                picCacheDataDAL.InsertPicUrl(picdata);
                            }
                        }
                    }

                };
                _task.ContinueWhenAll(new Task[] { _task.StartNew(_action) }, (action =>
                {
                    if (isOk)
                    {
                        this.BeginInvoke(new Action(() =>
                        {
                            webVideoList1_DownClick(Item);
                        }));
                    }
                    else
                    {
                        Api.OpenMessage(this, MessageType.Warn, "地址获取失败！");
                    }
                    video.isRun = false;
                }));
            }
        }

        private void MenuClick(object sender, MouseEventArgs e)
        {
            this.tMenuStrip1.Show(pictureBox2, new Point(0, pictureBox2.Height), ToolStripDropDownDirection.Right);
        }

        private void ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            最大资源网ToolStripMenuItem.Checked = oK资源网ToolStripMenuItem.Checked = 速播资源站ToolStripMenuItem.Checked = 麻花资源ToolStripMenuItem.Checked = 最新资源网ToolStripMenuItem.Checked = _123资源网ToolStripMenuItem.Checked = 超快资源网ToolStripMenuItem.Checked = 卧龙资源网ToolStripMenuItem.Checked = 高清资源网ToolStripMenuItem.Checked = false;

            ToolStripMenuItem toolStripMenuItem = sender as ToolStripMenuItem;
            SystemSettings.WebVideoSourceType = toolStripMenuItem.Text;

            toolStripMenuItem.Checked = true;

            switch (toolStripMenuItem.Text)
            {
                case "最大资源网":
                    dataClass = new OK_DataHtml(DataType.最大资源网);
                    break;
                case "OK资源网":
                    dataClass = new OK_DataHtml(DataType.OK资源网);
                    break;
                case "速播资源站":
                    dataClass = new OK_DataHtml(DataType.速播资源站);
                    break;
                case "麻花资源":
                    dataClass = new OK_DataHtml(DataType.麻花资源);
                    break;
                case "最新资源网":
                    dataClass = new OK_DataHtml(DataType.最新资源网);
                    break;
                case "123资源网":
                    dataClass = new OK_DataHtml(DataType._123资源网);
                    break;
                case "超快资源网":
                    dataClass = new OK_DataHtml(DataType.超快资源网);
                    break;
                case "卧龙资源网":
                    dataClass = new OK_DataHtml(DataType.卧龙资源网);
                    break;
                case "高清资源网":
                    dataClass = new OK_DataHtml(DataType.高清资源网);
                    break;
            }

            _LoadType();
        }
    }

    public class TvData
    {
        public string category { get; set; }
        public string logo { get; set; }
        public string name { get; set; }
        public string url { get; set; }
        public TvData_country country { get; set; }
        public List<TvData_country> language { get; set; }
        public TvData_tvg tvg { get; set; }

    }
    public class TvData_country
    {
        public string code { get; set; }
        public string name { get; set; }
    }

    public class TvData_tvg
    {
        public string name { get; set; }
        public string url { get; set; }
    }

}

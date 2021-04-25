using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TPlayerSupport;

namespace TPlayer.Frm
{
    public partial class WebZimu : NetDimension.WinForm.ModernUIForm
    {
        TPlayer player;
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
        public WebZimu(TPlayer player, string txt)
        {
            this.player = player;
            InitializeComponent();
            btn_close.Image = FontAwesome.GetImage("4FA3", 30, Color.Black);
            btn_close.ImageHove = FontAwesome.GetImage("4FA3", 30, Color.White);
            text_search.Text = txt;

            frmSearch1.CueBannerText = text_search.CueBannerText;
        }
        protected override void OnClosing(CancelEventArgs e)
        {
            player.webZimu = null;
            player.Activate();
            base.OnClosing(e);
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!string.IsNullOrEmpty(text_search.Text))
            {
                _Load(text_search.Text);
            }
        }
        TaskFactory _task = new TaskFactory();
        Image Image = FontAwesome.GetImage("51BC", 40, Color.Black);
        public void _Load(string name)
        {
            flowLayoutPanel1.Controls.Clear();
            List<PluginHelper.SubtitleWebList> zimuDATA = null;
            Action _action = () =>
            {
                try
                {
                    zimuDATA = PluginApi.SubtitleWebSearch(name);
                }
                catch (Exception ez) { Api.OpenMessage(this, MessageType.Error, ez.Message); }
            };
            _task.ContinueWhenAll(new Task[] { _task.StartNew(_action) }, (action =>
            {
                int width = flowLayoutPanel1.Width - 30;
                int width2 = width - 108 - 12;
                if (zimuDATA != null && zimuDATA.Count > 0)
                {
                    this.Invoke(new Action(() =>
                    {
                        foreach (var zimu1 in zimuDATA)
                        {
                            int he = zimu1.data.Count * 40;
                            Panel panel4 = new Panel
                            {
                                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                                Location = new Point(112, 66),
                                Size = new Size(width - 130, he)
                            };

                            foreach (var zimu in zimu1.data)
                            {
                                PictureBox pictureBox3 = new PictureBox
                                {
                                    InitialImage = Image,
                                    Dock = DockStyle.Left,
                                    ImageLocation = zimu.imgsrc,
                                    Size = new Size(28, 40),
                                    SizeMode = PictureBoxSizeMode.Zoom
                                };
                                Label label4 = new Label
                                {
                                    AutoEllipsis = true,
                                    Dock = DockStyle.Fill,
                                    Font = new Font("微软雅黑", 9F, FontStyle.Bold),
                                    Location = new Point(28, 0),
                                    Size = new Size(336, 40),
                                    Text = zimu.title,
                                    TextAlign = ContentAlignment.MiddleLeft,
                                };
                                Label label5 = new Label
                                {
                                    AutoSize = true,
                                    Dock = DockStyle.Right,
                                    Font = new Font("微软雅黑", 8F),
                                    Text = "下载：" + zimu.down,
                                    TextAlign = ContentAlignment.MiddleCenter,
                                };
                                Label label6 = new Label
                                {
                                    AutoEllipsis = true,
                                    Dock = DockStyle.Right,
                                    Font = new Font("微软雅黑", 9F, FontStyle.Bold),
                                    Size = new Size(40, 40),
                                    Text = zimu.type + "分",
                                    TextAlign = ContentAlignment.MiddleCenter,
                                };
                                if (zimu.type == 0)
                                {
                                    label6.ForeColor = Color.DarkGray;
                                }
                                else if (zimu.type > 8)
                                {
                                    label6.ForeColor = Color.Lime;
                                }
                                else if (zimu.type > 4)
                                {
                                    label6.ForeColor = Color.Orange;
                                }
                                ProgressBar progressBar = new ProgressBar
                                {
                                    Size = new Size(0, 2),
                                    Visible = false,
                                    Dock = DockStyle.Bottom
                                };

                                Panel panel5 = new Panel
                                {
                                    Dock = DockStyle.Top,
                                    Location = new Point(0, 0),
                                    Size = new Size(0, 40),
                                };
                                panel5.Controls.Add(progressBar);
                                panel5.Controls.Add(label4);
                                panel5.Controls.Add(label6);
                                panel5.Controls.Add(label5);
                                panel5.Controls.Add(pictureBox3);

                                this.metroToolTip1.SetToolTip(pictureBox3, zimu.imgalt);
                                panel4.Controls.Add(panel5);

                                #region 效果

                                pictureBox3.MouseEnter += (a, b) =>
                                    {
                                        panel5.BackColor = Color.White;
                                    };
                                panel5.MouseEnter += (a, b) =>
                                {
                                    panel5.BackColor = Color.White;
                                };
                                label4.MouseEnter += (a, b) =>
                                {
                                    panel5.BackColor = Color.White;
                                };
                                label5.MouseEnter += (a, b) =>
                                {
                                    panel5.BackColor = Color.White;
                                };
                                label6.MouseEnter += (a, b) =>
                                {
                                    panel5.BackColor = Color.White;
                                };


                                pictureBox3.MouseLeave += (a, b) =>
                                {
                                    panel5.BackColor = panel4.BackColor;
                                };
                                panel5.MouseLeave += (a, b) =>
                                {
                                    panel5.BackColor = panel4.BackColor;
                                };
                                label4.MouseLeave += (a, b) =>
                                {
                                    panel5.BackColor = panel4.BackColor;
                                };
                                label5.MouseLeave += (a, b) =>
                                {
                                    panel5.BackColor = panel4.BackColor;
                                };
                                label6.MouseLeave += (a, b) =>
                                {
                                    panel5.BackColor = panel4.BackColor;
                                };


                                string downPath = zimu1.title + "_" + zimu.url.Md5_16();
                                pictureBox3.Click += (a, b) =>
                                {
                                    DownZimu(downPath, panel5, progressBar, zimu.url);
                                };
                                panel5.Click += (a, b) =>
                                {
                                    DownZimu(downPath, panel5, progressBar, zimu.url);
                                };
                                label4.Click += (a, b) =>
                                {
                                    DownZimu(downPath, panel5, progressBar, zimu.url);
                                };
                                label5.Click += (a, b) =>
                                {
                                    DownZimu(downPath, panel5, progressBar, zimu.url);
                                };
                                label6.Click += (a, b) =>
                                {
                                    DownZimu(downPath, panel5, progressBar, zimu.url);
                                };

                                #endregion
                            }

                            PictureBox pictureBox1 = new PictureBox
                            {
                                InitialImage = Image,
                                ImageLocation = zimu1.img,
                                Location = new Point(12, 12),
                                Size = new Size(90, 126),
                                SizeMode = PictureBoxSizeMode.Zoom
                            };
                            Label label2 = new Label
                            {
                                AutoEllipsis = true,
                                Font = new Font("微软雅黑", 10F, FontStyle.Bold),
                                Location = new Point(108, 12),
                                Size = new Size(width2, 20),
                                Text = zimu1.title,
                            };
                            Label label3 = new Label
                            {
                                AutoEllipsis = true,
                                Location = new Point(108, 42),
                                Size = new Size(width2, 18),
                                Text = "又名：" + zimu1.intro,
                                TextAlign = ContentAlignment.MiddleLeft,
                            };
                            Panel panel2 = new Panel
                            {
                                Size = new Size(width, 100 + he),
                            };
                            panel2.Controls.Add(panel4);
                            if (!string.IsNullOrEmpty(zimu1.intro))
                            {
                                panel2.Controls.Add(label3);
                            }
                            panel2.Controls.Add(label2);
                            panel2.Controls.Add(pictureBox1);
                            flowLayoutPanel1.Controls.Add(panel2);
                        }
                        frmSearch1.Visible = false;
                    }));
                }
                else
                {
                    frmSearch1.Visible = true;
                }
            }));
        }
        void DownZimu(string title, Panel panel, ProgressBar progressBar, string url)
        {
            string basePath = Program.CachePath + "sub\\" + title.Replace(":", "") + "\\";
            List<string> Files = ScanFile(new DirectoryInfo(basePath));
            if (Files.Count > 0)
            {
                if (Files.Count > 1)
                {
                    UIListSel frm = new UIListSel(Files);
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        player.LoadZimu(frm.SelValue);
                    }
                }
                else
                {
                    player.LoadZimu(Files[0]);
                }
            }
            else
            {
                panel.Enabled = false;
                progressBar.Visible = true;
                progressBar.Style = ProgressBarStyle.Marquee;
                bool isok = false;
                Action _action = () =>
                {
                    try
                    {
                        List<HttpLib.Val> _header = new List<HttpLib.Val> {
                        new HttpLib.Val("Accept","text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9"),
                        new HttpLib.Val("Accept-Encoding","gzip, deflate"),
                        new HttpLib.Val("Accept-Language","zh-CN,zh;q=0.9,en;q=0.8,en-GB;q=0.7,en-US;q=0.6"),
                        new HttpLib.Val("User-Agent","Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/83.0.4103.61 Safari/537.36 Edg/83.0.478.37"),
                        };

                        string html = HttpLib.Http.Get(url).header(_header).redirect(true).request();
                        if (!string.IsNullOrEmpty(html))
                        {
                            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                            doc.LoadHtml(html);

                            string href = doc.DocumentNode.SelectSingleNode("//li[@class='li dlsub']/div/a[@id='down1']").GetAttributeValue("href", null);
                            Uri uri = new Uri(href);
                            string htmlhref = HttpLib.Http.Get(uri.AbsoluteUri).header(_header).redirect(true).request();
                            if (!string.IsNullOrEmpty(htmlhref))
                            {
                                HtmlAgilityPack.HtmlDocument dochref = new HtmlAgilityPack.HtmlDocument();
                                dochref.LoadHtml(htmlhref);
                                var downs = dochref.DocumentNode.SelectNodes("//div[@class='down clearfix']/ul/li");
                                foreach (var item in downs)
                                {
                                    string downhref = "http://" + uri.Host + item.SelectSingleNode("a").GetAttributeValue("href", null);
                                    try
                                    {
                                        HttpWebRequest Myrq = (HttpWebRequest)HttpWebRequest.Create(downhref);
                                        Myrq.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9";
                                        Myrq.Host = "zmk.pw";
                                        Myrq.Referer = url;
                                        Myrq.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/83.0.4103.61 Safari/537.36 Edg/83.0.478.37";
                                        Myrq.Timeout = 3000;
                                        using (HttpWebResponse response = (HttpWebResponse)Myrq.GetResponse())
                                        {
                                            if (response.ContentType.ToLower().Contains("application/octet-stream"))
                                            {
                                                string fileName = "字幕库.z";
                                                string Disposition = response.Headers["Content-Disposition"];
                                                if (!string.IsNullOrEmpty(Disposition))
                                                {
                                                    string filename = "";
                                                    filename = Disposition.Substring(Disposition.IndexOf("filename=") + 9);
                                                    if (filename.Contains(";"))
                                                    {
                                                        filename = filename.Substring(0, filename.IndexOf(";"));
                                                        if (filename.EndsWith("\""))
                                                        {
                                                            filename = filename.Substring(1, filename.Length - 2);
                                                        }
                                                    }
                                                    filename = filename.Trim('"');


                                                    byte[] byteArray = Encoding.GetEncoding("ISO-8859-1").GetBytes(filename);// System.Text.Encoding.Default.GetBytes(fileName);
                                                                                                                             // Encoding.GetEncoding("utf-8").GetBytes(fileName);
                                                    string urlFilename = Encoding.GetEncoding("utf-8").GetString(byteArray);
                                                    urlFilename = System.Web.HttpUtility.UrlDecode(urlFilename, Encoding.UTF8);

                                                    fileName = urlFilename;
                                                }
                                                else
                                                {
                                                    fileName = response.ResponseUri.Segments[response.ResponseUri.Segments.Length - 1];
                                                }
                                                if (!File.Exists(Program.CachePath + "sub\\" + fileName))
                                                {
                                                    //application/octet-stream
                                                    //_length = myrp.ContentLength;//总大小
                                                    //tProgressBar1.Invoke(new Action(() =>
                                                    //{
                                                    //    tProgressBar1.Maximum = (int)_length;
                                                    //}));
                                                    progressBar.Invoke(new Action(() =>
                                                    {
                                                        progressBar.Style = ProgressBarStyle.Blocks;
                                                        progressBar.Maximum = (int)response.ContentLength;
                                                    }));
                                                    using (Stream su = response.GetResponseStream())
                                                    {
                                                        (Program.CachePath + "sub\\").CreateDirectory(true);
                                                        int _downvalue = 0;
                                                        using (FileStream so = new FileStream(Program.CachePath + "sub\\" + fileName, FileMode.Create))
                                                        {
                                                            byte[] by = new byte[1024];
                                                            int osize = su.Read(by, 0, (int)by.Length);
                                                            while (osize > 0)
                                                            {
                                                                _downvalue += osize;
                                                                so.Write(by, 0, osize);
                                                                osize = su.Read(by, 0, (int)by.Length);
                                                                progressBar.Invoke(new Action(() =>
                                                                {
                                                                    progressBar.Value = _downvalue;
                                                                }));
                                                            }
                                                        }
                                                    }
                                                }
                                                //Program.CachePath + "sub\\" + 
                                                //srt;ass;ssa;smi;psb;idx;sub;sup
                                                string[] filters = new string[0];
                                                string filter = player.playerGetConfig(502);
                                                if (!string.IsNullOrEmpty(filter))
                                                {
                                                    filters = filter.Split(';');
                                                }

                                                basePath.CreateDirectory();

                                                if (filters.Contains(Path.GetExtension(fileName).TrimStart('.').ToLower()))
                                                {
                                                    File.Move(Program.CachePath + "sub\\" + fileName, basePath + fileName);
                                                    player.LoadZimu(basePath + fileName);
                                                }
                                                else
                                                {
                                                    string err;
                                                    List<string> files = Compression.DecompressionList(Program.CachePath + "sub\\" + fileName, basePath, out err);
                                                    if (files != null)
                                                    {
                                                        UIListSel frm = new UIListSel(files);
                                                        if (frm.ShowDialog() == DialogResult.OK)
                                                        {
                                                            string path_ = basePath + frm.SelValue;
                                                            if (File.Exists(path_))
                                                            {
                                                                player.LoadZimu(path_);
                                                            }
                                                        }
                                                        //File.Delete(Program.CachePath + "sub\\" + fileName);
                                                    }
                                                    else
                                                    {
                                                        Api.OpenMessage(this, MessageType.Warn, "解压失败：" + err);
                                                    }
                                                }
                                                return;
                                            }
                                            else
                                            {
                                                Myrq.Abort();
                                            }
                                        }
                                        isok = true;
                                        return;
                                    }
                                    catch (Exception ez)
                                    {

                                    }
                                }
                            }
                        }
                    }
                    catch
                    {
                        Api.OpenMessage(this, MessageType.Warn, "下载字幕失败:1");
                    }
                };
                _task.ContinueWhenAll(new Task[] { _task.StartNew(_action) }, (action =>
                {
                    this.Invoke(new Action(() =>
                    {
                        progressBar.Style = ProgressBarStyle.Blocks;
                        progressBar.Maximum = progressBar.Value = 0;
                        progressBar.Visible = false;
                        panel.Enabled = true;
                    }));
                }));
            }
        }


        List<string> ScanFile(DirectoryInfo dir)
        {
            List<string> temp = new List<string>();
            if (dir.Exists)
            {
                foreach (FileInfo item in dir.GetFiles())
                {
                    temp.Add(item.FullName);
                }
                foreach (DirectoryInfo item in dir.GetDirectories())
                {
                    temp.AddRange(ScanFile(item));
                }
            }
            return temp;
        }
        protected override void OnSizeChanged(EventArgs e)
        {
            if (Width > 0 && flowLayoutPanel1 != null)
            {
                int width = flowLayoutPanel1.Width - 30;
                foreach (Control item in flowLayoutPanel1.Controls)
                {
                    item.Width = width;
                }
            }
            base.OnSizeChanged(e);
        }
        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void text_search_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                e.Handled = true;
                string str = text_search.Text.Trim();
                _Load(str);
            }
        }

        private void frmSearch1_OnSearch(string e)
        {
            _Load(e.Trim());
        }
    }
}
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text.RegularExpressions;
using TPlayerSupport;

namespace TPlayer
{
    public static class Api
    {
        public static bool IsRunAsAdmin()
        {
            using (WindowsIdentity id = WindowsIdentity.GetCurrent())
            {
                WindowsPrincipal principal = new WindowsPrincipal(id);
                return principal.IsInRole(WindowsBuiltInRole.Administrator);
            }
        }

        public static void OpenMessage(this System.Windows.Forms.Form form, Frm.MessageType type, string txt)
        {
            if (form != null)
            {
                form.BeginInvoke(new Action(() =>
                {
                    Frm.Message message = new Frm.Message(form, type, txt);
                    message.Show(form);
                }));
            }
        }

        public static void MaskForm(System.Windows.Forms.Form fform, System.Windows.Forms.Form form, System.Drawing.Color back)
        {
            Frm.Mask mask = new Frm.Mask(fform, back);
            bool isclose = false;
            fform.Invoke(new Action(() =>
            {
                mask.Show(fform);
                mask.MouseUp += (s, e) =>
                {
                    if (e.Button == System.Windows.Forms.MouseButtons.Left)
                    {
                        isclose = true;
                        form.Close();
                        mask.Close();
                    }
                };

                form.Show(mask);

                form.FormClosing += (s, e) =>
                {
                    if (!isclose)
                    {
                        isclose = true;
                        mask.Close();
                    }
                };
            }));
        }

        public static List<TFileInfo> ScanPath(this string[] paths)
        {
            List<TFileInfo> files = new List<TFileInfo>();
            foreach (string path in paths)
            {
                files.AddRange(path.ScanPath());
            }
            return files;
        }
        public static List<TFileInfo> ScanPath(this string path)
        {
            List<TFileInfo> files = new List<TFileInfo>();
            if (File.Exists(path))
            {
                FileInfo fileInfo = new FileInfo(path);
                files.Add(new TFileInfo
                {
                    FilePath = fileInfo.FullName,
                    FileName = fileInfo.Name,
                    FileSize = fileInfo.Length
                });
            }
            else if (Directory.Exists(path))
            {
                files.AddRange(path.ScanPathDir());
            }
            return files;
        }
        static List<TFileInfo> ScanPathDir(this string path)
        {
            List<TFileInfo> files = new List<TFileInfo>();
            DirectoryInfo directory = new DirectoryInfo(path);
            try
            {
                foreach (var item in directory.GetFiles())
                {
                    files.Add(new TFileInfo
                    {
                        FilePath = item.FullName,
                        FileName = item.Name,
                        FileSize = item.Length
                    });
                }
            }
            catch { }
            try
            {
                foreach (var item in directory.GetDirectories())
                {
                    try
                    {
                        files.AddRange(item.FullName.ScanPathDir());
                    }
                    catch { }
                }
            }
            catch { }
            return files;
        }
        public static void OpenExplorer(this string path)
        {
            ProcessStartInfo psi = new ProcessStartInfo("Explorer.exe");
            psi.Arguments = "/e,/select," + path;
            //  psi.Arguments = " /select," + file;
            Process.Start(psi).WaitForExit();
        }

        public static bool OpenExe(this string path, string arguments, bool isadmin)
        {
            ProcessStartInfo proc = new ProcessStartInfo
            {
                UseShellExecute = true,
                WorkingDirectory = Environment.CurrentDirectory,
                Arguments = arguments,
                WindowStyle = ProcessWindowStyle.Hidden,
                CreateNoWindow = false,
                FileName = path,
            };
            if (isadmin && !IsRunAsAdmin())
            {
                // Launch as administrator
                proc.Verb = "runas";
            }
            try
            {
                Process.Start(proc).WaitForExit();
                return true;
            }
            catch
            {
                // The user refused the elevation.
                // Do nothing and return directly ...
                return false;
            }
        }
        public static bool OpenAssistExe(this object json, string action, bool isadmin = true)
        {
            return json.ToJson().OpenAssistExe(action, isadmin);
        }
        public static bool OpenAssistExe(this string json, string action, bool isadmin = true)
        {
            AdminAppMain adminAppMain = new AdminAppMain
            {
                action = action,
                json = json,
                appPath = Program.ExePath
            };
            string pathName = "TPlayerAssist.exe";
            //string pathName = "TPlayerAssist" + DateTime.Now.Ticks + ".exe";
            string path = Program.TempPath + pathName;
            if (!File.Exists(path))
            {
                Program.TempPath.CreateDirectory(true);
                File.WriteAllBytes(path, Properties.Resources.TPlayerAssist);
            }
            ProcessStartInfo proc = new ProcessStartInfo
            {
                UseShellExecute = true,
                WorkingDirectory = Environment.CurrentDirectory,
                Arguments = Convert.ToBase64String(System.Text.Encoding.Default.GetBytes(adminAppMain.ToJson())),
                WindowStyle = ProcessWindowStyle.Hidden,
                CreateNoWindow = false,
                FileName = path,
            };
            if (isadmin && !IsRunAsAdmin())
            {
                // Launch as administrator
                proc.Verb = "runas";
            }
            bool isOk = false;
            try
            {
                Process.Start(proc).WaitForExit();
                isOk = true;
            }
            catch
            {
                // The user refused the elevation.
                // Do nothing and return directly ...
            }
            File.Delete(path);
            return isOk;
        }
        public static string ToVideoName(this string url)
        {
            string _fileName = Path.GetFileName(File.Exists(url) ? url : new Uri(url).AbsoluteUri.ConvertVideoName());

            if (_fileName.Contains("%"))
            {
                int val = Regex.Matches(_fileName, "%").Count;

                if (val > _fileName.Length / 6)
                {
                    _fileName = System.Web.HttpUtility.UrlDecode(_fileName);
                }
            }
            return _fileName;
        }
        static string ConvertVideoName(this string url)
        {
            string str = url;
            if (str.EndsWith("?") || str.EndsWith("/") || str.EndsWith("#") || str.EndsWith("&") || str.EndsWith("."))
            {
                return str.Substring(0, str.Length - 1);
            }
            else if (str.Contains("?"))
            {
                return str.Substring(0, str.IndexOf("?"));
            }
            else if (str.Contains("#"))
            {
                return str.Substring(0, str.IndexOf("#"));
            }
            else { return str; }
        }

        public static System.Windows.Forms.OpenFileDialog OpenFile(this string filter, bool Multiselect = false)
        {
            System.Windows.Forms.OpenFileDialog ofd_file = new System.Windows.Forms.OpenFileDialog();
            ofd_file.Multiselect = Multiselect;
            if (filter == "video")
            {
                //ofd.Filter = "支持字幕格式|*.srt;*.ssa;*.ass;*.idx|所有文件|*.*";
                ofd_file.Filter = "支持格式|*.mp4;*.avi;*.rm;*.rmvb;*.flv;*.xr;*.mpg;*.vcd;*.svcd;*.dvd;*.vob;*.asf;*.wmv;*.mov;*.qt;*.3gp;*.sdp;*.yuv;*.mkv;*.dat;*.torrent;*.mp3;*.3g2;*.3gp2;*.3gpp;*.aac;*.ac3;*.aif;*.aifc;*.aiff;*.amr;*.amv;*.ape;*.asp;*.bik;*.csf;*.divx;*.evo;*.f4v;*.hlv;*.ifo;*.ivm;*.m1v;*.m2p;*.m2t;*.m2ts;*.m2v;*.m4b;*.m4p;*.m4v;*.mag;*.mid;*.mod;*.movie;*.mp2v;*.mp2;*.mpa;*.mpeg;*.mpeg4;*.mpv2;*.mts;*.ogg;*.ogm;*.pmp;*.pss;*.pva;*.qt;*.ram;*.rp;*.rpm;*.rt;*.scm;*.smi;*.smil;*.svx;*.swf;*.tga;*.tod;*.tp;*.tpr;*.ts;*.voc;*.vp6;*.wav;*.webm;*.wma;*.wm;*.wmp;*.xlmv;*.xv;*.xvid;*.tplayer;|所有文件|*.*";

            }
            else if (filter == "imgs")
            {
                ofd_file.Filter = "图片格式|*.png;*.gif;*.jpg;*.jpeg;*.bmp;|所有文件|*.*";
            }
            else if (filter == "img")
            {
                //ofd_file.Filter = "支持图片格式|*.png;*.gif;*.jpg;*.jpeg;*.bmp;|所有文件|*.*";
                ofd_file.Filter = "图片格式|*.jpg;*.jpeg;*.png;*.bmp;|所有文件|*.*";
            }
            else
            {
                ofd_file.Filter = filter;
            }
            return ofd_file;
        }
        public static Version GetVersion(this string verson)
        {
            return new Version(verson.Replace("v", "").Replace("V", ""));
        }

        #region Music

        public static Image GetMusicPicture(this string MusicPath)
        {
            //List<Image> imgList = new List<Image>();
            string nameMusic = null;
            try
            {
                if (File.Exists(MusicPath))
                {
                    using (var mp3 = new Id3.Mp3(MusicPath))
                    {
                        Id3.Id3Tag tag = mp3.GetTag(Id3.Id3TagFamily.Version2X);
                        if (tag == null)
                        {
                            tag = mp3.GetTag(Id3.Id3TagFamily.Version1X);
                        }
                        if (tag != null)
                        {
                            if (tag.Pictures != null && tag.Pictures.Count > 0)
                            {
                                return tag.Pictures[0].PictureData.ToImage();
                            }
                            else
                            {
                                nameMusic = tag.Artists;
                            }
                        }
                        //Console.WriteLine("Title: {0}", tag.Title);
                        //Console.WriteLine("艺术家: {0}", tag.Artists);
                        //Console.WriteLine("专辑: {0}", tag.Album);
                    }
                }
            }
            catch
            {
            }

            if (string.IsNullOrEmpty(nameMusic))
            {
                nameMusic = Path.GetFileNameWithoutExtension(MusicPath.ToVideoName());
            }
            return nameMusic.GetWebMusicPicture();
        }

        #region 获取专辑照片

        public static Image GetWebMusicPicture(this string MusicName)
        {
            var img = MusicName.GetWebMusicPictureByQQ();
            if (img == null)
            {
                if (MusicName.Contains(" - "))
                {
                    string[] nameMusics = Regex.Split(MusicName, " - ", RegexOptions.IgnoreCase);
                    foreach (var item in nameMusics)
                    {
                        Image image = item.GetWebMusicPictureByKugou();
                        if (image != null)
                        {
                            return image;
                        }
                    }
                }
                else
                {
                    return MusicName.GetWebMusicPictureByKugou();
                }
            }
            else { return img; }

            return null;
        }
        static Image GetWebMusicPictureByQQ(this string MusicName)
        {
            try
            {
                QQMusicList musicList = HttpLib.Http.Get("https://c.y.qq.com/soso/fcgi-bin/client_search_cp").data(new { w = MusicName, new_json = "1", format = "json" }).request().ToJson<QQMusicList>();
                if (musicList != null && musicList.code == 0 && musicList.data != null && musicList.data.song != null && musicList.data.song.list != null && musicList.data.song.list.Count > 0)
                {
                    string _resultDetail = HttpLib.Http.Get("https://i.y.qq.com/v8/playsong.html").data(new { songmid = musicList.data.song.list[0].mid }).header(new { UserAgent = "Mozilla/5.0 (iPhone; CPU iPhone OS 13_2_3 like Mac OS X) AppleWebKit/605.1.15 (KHTML, like Gecko) Version/13.0.3 Mobile/15E148 Safari/604.1" }).request();
                    HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                    doc.LoadHtml(_resultDetail);
                    var main = doc.DocumentNode.SelectSingleNode("/html/head");
                    if (main != null)
                    {
                        var sa = main.SelectSingleNode("//meta[contains(@itemprop,'image')]");
                        if (sa != null)
                        {
                            //name、description
                            string _imgUrl = sa.GetAttributeValue("content", null);
                            if (_imgUrl != null)
                            {
                                return HttpLib.Http.Get("http:" + _imgUrl).redirect(true).requestData().ToImage();
                            }
                        }
                    }
                }
            }
            catch { }

            return null;
        }

        static Image GetWebMusicPictureByKugou(this string MusicName)
        {
            try
            {
                string url = string.Format(@"http://mobilecdn.kugou.com/new/app/i/yueku.php?cmd=104&size=240&singer={0}&type=softhead", MusicName);
                SingerInfo singer = HttpLib.Http.Get(url).redirect(true).request().ToJson<SingerInfo>();
                if (singer != null && !string.IsNullOrEmpty(singer.url))
                {
                    return HttpLib.Http.Get(singer.url).redirect(true).requestData().ToImage();
                }
            }
            catch { }

            return null;
        }

        class QQMusicListAlbum
        {
            /// <summary>
            /// 
            /// </summary>
            public int id { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string mid { get; set; }
            /// <summary>
            /// 心事
            /// </summary>
            public string name { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string pmid { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string subtitle { get; set; }
            /// <summary>
            /// 心事
            /// </summary>
            public string title { get; set; }
            /// <summary>
            /// 心事
            /// </summary>
            public string title_hilight { get; set; }
        }

        class QQMusicListMv
        {
            /// <summary>
            /// 
            /// </summary>
            public int id { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string vid { get; set; }
        }

        class SingerItem
        {
            /// <summary>
            /// 
            /// </summary>
            public int id { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string mid { get; set; }
            /// <summary>
            /// 张信哲
            /// </summary>
            public string name { get; set; }
            /// <summary>
            /// 张信哲
            /// </summary>
            public string title { get; set; }
            /// <summary>
            /// 张信哲
            /// </summary>
            public string title_hilight { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int type { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int uin { get; set; }
        }

        class QQMusicListItem
        {
            /// <summary>
            /// 
            /// </summary>
            public QQMusicListAlbum album { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int chinesesinger { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string desc { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string desc_hilight { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string docid { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string es { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int fnote { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int genre { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public int id { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int index_album { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int index_cd { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int interval { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int isonly { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int language { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string lyric { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string lyric_hilight { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string mid { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public QQMusicListMv mv { get; set; }
            /// <summary>
            /// 爱如潮水
            /// </summary>
            public string name { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public List<SingerItem> singer { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int status { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string subtitle { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public string time_public { get; set; }
            /// <summary>
            /// 爱如潮水
            /// </summary>
            public string title { get; set; }
            /// <summary>
            /// 爱如潮水
            /// </summary>
            public string title_hilight { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int type { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string url { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int ver { get; set; }
        }

        class QQMusicListSong
        {
            /// <summary>
            /// 当前数量
            /// </summary>
            public int curnum { get; set; }
            /// <summary>
            /// 当前页
            /// </summary>
            public int curpage { get; set; }
            /// <summary>
            /// 数据
            /// </summary>
            public List<QQMusicListItem> list { get; set; }
            /// <summary>
            /// 总量
            /// </summary>
            public int totalnum { get; set; }
        }


        class QQMusicListData
        {
            /// <summary>
            /// 关键字
            /// </summary>
            public string keyword { get; set; }
            /// <summary>
            /// 歌曲
            /// </summary>
            public QQMusicListSong song { get; set; }
        }

        class QQMusicList
        {
            /// <summary>
            /// 
            /// </summary>
            public int code { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public QQMusicListData data { get; set; }
        }

        class SingerInfo
        {
            public int status { get; set; }

            /// <summary>
            /// 歌手名
            /// </summary>
            public string singer { get; set; }

            /// <summary>
            /// 图片地址
            /// </summary>
            public string url { get; set; }
        }
        #endregion

        #endregion

        #region 鼠标

        [DllImport("user32.dll", EntryPoint = "ShowCursor", CharSet = CharSet.Auto)]
        extern static void ShowCursor(int status);//调用ShowCursor(0)或ShowCursor(1)来设置.

        public static bool ShowCursor(System.Windows.Forms.Form fform, bool status)
        {
            try
            {
                fform.Invoke(new Action(() =>
                {
                    try
                    {
                        ShowCursor(status ? 1 : 0);
                    }
                    catch { }
                }));
                return true;
            }
            catch { }
            return false;
        }
        #endregion
    }

    public class TFileInfo
    {
        public string FilePath { get; set; }
        public string FileName { get; set; }
        public long FileSize { get; set; }
    }
}

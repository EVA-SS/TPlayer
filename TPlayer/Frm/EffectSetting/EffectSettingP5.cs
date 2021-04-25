using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;
using TPlayerSupport;

namespace TPlayer.Frm
{
    /// <summary>
    /// 字幕
    /// </summary>
    public partial class EffectSettingP5 : UserControl
    {
        EffectSetting effectSetting;
        public EffectSettingP5(EffectSetting effectSetting)
        {
            this.effectSetting = effectSetting;
            InitializeComponent();

            #region 字幕
            tCom_subtitle.DisplayMember = "Name";
            tCom_subtitle.ValueMember = "Value";

            if (effectSetting.player.player.GetConfig(501) == "1")
            {

                change_subtitle();
                btn_zimu_file.Click += Btn_zimu_file_Click;
                btn_zimu_web.Click += Btn_zimu_web_Click;
                if (effectSetting.player.player.GetConfig(504) == "1")
                { slideButton1.IsOn = true; }
                slideButton1.OnClick += subtitle_OnClick;
                string subtitle_timing = effectSetting.player.player.GetConfig(509).Split(';')[0];
                tong_subtitle_Num = Convert.ToInt32(subtitle_timing);
                string tong_Txt = (tong_subtitle_Num / 1000).ToString();
                tong_subtitle.Text = tong_Txt;
                tong_subtitle.TextChanged += tong_subtitle_TextChanged;
                tong_subtitle.KeyPress += tong_KeyPress;
                tong1_subtitle.Click += tong1_subtitle_Click;
                tong2_subtitle.Click += tong2_subtitle_Click;
                tBut1_subtitle.Click += tong3_subtitle_Click;

                tCom_subtitle.SelectedValueChanged += TCom_subtitle_SelectedValueChanged;
            }
            else
            {
                tCom_subtitle.Enabled = btn_zimu_web.Enabled = btn_zimu_file.Enabled = tBut1_subtitle.Enabled = tong_subtitle.Enabled = btn_webzimu.Enabled = tong1_subtitle.Enabled = tong2_subtitle.Enabled = false;
                label22.Visible = true;
                //this.Enabled = false;
                //b4.Enabled = p4.Enabled = false;
            }

            #endregion
        }

        private void TCom_subtitle_SelectedValueChanged(object sender, EventArgs e)
        {
            HttpLib.Val item = tCom_subtitle.SelectedItem as HttpLib.Val;
            if (item != null)
            {
                int index;
                if (int.TryParse(item.Value, out index))
                {
                    effectSetting.player.ShowPrompt("当前字幕", item.Key);
                    effectSetting.player.player.SetConfig(506, item.Value);
                }
                else
                {
                    Uri uri = new Uri(item.Value);
                    string fileName = uri.Segments[uri.Segments.Length - 1];
                    string basePath = Program.CachePath + "sub\\" + item.Key + "\\";

                    if (File.Exists(basePath + fileName))
                    {
                        effectSetting.player.LoadZimu(basePath + fileName);
                        effectSetting.player.ShowPrompt("当前字幕", item.Key);
                    }
                    else
                    {
                        basePath.CreateDirectory();
                        bool isok = false;
                        Action _action = () =>
                        {
                            HttpWebRequest Myrq = (HttpWebRequest)HttpWebRequest.Create(uri);
                            Myrq.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.9";
                            Myrq.Host = uri.Host;
                            Myrq.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/83.0.4103.61 Safari/537.36 Edg/83.0.478.37";
                            Myrq.Timeout = 3000;
                            using (HttpWebResponse response = (HttpWebResponse)Myrq.GetResponse())
                            {
                                if (response.ContentType.ToLower().Contains("application/octet-stream"))
                                {
                                    //application/octet-stream
                                    //_length = myrp.ContentLength;//总大小
                                    //tProgressBar1.Invoke(new Action(() =>
                                    //{
                                    //    tProgressBar1.Maximum = (int)_length;
                                    //}));
                                    using (Stream su = response.GetResponseStream())
                                    {
                                        int _downvalue = 0;
                                        using (FileStream so = new FileStream(basePath + fileName, FileMode.Create))
                                        {
                                            byte[] by = new byte[1024];
                                            int osize = su.Read(by, 0, (int)by.Length);
                                            while (osize > 0)
                                            {
                                                _downvalue += osize;
                                                so.Write(by, 0, osize);
                                                osize = su.Read(by, 0, (int)by.Length);
                                            }
                                        }
                                    }
                                    isok = true;

                                }
                                else
                                {
                                    Myrq.Abort();
                                }
                            }
                        };
                        effectSetting.player._task.ContinueWhenAll(new Task[] { effectSetting.player._task.StartNew(_action) }, (action =>
                        {
                            if (isok)
                            {
                                effectSetting.player.LoadZimu(basePath + fileName);
                                effectSetting.player.ShowPrompt("当前字幕", item.Key);
                            }
                            //this.Invoke(new Action(() =>
                            //{
                            //    btn_zimu_web.Stop();
                            //    btn_zimu_web.Enabled = true;
                            //}));
                        }));
                    }
                }
            }
        }

        #region 字幕

        /// <summary>
        /// 字幕显示
        /// </summary>
        /// <param name="isOn"></param>
        private void subtitle_OnClick(bool isOn)
        {
            effectSetting.player.player.SetConfig(504, isOn ? "1" : "0");
            effectSetting.player.RefreshMeunBySubtilteEnable();
        }

        public void change_subtitle()
        {
            //tCom_subtitle.SelectedIndexChanged -= TComboBox_SelectedIndexChanged;
            tCom_subtitle.Items.Clear();
            string SubtilteLanguageList = effectSetting.player.player.GetConfig(505);
            if (!string.IsNullOrEmpty(SubtilteLanguageList))
            {
                int SubtilteLanguageCurrent = Convert.ToInt32(effectSetting.player.player.GetConfig(506));
                string[] split = SubtilteLanguageList.Split(';');
                for (int i = 0; i < split.Length; i++)
                {
                    tCom_subtitle.Items.Add(new HttpLib.Val(split[i], i.ToString()));
                    //tCom_subtitle.Items.Add(split[i]);
                    if (i == SubtilteLanguageCurrent)
                    {
                        tCom_subtitle.Text = split[i];
                    }
                }
                //tCom_subtitle.SelectedIndexChanged += TComboBox_SelectedIndexChanged;
            }
        }

        private void Btn_zimu_file_Click(object sender, EventArgs e)
        {
            string filter = effectSetting.player.player.GetConfig(502);
            if (!string.IsNullOrEmpty(filter))
            {
                string[] filters = filter.Split(';');
                string _filter = "支持的字幕格式|";
                foreach (string item in filters)
                {
                    _filter += $"*.{item};";
                }
                effectSetting.isOp = true;
                using (OpenFileDialog ofd = (_filter + "|所有文件|*.*").OpenFile())
                {
                    if (ofd.ShowDialog(this) == DialogResult.OK)
                    {
                        effectSetting.player.LoadZimu(ofd.FileName);
                    }
                    this.Focus();
                    effectSetting.isOp = false;
                }
            }
        }


        #region 同步
        int tong_subtitle_Num = 0;
        private void tong_subtitle_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(tong_subtitle.Text))
                {
                    effectSetting.player.player.SetConfig(509, "0;1000;1000");
                    string subtitle_timing = effectSetting.player.player.GetConfig(509).Split(';')[0];
                    tong_subtitle_Num = Convert.ToInt32(subtitle_timing);
                }
                else
                {
                    tong_subtitle_Num = (int)(Math.Round(Convert.ToDouble(tong_subtitle.Text), 3) * 1000);
                    effectSetting.player.player.SetConfig(405, tong_subtitle_Num.ToString());
                    string tong_Txt = ((tong_subtitle_Num * 1.0) / 1000).ToString();
                    effectSetting.player.ShowPrompt("字幕同步", tong_Txt == "0" ? "正常" : tong_Txt);
                }
            }
            catch { }
        }

        private void tong1_subtitle_Click(object sender, EventArgs e)
        {
            try
            {
                tong_subtitle_Num += 500;
                string tong_Txt = ((tong_subtitle_Num * 1.0) / 1000).ToString();
                effectSetting.player.player.SetConfig(509, tong_subtitle_Num + ";1000;1000");
                effectSetting.player.ShowPrompt("字幕同步", tong_Txt == "0" ? "正常" : tong_Txt);
                tong_subtitle.Text = tong_Txt;
            }
            catch
            {
            }
        }

        private void tong2_subtitle_Click(object sender, EventArgs e)
        {
            try
            {
                tong_subtitle_Num -= 500;
                string tong_Txt = ((tong_subtitle_Num * 1.0) / 1000).ToString();
                effectSetting.player.player.SetConfig(509, tong_subtitle_Num + ";1000;1000");
                effectSetting.player.ShowPrompt("字幕同步", tong_Txt == "0" ? "正常" : tong_Txt);
                tong_subtitle.Text = tong_Txt;
            }
            catch
            {
            }
        }
        private void tong3_subtitle_Click(object sender, EventArgs e)
        {
            try
            {
                tong_subtitle_Num = 0;
                string tong_Txt = "0";
                effectSetting.player.player.SetConfig(509, "0;1000;1000");
                effectSetting.player.ShowPrompt("字幕同步", "正常");
                tong_subtitle.Text = tong_Txt;
            }
            catch
            {
            }
        }
        #endregion

        #endregion

        #region Core

        private void tong_KeyPress(object sender, KeyPressEventArgs e)
        {
            int _int = (int)e.KeyChar;
            if ((_int < 48 || _int > 57) && _int != 8 && _int != 46)
                e.Handled = true;
            if (_int == 46)
            {
                if (tong_subtitle.Text.Length <= 0)
                    e.Handled = true;   //小数点不能在第一位
                else
                {
                    float f;
                    float oldf;
                    bool b1 = false, b2 = false;
                    b1 = float.TryParse(tong_subtitle.Text, out oldf);
                    b2 = float.TryParse(tong_subtitle.Text + e.KeyChar.ToString(), out f);
                    if (b2 == false)
                    {
                        if (b1 == true)
                            e.Handled = true;
                        else
                            e.Handled = false;
                    }
                }
            }
        }

        #endregion


        private void Btn_zimu_web_Click(object sender, EventArgs e)
        {
            if (effectSetting.player.IsPlaying)
            {
                int videolength = effectSetting.player.player.GetDuration();
                if (videolength > 0)
                {
                    effectSetting.player.ShowPrompt("正在自动匹配字幕");
                    btn_zimu_web.Enabled = false;
                    btn_zimu_web.State = true;
                    string videoName = effectSetting.player.Text;
                    Action _action = () =>
                    {
                        List<HttpLib.Val> _header = new List<HttpLib.Val> {
                            new HttpLib.Val("Accept-Encoding","gzip, deflate"),
                            new HttpLib.Val("Accept-Language","zh-CN,zh;q=0.9,en;q=0.8,en-GB;q=0.7,en-US;q=0.6"),
                            new HttpLib.Val("User-Agent","Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/83.0.4103.61 Safari/537.36 Edg/83.0.478.37"),
                        };

                        Struct_ThunderSubtitles json = HttpLib.Http.Get($"http://subtitle.kankan.xunlei.com:8000/search.json/mname={videoName}&videolength={videolength}").header(_header).redirect(true).request().ToJson<Struct_ThunderSubtitles>();
                        if (json != null && json.sublist.Count > 0)
                        {
                            tCom_subtitle.Invoke(new Action(() =>
                            {
                                tCom_subtitle.Items.Clear();
                                foreach (var item in json.sublist)
                                {
                                    if (item.sname != null && item.surl != null)
                                    {
                                        tCom_subtitle.Items.Add(new HttpLib.Val(item.sname, item.surl));
                                    }
                                }
                            }));
                        }
                        else
                        {
                            effectSetting.player.ShowPrompt("没有发现匹配字幕");
                        }
                    };
                    effectSetting.player._task.ContinueWhenAll(new Task[] { effectSetting.player._task.StartNew(_action) }, (action =>
                    {
                        this.Invoke(new Action(() =>
                        {
                            btn_zimu_web.State = false;
                            btn_zimu_web.Enabled = true;
                        }));
                    }));
                }
            }
        }


        private void btn_webzimu_Click(object sender, EventArgs e)
        {
            if (effectSetting.player.IsPlaying)
            {
                string videoName = effectSetting.player.Text;

                if (effectSetting.player.webZimu == null)
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
                    effectSetting.player.webZimu = new WebZimu(effectSetting.player, _videoName);
                    effectSetting.player.webZimu.Show();
                }
                else
                {
                    effectSetting.player.webZimu._Load(videoName);
                    effectSetting.player.webZimu.Activate();
                }
            }
            else
            {
                if (effectSetting.player.webZimu == null)
                {
                    effectSetting.player.webZimu = new WebZimu(effectSetting.player, null);
                    effectSetting.player.webZimu.Show();
                    //webZimu._Load(null);
                }
                else
                {
                    effectSetting.player.webZimu.Activate();
                }
            }
        }



        public class Struct_ThunderSubtitles
        {
            public List<Sublist> sublist = new List<Sublist>();
            public class Sublist
            {
                public string scid { get; set; }
                public string sname { get; set; }
                public string sext { get; set; }
                public string language { get; set; }
                public string simility { get; set; }
                public string surl { get; set; }
            }
        }
    }
}

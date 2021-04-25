using System;
using System.Drawing;
using System.Windows.Forms;

namespace TPlayer.Frm
{
    /// <summary>
    /// 画面
    /// </summary>
    public partial class EffectSettingP2 : UserControl
    {
        EffectSetting effectSetting;
        public EffectSettingP2(EffectSetting effectSetting)
        {
            this.effectSetting = effectSetting;
            InitializeComponent();


            #region 画面

            switch (effectSetting.player.FrameStyle)
            {
                case PlayFrameStyle.None:
                    btn_scale1.Checked = true;
                    break;
                case PlayFrameStyle._4_3:
                    btn_scale2.Checked = true;
                    break;
                case PlayFrameStyle._16_9:
                    btn_scale3.Checked = true;
                    break;
                case PlayFrameStyle.Paved:
                    btn_scale4.Checked = true;
                    break;
            }

            //开启硬件加速
            if (effectSetting.player.player.GetConfig(209) == "1")//获取或设置是否开启硬件加速，1-开启，0-不开启
            {
                btn_accelerate.Checked = true;
            }


            if (effectSetting.player.player.GetConfig(305) == "1")//画质增强, 1-开启, 0-不开启
            {
                btn_quality.Checked = true;
            }



            if (effectSetting.player.player.GetConfig(302) == "1")
            {
                btn_rotate3.BackColor2 = Color.FromArgb(100, Color.DodgerBlue);
                btn_rotate3.Tag = "";
            }
            if (effectSetting.player.player.GetConfig(303) == "1")
            {
                btn_rotate4.BackColor2 = Color.FromArgb(100, Color.DodgerBlue);
                btn_rotate4.Tag = "";
            }

            if (effectSetting.player.player.GetConfig(301) == "0")
            {
                btn_rotate1.Enabled = btn_rotate2.Enabled = btn_rotate3.Enabled = btn_rotate4.Enabled = btn_quality.Enabled = false;
            }

            btn_rotate1.Tag = effectSetting.player.player.GetConfig(304);

            this.btn_accelerate.CheckedChanged += btn_accelerate_CheckedChanged;
            this.btn_quality.CheckedChanged += btn_quality_CheckedChanged;

            #endregion
        }




        #region 画面

        private void Change_XS(object sender, EventArgs e)
        {
            TSkin.TRadioButton f = (TSkin.TRadioButton)sender;
            //SetT("当前分辨率：" + f.Text);
            switch (f.Text)
            {
                case "铺满屏幕":
                    effectSetting.player.FrameStyle = PlayFrameStyle.Paved;
                    break;
                case "4:3":
                    effectSetting.player.FrameStyle = PlayFrameStyle._4_3;
                    break;
                case "16:9":
                    effectSetting.player.FrameStyle = PlayFrameStyle._16_9;
                    break;
                default:
                    effectSetting.player.FrameStyle = PlayFrameStyle.None;
                    break;
            }
            effectSetting.player.ShowPrompt("当前比例", f.Text);
        }


        private void btn_accelerate_CheckedChanged(object sender, EventArgs e)
        {
            //硬件加速
            if (btn_accelerate.Checked)
            {
                effectSetting.player.player.SetConfig(305, "0");
                effectSetting.player.player.SetConfig(209, "1");
                //返回硬件加速的开启状态：0 - 未开启, 1 - 开启成功, 2 - 未知错误, 3 - 设备不支持, 4 - 格式不支持, 5 - 操作系统不支持, 6 - 解码器不支持
                string state = effectSetting.player.player.GetConfig(211);
                if (state == "1")
                {
                    btn_quality.Checked = false;
                    effectSetting.player.ShowPrompt("硬件加速", "开启");
                }
                else
                {
                    switch (state)
                    {
                        case "2":
                            effectSetting.player.ShowPrompt("硬件加速", "未知错误");
                            break;
                        case "3":
                            effectSetting.player.ShowPrompt("硬件加速", "设备不支持");
                            break;
                        case "4":
                            effectSetting.player.ShowPrompt("硬件加速", "格式不支持");
                            break;
                        case "5":
                            effectSetting.player.ShowPrompt("硬件加速", "操作系统不支持");
                            break;
                        case "6":
                            effectSetting.player.ShowPrompt("硬件加速", "解码器不支持");
                            break;
                        default:
                            effectSetting.player.ShowPrompt("硬件加速", "开启失败");
                            break;
                    }
                }
            }
            else
            {
                effectSetting.player.player.SetConfig(209, "0");
                string state = effectSetting.player.player.GetConfig(209);
                if (state == "0")
                {
                    effectSetting.player.ShowPrompt("硬件加速", "关闭");
                }
                else
                {
                    effectSetting.player.ShowPrompt("硬件加速", "关闭失败");
                }
            }
        }
        bool nocheck1 = false;
        private void btn_quality_CheckedChanged(object sender, EventArgs e)
        {
            if (!nocheck1)
            {
                //画质增强
                if (effectSetting.player.player.GetConfig(301) == "0")
                {
                    Repair();
                    effectSetting.player.ShowPrompt("画质增强", "不可用");
                }
                else
                {
                    if (btn_quality.Checked)
                    {
                        effectSetting.player.player.SetConfig(305, "1");
                        if (effectSetting.player.player.GetConfig(305) == "1")
                        {
                            effectSetting.player.ShowPrompt("画质增强", "开启");
                        }
                        else
                        {
                            nocheck1 = true;
                            btn_quality.Checked = false;
                            effectSetting.player.ShowPrompt("画质增强", "开启失败");
                            nocheck1 = false;
                        }
                    }
                    else
                    {
                        effectSetting.player.player.SetConfig(305, "0");
                        effectSetting.player.ShowPrompt("画质增强", "关闭");
                    }
                }
            }
        }

        private void btn_rotate3_Click(object sender, EventArgs e)
        {
            if (effectSetting.player.player.GetConfig(301) == "0")
            {
                Repair();
                effectSetting.player.ShowPrompt("画面旋转", "不可用");
            }
            else
            {
                if (btn_rotate3.Tag == "1")
                {
                    effectSetting.player.player.SetConfig(302, "1");
                    btn_rotate3.IsActive = true;
                    btn_rotate3.Tag = "";
                }
                else
                {
                    effectSetting.player.player.SetConfig(302, "0");
                    btn_rotate3.IsActive = false;
                    btn_rotate3.Tag = "1";
                }
            }
        }

        private void btn_rotate4_Click(object sender, EventArgs e)
        {
            if (effectSetting.player.player.GetConfig(301) == "0")
            {
                Repair();
                effectSetting.player.ShowPrompt("画面旋转", "不可用");
            }
            else
            {
                if (btn_rotate4.Tag == "1")
                {
                    effectSetting.player.player.SetConfig(303, "1");
                    btn_rotate4.IsActive = true;
                    btn_rotate4.Tag = "";
                }
                else
                {
                    effectSetting.player.player.SetConfig(303, "0");
                    btn_rotate4.IsActive = false;
                    btn_rotate4.Tag = "1";
                }
            }
        }

        private void btn_rotate1_Click(object sender, EventArgs e)
        {
            if (effectSetting.player.player.GetConfig(301) == "0")
            {
                Repair();
                effectSetting.player.ShowPrompt("画面旋转", "不可用");
            }
            else
            {
                int value = Convert.ToInt32(btn_rotate1.Tag) + 90;
                btn_rotate1.Tag = value;
                effectSetting.player.player.SetConfig(304, value.ToString());
            }
        }

        private void btn_rotate2_Click(object sender, EventArgs e)
        {
            if (effectSetting.player.player.GetConfig(301) == "0")
            {
                Repair();
                effectSetting.player.ShowPrompt("画面旋转", "不可用");
            }
            else
            {
                int value = Convert.ToInt32(btn_rotate1.Tag) - 90;
                btn_rotate1.Tag = value;
                effectSetting.player.player.SetConfig(304, value.ToString());
            }
        }

        void Repair()
        {
            effectSetting.isOp = true;
            if (!effectSetting.player.RepairCode())
            {
                effectSetting.isOp = false;
            }
            //return;
            //string courl = Program.CodecsPath + "vsfilter.dll";
            //if (!File.Exists(courl))
            //{
            //    bool isok = false;
            //    effectSetting.player.SetLoadIng(true);
            //    Action _action = () =>
            //    {
            //        string name = "vsfilter.dll";
            //        if (effectSetting.player._loading != null)
            //        {
            //            effectSetting.player._loading.Titite = name + " 准备就绪";
            //        }
            //        string basecodeurl = Program.CodecsPath;

            //        string codes_url = "http://xmp.down.sandai.net/kankan/codecs3/" + name;
            //        basecodeurl.CreateDirectory();
            //        try
            //        {
            //            HttpWebRequest Myrq = (HttpWebRequest)HttpWebRequest.Create(codes_url);
            //            Myrq.Timeout = 3000;
            //            using (HttpWebResponse myrp = (HttpWebResponse)Myrq.GetResponse())
            //            {
            //                if (myrp.ContentType.Contains("text/html"))
            //                {
            //                    Myrq.Abort();
            //                }
            //                else
            //                {
            //                    //application/octet-stream
            //                    long totalBytes = myrp.ContentLength;//总大小
            //                    using (Stream su = myrp.GetResponseStream())
            //                    {
            //                        using (FileStream so = new FileStream(courl, FileMode.Create))
            //                        {
            //                            long totalDownloadedByte = 0;
            //                            byte[] by = new byte[1024];
            //                            int osize = su.Read(by, 0, (int)by.Length);
            //                            while (osize > 0)
            //                            {
            //                                totalDownloadedByte = osize + totalDownloadedByte;
            //                                so.Write(by, 0, osize);
            //                                osize = su.Read(by, 0, (int)by.Length);

            //                                int Tj = Convert.ToInt32(Convert.ToDouble(totalDownloadedByte) / Convert.ToDouble(totalBytes) * 100.0);

            //                                if (effectSetting.player._loading != null)
            //                                {
            //                                    effectSetting.player._loading.Titite = string.Format("解码器：{0} {1}%", name, Tj);
            //                                }
            //                            }
            //                        }
            //                    }
            //                    isok = true;
            //                }
            //            }
            //        }
            //        catch (Exception ez)
            //        {
            //            if (ez is IOException)
            //            {
            //                if (effectSetting.player._loading != null)
            //                {
            //                    effectSetting.player._loading.Titite = name + " 文件无法写入";
            //                    System.Threading.Thread.Sleep(2000);
            //                }
            //            }
            //        }
            //    };
            //    effectSetting.player._task.ContinueWhenAll(new Task[] { effectSetting.player._task.StartNew(_action) }, (action =>
            //    {
            //        effectSetting.player.SetLoadIng(false);
            //        if (isok)
            //        {
            //            if (effectSetting.player.playerGetConfig(301) == "1" && File.Exists(courl))
            //            {
            //                effectSetting.player.ShowPrompt("画质设置", "修复成功");
            //            }
            //            else
            //            {
            //                effectSetting.player.ShowPrompt("画质设置", "修复失败");
            //            }
            //        }
            //        else
            //        {
            //            effectSetting.player.ShowPrompt("画质设置", "修复失败");
            //        }
            //    }));

            //}
        }

        #endregion
    }
}

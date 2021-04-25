using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using TPlayerSupport;

namespace TPlayer.Frm
{
    public partial class Property : NetDimension.WinForm.ModernUIForm
    {
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

        TPlayer player;
        public Property(TPlayer player)
        {
            this.player = player;
            InitializeComponent();
        }
        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            player.property = null;
        }

        protected override void OnLoad(EventArgs e)
        {
            LoadVideoInfo();
            LoadVideoProg();
            base.OnLoad(e);
        }
        public void LoadVideoProg()
        {
            loadingMaterial.Value = player.CacheProg;
            loadingMaterial.State = (player.CacheProg < 100 && player.CacheProg != -1);
        }
        bool isDownSpeed = false;
        public void LoadVideoInfo()
        {
            isDownSpeed = false;
            MaxByte = 0;
            label7.Text = label10.Text = label11.Text = null;
            label7.ForeColor = Color.DimGray;
            if (player.IsPlaying)
            {
                curve.Clear();
                string uri = player.player.GetConfig(4);
                if (!string.IsNullOrEmpty(uri))
                {
                    if (File.Exists(uri))
                    {
                        label7.Text = "   本地视频文件";
                    }
                    else
                    {
                        UpByte = player.player.GetConfig(29).ToDouble();
                        if (player.CacheProg < 100 && player.CacheProg != -1)
                        {
                            isDownSpeed = true;
                        }
                        else
                        {
                            label7.ForeColor = label11.ForeColor;
                            label7.Text = "   缓存完成";
                        }
                    }
                }
                timBk.Enabled = true;

                double playersize = player.player.GetConfig(5).ToDouble();
                double maxvalue = player.player.GetDuration();


                label1.Text = player.Text;
                if (maxvalue > 0)
                {
                    label9.Text = "文件时长：" + maxvalue.ToTimeStr();
                    label8.Text = "文件大小：" + playersize.CountSize();
                }
                else
                {
                    label9.Text = "文件时长：直播";
                    label8.Text = "文件大小：∞";
                }
                if (maxvalue > 0)
                {
                    label6.Text = "码率信息：" + Math.Round((playersize * 8) / maxvalue) + "Kbps";
                }
                else
                {
                    label6.Text = "码率信息：∞Kbps";
                }
                int _w = player.player.GetVideoWidth(), _h = player.player.GetVideoHeight();
                if (_w > 0 && _h > 0)
                {
                    label11.Visible = true;
                    label3.Text = "分 辨 率 ：" + _w + "×" + _h;
                    label5.Text = "视频帧率：" + Math.Round(1000 / player.player.GetConfig(117).ToDouble()) + " fps";
                }
                else
                {
                    label11.Visible = false;
                    label3.Text = "分 辨 率 ：无画面";
                    label5.Text = GetParams("音频信息：", " - ", GetParams("声道数:", player.player.GetConfig(411)), GetParams("采样率:", player.player.GetConfig(412)), GetParams("采样位数:", player.player.GetConfig(413)));
                }

                //label5.Text = "视频帧率：" + Convert.ToDouble(player.player.GetConfig(121)) + " fps";//渲染帧率


                label4.Text = GetParams("编码格式：", " / ", player.player.GetConfig(221), player.player.GetConfig(410));//编码格式

                //string v1 = player.player.GetConfig(410);//编码格式
                //string v2 = player.player.GetConfig(411);//声道数
                //string v3 = player.player.GetConfig(412);//采样率 Hz

                //double channels = Convert.ToDouble(player.player.GetConfig(411));//声道数
                //double sampling_rate = Convert.ToDouble(player.player.GetConfig(412));//采样率
                //double sampling_digit = Convert.ToDouble(player.player.GetConfig(413));//采样位数

                //try
                //{
                //    int op = (int)((playersize * 8) / maxvalue);
                //    if (op > 0)
                //    {

                //        label7.Text = op + "kbps";
                //    }
                //    else { label7.Text = "未知"; }
                //}
                //catch { }
            }
            else
            {
                timBk.Enabled = false;
            }
        }
        string GetParams(string key, string val)
        {
            if (string.IsNullOrEmpty(val))
            {
                return null;
            }
            else
            {
                return key + val;
            }
        }
        string GetParams(string key, string and, params string[] val)
        {
            return key + GetParams(and, val);
        }
        string GetParams(string and, params string[] val)
        {
            return string.Join(and, val.Where(s => !string.IsNullOrEmpty(s)));
        }
        public void CloseVideoInfo()
        {
            isDownSpeed = false;
            timBk.Enabled = false;
        }


        double UpByte = 0, MaxByte = 0;

        private void Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void timBk_Tick(object sender, EventArgs e)
        {
            if (isDownSpeed)
            {
                //通过参数 GetConfig(29) 获取 APlayer 的读取字节数，根据下式计算出下载速度：
                //下载速度（KB/S） = （ 第二次获取的字节数 -  第一次获取的字节数）/ 1024 /  两次获取的隔秒数
                //如果要换成 Kbps（比特率），则结果应该再乘以 8。
                double bytestr = player.player.GetConfig(29).ToDouble();
                if (bytestr > 0)
                {
                    double kb = (bytestr - UpByte);
                    UpByte = bytestr;

                    if (kb > MaxByte)
                    {
                        MaxByte = kb;//更新最大值
                        label10.Text = "   最大速度：" + kb.CountSize();
                    }
                    //data[0].name = sud;
                    label7.Text = "   连接速度：" + kb.CountSize() + " /S";

                    curve.Add((float)kb);
                }
            }



            double fps = player.player.GetConfig(121).ToDouble();
            if (fps > 0)
            {
                label11.Text = Math.Round(fps, 1) + " fps";//渲染帧率
            }
            else
            {
                label11.Text = null;
            }
        }

    }
}

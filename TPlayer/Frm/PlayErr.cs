using System;
using System.Drawing;
using System.Windows.Forms;

namespace TPlayer
{
    public partial class PlayErr : NetDimension.WinForm.ModernUIForm
    {
        string Err;
        TPlayer player;
        public PlayErr(TPlayer player, string e)
        {
            this.player = player;
            Err = e;
            InitializeComponent();
        }
        protected override void OnLoad(EventArgs e)
        {
            if (player.Left > 0 || player.Top > 0) { this.Location = new Point((player.Left + player.Width / 2) - this.Width / 2, (player.Top + player.Height / 2) - this.Height / 2); } else { this.Top = Screen.PrimaryScreen.WorkingArea.Height - 150; this.Left = Screen.PrimaryScreen.WorkingArea.Width - Width; }
            switch (Err)
            {
                case "-2147287038":
                    label1.Text = "找不到指定的资源";
                    label2.Text = "错误码：0x80030002(" + Err + "）";
                    break;
                case "-2147220927":
                    label1.Text = "无法识别的地址";
                    label2.Text = "您输入的地址可能错在错误";
                    break;
                case "-1968570365":
                    label1.Text = "Http地址内容无法连接";
                    label2.Text = "您输入的网址可能错在错误";
                    break;
                case "-2147467259":
                    label1.Text = "m3u8/rtmp地址内容无法连接";
                    label2.Text = "您输入的网址可能错在错误";
                    break;
                case "-2147024809":
                    label1.Text = "ftp地址内容无法连接";
                    label2.Text = "您输入的网址可能错在错误";
                    break;
                case "-1072889803":
                    label1.Text = "rtsp/mms地址内容无法连接";
                    label2.Text = "您输入的网址可能错在错误";
                    break;
                case "-2147220968":
                    label1.Text = "无法解码该文件"; jfa = true;
                    label2.Text = "找不到所需的解码器或格式不支持,点击重试尝试系统解码";
                    break;
                case "-1968570364":
                    label1.Text = "不支持这样的格式转换";
                    label2.Text = "您需要更换别的转码方案";
                    break;
                case "-1968570366":
                    label1.Text = "这是一个文件夹路径";
                    label2.Text = "您需要选择对应文件，或者拖动多个文件至播放器";
                    break;
                case "-2147221000":
                    jfa = true;
                    label1.Text = "无法访问网络";
                    label2.Text = "无法下载解码器,点击重试尝试系统解码";
                    break;
                case "-2147220891":
                    ok.ForeColor = Color.DimGray;
                    ok.Enabled = false;
                    player.player.SetConfig(9, "0");
                    label1.Text = "不支持此类视频文件";
                    label2.Text = "视频文件可能已经损坏";
                    break;
                default:
                    label1.Text = "未知错误";
                    try
                    {
                        label2.Text = "希望您能反馈给我们这样的错误\n错误代码（" + Err.Substring(1, Err.Length - 1) + "）";
                    }
                    catch
                    {
                        label2.Text = "这是一个未知的错误类型";
                    }
                    break;
            }
            base.OnLoad(e);
        }

        bool jfa = false;
        private void ok_Click(object sender, EventArgs e)
        {
            if (jfa)
            {
                player.player.SetConfig(9, "1");
                //9 - Use open chain                   int R/ W         当 APlayer 内部解码器播放失败后尝试使用系统解码器，0 - 不尝试，1 - 尝试，默认为0，尝试使用系统解码器可能会造成播放不稳定。
                player.ShowPrompt("使用系统解码器可能会造成播放不稳定");
            }
            DialogResult = DialogResult.OK;
        }

        private void no_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.No;
        }

    }
}

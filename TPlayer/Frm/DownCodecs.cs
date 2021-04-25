using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using TPlayerSupport;

namespace TPlayer
{
    public partial class DownCodecs : NetDimension.WinForm.ModernUIForm
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

        public DownCodecs()
        {
            InitializeComponent();
            btn_min.Image = FontAwesome.GetImage("4FA9", 30, Color.Black);
            btn_close.Image = FontAwesome.GetImage("4FA3", 30, Color.Black);
            btn_close.ImageHove = FontAwesome.GetImage("4FA3", 30, Color.White);
        }

        TaskFactory _task = new TaskFactory();
        bool isok = false;
        protected override void OnLoad(EventArgs e1)
        {
            base.OnLoad(e1);
            string codecsPath = Program.TempPath + "codecs.zip";
            Program.TempPath.CreateDirectory(true);

            Action _action = () =>
            {
                Helper.DownCore core = new Helper.DownCore();
                core.MaxValueChange += (s, e) =>
                {
                    tProgressBar1.Invoke(new Action(() =>
                    {
                        tProgressBar1.Maximum = (int)e;
                    }));
                };
                core.ValueChange += (s, e) =>
                {
                    tProgressBar1.Invoke(new Action(() =>
                    {
                        try
                        {
                            tProgressBar1.Value = (int)e;
                        }
                        catch { }
                    }));
                };
                core.SpeedChange += (s, e) =>
                {
                    label2.Invoke(new Action(() =>
                    {
                        label2.Text = e.CountSize() + " /s";
                    }));
                };
                string InitErr;
                if (core.DownInit("http://aplayer.open.xunlei.com/codecs.zip", "codecs.zip", out InitErr))
                {
                    string Err;
                    if (core.DownUrl(codecsPath, Program.TempPath + "codecs\\", out Err))
                    {
                        isok = true;

                        if (File.Exists(codecsPath))
                        {
                            this.Invoke(new Action(() =>
                            {
                                label2.Text = "下载完毕，正在安装";
                            }));
                            Directory.Delete(Program.CodecsPath, true);
                            string err;
                            if (Compression.Decompression(codecsPath, Program.CodecsPath, out err))
                            {
                                this.Invoke(new Action(() =>
                                {
                                    label1.Text = "修复解码器成功";
                                    label2.Text = "完成";
                                }));
                            }
                            else
                            {
                                this.Invoke(new Action(() =>
                                {
                                    tProgressBar1.State = TSkin.ProgressBarState.Error;
                                    tProgressBar1.Maximum = tProgressBar1.Value = 1;
                                    label1.Text = "修复解码器失败";
                                    label2.Text = "原因：" + err;
                                }));
                            }
                            try
                            {
                                if (File.Exists(codecsPath))
                                {
                                    File.Delete(codecsPath);
                                }
                            }
                            catch
                            { }
                        }
                    }
                    else
                    {
                        //下载失败

                        try
                        {
                            if (File.Exists(codecsPath))
                            {
                                File.Delete(codecsPath);
                            }
                        }
                        catch
                        { }
                        this.Invoke(new Action(() =>
                        {
                            tProgressBar1.State = TSkin.ProgressBarState.Error;
                            tProgressBar1.Maximum = tProgressBar1.Value = 1;
                            label1.Text = "修复解码器失败";
                            label2.Text = "原因：" + Err;
                        }));
                    }
                }
                else
                {
                    try
                    {
                        if (File.Exists(codecsPath))
                        {
                            File.Delete(codecsPath);
                        }
                    }
                    catch
                    { }
                    this.Invoke(new Action(() =>
                    {
                        tProgressBar1.State = TSkin.ProgressBarState.Error;
                        tProgressBar1.Maximum = tProgressBar1.Value = 1;
                        label1.Text = "修复解码器失败";
                        label2.Text = "原因：" + InitErr;
                    }));
                }
            };
            _task.ContinueWhenAll(new Task[] { _task.StartNew(_action) }, (action =>
            {
                btn_close.Invoke(new Action(() =>
                {
                    btn_close.Enabled = true;
                }));
            }));
        }

        private void btn_min_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        private void btn_close_Click(object sender, EventArgs e)
        {
            //Application.Restart();
            this.Close();
            if (isok)
            {
                new Process
                {
                    StartInfo = new ProcessStartInfo(Program.ExePath)
                    { UseShellExecute = false },
                }.Start();
            }
        }

    }
}

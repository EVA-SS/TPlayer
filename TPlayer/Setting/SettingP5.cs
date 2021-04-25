using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using TPlayerSupport;

namespace TPlayer.Frm
{
    public partial class SettingP5 : UserControl
    {
        Setting setting;
        public SettingP5(Setting setting)
        {
            this.setting = setting;
            InitializeComponent();
            label_ProductVersion.Text += Application.ProductVersion;
            check_pre.Checked = SystemSettings.UpdatePie;
            check_pre.CheckedChanged += (a, b) =>
            {
                SystemSettings.UpdateInfo = null;
                SystemSettings.UpdatePie = check_pre.Checked;
                if (!isdown)
                {
                    btn_update.Text = "检查更新";
                }
            };
            UpdateInfo();
            //_Load();
        }
        #region 操作更新UI

        void UpdateInfo(bool htmlErr = false)
        {
            pic_err.Visible = htmlErr;
            if (htmlErr)
            {
                label_NewVersion.Text = "检查更新失败";
                btn_update.Text = "检查更新";
                btn_update.IsActive = false;
            }
            else
            {
                if (SystemSettings.UpdateInfo == null)
                {
                    label_NewVersion.Text = "等待检查更新";
                    btn_update.Text = "检查更新";
                    btn_update.IsActive = false;
                }
                else
                {
                    Version verson = new Version(Application.ProductVersion);

                    Version newverson = SystemSettings.UpdateInfo.verson.GetVersion();
                    int tm = newverson.CompareTo(verson);
                    if (tm > 0)
                    {
                        label_NewVersion.Text = $"最新版本“{SystemSettings.UpdateInfo.verson}”";
                        btn_update.Text = "下载";
                        btn_update.IsActive = true;
                    }
                    else
                    {
                        label_NewVersion.Text = "您使用的是最新版本";
                        btn_update.Text = "检查更新";
                        btn_update.IsActive = false;
                    }
                    btn_info.Left = label_NewVersion.Left + label_NewVersion.PreferredSize.Width;
                    btn_info.IsActive = SystemSettings.UpdateInfo.pre;
                    if (SystemSettings.UpdateInfo.pre)
                    {
                        btn_info.Text = "预览版";
                    }
                    else
                    {
                        btn_info.Text = "正式版";
                    }
                    btn_info.Visible = true;
                }

                #region 时间控制

                if (string.IsNullOrEmpty(SystemSettings.UpdateTime))
                {
                    label_updateInfo.Text = "从未更新过";
                }
                else
                {
                    DateTime timeNow = DateTime.Now;
                    DateTime time = SystemSettings.UpdateTime.ToDateTime();
                    if (time.ToString("yyyyMMdd") == timeNow.ToString("yyyyMMdd"))
                    {
                        label_updateInfo.Text = "上次检查时间：今天，" + time.ToString("HH:mm");
                    }
                    else if (time.ToString("yyyyMMdd") == timeNow.AddDays(-1).ToString("yyyyMMdd"))
                    {
                        label_updateInfo.Text = "上次检查时间：昨天，" + time.ToString("HH:mm");
                    }
                    else if (time.ToString("yyyyMMdd") == timeNow.AddDays(-2).ToString("yyyyMMdd"))
                    {
                        label_updateInfo.Text = "上次检查时间：前天，" + time.ToString("HH:mm");
                    }
                    else if (time > timeNow)
                    {
                        label_updateInfo.Text = "上次检查时间：未来？";
                    }
                    else
                    {
                        label_updateInfo.Text = "上次检查时间：" + time.ToString("yyyy-MM-dd HH:mm");
                    }
                }

                #endregion
            }
        }

        #region 检查更新

        void UpdateText(string up = "检查更新")
        {
            btn_update.Invoke(new Action(() =>
            {
                btn_update.IsActive = false;
                btn_update.Text = up;
            }));
        }

        void UpdateText(string up, string txt)
        {
            btn_update.Invoke(new Action(() =>
            {
                btn_update.IsActive = false;
                btn_update.Text = up;
                label_NewVersion.Text = txt;
            }));
        }

        #endregion


        #endregion
        public void _Load()
        {
            //label3.Text = "APlayer " + setting.player.player.GetVersion() + "\n";
            //label3.Text += Getbool(213) + "颜色调节\n";
            //label3.Text += Getbool(301) + "图像处理\n";
            //label3.Text += Getbool(401) + "声音处理\n";
            //label3.Text += Getbool(501) + "字幕加载\n";
            //label3.Text += Getbool(601) + "视频叠图\n";
            //label3.Text += Getbool(701) + "截图功能\n";
            //label3.Text += Getbool(801) + "视频截取\n";
        }
        //private string Getbool(int j)
        //{
        //    string z = "×     ";
        //    if (setting.player.playerGetConfig(j) == "1")
        //    {
        //        z = "●     ";
        //    }
        //    return z;
        //}

        TaskFactory _task = new TaskFactory();
        bool isdown = false;
        private void btn_update_Click(object sender, EventArgs e2)
        {
            if (isdown) { return; }
            if (SystemSettings.UpdateInfo != null && btn_update.IsActive)
            {
                UpdateInfo updateInfo = SystemSettings.UpdateInfo;
                if (updateInfo.files.Count > 0)
                {
                    isdown = true;
                    //btn_update.Enabled = false;
                    label_updateInfo.Visible = false;
                    metroLoading.Visible = metroLoading.State = true;
                    btn_update.Width = 160;
                    bool isok = false;
                    string updatebasePath = Program.UpdatePath + updateInfo.verson + "\\";
                    string updatePath = updatebasePath + updateInfo.files[0].name;
                    Action _action = () =>
                    {
                        if (File.Exists(updatePath))
                        {
                            isok = true;
                        }
                        else
                        {
                            btn_update.Invoke(new Action(() =>
                            {
                                btn_update.Enabled = false;
                                btn_update.Text = "正在下载";
                            }));
                            Helper.DownCore core = new Helper.DownCore();
                            core.MaxValueChange += (s, e) =>
                            {
                                metroLoading.MaxValue = e;
                            };
                            core.ValueChange += (s, e) =>
                            {
                                metroLoading.Value = e;
                            };
                            core.SpeedChange += (s, e) =>
                            {
                                btn_update.Invoke(new Action(() =>
                                {
                                    btn_update.Text = "正在下载 " + e.CountSize();
                                }));
                            };
                            string InitErr;
                            if (core.DownInit(updateInfo.files[0].url, updateInfo.files[0].name, out InitErr))
                            {
                                string Err;
                                if (core.DownUrl(updatePath, updatebasePath + "temp\\", out Err))
                                {
                                    isok = true;
                                    //btn_update.Invoke(new Action(() =>
                                    //{
                                    //    btn_update.MaxValue = btn_update.Value = 0;
                                    //    btn_update.Text = "点击安装";
                                    //    //label1.Text = verson;
                                    //}));
                                }
                                else
                                {
                                    //下载失败
                                    btn_update.Invoke(new Action(() =>
                                        {
                                            btn_update.IsActive = false;
                                            btn_update.State = false;
                                            btn_update.ForeColor = Color.Red;
                                            btn_update.Text = Err;
                                        }));
                                    System.Threading.Thread.Sleep(2000);
                                    btn_update.Invoke(new Action(() =>
                                    {
                                        btn_update.ForeColor = this.ForeColor;
                                        btn_update.Text = "检查更新";
                                        btn_update.Enabled = true;
                                    }));
                                }
                            }
                            else
                            {
                                btn_update.Invoke(new Action(() =>
                                {
                                    btn_update.IsActive = false;
                                    btn_update.State = false;
                                    btn_update.ForeColor = Color.Red;
                                    btn_update.Text = InitErr;
                                }));
                                System.Threading.Thread.Sleep(2000);
                                btn_update.Invoke(new Action(() =>
                                {
                                    btn_update.ForeColor = this.ForeColor;
                                    btn_update.Text = "检查更新";
                                    btn_update.Enabled = true;
                                }));
                            }
                        }
                    };
                    _task.ContinueWhenAll(new Task[] { _task.StartNew(_action) }, (action =>
                    {
                        if (isok)
                        {
                            if (updatePath.EndsWith(".exe"))
                            {
                                AdminAppMainUpdate adminAppMainUpdate = new AdminAppMainUpdate(updatebasePath, new List<string> { "TPlayer.exe" });

                                btn_update.Invoke(new Action(() =>
                                {
                                    btn_update.BackColorActive = btn_update.BackColorActive2 = Color.DodgerBlue;
                                    btn_update.Text = "等待升级";
                                }));

                                if (adminAppMainUpdate.OpenAssistExe("Update"))
                                {
                                    btn_update.Invoke(new Action(() =>
                                    {
                                        Environment.Exit(0);
                                    }));
                                    return;
                                }
                                else
                                {
                                    btn_update.Invoke(new Action(() =>
                                    {
                                        btn_update.IsActive = false;
                                        btn_update.ForeColor = Color.Red;
                                        btn_update.Text = "启动更新程序失败";
                                    }));
                                    System.Threading.Thread.Sleep(2000);
                                    btn_update.Invoke(new Action(() =>
                                    {
                                        btn_update.ForeColor = this.ForeColor;
                                        btn_update.Text = "检查更新";
                                    }));
                                }
                                Directory.Delete(updatebasePath, true);
                            }
                            else
                            {
                                string err;
                                List<string> files = Compression.DecompressionList(updatePath, updatebasePath, out err);
                                if (files != null && files.Count > 0)
                                {
                                    AdminAppMainUpdate adminAppMainUpdate = new AdminAppMainUpdate(updatebasePath, files);

                                    btn_update.Invoke(new Action(() =>
                                    {
                                        btn_update.ForeColor = Color.DodgerBlue;
                                        btn_update.Text = "等待升级";
                                    }));
                                    if (adminAppMainUpdate.OpenAssistExe("Update"))
                                    {
                                        btn_update.Invoke(new Action(() =>
                                        {
                                            Environment.Exit(0);
                                        }));
                                        return;
                                    }
                                    else
                                    {
                                        btn_update.Invoke(new Action(() =>
                                        {
                                            btn_update.State = false;
                                            btn_update.ForeColor = Color.Red;
                                            btn_update.Text = "启动更新程序失败";
                                        }));
                                        System.Threading.Thread.Sleep(2000);
                                        btn_update.Invoke(new Action(() =>
                                        {
                                            btn_update.ForeColor = this.ForeColor;
                                            btn_update.Text = "检查更新";
                                        }));
                                    }

                                    Directory.Delete(updatebasePath, true);
                                }
                                else
                                {
                                    File.Delete(updatePath);
                                    btn_update.Invoke(new Action(() =>
                                    {
                                        btn_update.State = false;
                                        btn_update.ForeColor = Color.Red;
                                        btn_update.Text = "文件已损坏";
                                    }));
                                    System.Threading.Thread.Sleep(2000);
                                    btn_update.Invoke(new Action(() =>
                                    {
                                        btn_update.ForeColor = this.ForeColor;
                                        btn_update.Text = "检查更新";
                                    }));
                                }
                            }
                        }
                        this.Invoke(new Action(() =>
                        {
                            btn_update.Width = 114;
                            metroLoading.Visible = metroLoading.State = false;
                            btn_update.Enabled = true;
                        }));
                        isdown = false;
                    }));
                }
                else
                {
                    SystemSettings.UpdateInfo = null;
                    metroLoading.State = false;
                    metroLoading.Visible = false;
                    label_updateInfo.Visible = btn_update.Visible = true;
                    UpdateInfo();
                    btn_update_Click(sender, e2);
                }
            }
            else
            {
                label_NewVersion.Text = "正在检查更新";

                label_updateInfo.Visible = btn_info.Visible = btn_update.Visible = false;
                metroLoading.Visible = true;
                metroLoading.State = true;
                SystemSettings.UpdateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
                bool htmlErr = false;
                Action _action = () =>
                {
                    Version verson = new Version(Application.ProductVersion);
                    UpdateInfo updateInfo;
                    if (UpdateCore.Update(verson, out updateInfo, out htmlErr, SystemSettings.UpdatePie))
                    {
                        SystemSettings.UpdateInfo = updateInfo;
                        //this.BeginInvoke(new Action(() =>
                        //{
                        //    Frm.Update update = new Frm.Update(this, verson.ToString(), updateInfo);
                        //    update.Show();
                        //}));
                        //Debug.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(updateInfo));
                    }
                    else
                    {
                        SystemSettings.UpdateInfo = null;
                    }
                };
                _task.ContinueWhenAll(new Task[] { _task.StartNew(_action) }, (action =>
                {
                    this.Invoke(new Action(() =>
                    {
                        metroLoading.State = false;
                        metroLoading.Visible = false;
                        label_updateInfo.Visible = btn_update.Visible = true;
                        UpdateInfo(htmlErr);
                    }));
                }));
            }
        }

        private void Frm_Move(object sender, MouseEventArgs e)
        {
            setting.FrmMove(sender, e);
        }

        private void btn_info_Click(object sender, EventArgs e)
        {
            if (SystemSettings.UpdateInfo != null)
            {
                new Update(SystemSettings.UpdateInfo).ShowDialog();
            }
        }

        private void btn_feedback_Click(object sender, EventArgs e)
        {
            new FeedBack().Show();
        }
    }
}

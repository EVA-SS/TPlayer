using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TPlayer.Frm
{
    public partial class SettingP1 : UserControl
    {

        Setting setting;
        public SettingP1(Setting setting)
        {
            this.setting = setting;
            InitializeComponent();

            #region 读取设置

            check_animation.Checked = SystemSettings.Animation;
            check_minimizeToTray.Checked = SystemSettings.MinimizeToTray;
            check_multiple.Checked = SystemSettings.Multiple;
            check_rememberLocation.Checked = SystemSettings.RememberLocation;
            check_accelerate.Checked = SystemSettings.SpeedupEnable;
            switch (SystemSettings.VideoOpenFrame)
            {
                case 0:
                    tRadioButton1.Checked = true;
                    break;
                case 1:
                    tRadioButton3.Checked = true;
                    break;
                case 2:
                    tRadioButton2.Checked = true;
                    break;
            }

            check_cache_video.Checked = SystemSettings.CacheVideo;
            check_cache_greed.Checked = SystemSettings.CacheGreed;
            #endregion

            #region 关联

            int _tops = 14;
            int total = 0, music_total = 0, video_total = 0;
            List<string> selglLists = new List<string>(), selglmusic_Lists = new List<string>(), selglvideo_Lists = new List<string>();
            bool enbtn = false;
            foreach (AppLinkList item in AppLink.LinkList)
            {
                total += item.value.Count;
                if (AppLink.Videos.Contains(item.key))
                {
                    video_total += item.value.Count;
                }
                else if (item.key == "其它音频文件")
                {
                    music_total += item.value.Count;
                }
                item.checks = new List<TSkin.TCheckBox>();
                TSkin.TCheckBox checkBox = new TSkin.TCheckBox
                {
                    AutoSize = true,
                    Location = new System.Drawing.Point(14, _tops),
                    Size = new System.Drawing.Size(100, 30),
                    Text = item.key,
                    UseVisualStyleBackColor = true
                };
                panel4.Controls.Add(checkBox);
                _tops += 40;
                int _lefts = 34;
                foreach (string items in item.value)
                {
                    TSkin.TCheckBox checkBoxs = new TSkin.TCheckBox
                    {
                        AutoEllipsis = true,
                        Location = new System.Drawing.Point(_lefts, _tops),
                        Size = new System.Drawing.Size(70, 30),
                        Text = items,
                        UseVisualStyleBackColor = true
                    };
                    panel4.Controls.Add(checkBoxs);
                    item.checks.Add(checkBoxs);
                    checkBoxs.CheckedChanged += (a, b) =>
                    {
                        if (checkBoxs.Checked)
                        {
                            if (!selglLists.Contains(checkBoxs.Text))
                            {
                                selglLists.Add(checkBoxs.Text);
                                item.selcount++;
                                checkBox.Checked = item.selcount == item.value.Count;

                                if (AppLink.Videos.Contains(item.key))
                                {
                                    selglvideo_Lists.Add(checkBoxs.Text);
                                }
                                else if (item.key == "其它音频文件")
                                {
                                    selglmusic_Lists.Add(checkBoxs.Text);
                                }
                            }
                        }
                        else
                        {
                            if (selglLists.Contains(checkBoxs.Text))
                            {
                                selglLists.Remove(checkBoxs.Text);
                                item.selcount--;
                                checkBox.Checked = false;

                                if (AppLink.Videos.Contains(item.key))
                                {
                                    selglvideo_Lists.Remove(checkBoxs.Text);
                                }
                                else if (item.key == "其它音频文件")
                                {
                                    selglmusic_Lists.Remove(checkBoxs.Text);
                                }
                            }
                        }
                        check_link_all.Checked = selglLists.Count == total;
                        check_link_video.Checked = video_total == selglvideo_Lists.Count;
                        check_link_music.Checked = music_total == selglmusic_Lists.Count;
                        if (enbtn)
                        {
                            btn_link_save.Enabled = true;
                        }
                    };

                    _lefts += 90;
                    if ((_lefts + 90) > panel4.Width)
                    {
                        _lefts = 34;
                        _tops += 34;
                    }
                }
                checkBox.Click += (a, b) =>
                {
                    if (checkBox.Checked)
                    {
                        foreach (TSkin.TCheckBox check in item.checks)
                        {
                            if (!check.Checked)
                            {
                                check.Checked = true;
                            }
                        }
                    }
                    else
                    {
                        foreach (TSkin.TCheckBox check in item.checks)
                        {
                            if (check.Checked)
                            {
                                check.Checked = false;
                            }
                        }
                    }
                };

                if (_lefts != 34)
                {
                    _tops += 44;
                }
                else
                {
                    _tops += (44 - 34);
                }
            }
            //panel4.Tag = _tops;

            label10.Click += (a, b) =>
            {
                if (panel4.Height == 0)
                {
                    label11.Text = "▲";
                    panel4.Height = _tops;
                }
                else
                {
                    label11.Text = "▼";
                    panel4.Height = 0;
                }
            };
            label11.Click += (a, b) =>
            {
                if (panel4.Height == 0)
                {
                    label11.Text = "▲";
                    panel4.Height = _tops;
                }
                else
                {
                    label11.Text = "▼";
                    panel4.Height = 0;
                }
            };

            check_link_all.Click += (a, b) =>
            {
                if (check_link_all.Checked)
                {
                    foreach (AppLinkList item in AppLink.LinkList)
                    {
                        foreach (TSkin.TCheckBox check in item.checks)
                        {
                            if (!check.Checked)
                            {
                                check.Checked = true;
                            }
                        }
                    }
                }
                else
                {
                    foreach (AppLinkList item in AppLink.LinkList)
                    {
                        foreach (TSkin.TCheckBox check in item.checks)
                        {
                            if (check.Checked)
                            {
                                check.Checked = false;
                            }
                        }
                    }
                }
            };

            check_link_video.Click += (a, b) =>
            {
                if (check_link_video.Checked)
                {
                    foreach (AppLinkList item in AppLink.LinkList)
                    {
                        if (AppLink.Videos.Contains(item.key))
                        {
                            foreach (TSkin.TCheckBox check in item.checks)
                            {
                                if (!check.Checked)
                                {
                                    check.Checked = true;
                                }
                            }
                        }
                    }
                }
                else
                {
                    foreach (AppLinkList item in AppLink.LinkList)
                    {
                        if (AppLink.Videos.Contains(item.key))
                        {
                            foreach (TSkin.TCheckBox check in item.checks)
                            {
                                if (check.Checked)
                                {
                                    check.Checked = false;
                                }
                            }
                        }
                    }
                }
            };
            check_link_music.Click += (a, b) =>
            {
                if (check_link_music.Checked)
                {
                    foreach (AppLinkList item in AppLink.LinkList)
                    {
                        if (item.key == "其它音频文件")
                        {
                            foreach (TSkin.TCheckBox check in item.checks)
                            {
                                if (!check.Checked)
                                {
                                    check.Checked = true;
                                }
                            }
                        }
                    }
                }
                else
                {
                    foreach (AppLinkList item in AppLink.LinkList)
                    {
                        if (item.key == "其它音频文件")
                        {
                            foreach (TSkin.TCheckBox check in item.checks)
                            {
                                if (check.Checked)
                                {
                                    check.Checked = false;
                                }
                            }
                        }
                    }
                }
            };

            btn_link_reset.Click += (a, b) =>
            {
                foreach (AppLinkList item in AppLink.LinkList)
                {
                    foreach (TSkin.TCheckBox check in item.checks)
                    {
                        if (AppLink.Recommend.Contains(check.Text))
                        {
                            if (!check.Checked)
                            {
                                check.Checked = true;
                            }
                        }
                        else
                        {
                            if (check.Checked)
                            {
                                check.Checked = false;
                            }
                        }
                    }
                }
            };

            btn_link_save.Click += (a, b) =>
            {
                btn_link_save.Text = null;
                btn_link_save.State = true;
                btn_link_save.Enabled = false;
                bool isOk = false;
                Action _action = () =>
                {
                    List<string> delglLists = new List<string>();
                    foreach (string item in AppLink.GetAllLinkList)
                    {
                        if (!selglLists.Contains(item))
                        {
                            delglLists.Add(item);
                        }
                    }

                    AdminAppMainSetLink adminAppMainSetLink = new AdminAppMainSetLink(selglLists, delglLists);
                    isOk = adminAppMainSetLink.OpenAssistExe("SetLink");
                    if (isOk)
                    {
                        SystemSettings.SetLink = string.Join(";", selglLists);
                    }
                };
                setting.player._task.ContinueWhenAll(new Task[] { setting.player._task.StartNew(_action) }, (action =>
                {
                    btn_link_save.Invoke(new Action(() =>
                    {
                        btn_link_save.State = false;
                        btn_link_save.Text = "保存";
                        btn_link_save.Enabled = !isOk;
                    }));
                }));

            };

            string[] setlink = SystemSettings.GetLink;
            foreach (AppLinkList item in AppLink.LinkList)
            {
                foreach (TSkin.TCheckBox check in item.checks)
                {
                    if (setlink.Contains(check.Text))
                    {
                        if (!check.Checked)
                        {
                            check.Checked = true;
                        }
                    }
                }
            }
            enbtn = true;
            #endregion

            #region 修改设置

            check_animation.CheckedChanged += (a, b) =>
            {
                SystemSettings.Animation = check_animation.Checked;
            };
            check_minimizeToTray.CheckedChanged += (a, b) =>
            {
                SystemSettings.MinimizeToTray = check_minimizeToTray.Checked;
            };
            check_multiple.CheckedChanged += (a, b) =>
            {
                SystemSettings.Multiple = check_multiple.Checked;
            };
            check_rememberLocation.CheckedChanged += (a, b) =>
            {
                SystemSettings.RememberLocation = check_rememberLocation.Checked;
            };
            check_accelerate.CheckedChanged += (a, b) =>
            {
                SystemSettings.SpeedupEnable = check_accelerate.Checked;
                if (check_accelerate.Checked)
                {
                    setting.player.player.SetConfig(209, "1");
                    //返回硬件加速的开启状态：0 - 未开启, 1 - 开启成功, 2 - 未知错误, 3 - 设备不支持, 4 - 格式不支持, 5 - 操作系统不支持, 6 - 解码器不支持
                    string state = setting.player.player.GetConfig(211);
                    if (state == "1")
                    {
                        setting.player.ShowPrompt("硬件加速", "开启");
                    }
                    else
                    {
                        switch (state)
                        {
                            case "2":
                                setting.player.ShowPrompt("硬件加速", "未知错误");
                                break;
                            case "3":
                                setting.player.ShowPrompt("硬件加速", "设备不支持");
                                break;
                            case "4":
                                setting.player.ShowPrompt("硬件加速", "格式不支持");
                                break;
                            case "5":
                                setting.player.ShowPrompt("硬件加速", "操作系统不支持");
                                break;
                            case "6":
                                setting.player.ShowPrompt("硬件加速", "解码器不支持");
                                break;
                            default:
                                setting.player.ShowPrompt("硬件加速", "开启失败");
                                break;
                        }
                    }
                }
                else
                {
                    setting.player.player.SetConfig(209, "0");
                    string state = setting.player.player.GetConfig(209);
                    if (state == "0")
                    {
                        setting.player.ShowPrompt("硬件加速", "关闭");
                    }
                    else
                    {
                        setting.player.ShowPrompt("硬件加速", "关闭失败");
                    }
                }
            };


            tRadioButton1.Click += (a, b) =>
            {
                SystemSettings.VideoOpenFrame = 0;
            };
            tRadioButton3.Click += (a, b) =>
            {
                SystemSettings.VideoOpenFrame = 1;
            };
            tRadioButton2.Click += (a, b) =>
            {
                SystemSettings.VideoOpenFrame = 2;
            };


            check_cache_video.CheckedChanged += (a, b) =>
            {
                SystemSettings.CacheVideo = check_cache_video.Checked;
            };
            check_cache_greed.CheckedChanged += (a, b) =>
            {
                SystemSettings.CacheGreed = check_cache_greed.Checked;
                setting.player.player.SetConfig(2207, SystemSettings.CacheGreed ? "1" : "0");
            };
            #endregion
        }

        public void Resume()
        {
            SystemSettings.Animation = SystemSettings.AnimationDefault;
            SystemSettings.MinimizeToTray = SystemSettings.MinimizeToTrayDefault;
            SystemSettings.Multiple = SystemSettings.MultipleDefault;
            SystemSettings.RememberLocation = SystemSettings.RememberLocationDefault;
            SystemSettings.SpeedupEnable = SystemSettings.SpeedupEnableDefault;

            SystemSettings.VideoOpenFrame = SystemSettings.VideoOpenFrameDefault;

            SystemSettings.CacheVideo = SystemSettings.CacheVideoDefault;
            SystemSettings.CacheGreed = SystemSettings.CacheGreedDefault;
            
            if (SystemSettings.GetLink.Length > 0)
            {
                AdminAppMainSetLink adminAppMainSetLink = new AdminAppMainSetLink(null, AppLink.GetAllLinkList);
                if (adminAppMainSetLink.OpenAssistExe("SetLink"))
                {
                    SystemSettings.SetLink = null;
                }
            }

        }
        private void Frm_Move(object sender, MouseEventArgs e)
        {
            setting.FrmMove(sender, e);
        }

    }
}

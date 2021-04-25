using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using TPlayerSupport;

namespace TPlayer.Frm
{
    public partial class SettingP4 : UserControl
    {
        List<FileSizeTemp> sizetemps = new List<FileSizeTemp>();
        List<FileTemp> temps = new List<FileTemp>();
        List<DirTemp> dirtemps = new List<DirTemp>();
        Setting setting;
        public SettingP4(Setting setting)
        {
            this.setting = setting;
            InitializeComponent();

            ScanTemp(new DirectoryInfo(Program.BasePath + "log"), "log");
            ScanTemp(new DirectoryInfo(Program.UpdatePath), "temp");
            ScanTemp(new DirectoryInfo(Program.TempPath), "temp");

            ScanTemp(new DirectoryInfo(Program.DownLoadPath), "download");
            ScanTemp(new DirectoryInfo(Program.CachePath + "coll\\"), "coll");
            ScanTemp(new DirectoryInfo(Program.CachePath + "video\\"), "video");
            ScanTemp(new DirectoryInfo(Program.CachePath + "sub\\"), "sub");

            foreach (FileTemp item in temps)
            {
                FileSizeTemp fileTemp = sizetemps.Find(ab => ab.type == item.type);
                if (fileTemp != null)
                {
                    fileTemp.size += item.size;
                    fileTemp.paths.Add(item.path);
                }
                else
                {
                    sizetemps.Add(new FileSizeTemp
                    {
                        type = item.type,
                        size = item.size,
                        paths = new List<string> { item.path }
                    });
                }
            }

            sizetemps.Sort((x, y) =>
            {
                return y.size.CompareTo(x.size);
            });
            double total = temps.Sum(ab => ab.size);
            btn.ValueMiddle = total;
            label2.Text = "总共扫描出 " + total.CountSize();

            foreach (FileSizeTemp item in sizetemps)
            {
                switch (item.type)
                {
                    case "log":
                        AddCom(item, false, "日志文件", "TPlayer运行中的日志", total, Properties.Resources.filetype_log);
                        break;
                    case "temp":
                        AddCom(item, true, "临时文件", "清除不影响的数据垃圾", total, Properties.Resources.filetype_temp);
                        break;
                    case "coll":
                        AddCom(item, false, "记忆文件", "缓存视频集数和播放进度", total, Properties.Resources.filetype_list);
                        break;
                    case "download":
                        AddCom(item, false, "视频文件", "已下载的视频文件", total, Properties.Resources.filetype_videod);
                        break;
                    case "cache":
                        AddCom(item, false, "播放缓存", "缓存未完成的视频文件", total, Properties.Resources.filetype_videoc);
                        break;
                    case "video":
                        AddCom(item, false, "视频缓存", "缓存已完成的视频文件", total, Properties.Resources.filetype_video);
                        break;
                    case "sub":
                        AddCom(item, true, "字幕缓存", "在线下载的字幕文件", total, Properties.Resources.filetype_zip);
                        break;
                    case "subs":
                        AddCom(item, false, "字幕文件", "在线缓存的字幕文件", total, Properties.Resources.filetype_sub);
                        break;
                }
            }
            //Scan(Program.CachePath + "video\\");
            //Scan(Program.CachePath + "sub\\");

            btn.Click += (a, b) =>
            {
                btn.Enabled = btn.IsActive = panel1.Enabled = false;
                Action _action = () =>
                {
                    foreach (FileSizeTemp item in selsizeTemp)
                    {
                        List<string> okfile = new List<string>();
                        foreach (string items in item.paths)
                        {
                            FileInfo fileInfo = new FileInfo(items);
                            try
                            {
                                long size = fileInfo.Length;
                                fileInfo.Delete();
                                okfile.Add(items);
                                item.size -= size;
                                item.prog.Invoke(new Action(() =>
                                {
                                    item.prog.Value = item.size;
                                }));
                            }
                            catch { }
                        }
                        if (okfile.Count > 0)
                        {
                            item.paths.RemoveAll(ab => okfile.Contains(ab));

                            List<DirTemp> dirs = dirtemps.FindAll(ab => ab.type == item.type);
                            int errCount = 0;
                            while (dirs.Count > 0)
                            {
                                for (int i = 0; i < dirs.Count; i++)
                                {
                                    try
                                    {
                                        Directory.Delete(dirs[i].path);
                                        dirs.Remove(dirs[i]);
                                        i--;
                                    }
                                    catch { }
                                }
                                errCount++;
                                if (errCount > 3)
                                {
                                    break;
                                }
                            }


                            switch (item.type)
                            {
                                case "log":
                                    try
                                    {
                                        Directory.Delete(Program.BasePath + "log");
                                    }
                                    catch { }
                                    break;
                                case "temp":
                                    try
                                    {
                                        Directory.Delete(Program.UpdatePath);
                                    }
                                    catch { }
                                    try
                                    {
                                        Directory.Delete(Program.TempPath);
                                    }
                                    catch { }
                                    break;
                                case "coll":
                                    try
                                    {
                                        Directory.Delete(Program.CachePath + "coll\\");
                                    }
                                    catch { }
                                    break;
                                case "cache":
                                    try
                                    {
                                        Directory.Delete(Program.CachePath);
                                    }
                                    catch { }
                                    break;
                                case "video":
                                    try
                                    {
                                        Directory.Delete(Program.CachePath + "video\\");
                                    }
                                    catch { }
                                    break;
                                case "sub":

                                    try
                                    {
                                        Directory.Delete(Program.CachePath + "sub\\");
                                    }
                                    catch { }
                                    break;
                                case "subs":
                                    try
                                    {
                                        Directory.Delete(Program.CachePath + "sub\\");
                                    }
                                    catch { }
                                    break;
                            }


                            item._size.Invoke(new Action(() =>
                            {
                                item._size.Text = item.size.CountSize();
                            }));
                        }
                    }
                };
                _task.ContinueWhenAll(new Task[] { _task.StartNew(_action) }, (action =>
                {
                    double tsum = selsizeTemp.Sum(ab => ab.size);
                    this.Invoke(new Action(() =>
                    {
                        panel1.Enabled = true;

                        btn.Value = tsum;
                        if (tsum > 0)
                        {
                            btn.Text = "释放空间 （" + tsum.CountSize() + "）";
                        }
                        else
                        {
                            btn.Text = "释放空间";
                        }

                        btn.Enabled = btn.IsActive = tsum > 0;
                    }));
                }));
            };
        }
        TaskFactory _task = new TaskFactory();
        List<FileSizeTemp> selsizeTemp = new List<FileSizeTemp>();
        void AddCom(FileSizeTemp item, bool check, string title, string desc, double total, Image img)
        {
            Label panel3 = new Label
            {
                Dock = DockStyle.Top,
                Size = new Size(0, 50)
            };
            CheckBox checkBox = new CheckBox
            {
                Dock = DockStyle.Left,
                Size = new Size(30, 50),
                TextAlign = ContentAlignment.MiddleCenter
            };
            Label label6 = new Label
            {
                AutoEllipsis = true,
                Dock = DockStyle.Fill,
                Font = new Font("微软雅黑", 9F),
                ForeColor = Color.DimGray,
                Text = desc,
                TextAlign = ContentAlignment.MiddleLeft
            };
            item.prog = new TSkin.TProg
            {
                Dock = DockStyle.Top,
                MaxValue = total,
                Size = new Size(0, 6),
                Value = item.size,
            };
            Panel panel5 = new Panel
            {
                Dock = DockStyle.Top,
                Size = new Size(0, 28),
            };

            LinkLabel label4 = new LinkLabel
            {
                AutoEllipsis = true,
                LinkBehavior = LinkBehavior.HoverUnderline,
                LinkColor = Color.Black,
                VisitedLinkColor = Color.Black,
                ActiveLinkColor = Color.Black,
                Dock = DockStyle.Fill,
                Text = title,
                TextAlign = ContentAlignment.MiddleLeft
            };
            item._size = new Label
            {
                AutoEllipsis = true,
                Dock = DockStyle.Right,
                ForeColor = Color.DimGray,
                Size = new Size(100, 28),
                Text = item.size.CountSize(),
                TextAlign = ContentAlignment.MiddleRight,
            };

            panel5.Controls.Add(label4);
            panel5.Controls.Add(item._size);

            PictureBox panel_img = new PictureBox
            {
                Dock = DockStyle.Left,
                Size = new Size(50, 50),
            };
            PictureBox pic = new PictureBox
            {
                Image = img,
                Dock = DockStyle.Left,
                SizeMode = PictureBoxSizeMode.Zoom,
                Location = new Point(11, 11),
                Size = new Size(28, 28),
            };
            panel_img.Controls.Add(pic);


            panel3.Controls.Add(label6);
            panel3.Controls.Add(item.prog);
            panel3.Controls.Add(panel5);
            panel3.Controls.Add(panel_img);
            panel3.Controls.Add(checkBox);

            panel3.Controls.Add(new PictureBox
            {
                Dock = DockStyle.Right,
                Size = new Size(10, 0),
            });

            panel1.Controls.Add(panel3);

            checkBox.CheckedChanged += (a, b) =>
            {
                if (checkBox.Checked)
                {
                    selsizeTemp.Add(item);
                }
                else
                {
                    selsizeTemp.Remove(item);

                }
                double tsum = selsizeTemp.Sum(ab => ab.size);
                btn.Value = tsum;
                if (tsum > 0)
                {
                    btn.Text = "释放空间 （" + tsum.CountSize() + "）";
                }
                else
                {
                    btn.Text = "释放空间";
                }
                btn.Enabled = btn.IsActive = tsum > 0;
            };

            label4.LinkClicked += (a, b) =>
            {
                new UIListFile(item.paths).ShowDialog();
            };
            panel3.BringToFront();
            if (check)
            {
                checkBox.Checked = true;
            }
        }
        protected override void OnLoad(EventArgs e)
        {
            DriveInfo[] drives = DriveInfo.GetDrives();
            foreach (DriveInfo drive in drives)
            {
                if (Program.BasePath.StartsWith(drive.Name))
                {
                    btn.MaxValue = drive.TotalFreeSpace;
                }
            }

            base.OnLoad(e);
        }

        void ScanTemp(DirectoryInfo dir, string type)
        {
            //List<TFileInfo> files = dir.FullName.ScanPath();

            //foreach (TFileInfo item in files)
            //{
            //    if (type == "video")
            //    {
            //        if (item.FileName == "cache")
            //        {
            //            temps.Add(new FileTemp
            //            {
            //                path = item.FilePath,
            //                size = item.FileSize,
            //                type = "cache"
            //            });
            //        }
            //        else if (item.FileName.EndsWith("_ini"))
            //        {
            //            temps.Add(new FileTemp
            //            {
            //                path = item.FilePath,
            //                size = item.FileSize,
            //                type = "coll"
            //            });
            //        }
            //        else
            //        {
            //            temps.Add(new FileTemp
            //            {
            //                path = item.FilePath,
            //                size = item.FileSize,
            //                type = type
            //            });
            //        }
            //    }
            //    else
            //    {
            //        if (item.FilePath.StartsWith(Program.CachePath + "sub\\"))
            //        {
            //            temps.Add(new FileTemp
            //            {
            //                path = item.FilePath,
            //                size = item.FileSize,
            //                type = "subs"
            //            });
            //        }
            //        else
            //        {
            //            temps.Add(new FileTemp
            //            {
            //                path = item.FilePath,
            //                size = item.FileSize,
            //                type = type
            //            });
            //        }
            //    }
            //}
            //return;
            if (dir.Exists)
            {
                foreach (FileInfo item in dir.GetFiles())
                {
                    if (type == "video")
                    {
                        if (item.Name == "cache" || item.Name == "config.json")
                        {
                            temps.Add(new FileTemp
                            {
                                path = item.FullName,
                                size = item.Length,
                                type = "cache"
                            });
                        }
                        else if (item.Name.EndsWith("_ini"))
                        {
                            temps.Add(new FileTemp
                            {
                                path = item.FullName,
                                size = item.Length,
                                type = "coll"
                            });
                        }
                        else
                        {
                            temps.Add(new FileTemp
                            {
                                path = item.FullName,
                                size = item.Length,
                                type = type
                            });
                        }
                    }
                    else
                    {
                        temps.Add(new FileTemp
                        {
                            path = item.FullName,
                            size = item.Length,
                            type = type
                        });
                    }
                }
                foreach (DirectoryInfo item in dir.GetDirectories())
                {
                    dirtemps.Add(new DirTemp
                    {
                        path = item.FullName,
                        type = type
                    });
                    if (type == "sub")
                    {
                        ScanTemp(item, "subs");
                    }
                    else
                    {
                        ScanTemp(item, type);
                    }
                }
            }
        }

        private void Frm_Move(object sender, MouseEventArgs e)
        {
            setting.FrmMove(sender, e);
        }
        class FileTemp
        {
            public string type { get; set; }
            public double size { get; set; }
            public string path { get; set; }
        }

        class DirTemp
        {
            public string type { get; set; }
            public string path { get; set; }
        }
        class FileSizeTemp
        {
            public string type { get; set; }
            public double size { get; set; }
            public List<string> paths { get; set; }
            public List<string> dirs { get; set; }
            public TSkin.TProg prog { get; set; }
            public Label _size { get; set; }
        }
    }
}

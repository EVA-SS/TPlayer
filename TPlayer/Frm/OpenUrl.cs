using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace TPlayer
{
    public partial class OpenUrl : NetDimension.WinForm.ModernUIForm
    {
        [DllImport("User32.DLL")]
        public static extern int SendMessage(IntPtr hWnd, uint Msg, int wParam, int lParam);
        [DllImport("User32.DLL")]
        public static extern bool ReleaseCapture();
        public void FrmMove(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(Handle, 0x0112, 61456 | 2, 0);
        }
        public OpenUrl()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            string url = Clipboard.GetText();
            if (!string.IsNullOrEmpty(url) && !url.Contains("\n"))
            {
                if (url.Trim().StartsWith("http"))
                {
                    try
                    {
                        Uri uri = new Uri(url.Trim());
                        comboBox1.Text = uri.AbsoluteUri;
                    }
                    catch
                    {
                        if (url.Contains("\\/"))
                        {
                            try
                            {
                                Uri uri = new Uri(url.Replace("\\/", "/").Trim());
                                comboBox1.Text = uri.AbsoluteUri;
                            }
                            catch
                            {
                            }
                        }
                    }
                }
            }
            if (File.Exists(Program.BasePath + "url.log"))
            {
                string[] lists = File.ReadAllLines(Program.BasePath + "url.log");
                if (lists != null && lists.Length > 0)
                {
                    btn_clear.Visible = true;
                    for (int i = lists.Length - 1; i >= 0; i--)
                    {
                        comboBox1.Items.Add(lists[i]);
                    }
                }
            }
            //Cabinink.Windows.Privileges.PrivilegeGetter.NeedAdministratorsPrivilege();

            base.OnLoad(e);
        }

        #region 检测URL

        public void Time(object url)
        {
            try
            {
                Uri uri = new Uri(url.ToString());
                bool ok = GetBoolUrl(uri);
                System.Net.IPHostEntry host = System.Net.Dns.GetHostEntry(uri.Host);
                if (ok)
                {
                    btn_err.Invoke(new Action(() =>
                    {
                        btn_err.Tag = null;
                        btn_err.Image = Properties.Resources.icon_success;
                    }));
                }
                else
                {
                    bool isIpOk = false;
                    //double sa = Cabinink.Network.InternetDiagnosticsTool.Ping(uri.Host, 4, 100);
                    //if (sa > 0)
                    //{
                    //    isIpOk = true;
                    //}
                    //if (uri.HostNameType == UriHostNameType.IPv4)
                    //{
                    //    //var dsa = Cabinink.Network.InternetDiagnosticsTool.GetOutsideNetAddress();
                    //    IPAddress ip;
                    //    if (IPAddress.TryParse(uri.Host, out ip))
                    //    {
                    //        //获取本地的IP地址
                    //        List<string> AddressIP = new List<string>();
                    //        foreach (IPAddress _IPAddress in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
                    //        {
                    //            if (_IPAddress.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    //            {
                    //                AddressIP.Add(_IPAddress.ToString());
                    //            }
                    //        }
                    //        if (AddressIP.Count > 0)
                    //        {
                    //            string enIP;
                    //            if (AddressIP.Count > 1)
                    //            {
                    //                string ipF = AddressIP.Find(ab => ab.StartsWith("192.168"));
                    //                if (ipF != null)
                    //                {
                    //                    enIP = ipF;
                    //                }
                    //                else
                    //                {
                    //                    string ipF2 = AddressIP.Find(ab => ab.StartsWith("127."));
                    //                    if (ipF2 != null)
                    //                    {
                    //                        enIP = ipF2;
                    //                    }
                    //                    else
                    //                    {
                    //                        enIP = AddressIP[AddressIP.Count - 1];
                    //                    }
                    //                }
                    //            }
                    //            else
                    //            {
                    //                enIP = AddressIP[0];
                    //            }
                    //        }

                    //    }
                    //}
                    if (!isIpOk)
                    {
                        btn_err.Invoke(new Action(() =>
                        {
                            btn_err.Tag = uri;
                            btn_err.Image = Properties.Resources.icon_error;
                        }));
                    }
                    else
                    {
                        btn_err.Invoke(new Action(() =>
                        {
                            btn_err.Tag = uri;
                            btn_err.Image = Properties.Resources.icon_success;
                        }));
                    }
                }
            }
            catch
            {
                //tBut1.Invoke(new Action(() =>
                //{
                //    tBut1.Visible = false;
                //}));
            }
            try
            {
                btn_err.Invoke(new Action(() =>
            {
                btn_err.State = false;
            }));
            }
            catch
            {
            }
        }
        bool GetBoolUrl(Uri uri)
        {
            try
            {
                bool flag = false;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
                request.Method = "GET";
                request.Host = uri.Host;
                request.Timeout = 10000;
                request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/81.0.4044.26 Safari/537.36 Edg/81.0.416.16";
                request.Referer = uri.AbsoluteUri;
                request.Proxy = null;
                using (HttpWebResponse myResponse = (HttpWebResponse)request.GetResponse())
                {
                    flag = (myResponse.StatusCode == HttpStatusCode.OK);
                    return flag;
                }

            }
            catch
            {
                return false;
            }
        }

        #endregion

        public string Url { get; set; }
        private void btn_ok_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(comboBox1.Text))
            {
                Url = comboBox1.Text;
                if (File.Exists(Program.BasePath + "url.log"))
                {
                    string[] lists = File.ReadAllLines(Program.BasePath + "url.log");
                    if (lists != null && lists.Length > 0)
                    {
                        if (!lists.Contains(Url))
                        {
                            File.AppendAllLines(Program.BasePath + "url.log", new string[] { Url });
                        }
                    }
                    else
                    {
                        File.AppendAllLines(Program.BasePath + "url.log", new string[] { Url });
                    }

                }
                else
                {
                    File.AppendAllLines(Program.BasePath + "url.log", new string[] { Url });
                }
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                this.DialogResult = DialogResult.No;
            }
        }

        private void btn_no_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
            try
            {
                File.Delete(Program.BasePath + "url.log");
            }
            catch { }
            btn_clear.Visible = false;
        }

        Thread Tim = null;

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            string url = comboBox1.Text;
            if (!string.IsNullOrEmpty(url) && url.Length > 10 && !url.Contains("magnet:?xt=urn:btih:"))
            {
                if (Tim != null)
                {
                    try
                    {
                        Tim.Abort(); Tim = null;
                    }
                    catch { }
                }
                if (Tim == null)
                {
                    btn_err.State = true;
                    Tim = new Thread(Time);
                    Tim.IsBackground = true;
                    Tim.Start(url);
                }
            }
        }

        private void btn_err_Click(object sender, EventArgs e)
        {
            Control control = sender as Control;
            if (control.Tag != null)
            {
                Uri uri = control.Tag as Uri;
                if (uri.HostNameType == UriHostNameType.IPv4)
                {
                    //var dsa = Cabinink.Network.InternetDiagnosticsTool.GetOutsideNetAddress();
                    IPAddress ip;
                    if (IPAddress.TryParse(uri.Host, out ip))
                    {
                        ///获取本地的IP地址
                        List<string> AddressIP = new List<string>();
                        foreach (IPAddress _IPAddress in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
                        {
                            if (_IPAddress.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                            {
                                AddressIP.Add(_IPAddress.ToString());
                            }
                        }
                        if (AddressIP.Count > 0)
                        {
                            string enIP;
                            if (AddressIP.Count > 1)
                            {
                                string ipF = AddressIP.Find(ab => ab.StartsWith("192.168"));
                                if (ipF != null)
                                {
                                    enIP = ipF;
                                }
                                else
                                {
                                    string ipF2 = AddressIP.Find(ab => ab.StartsWith("127."));
                                    if (ipF2 != null)
                                    {
                                        enIP = ipF2;
                                    }
                                    else
                                    {
                                        enIP = AddressIP[AddressIP.Count - 1];
                                    }
                                }
                            }
                            else
                            {
                                enIP = AddressIP[0];
                            }


                        }

                    }
                }
            }
        }
    }
}

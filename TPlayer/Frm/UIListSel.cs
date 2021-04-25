using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TPlayer.Frm
{
    public partial class UIListSel : TSkin.Main
    {
        #region 窗口基本操作

        #region 调用非托管的动态链接库来让窗体可以拖动
        [DllImport("User32.DLL")]
        public static extern int SendMessage(IntPtr hWnd, uint Msg, int wParam, int lParam);
        [DllImport("User32.DLL")]
        public static extern bool ReleaseCapture();
        public void Frm_Move(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, 0x0112, 61456 | 2, 0);
            }
        }
        #endregion

        #endregion
        Label labels = null;
        public UIListSel(string[] data, string selvalue = null, string title = "选择")
        {
            InitializeComponent();
            panel5.Text = " " + title;
            for (int i = data.Length - 1; i > -1; i--)
            {
                int index = i;
                string item = data[i];
                Label label = new Label
                {
                    AutoEllipsis = true,
                    Size = new Size(0, 30),
                    Text = item,
                    Dock = DockStyle.Top,
                    TextAlign = ContentAlignment.MiddleLeft,
                };

                label.MouseEnter += (a, b) =>
                {
                    label.BackColor = Color.White;
                };
                label.MouseLeave += (a, b) =>
                {
                    label.BackColor = titleCheckControl1.BackColor;
                };
                label.Click += (a, b) =>
                {
                    SelIndex = index;
                    SelValue = item;
                    this.DialogResult = DialogResult.OK;
                };

                titleCheckControl1.Controls.Add(label);
                if (selvalue == item)
                {
                    labels = label;
                }
            }
            int height = data.Length * 30;
            if (height > 200) { height = 200; titleCheckControl1.AutoScroll = true; }
            this.Height = panel5.Height + height + btn_no.Height;


        }
        public UIListSel(List<string> data, string selvalue = null, string title = "选择")
        {
            InitializeComponent();
            panel5.Text = " " + title;
            for (int i = data.Count - 1; i > -1; i--)
            {
                int index = i;
                string item = data[i];
                Label label = new Label
                {
                    AutoEllipsis = true,
                    Size = new Size(0, 30),
                    Text = item,
                    Dock = DockStyle.Top,
                    TextAlign = ContentAlignment.MiddleLeft,
                };
                if (File.Exists(item))
                {
                    label.Text = Path.GetFileName(item);
                }
                label.MouseEnter += (a, b) =>
                {
                    label.BackColor = Color.White;
                };
                label.MouseLeave += (a, b) =>
                {
                    label.BackColor = titleCheckControl1.BackColor;
                };
                label.Click += (a, b) =>
                {
                    SelIndex = index;
                    SelValue = item;
                    this.DialogResult = DialogResult.OK;
                };

                titleCheckControl1.Controls.Add(label);
                if (selvalue == item)
                {
                    labels = label;
                }
            }
            int height = data.Count * 30;
            if (height > 200) { height = 200; titleCheckControl1.AutoScroll = true; }
            this.Height = panel5.Height + height + btn_no.Height;

        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (labels != null)
            {
                labels.Font = new Font(labels.Font, FontStyle.Bold);
                labels.ForeColor = Color.FromArgb(26, 173, 25);

                TaskFactory _task = new TaskFactory();

                Action _action = () =>
                {
                    System.Threading.Thread.Sleep(100);
                };
                _task.ContinueWhenAll(new Task[] { _task.StartNew(_action) }, (action =>
                {
                    labels.Invoke(new Action(() =>
                    {
                        labels.Focus();
                    }));
                    //Jxaiaike();
                }));
            }
        }
        public string SelValue = null; public int SelIndex = -1;
        private void btn_no_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace TPlayer.Frm
{
    public partial class UIListFile : TSkin.Main
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
        public UIListFile(List<string> data, string title = "选择")
        {
            InitializeComponent();
            panel5.Text = " " + title;
            for (int i = data.Count - 1; i > -1; i--)
            {
                int index = i;
                string item = data[i];
                LinkLabel label = new LinkLabel
                {
                    AutoEllipsis = true,
                    Dock = DockStyle.Top,
                    Size = new Size(0, 30),
                    Text = item,
                    //Dock = DockStyle.Fill,
                    TextAlign = ContentAlignment.MiddleLeft,
                    LinkBehavior = LinkBehavior.HoverUnderline,
                    LinkColor = Color.Black,
                    VisitedLinkColor = Color.Black,
                    ActiveLinkColor = Color.Black,
                };
                //CheckBox checkBox = new CheckBox
                //{
                //    Dock = DockStyle.Left,
                //    Size = new Size(30, 50),
                //    TextAlign = ContentAlignment.MiddleCenter
                //};
                //Panel panel5 = new Panel
                //{
                //    Dock = DockStyle.Top,
                //    Size = new Size(0, 30),
                //};
                //if (File.Exists(item))
                //{
                //    label.Text = Path.GetFileName(item);
                //}
                label.MouseEnter += (a, b) =>
                {
                    label.BackColor = Color.White;
                };
                label.MouseLeave += (a, b) =>
                {
                    label.BackColor = titleCheckControl1.BackColor;
                };
                label.LinkClicked += (a, b) =>
                {
                    label.Text.OpenExplorer();
                };

                //panel5.Controls.Add(label);
                //panel5.Controls.Add(checkBox);
                //panel5.Controls.Add(new PictureBox
                //{
                //    Dock = DockStyle.Left,
                //    Size = new Size(10, 0),
                //});
                titleCheckControl1.Controls.Add(label);
            }


            string maxTxt = "";
            foreach (string item in data)
            {
                if (item.Length > maxTxt.Length)
                {
                    maxTxt = item;
                }
            }

            int sizeWidth = TextRenderer.MeasureText(maxTxt, this.Font).Width + 10;

            if (sizeWidth < 340)
            {
                sizeWidth = 340;
            }

            int height = data.Count * 30;
            if (height > 200) { height = 200; titleCheckControl1.AutoScroll = true; }
            this.Size = new Size(sizeWidth, panel5.Height + height + btn_no.Height);

        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }
        public string SelValue = null; public int SelIndex = -1;
        private void btn_no_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
        }

    }
}

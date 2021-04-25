using System;
using System.Drawing;
using System.Windows.Forms;

namespace TPlayerFrm
{
    public partial class FrmSearch : UserControl
    {
        public FrmSearch()
        {
            InitializeComponent();
            btn_search.Image = TPlayer.FontAwesome.GetImage("4FA7", 30, Color.White);
        }
        public string CueBannerText
        {
            get
            {
                return text_search.CueBannerText;
            }
            set
            {
                text_search.CueBannerText = value; ;
            }
        }

        protected override void OnBackColorChanged(EventArgs e)
        {
            base.OnBackColorChanged(e);
            text_search.BackColor = panel.BackColor;
        }
        protected override void OnForeColorChanged(EventArgs e)
        {
            base.OnForeColorChanged(e);
            text_search.ForeColor = panel.ForeColor;
        }

        private void panel_MouseDown(object sender, MouseEventArgs e)
        {
            this.OnMouseDown(e);
        }
        public event StrEventHandler OnSearch;
        public delegate void StrEventHandler(string e);
        private void btn_search_Click(object sender, EventArgs e)
        {
            if (OnSearch != null)
            {
                OnSearch(text_search.Text);
            }
        }

        private void text_search_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                e.Handled = true;

                if (OnSearch != null)
                {
                    OnSearch(text_search.Text);
                }
            }
        }
    }
}

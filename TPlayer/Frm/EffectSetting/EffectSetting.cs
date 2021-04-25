using System;
using System.ComponentModel;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TPlayer
{
    public partial class EffectSetting : TSkin.Main
    {
        public TPlayer player;
        public EffectSetting(TPlayer player)
        {
            this.player = player;
            InitializeComponent();
            if (player.player.GetConfig(309) == "1")
            {
                b5.Visible = true;
            }
            if (player.player.GetConfig(2401) == "1")
            {
                b6.Visible = true;
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            player.LocationChanged -= Player_LocationSizeChanged;
            player.SizeChanged -= Player_LocationSizeChanged;
            player.effectSetting = null;
            player.Activate();
            base.OnClosing(e);
        }
        protected override void OnLoad(EventArgs e)
        {
            Player_LocationSizeChanged(this, e);
            player.LocationChanged += Player_LocationSizeChanged;
            player.SizeChanged += Player_LocationSizeChanged;
            p1 = new Frm.EffectSettingP1(this)
            {
                Dock = DockStyle.Fill
            };
            panel2.Controls.Add(p1);
            base.OnLoad(e);
        }
        public bool isOp = false;
        protected override void OnDeactivate(EventArgs e)
        {
            if (!isOp)
            {
                this.Close();
            }
            base.OnDeactivate(e);
        }

        void Player_LocationSizeChanged(object sender, EventArgs e)
        {
            this.Location = new Point(player.Left + player.Width - this.Width - 180 - ((60 - this.Width) / 2), player.Top + (player.Height - this.Height) - 60);
        }
        public Frm.EffectSettingP1 p1 = null;
        public Frm.EffectSettingP2 p2 = null;
        public Frm.EffectSettingP3 p3 = null;
        public Frm.EffectSettingP4 p4 = null;
        public Frm.EffectSettingP5 p5 = null;
        public Frm.EffectSettingP6 p6 = null;
        public Frm.EffectSettingP7 p7 = null;
        private void TypeChange(object sender, EventArgs e)
        {
            b0.IsActive = b1.IsActive = b2.IsActive = b3.IsActive = b4.IsActive = b5.IsActive = b6.IsActive = false;
            TSkin.TBut f = (TSkin.TBut)sender;
            f.IsActive = true;
            switch (f.Tag.ToString())
            {
                case "0":
                    if (p1 == null)
                    {
                        p1 = new Frm.EffectSettingP1(this)
                        {
                            Dock = DockStyle.Fill
                        };
                        panel2.Controls.Add(p1);
                    }
                    p1.BringToFront();
                    break;
                case "1":
                    if (p2 == null)
                    {
                        p2 = new Frm.EffectSettingP2(this)
                        {
                            Dock = DockStyle.Fill
                        };
                        panel2.Controls.Add(p2);
                    }
                    p2.BringToFront();
                    break;
                case "2":
                    if (p3 == null)
                    {
                        p3 = new Frm.EffectSettingP3(this)
                        {
                            Dock = DockStyle.Fill
                        };
                        panel2.Controls.Add(p3);
                    }
                    p3.BringToFront();
                    break;
                case "3":
                    if (p4 == null)
                    {
                        p4 = new Frm.EffectSettingP4(this)
                        {
                            Dock = DockStyle.Fill
                        };
                        panel2.Controls.Add(p4);
                    }
                    p4.BringToFront();
                    break;
                case "4":
                    if (p5 == null)
                    {
                        p5 = new Frm.EffectSettingP5(this)
                        {
                            Dock = DockStyle.Fill
                        };
                        panel2.Controls.Add(p5);
                    }
                    p5.BringToFront();
                    break;
                case "5":
                    if (p6 == null)
                    {
                        p6 = new Frm.EffectSettingP6(this)
                        {
                            Dock = DockStyle.Fill
                        };
                        panel2.Controls.Add(p6);
                    }
                    p6.BringToFront();
                    break;
                case "6":
                    if (p7 == null)
                    {
                        p7 = new Frm.EffectSettingP7(this)
                        {
                            Dock = DockStyle.Fill
                        };
                        panel2.Controls.Add(p7);
                    }
                    p7.BringToFront();
                    break;
            }
        }
    }
}

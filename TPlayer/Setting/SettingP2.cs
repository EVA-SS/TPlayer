using System;
using System.Drawing;
using System.Windows.Forms;

namespace TPlayer.Frm
{
    public partial class SettingP2 : UserControl
    {
        Setting setting;
        public SettingP2(Setting setting)
        {
            this.setting = setting;
            InitializeComponent();
            if (setting.player.backImage.BackgroundImage != null)
            {
                backImage.BackgroundImage = setting.player.backImage.BackgroundImage;
            }
            check_showlogo.Checked = SystemSettings.ShowLogo;
            check_showlogo.CheckedChanged += (a, b) =>
            {
                SystemSettings.ShowLogo = check_showlogo.Checked;
                setting.player.pictureBox1.Visible = setting.player.label2.Visible = !SystemSettings.ShowLogo;
            };
        }

        private void backImage_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd_file = "img".OpenFile())
            {
                if (ofd_file.ShowDialog(this) == DialogResult.OK)
                {
                    SystemSettings.BackImgUrl = ofd_file.FileName;
                    setting.player.backImage.BackgroundImage = backImage.BackgroundImage = Image.FromFile(ofd_file.FileName);
                }
            }
        }
        public void Resume()
        {
            setting.player.backImage.BackgroundImage = null;
            setting.player.pictureBox1.Visible = setting.player.label2.Visible = true;
            SystemSettings.ShowLogo = SystemSettings.ShowLogoDefault;
            SystemSettings.BackImgUrl = SystemSettings.BackImgUrlDefault;
        }
        private void Frm_Move(object sender, MouseEventArgs e)
        {
            setting.FrmMove(sender, e);
        }
    }
}

using System.Windows.Forms;

namespace TPlayer.Frm
{
    public partial class SettingP6 : UserControl
    {
        Setting setting;
        public SettingP6(Setting setting)
        {
            this.setting = setting;
            InitializeComponent();


            #region 读取设置

            check_3D.Checked = SystemSettings._3D;
            panel1.Enabled = tComboBox2.Enabled = SystemSettings._3D;
            switch (SystemSettings._3DMode)
            {
                case 1: tRadioButton1.Checked = true; break;
                case 2: tRadioButton3.Checked = true; break;
                case 3: tRadioButton2.Checked = true; break;
            }

            tComboBox2.SelectedIndex = SystemSettings._3DColor - 1;

            check_VREnable.Checked = SystemSettings.VR;

            #endregion

            #region 修改设置
            check_3D.CheckedChanged += (a, b) =>
            {
                SystemSettings._3D = check_3D.Checked;
                setting.player.player.SetConfig(308, SystemSettings._3D ? "1" : "0");
                panel1.Enabled = tComboBox2.Enabled = SystemSettings._3D;
            };

            tRadioButton1.Click += (a, b) =>
            {
                SystemSettings._3DMode = 1;
                setting.player.player.SetConfig(311, SystemSettings._3DMode.ToString());
            };
            tRadioButton3.Click += (a, b) =>
            {
                SystemSettings._3DMode = 2;
                setting.player.player.SetConfig(311, SystemSettings._3DMode.ToString());
            };
            tRadioButton2.Click += (a, b) =>
            {
                SystemSettings._3DMode = 3;
                setting.player.player.SetConfig(311, SystemSettings._3DMode.ToString());
            };
            tComboBox2.SelectedIndexChanged += (a, b) =>
            {
                SystemSettings._3DColor = tComboBox2.SelectedIndex + 1;
                setting.player.player.SetConfig(312, SystemSettings._3DColor.ToString());
            };

            check_VREnable.CheckedChanged += (a, b) =>
            {
                SystemSettings.VR = check_VREnable.Checked;
                setting.player.player.SetConfig(2401, SystemSettings.VR ? "1" : "0");
            };
            #endregion
        }
        public void Resume()
        {
            SystemSettings._3D = SystemSettings._3DDefault;
            SystemSettings._3DMode = SystemSettings._3DModeDefault;
            SystemSettings._3DColor = SystemSettings._3DColorDefault;

            SystemSettings.VR = SystemSettings.VRDefault;
        }
        private void Frm_Move(object sender, MouseEventArgs e)
        {
            setting.FrmMove(sender, e);
        }
    }
}

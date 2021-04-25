using System.Windows.Forms;

namespace TPlayer.Frm
{
    public partial class SettingP3 : UserControl
    {
        Setting setting;
        public SettingP3(Setting setting)
        {
            this.setting = setting;
            InitializeComponent();
        }
        public void Resume() { }
        private void Frm_Move(object sender, MouseEventArgs e)
        {
            setting.FrmMove(sender, e);
        }
    }
}

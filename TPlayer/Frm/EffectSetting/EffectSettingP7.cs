using System;
using System.Drawing;
using System.Windows.Forms;

namespace TPlayer.Frm
{
    /// <summary>
    /// 播放
    /// </summary>
    public partial class EffectSettingP7 : UserControl
    {
        EffectSetting effectSetting;
        public EffectSettingP7(EffectSetting effectSetting)
        {
            this.effectSetting = effectSetting;
            InitializeComponent();
            switch (effectSetting.player.player.GetConfig(2402))
            {
                case "0":
                    tBut1.IsActive = true;
                    break;
                case "1":
                    tBut2.IsActive = true;
                    break;
                case "3":
                    tBut3.IsActive = true;
                    break;
                case "4":
                    tBut4.IsActive = true;
                    break;
                case "7":
                    tBut5.IsActive = true;
                    break;
                case "9":
                    tBut6.IsActive = true;
                    break;
                case "11":
                    tBut7.IsActive = true;
                    break;
                case "15":
                    tBut8.IsActive = true;
                    break;
            }
        }

        private void ChangeVr(object sender, EventArgs e)
        {
            tBut1.IsActive = tBut2.IsActive = tBut3.IsActive = tBut4.IsActive = tBut5.IsActive = tBut6.IsActive = tBut7.IsActive = tBut8.IsActive = false;
            TSkin.TBut but = sender as TSkin.TBut;
            but.IsActive = true; 
            int state = Convert.ToInt32(but.Tag);
            SystemSettings.VRMode = state;
            effectSetting.player.player.SetConfig(2402, state.ToString());
            if (state == 0) { effectSetting.player.isVr = false; } else { effectSetting.player.isVr = true; }
        }
    }
}

using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace TPlayer.Frm
{
    /// <summary>
    /// 播放
    /// </summary>
    public partial class EffectSettingP1 : UserControl
    {
        EffectSetting effectSetting;
        public EffectSettingP1(EffectSetting effectSetting)
        {
            this.effectSetting = effectSetting;
            InitializeComponent();

            #region 播放
            //1-Overlay, 2-Renderless, 3-EVR, 4-EVRCP, 5-AVR

            switch (effectSetting.player.player.GetConfig(201))
            {
                case "1":
                    btn_rende1.Checked = true;
                    break;
                case "2":
                    btn_rende2.Checked = true;
                    break;
                case "3":
                    btn_rende3.Checked = true;
                    break;
                case "4":
                    btn_rende4.Checked = true;
                    break;
                case "5":
                    btn_rende5.Checked = true;
                    break;
            }

            #endregion
        }
        #region 播放

        private void chang_rende(object sender, EventArgs e)
        {
            TSkin.TRadioButton f = (TSkin.TRadioButton)sender;
            effectSetting.player.ShowPrompt("渲染模式", f.Text);
            effectSetting.player.player.SetConfig(201, f.Tag.ToString());
        }

        #endregion

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            effectSetting.isOp = true;
            if (!effectSetting.player.RepairCode())
            {
                effectSetting.isOp = false;
            }
        }
    }
}

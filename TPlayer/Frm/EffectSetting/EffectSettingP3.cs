using System;
using System.Drawing;
using System.Windows.Forms;

namespace TPlayer.Frm
{
    /// <summary>
    /// 色彩
    /// </summary>
    public partial class EffectSettingP3 : UserControl
    {
        EffectSetting effectSetting;
        public EffectSettingP3(EffectSetting effectSetting)
        {
            this.effectSetting = effectSetting;
            InitializeComponent();

            #region 色彩
            SC();
            if (effectSetting.player.player.GetConfig(213) == "1")
            {

                prog_brightness.ValueChange += (double value) =>
                {
                    effectSetting.player.player.SetConfig(214, ((int)value).ToString());
                    SC();
                };
                prog_contrast.ValueChange += (double value) =>
                {
                    effectSetting.player.player.SetConfig(215, ((int)value).ToString());
                    SC();
                };
                prog_saturation.ValueChange += (double value) =>
                {
                    effectSetting.player.player.SetConfig(216, ((int)value).ToString());
                    SC();
                };
                prog_hue.ValueChange += (double value) =>
                {
                    effectSetting.player.player.SetConfig(217, ((int)value).ToString());
                    SC();
                };
            }
            else
            {
                prog_brightness.DefaultColor = prog_saturation.DefaultColor = prog_contrast.DefaultColor = prog_hue.DefaultColor = Color.FromArgb(200, 0, 0, 0);
                label8.Enabled = label13.Enabled = label14.Enabled = btn_color1.Enabled = btn_color2.Enabled = btn_color3.Enabled = prog_brightness.Enabled = prog_saturation.Enabled = prog_contrast.Enabled = prog_hue.Enabled = false;
                label6.Visible = true;
                //b2.Enabled = p2.Enabled = false;
            }
            #endregion
        }


        #region 颜色

        void SC()
        {
            int Brightness = Convert.ToInt32(effectSetting.player.player.GetConfig(214));//亮度
            int Contrast = Convert.ToInt32(effectSetting.player.player.GetConfig(215));//对比度调节
            int Saturation = Convert.ToInt32(effectSetting.player.player.GetConfig(216));//饱和度调节
            int Hue = Convert.ToInt32(effectSetting.player.player.GetConfig(217));//色相调节
            label_brightness.Text = Brightness.ToString();
            prog_brightness.Value = Brightness;

            label_contrast.Text = Contrast.ToString();
            prog_contrast.Value = Contrast;

            label_saturation.Text = Saturation.ToString();
            prog_saturation.Value = Saturation;

            label_hue.Text = Hue.ToString();
            prog_hue.Value = Hue;

            label8.ForeColor = label13.ForeColor = label14.ForeColor = Color.White;
            if (Brightness == 50 && Contrast == 50 && Saturation == 50 && Hue == 50)
            {
                label8.ForeColor = Color.DodgerBlue;
            }
            else if (Brightness == 60 && (Saturation == 53 || Saturation == 54) && (Contrast == 52 || Contrast == 51) && (Hue == 50 || Hue == 51))
            {
                label13.ForeColor = Color.DodgerBlue;
            }
            else if (Brightness == 40 && (Saturation == 35 || Saturation == 36) && (Contrast == 45 || Contrast == 46) && Hue == 50)
            {
                label14.ForeColor = Color.DodgerBlue;
            }

        }
        private void btn_color1_Click(object sender, EventArgs e)
        {
            effectSetting.player.player.SetConfig(214, "50");
            effectSetting.player.player.SetConfig(215, "50");
            effectSetting.player.player.SetConfig(216, "50");
            effectSetting.player.player.SetConfig(217, "50");
            SC();
        }


        private void btn_color2_Click(object sender, EventArgs e)
        {
            effectSetting.player.player.SetConfig(214, "60");
            effectSetting.player.player.SetConfig(216, "54");
            effectSetting.player.player.SetConfig(215, "52");
            effectSetting.player.player.SetConfig(217, "51");
            SC();
        }
        private void btn_color3_Click(object sender, EventArgs e)
        {
            effectSetting.player.player.SetConfig(214, "40");
            effectSetting.player.player.SetConfig(216, "36");
            effectSetting.player.player.SetConfig(215, "46");
            effectSetting.player.player.SetConfig(217, "50");
            SC();
        }
        #endregion
    }
}

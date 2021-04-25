using System.Windows.Forms;

namespace TPlayer.Frm
{
    /// <summary>
    /// 字幕
    /// </summary>
    public partial class EffectSettingP6 : UserControl
    {
        EffectSetting effectSetting;
        public EffectSettingP6(EffectSetting effectSetting)
        {
            this.effectSetting = effectSetting;
            InitializeComponent();

            #region 3D

            if (effectSetting.player.player.GetConfig(310) == "1")
            {
                slideButton1.IsOn = true;
            }

            switch (effectSetting.player.player.GetConfig(311))
            {
                case "1": tRadioButton1.Checked = true; break;
                case "2": tRadioButton3.Checked = true; break;
                case "3": tRadioButton2.Checked = true; break;
            }
            int _index;
            if (int.TryParse(effectSetting.player.player.GetConfig(312), out _index))
            {
                tComboBox2.SelectedIndex = _index;
            }

            slideButton1.OnClick += (a) =>
            {
                effectSetting.player.player.SetConfig(310, a ? "1" : "0");
            };

            tRadioButton1.Click += (a, b) =>
            {
                SystemSettings._3DMode = 1;
                effectSetting.player.player.SetConfig(311, SystemSettings._3DMode.ToString());
            };
            tRadioButton3.Click += (a, b) =>
            {
                SystemSettings._3DMode = 2;
                effectSetting.player.player.SetConfig(311, SystemSettings._3DMode.ToString());
            };
            tRadioButton2.Click += (a, b) =>
            {
                SystemSettings._3DMode = 3;
                effectSetting.player.player.SetConfig(311, SystemSettings._3DMode.ToString());

            };
            tComboBox2.SelectedIndexChanged += (a, b) =>
            {
                SystemSettings._3DColor = tComboBox2.SelectedIndex + 1;
                effectSetting.player.player.SetConfig(312, SystemSettings._3DColor.ToString());
            };
            #endregion
        }




    }
}

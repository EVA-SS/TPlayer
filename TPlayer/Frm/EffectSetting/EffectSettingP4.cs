using System;
using System.Windows.Forms;
using TPlayerSupport;

namespace TPlayer.Frm
{
    /// <summary>
    /// 声音
    /// </summary>
    public partial class EffectSettingP4 : UserControl
    {
        EffectSetting effectSetting;
        public EffectSettingP4(EffectSetting effectSetting)
        {
            this.effectSetting = effectSetting;
            InitializeComponent();

            #region 声音

            if (effectSetting.player.player.GetConfig(401) == "1")
            {
                switch (effectSetting.player.player.GetConfig(404).ToInt())
                {
                    case 0:
                        btn_sound_channel1.Checked = true;
                        break;
                    case 1:
                        btn_sound_channel2.Checked = true;
                        break;
                    case 2:
                        btn_sound_channel3.Checked = true;
                        break;
                    case 3:
                        btn_sound_channel4.Checked = true;
                        break;
                }
                change_track();
                tong_Num = effectSetting.player.player.GetConfig(405).ToInt();
                string tong_Txt = (tong_Num / 1000).ToString();
                tong.Text = tong_Txt;
                tong.TextChanged += tong_TextChanged;
                tong.KeyPress += tong_KeyPress;
                tong1.Click += tong1_Click;
                tong2.Click += tong2_Click;
                tBut1.Click += tong3_Click;
            }
            else
            {
                panel2.Enabled = btn_add_track.Enabled = tCom_track.Enabled = tBut1.Enabled = tong.Enabled = tong1.Enabled = tong2.Enabled = false;
                label24.Visible = false;
            }

            #endregion
        }



        #region 声音

        void change_track()
        {
            tCom_track.SelectedIndexChanged -= TComboBox_SelectedIndexChanged;
            tCom_track.Items.Clear();
            string AudioTrackList = effectSetting.player.player.GetConfig(402);
            if (!string.IsNullOrEmpty(AudioTrackList))
            {
                int AudioTrackListCurrent = Convert.ToInt32(effectSetting.player.player.GetConfig(403));
                string[] splitAudioTrackList = AudioTrackList.Split(';');
                for (int i = 0; i < splitAudioTrackList.Length; i++)
                {
                    tCom_track.Items.Add(splitAudioTrackList[i]);
                    if (i == AudioTrackListCurrent)
                    {
                        tCom_track.Text = splitAudioTrackList[i];
                    }
                }
                tCom_track.SelectedIndexChanged += TComboBox_SelectedIndexChanged;
            }
        }

        /// <summary>
        /// 改变声道
        /// </summary>
        private void change_sound_channel(object sender, EventArgs e)
        {
            TSkin.TRadioButton f = (TSkin.TRadioButton)sender;
            int index = f.Tag.ToInt();
            effectSetting.player.player.SetConfig(404, index.ToString());
            effectSetting.player.ShowPrompt("音道模式", f.Text);
            effectSetting.player.RefreshMeunPlayerByAudioChannel(index);
        }

        /// <summary>
        /// 修改音轨
        /// </summary>
        private void TComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = tCom_track.SelectedIndex;
            if (!isyin)
            {
                effectSetting.player.player.SetConfig(403, index.ToString());
            }
            effectSetting.player.ShowPrompt("当前音轨", tCom_track.Items[index].ToString());
            effectSetting.player.RefreshMeunByAudioTrack(index);
        }
        bool isyin = false;

        /// <summary>
        /// 添加音轨
        /// </summary>
        private void btn_add_track_Click(object sender, EventArgs e)
        {
            effectSetting.isOp = true;
            using (OpenFileDialog ofd_file = "video".OpenFile())
            {
                if (ofd_file.ShowDialog(this) == DialogResult.OK)
                {
                    isyin = true;
                    effectSetting.player.player.SetConfig(409, ofd_file.FileName);
                    change_track();
                    isyin = false;
                }

                this.Focus();
                effectSetting.isOp = false;
                effectSetting.player.RefreshMeunByAudioTrack();
            }
        }

        #region 同步
        int tong_Num = 0;
        private void tong_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(tong.Text))
                {
                    effectSetting.player.player.SetConfig(405, "0");
                }
                else
                {
                    tong_Num = (int)(Math.Round(Convert.ToDouble(tong.Text), 3) * 1000);
                    effectSetting.player.player.SetConfig(405, tong_Num.ToString());
                    string tong_Txt = ((tong_Num * 1.0) / 1000).ToString();
                    effectSetting.player.ShowPrompt("声音同步", tong_Txt == "0" ? "正常" : tong_Txt);
                }
            }
            catch { }
        }
        private void tong_KeyPress(object sender, KeyPressEventArgs e)
        {
            int _int = (int)e.KeyChar;
            if ((_int < 48 || _int > 57) && _int != 8 && _int != 46)
                e.Handled = true;
            if (_int == 46)
            {
                if (tong.Text.Length <= 0)
                    e.Handled = true;   //小数点不能在第一位
                else
                {
                    float f;
                    float oldf;
                    bool b1 = false, b2 = false;
                    b1 = float.TryParse(tong.Text, out oldf);
                    b2 = float.TryParse(tong.Text + e.KeyChar.ToString(), out f);
                    if (b2 == false)
                    {
                        if (b1 == true)
                            e.Handled = true;
                        else
                            e.Handled = false;
                    }
                }
            }
        }

        private void tong1_Click(object sender, EventArgs e)
        {
            try
            {
                tong_Num += 500;
                string tong_Txt = ((tong_Num * 1.0) / 1000).ToString();
                effectSetting.player.player.SetConfig(405, tong_Num.ToString());
                effectSetting.player.ShowPrompt("声音同步", tong_Txt == "0" ? "正常" : tong_Txt);
                tong.Text = tong_Txt;
            }
            catch
            {
            }
        }

        private void tong2_Click(object sender, EventArgs e)
        {
            try
            {
                tong_Num -= 500;
                string tong_Txt = ((tong_Num * 1.0) / 1000).ToString();
                effectSetting.player.player.SetConfig(405, tong_Num.ToString());
                effectSetting.player.ShowPrompt("声音同步", tong_Txt == "0" ? "正常" : tong_Txt);
                tong.Text = tong_Txt;
            }
            catch
            {
            }
        }
        private void tong3_Click(object sender, EventArgs e)
        {
            try
            {
                tong_Num = 0;
                string tong_Txt = "0";
                effectSetting.player.player.SetConfig(405, "0");
                effectSetting.player.ShowPrompt("声音同步", "正常");
                tong.Text = tong_Txt;
            }
            catch
            {
            }
        }
        #endregion

        #endregion
    }
}

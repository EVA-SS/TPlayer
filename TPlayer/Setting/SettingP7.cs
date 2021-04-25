using System;
using System.Collections.Generic;
using System.Windows.Forms;
using TPlayerSupport;

namespace TPlayer.Frm
{
    public partial class SettingP7 : UserControl
    {
        Setting setting;
        public SettingP7(Setting setting)
        {
            this.setting = setting;
            InitializeComponent();
            sliderBar_DownloadCacheCount.Points = new List<TSkin.SliderPoints> {
                new TSkin.SliderPoints(2048),
                new TSkin.SliderPoints(4096),
                new TSkin.SliderPoints(8192),
                new TSkin.SliderPoints(12288),
                new TSkin.SliderPoints(16384),
                new TSkin.SliderPoints(20480),
            };
            label8.Text += $"（{Environment.ProcessorCount / 2}）";


            sliderBar_DownloadCount.Value = SystemSettings.DownloadCount;
            sliderBar_DownloadTaskCount.Value = SystemSettings.DownloadTaskCount;
            sliderBar_DownloadCacheCount.Value = SystemSettings.DownloadCacheCount;
            sliderBar_DownloadRetryCount.Value = SystemSettings.DownloadRetryCount;
            sliderBar_DownloadTimeOut.Value = SystemSettings.DownloadTimeOut;


            label_DownloadCount.Text = SystemSettings.DownloadCount.ToString();
            label_DownloadTaskCount.Text = SystemSettings.DownloadTaskCount.ToString();
            label_DownloadCacheCount.Text = SystemSettings.DownloadCacheCount.CountSize(1);
            label_DownloadRetryCount.Text = SystemSettings.DownloadRetryCount.ToString();

            if (SystemSettings.DownloadTimeOut > 1000)
            {
                label_DownloadTimeOut.Text = Math.Round(SystemSettings.DownloadTimeOut * 1.0 / 1000, 1).ToString() + "s";
            }
            else
            {
                label_DownloadTimeOut.Text = SystemSettings.DownloadTimeOut.ToString() + "ms";
            }

            sliderBar_DownloadCount.ValueChange += (e) =>
            {
                SystemSettings.DownloadCount = e;
                label_DownloadCount.Text = e.ToString();
            };
            sliderBar_DownloadTaskCount.ValueChange += (e) =>
            {
                SystemSettings.DownloadTaskCount = e;
                label_DownloadTaskCount.Text = e.ToString();
            };
            sliderBar_DownloadCacheCount.ValueChange += (e) =>
            {
                SystemSettings.DownloadCacheCount = e;
                label_DownloadCacheCount.Text = e.CountSize(1);
            };
            sliderBar_DownloadRetryCount.ValueChange += (e) =>
            {
                SystemSettings.DownloadRetryCount = e;
                label_DownloadRetryCount.Text = e.ToString();
            };
            sliderBar_DownloadTimeOut.ValueChange += (e) =>
            {
                SystemSettings.DownloadTimeOut = e;
                if (e > 1000)
                {
                    label_DownloadTimeOut.Text = Math.Round(e * 1.0 / 1000, 1).ToString() + "s";
                }
                else
                {
                    label_DownloadTimeOut.Text = e.ToString() + "ms";
                }
            };
        }
        public void Resume()
        {
            SystemSettings.DownloadCount = SystemSettings.DownloadCountDefault;
            SystemSettings.DownloadTaskCount = SystemSettings.DownloadTaskCountDefault;
            SystemSettings.DownloadCacheCount = SystemSettings.DownloadCacheCountDefault;
            SystemSettings.DownloadRetryCount = SystemSettings.DownloadRetryCountDefault;
            SystemSettings.DownloadTimeOut = SystemSettings.DownloadTimeOutDefault;
        }
        private void Frm_Move(object sender, MouseEventArgs e)
        {
            setting.FrmMove(sender, e);
        }
    }
}

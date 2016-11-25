using System;
using Windows.UI.Xaml.Controls;
using Windows.ApplicationModel.DataTransfer;
using Windows.Media.Capture;
using Windows.Storage;
using Windows.Graphics.Imaging;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;
using Windows.Devices.Power;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上有介绍

namespace MySample.Pages.Services
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class AppLink : Page
    {
        public AppLink()
        {
            this.InitializeComponent();
        }
        DataPackage dataPackage = new DataPackage();

        private async void copy_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            DataPackageView dpv = Clipboard.GetContent();
            if (dpv.Contains(StandardDataFormats.Text)) 
            {
                string txt = await dpv.GetTextAsync();
                txt2.Text = txt;
            }
        }

        private void clone_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            dataPackage.RequestedOperation = DataPackageOperation.Copy;
            dataPackage.SetText(txt.Text);
            Clipboard.SetContent(dataPackage);
        }

        private void share_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            DataTransferManager dtm = DataTransferManager.GetForCurrentView();
            dtm.DataRequested += dtm_DataRequseted;
            if (txt.Text != null && txt.Text.Length != 0) 
                DataTransferManager.ShowShareUI();
        }

        private void dtm_DataRequseted(DataTransferManager sender, DataRequestedEventArgs args)
        {
            DataRequest request = args.Request;
            request.Data.SetText(txt.Text);
            request.Data.Properties.Title = "分享";
            request.Data.Properties.Description = "共享文本信息";
        }

        private async void photo_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            CameraCaptureUI captureUI = new CameraCaptureUI();
            captureUI.PhotoSettings.Format = CameraCaptureUIPhotoFormat.Jpeg;
            StorageFile photo = await captureUI.CaptureFileAsync(CameraCaptureUIMode.Photo);
            if (photo == null)
                return;
            IRandomAccessStream stream = await photo.OpenAsync(FileAccessMode.Read);
            BitmapDecoder decoder = await BitmapDecoder.CreateAsync(stream);
            SoftwareBitmap swb = await decoder.GetSoftwareBitmapAsync();
            SoftwareBitmap swb_BGRA8 = SoftwareBitmap.Convert(swb, BitmapPixelFormat.Bgra8, BitmapAlphaMode.Premultiplied);
            SoftwareBitmapSource bmpSource = new SoftwareBitmapSource();
            await bmpSource.SetBitmapAsync(swb_BGRA8);
            pic.Source = bmpSource;
        }

        private void Batter_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            var aggbat = Battery.AggregateBattery;
            var report = aggbat.GetReport();
            txt3.Text = "充电率 (mW): " + report.ChargeRateInMilliwatts.ToString();
            txt4.Text = "设计容量 (mWh): " + report.DesignCapacityInMilliwattHours.ToString();
            txt5.Text = "完全充满容量 (mWh): " + report.FullChargeCapacityInMilliwattHours.ToString();
            txt6.Text = "剩余容量 (mWh): " + report.RemainingCapacityInMilliwattHours.ToString();
            txt7.Text = ((report.RemainingCapacityInMilliwattHours / report.FullChargeCapacityInMilliwattHours) * 100).ToString() + "%";
        }
    }
}

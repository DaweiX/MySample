using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.Data.Pdf;
using Windows.Storage.Pickers;
using Windows.Storage;
using System;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上有介绍

namespace MySample.Pages.Utilities
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class PDFReader : Page
    {
        private PdfDocument pdfDoc;
        public PDFReader()
        {
            this.InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            LoadDoc();
        }

        private async void LoadDoc()
        {
            txt_page.Text = "1";
            var picker = new FileOpenPicker();
            picker.FileTypeFilter.Add(".pdf");
            StorageFile file = await picker.PickSingleFileAsync();
            if (file != null)
            {
                ring.Visibility = Visibility.Visible;
                try
                {
                    pdfDoc = await PdfDocument.LoadFromFileAsync(file);
                }
                catch(Exception err)
                {
                    txt_page.Text = err.Message;
                }
            }
            if (pdfDoc != null)
            {
                ring.Visibility = Visibility.Collapsed;
            }
        }
        private async void View()
        {
            uint pageNumber = System.Convert.ToUInt32(txt_page.Text);
            ring.Visibility = Visibility.Visible;
            uint pageindex = pageNumber - 1;
            using (PdfPage page = pdfDoc.GetPage(pageindex))
            {
                var stream = new InMemoryRandomAccessStream();
                //var option = new PdfPageRenderOptions();
                await page.RenderToStreamAsync(stream);
                BitmapImage src = new BitmapImage();
                viewPage.Source = src;
                await src.SetSourceAsync(stream);
            }
        }

        private void go_Click(object sender, RoutedEventArgs e)
        {
            View();
        }
    }
}

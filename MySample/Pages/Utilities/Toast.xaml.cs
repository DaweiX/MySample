using System;
using Windows.UI.Notifications;
using Windows.UI.Popups;
using Windows.UI.StartScreen;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上有介绍

namespace MySample.Pages.Utilities
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class Toast : Page
    {
        public Toast()
        {
            this.InitializeComponent();
            string[] modes = Enum.GetNames(typeof(ToastTemplateType));
            mode.ItemsSource = modes;
        }
        private void Sendtoast_Click(object sender, RoutedEventArgs e)
        {
            warn.Visibility = Visibility.Collapsed;
            var tmp = ToastNotificationManager.GetTemplateContent(M(m));
            var txtNodes = tmp.GetElementsByTagName("text");
            var imageNodes = tmp.GetElementsByTagName("image");
            if (!(txtNodes == null || txtNodes.Length == 0)) 
            {
                var txtnode = txtNodes[0];
                if (!(imageNodes == null || imageNodes.Length == 0))
                {
                    var imagenode = imageNodes[0];
                    if (imagenode != null)
                    {
                        var attr = imagenode.Attributes[1].NodeValue = "ms-appx:///Assets/Images/Van.png";
                    }
                }
                if (txt.Text.Length != 0)
                {
                    txtnode.InnerText = txt.Text + Environment.NewLine + "模板：" + Environment.NewLine + m;                
                    ToastNotification toast = new ToastNotification(tmp);
                    ToastNotificationManager.CreateToastNotifier().Show(toast);
                }
                else warn.Visibility = Visibility.Visible;
            }           
        }
        ToastTemplateType M(string a)
        {
            switch (a)
            {
                case "ToastImageAndText01":return ToastTemplateType.ToastImageAndText01;
                case "ToastImageAndText02": return ToastTemplateType.ToastImageAndText02;
                case "ToastImageAndText03": return ToastTemplateType.ToastImageAndText03;
                case "ToastImageAndText04": return ToastTemplateType.ToastImageAndText04;
                case "ToastText01":return ToastTemplateType.ToastText01;
                case "ToastText02": return ToastTemplateType.ToastText02;
                case "ToastText03": return ToastTemplateType.ToastText03;
                case "ToastText04": return ToastTemplateType.ToastText04;
                default:return ToastTemplateType.ToastImageAndText01;
            }
        }
        string m = "ToastImageAndText01";
        private void mode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            m = mode.SelectedItem.ToString();
        }

        private async void creat_Click(object sender, RoutedEventArgs e)
        {
            string tileID = "tlie_1";
            string displayName = "测试磁贴";
            string args = string.Format("clicked@{0:yyyy-MM-dd HH:mm:ss}", DateTime.Now);
            Uri logoUri = new Uri("ms-appx:///Assets/Square150x150Logo.scale-200.png");
            var size = TileSize.Square150x150;
            SecondaryTile tile = new SecondaryTile(tileID, displayName, args, logoUri, size);
            tile.VisualElements.ShowNameOnSquare150x150Logo = true;
            bool isCreated = await tile.RequestCreateAsync();
            if (isCreated)
            {
                await new MessageDialog("固定磁贴成功！").ShowAsync();
            }
        }

        private void Notify_Click(object sender, RoutedEventArgs e)
        {
            var TileNoti_tmp = TileUpdateManager.GetTemplateContent(TileTemplateType.TileSquare150x150Text03);
            var textNodes = TileNoti_tmp.GetElementsByTagName("text");
            textNodes[0].InnerText = "第一行";
            textNodes[1].InnerText = "第二行";
            textNodes[2].InnerText = "第三行";
            TileNotification tileNoti = new TileNotification(TileNoti_tmp);
            tileNoti.ExpirationTime = DateTime.Now.AddSeconds(7);
            TileUpdateManager.CreateTileUpdaterForApplication().Update(tileNoti);
        }
    }
}

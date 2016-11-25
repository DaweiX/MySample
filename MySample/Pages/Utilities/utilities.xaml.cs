using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上有介绍

namespace MySample.Pages.Utilities
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class utilities : Page
    {
        public utilities()
        {
            this.InitializeComponent();
        }
        private void Ani_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Animation));
        }

        private void Cov_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Convert));
        }

        private void Map_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Map));
        }

        private void Noti_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Toast));
        }

        private void Fit_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AutoFit));
        }
    }
}

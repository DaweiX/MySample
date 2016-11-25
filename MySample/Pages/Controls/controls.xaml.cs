using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上有介绍

namespace MySample.Pages.Controls
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class controls : Page
    {
        public controls()
        {
            this.InitializeComponent();
        }

        private void lst_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Lists));
        }

        private void con_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Ctrls));
        }
    }
}

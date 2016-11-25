using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上有介绍

namespace MySample.Pages.Services
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class Navigate : Page
    {
        public Navigate()
        {
            this.InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }

        private async void HyperlinkButton_Click(object sender, RoutedEventArgs e)
        {
            HyperlinkButton btn = sender as HyperlinkButton;
            string tag = btn.Tag as string;
            switch (tag)
            {
                case "b": await Launcher.LaunchUriAsync(new Uri("ms-settings-bluetooth:")); break;
                case "w": await Launcher.LaunchUriAsync(new Uri("ms-settings-wifi:")); break;
                case "p": await Launcher.LaunchUriAsync(new Uri("ms-settings-power:")); break;
                case "a": await Launcher.LaunchUriAsync(new Uri("ms-settings-airplanemode:")); break;
                case "l": await Launcher.LaunchUriAsync(new Uri("ms-settings-lock:")); break;
                case "m": StorageFolder music = KnownFolders.MusicLibrary;
                          await Launcher.LaunchFolderAsync(music); break;
            }
        }
    }
}

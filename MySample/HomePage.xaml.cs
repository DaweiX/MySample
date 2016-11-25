using System.Collections.Generic;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上有介绍

namespace MySample
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class HomePage : Page
    {
        List<BitmapImage> a = new List<BitmapImage>()
            {
                new BitmapImage {UriSource=new System.Uri("ms-appx:///Assets//Images//1.jpg",System.UriKind.Absolute)},
                new BitmapImage {UriSource=new System.Uri("ms-appx:///Assets//Images//2.jpg",System.UriKind.Absolute)},
                new BitmapImage {UriSource=new System.Uri("ms-appx:///Assets//Images//3.jpg",System.UriKind.Absolute)},
                new BitmapImage {UriSource=new System.Uri("ms-appx:///Assets//Images//4.jpg",System.UriKind.Absolute)},
                new BitmapImage {UriSource=new System.Uri("ms-appx:///Assets//Images//5.jpg",System.UriKind.Absolute)},
            };
        int i = 0;
        public HomePage()
        {
            this.InitializeComponent();           
            left.Source = a[0];
            center.Source = a[1];
            right.Source = a[2];
        }

        private void btn_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            switch(btn.Tag as string)
            {
                case "L":
                    {
                        i--;
                        if (i == -1)
                            i = 4;
                    }
                    break;
                case "R":
                    {
                        i++;
                        if (i == 5) 
                            i = 0;
                    }
                    break;
            }
            left.Source = a[i];
            if (i < 4)
            {
                center.Source = a[i + 1];
                if (i < 3)
                    right.Source = a[i + 2];
                else
                    right.Source = a[0];
            }
            else
            {
                center.Source = a[0];
                right.Source = a[1];
            }

            int index = i;
            Border dot = this.FindName("dot" + index) as Border;
            if (dot == null) return;
            for (int i = 0; i < 5; i++)
            {
                Border dots = this.FindName("dot" + i) as Border;
                dots.BorderBrush = new SolidColorBrush(Colors.Gray);
            }
            dot.BorderBrush = new SolidColorBrush(Colors.Green);
        }

        private void mouseOver(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            UIElement a = sender as UIElement;
            a.Opacity = 0.8;
        }

        private void mouseLeft(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            UIElement a = sender as UIElement;
            a.Opacity = 1;
        }
    }
}

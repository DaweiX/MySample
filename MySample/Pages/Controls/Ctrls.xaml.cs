using System;
using System.Collections.Generic;
using System.ComponentModel;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上有介绍

namespace MySample.Pages.Controls
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class Ctrls : Page
    {
        public Ctrls()
        {
            this.InitializeComponent();
            List<FlipItems> items = new List<FlipItems>();
            items.Add(new FlipItems { pic = "/Assets/Images/BigFourSummerHeat.png", txt = "这是第一张图片" });
            items.Add(new FlipItems { pic = "/Assets/Images/ColumbiaRiverGorge.png", txt = "这是第二张图片" });
            items.Add(new FlipItems { pic = "/Assets/Images/RunningDogPacificCity.png", txt = "这是第三张图片" });
            items.Add(new FlipItems { pic = "/Assets/Images/MilkyWayStHelensHikePurple.png", txt = "这是第四张图片" });
            items.Add(new FlipItems { pic = "/Assets/Images/OregonWineryNamaste.png", txt = "这是第五张图片" });
            this.DataContext = this;
            this.DataContext = mydata;
            flip.ItemsSource = items;
        }
        public class MyData:INotifyPropertyChanged
        {
            private string str;
            public string Str
            {
                get { return str; }
                set
                {
                    str = value;
                    OnpropertyChanged("Str");
                }
            }
            public event PropertyChangedEventHandler PropertyChanged;
            protected void OnpropertyChanged(string name)
            {
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        int n = 0;
        MyData mydata = new MyData { Str = "按钮" };
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            n++;
            mydata.Str = "这是第" + n + "次点击";
        }

        private void Begin_Click(object sender, RoutedEventArgs e)
        {
            if (mode1.IsChecked == true)
            {
                if(bar.IsChecked==true)
                {
                    progress_b.IsIndeterminate = false;
                    progress_b.Visibility = Visibility.Visible;
                    Go.Visibility = Visibility.Visible;
                }
                if (dot.IsChecked == true) 
                {
                    progress_b.IsIndeterminate = true;
                    progress_b.Visibility = Visibility.Visible;
                }                      
            }
            if (mode2.IsChecked == true)
                progress_r.Visibility = Visibility.Visible;
            progress_r.IsActive = true;
        }

        private void mode1_Click(object sender, RoutedEventArgs e)
        {
            dot.IsEnabled = true;
            bar.IsEnabled = true;
        }

        private void mode2_Click(object sender, RoutedEventArgs e)
        {
            dot.IsEnabled = false;
            bar.IsEnabled = false;
        }

        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            progress_r.IsActive = false;
            progress_r.Visibility = Visibility.Collapsed;
            progress_b.Visibility = Visibility.Collapsed;
        }

        async void Go_Click(object sender, RoutedEventArgs e)
        {
            progress_b.Value += 20;
            if (progress_b.Value == 100)
            {
                progress_b.Value = 0;
                await new MessageDialog("完成！").ShowAsync();
                Go.Visibility = Visibility.Collapsed;
            }
        }

        private void FlipView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var flip = sender as FlipView;
            if (flip == null) return;
            int index = flip.SelectedIndex;
            Border dot = this.FindName("dot" + index) as Border;
            if (dot == null) return;
            for (int i = 0; i < 5; i++)
            {
                Border dots = this.FindName("dot" + i) as Border;
                dots.BorderBrush = new SolidColorBrush(Colors.Gray);
            }
            dot.BorderBrush = new SolidColorBrush(Colors.Green);
        }
        private class FlipItems
        {
            public string pic { get; set; }
            public string txt { get; set; }
        }
        FlipItems flipItems = new FlipItems();
    }
}

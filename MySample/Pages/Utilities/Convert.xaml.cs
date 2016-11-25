using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上有介绍

namespace MySample.Pages.Utilities
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class Convert : Page
    {
        public Convert()
        {
            this.InitializeComponent();
            pic2.Visibility = (Visibility)convert(txt.Text.ToString());
        }

        public object convert(string a)
        {
            a = txt.Text.ToString();
            if (a != null && a.Length != 0)
            {
                return Visibility.Visible;
            }
            else
                return Visibility.Collapsed;
        }

        private void txt_TextChanged(object sender, TextChangedEventArgs e)
        {
            pic2.Visibility = (Visibility)convert(txt.Text.ToString());
        }
    }
    //public class BoolToVisibilityConverter : IValueConverter
    //{
    //    public object Convert(object value, Type targetType, object parameter, string language)
    //    {
    //        bool? b = (bool?)value;         //bool?类型：bool + null
    //        if (b == true) return Visibility.Visible;
    //        return Visibility.Collapsed;
    //    }
    //    public object ConvertBack(object value, Type targetType, object parameter, string language)
    //    {
    //        return null;
    //    }
    //}
    public class Aaaa : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            bool? b = (bool?)value;         //bool?类型：bool + null
            if (b == true) return Visibility.Visible;
            return Visibility.Collapsed;
        }
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return null;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上有介绍

namespace MySample.Pages.Viewpanels
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class Group : Page
    {
        public Group()
        {
            this.InitializeComponent();
            List<News> newsList = new List<News>();
            for (int i = 1; i <= 5; i++)
            {
                newsList.Add(new News { Category = "国际", Title = "示例新闻" + i.ToString(), Content = "内容" + i.ToString(), PublishDate = DateTime.Now.AddDays(i) });
            }
            for (int i = 6; i <=10; i++)
            {
                newsList.Add(new News { Category = "国内", Title = "示例新闻" + i.ToString(), Content = "内容" + i.ToString(), PublishDate = DateTime.Now.AddDays(i) });
            }
            for (int i = 11; i <=15; i++)
            {
                newsList.Add(new News { Category = "热榜", Title = "示例新闻" + i.ToString(), Content = "内容" + i.ToString(), PublishDate = DateTime.Now.AddDays(i) });
            }
            var groups = from n in newsList group n by n.Category;
            this.cvs.Source = groups;
        }
        public class News
        {
            public string Category { get; set; }
            public string Title { get; set; }
            public DateTime PublishDate { get; set; }
            public string Content { get; set; }
        }
    }
    public sealed class DateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            DateTime dt = (DateTime)value;
            return dt.ToString("yyyy-M-d");
        }
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return null;
        }
    }
}

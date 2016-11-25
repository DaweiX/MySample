using System;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.Web.Syndication;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上有介绍

namespace MySample.Pages.Web
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class RSS : Page
    {
        public RSS()
        {
            this.InitializeComponent();
            txtUri.Items.Add("http://blog.sina.com.cn/rss/yilinzazhi.xml  (意林)");
            txtUri.Items.Add("http://www.zhihu.com/rss （知乎）");
            txtUri.Items.Add("http://www.zreading.cn/feed （左岸）");
        }

        private void lvItems_ItemClick(object sender, ItemClickEventArgs e)
        {
            SyndicationItem feeditem = e.ClickedItem as SyndicationItem;
            if (feeditem != null)
            {
                if (feeditem.Content == null) 
                {
                    wv.NavigateToString(feeditem.Summary.Text);
                }
                else
                {
                    wv.NavigateToString(feeditem.Content.Text);
                }
            }
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SyndicationClient client = new SyndicationClient();
                SyndicationFeed feed = await client.RetrieveFeedAsync(new Uri(txtUri.SelectedItem.ToString().Split(' ')[0].Trim()));
                if (feed != null)
                    this.lvItems.ItemsSource = feed.Items;
            }
            catch(Exception ex)
            {
                await new MessageDialog(ex.Message.ToString()).ShowAsync();
                return;
            }
        }
    }
}

using System.Collections.Generic;
using System.Collections.ObjectModel;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上有介绍

namespace MySample.Pages.Controls
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class Lists : Page
    {
        List<Item> items = new List<Item>();                
        public ObservableCollection<Item> Items { get; set; }
        /// <summary>
        /// items:静态的list<Item>的实例
        /// Items:动态的ObservableCollection<Item>的实例
        /// Item:上述两者共用的数据类，包含属性
        /// </summary>
        public Lists()
        {
            this.InitializeComponent();
            Items = new ObservableCollection<Item>();
            for (int i = 1; i <= 20; i++)
            {
                items.Add(new Item { n = i.ToString() });
            }
            for (int i = 1; i <= 20; i++)
            {
                Items.Add(new Item { n = i.ToString() });
            }
            itemscontrol.ItemsSource = items;
            gridview.ItemsSource = Items;
            comboBox.ItemsSource = items;
            this.DataContext = this;
        }

        public class Item
        {
            public string n { get; set; }
        }

        private void more_Click(object sender,RoutedEventArgs e)
        {
            int count = Items.Count;
            for (int i = count + 1; i < count + 6; i++) 
            {
                Items.Add(new Item { n = i.ToString() });
            }   
        }

        private void ComboBox_DropDownClosed(object sender, object e)
        {
            Item item = comboBox.SelectedItem as Item;
            if (item != null) txt2.Text = "第" + item.n + "项被选中";
        }
    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Windows.UI.Xaml.Controls;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上有介绍

namespace MySample.Pages.Services
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>   
    public sealed partial class Binding : Page
    {
        public class MyData
        {
            private string title;
            public string Title         //只有属性才能参与绑定
            {
                get { return title; }
                set { title = value; }
            }
        }
        public List<Lang> SampleLangs { get; set; }
        public ObservableCollection<NumModel> Nums = new ObservableCollection<NumModel>();
        public Binding()
        {
            this.InitializeComponent();
            this.DataContext = myData;

            SampleLangs = new List<Lang>();
            Lang item0 = new Lang { Name = "Visual Basic", IconURI = "/Assets/Listicon/vb.ico" };
            Lang item1 = new Lang { Name = "JavaScript", IconURI = "/Assets/Listicon/js.ico" };
            Lang item2 = new Lang { Name = "C Sharp", IconURI = "/Assets/Listicon/cs.ico" };
            SampleLangs.Add(item0);
            SampleLangs.Add(item1);
            SampleLangs.Add(item2);
            myListView.ItemsSource = SampleLangs;

            list.ItemsSource = Nums;
        }

        MyData myData = new MyData { Title = "数据绑定" };

        public class Lang
        {
            public string Name { get; set; }
            public string IconURI { get; set; }
        }

        public class NumModel
        {
            public string num { get; set; }
        }

        private void AddButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Random randon = new Random();
            Nums.Add(new NumModel { num = randon.Next(100).ToString() });
        }

        private void DelButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (list.SelectedItem != null)
            {
                NumModel nm = list.SelectedItem as NumModel;
                if (Nums.Contains(nm))
                    Nums.Remove(nm);
            }
        }

        private void Button_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Pages.Controls.controls));
        }
    }
}

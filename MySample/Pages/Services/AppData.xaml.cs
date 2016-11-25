using Windows.Storage;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using System;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上有介绍

namespace MySample.Pages.Services
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class AppData : Page
    {
        private ApplicationDataContainer _appSettings;  //声明容器实例
        public AppData()
        {
            this.InitializeComponent();
            _appSettings = ApplicationData.Current.LocalSettings;   //获取本地设置根容器
            BindKeyList();
        }

        private async void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtKey.Text))
            {
                _appSettings.Values[txtKey.Text] = txtValue.Text;
                BindKeyList();
            }
            else
            {
                await new MessageDialog("请输入Key值").ShowAsync();
            }
        }

        private void btnDel_Click(object sender, RoutedEventArgs e)
        {
            if (lstKeys.SelectedIndex > -1)     //有项被选中
            {
                _appSettings.Values.Remove(lstKeys.SelectedItem.ToString());
                BindKeyList();
            }
        }

        private void lstKeys_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                string key = e.AddedItems[0].ToString();
                if (_appSettings.Values.ContainsKey(key)) 
                {
                    txtKey.Text = lstKeys.SelectedItem.ToString();
                    txtValue.Text = _appSettings.Values[key].ToString();
                }
            }
        }
        private void BindKeyList()
        {
            lstKeys.Items.Clear();
            foreach (string key in _appSettings.Values.Keys) 
            {
                lstKeys.Items.Add(key);
            }
            txtKey.Text = "";
            txtValue.Text = "";
        }
    }
}

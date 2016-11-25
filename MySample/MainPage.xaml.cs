using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using System;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml.Media;
using Windows.Storage;
using Windows.UI.Core;
using Windows.UI.Xaml.Navigation;
using System.Collections.Generic;

//“空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409 上有介绍

namespace MySample
{ 
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private ApplicationDataContainer _appSettings;     
        public MainPage()
        {
            this.InitializeComponent();          
            fr.Navigate(typeof(HomePage));
            this.DataContext = this;
            //返回键（PC）
            SystemNavigationManager.GetForCurrentView().BackRequested += BackRequested;
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = mainFrame.CanGoBack ?
    AppViewBackButtonVisibility.Visible : AppViewBackButtonVisibility.Collapsed;
            mainFrame.Navigated += OnNavigated;

            _appSettings = ApplicationData.Current.LocalSettings;
            RequestedTheme = ElementTheme.Light;
            if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
            {
                Windows.Phone.UI.Input.HardwareButtons.BackPressed += OnBackPressed;
            }
            if (!(_appSettings.Values["_nighttheme"] == null))
            {
                night.IsOn = Convert.ToBoolean((_appSettings.Values["_nighttheme"]).ToString());
                if (_appSettings.Values["_nighttheme"].ToString() == "True") RequestedTheme = ElementTheme.Dark;
                else if (_appSettings.Values["_nighttheme"].ToString() == "False") RequestedTheme = ElementTheme.Light;
            }
            //if (!(_appSettings.Values["_topbar"] == null))
            //{
            //    Top.IsOn = Convert.ToBoolean((_appSettings.Values["_topbar"]).ToString());
            //    topShowOrHide();
            //}
            //else
            //{
            //    StatusBar sb = StatusBar.GetForCurrentView();
            //    sb.BackgroundColor = ((SolidColorBrush)bar.Background).Color;
            //    sb.BackgroundOpacity = 1;
            //}
            if (!(_appSettings.Values["_hamhide"] == null))     //应该可以用twoway绑定来设置开关状态
            {
                isHamHide.IsOn = Convert.ToBoolean((_appSettings.Values["_hamhide"]).ToString());
                if (_appSettings.Values["_hamhide"].ToString() == "True") ham.DisplayMode = SplitViewDisplayMode.CompactOverlay;
                else if (_appSettings.Values["_hamhide"].ToString() == "False") ham.DisplayMode = SplitViewDisplayMode.Overlay;
            }
            if (!(_appSettings.Values["_isfull"] == null))
            {
                var view = ApplicationView.GetForCurrentView();
                full.IsOn = Convert.ToBoolean((_appSettings.Values["_isfull"]).ToString());
                if (_appSettings.Values["_isfull"].ToString() == "True") view.TryEnterFullScreenMode();
                else if (_appSettings.Values["_isfull"].ToString() == "False") view.ExitFullScreenMode();
            }
            if (!(_appSettings.Values["_appback"] == null))
            {
                appback.IsOn = Convert.ToBoolean((_appSettings.Values["_appback"]).ToString());
            }
        }
        //async void topShowOrHide()
        //{
        //    if (_appSettings.Values["_topbar"].ToString() == "False")
        //    {
        //        await sb.ShowAsync();
        //        sb.BackgroundColor = ((SolidColorBrush)bar.Background).Color;
        //        sb.BackgroundOpacity = 1;
        //    }
        //    else if (_appSettings.Values["_topbar"].ToString() == "True")
        //    {
        //        await sb.HideAsync();
        //    }
        //}
        //StatusBar sb = StatusBar.GetForCurrentView();
        private void night_Toggled(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(night.IsOn.ToString()))
            {
                _appSettings.Values["_nighttheme"] = night.IsOn.ToString();
                if (_appSettings.Values["_nighttheme"].ToString() == "True") RequestedTheme = ElementTheme.Dark;
                else if (_appSettings.Values["_nighttheme"].ToString() == "False") RequestedTheme = ElementTheme.Light;
            }
        }

        //private async void top_Toggled(object sender, RoutedEventArgs e)
        //{
        //    if (!string.IsNullOrEmpty(Top.IsOn.ToString()))
        //    {
        //        _appSettings.Values["_topbar"] = Top.IsOn.ToString();

        //        if (_appSettings.Values["_topbar"].ToString() == "False")
        //        {
        //            await sb.ShowAsync();
        //            sb.BackgroundColor = ((SolidColorBrush)bar.Background).Color;
        //            sb.BackgroundOpacity = 1;
        //        }
        //        else if (_appSettings.Values["_topbar"].ToString() == "True")
        //        {
        //            await sb.HideAsync();
        //        }
        //    }
        //}

        private void isHamHide_Toggled(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(isHamHide.IsOn.ToString()))
            {
                _appSettings.Values["_hamhide"] = isHamHide.IsOn.ToString();
                if (_appSettings.Values["_hamhide"].ToString() == "True") ham.DisplayMode = SplitViewDisplayMode.CompactOverlay;
                else if (_appSettings.Values["_hamhide"].ToString() == "False")
                {
                    ham.DisplayMode = SplitViewDisplayMode.Overlay;
                    //子页面的显示问题
                }
            }               
        }
        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (i0.IsSelected && i0.FocusState == FocusState.Pointer) 
            {
                Frame.Navigate(typeof(MainPage));
                Title.Text = "UWP Samples";
                ham.IsPaneOpen = false;
            }
            if (i1.IsSelected)
            {
                mainFrame.Navigate(typeof(Pages.Viewpanels.viewpanels));
                Title.Text = "界面编程";
                ham.IsPaneOpen = false;                
            }
            if (i2.IsSelected)
            {
                mainFrame.Navigate(typeof(Pages.Controls.controls));
                Title.Text = "基础控件";
                ham.IsPaneOpen = false;
            }
            if (i3.IsSelected)
            {
                mainFrame.Navigate(typeof(Pages.Services.services));
                Title.Text = "应用服务";
                ham.IsPaneOpen = false;
            }
            if (i4.IsSelected)
            {
                mainFrame.Navigate(typeof(Pages.Utilities.utilities));
                Title.Text = "实用工具";
                ham.IsPaneOpen = false;
            }
            if (i5.IsSelected)
            {
                mainFrame.Navigate(typeof(Pages.Web.web));
                Title.Text = "网络编程";
                ham.IsPaneOpen = false;
            }           
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ham.IsPaneOpen = !ham.IsPaneOpen;
        }
   
        private async void btn_CS(object sender, RoutedEventArgs e)
        {
            string page;
            if (mainFrame.CurrentSourcePageType != null)  
            {
                page = mainFrame.CurrentSourcePageType.ToString();
            }
            else page = "Welcome To Back Home！";
            await new MessageDialog(page.ToString()).ShowAsync();
        }

        public void OnBackPressed(object sender, Windows.Phone.UI.Input.BackPressedEventArgs e)
        {
        //    //后退键逻辑
        //    if (mainFrame.Content != null) 
        //    {
        //        if (mainFrame.CanGoBack)
        //        {
        //            e.Handled = true;
        //            mainFrame.GoBack();
        //        }
        //    }
        }

        private void share_click(object sender, RoutedEventArgs e)
        {
                        
        }

        private void ListBox2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (i6.IsSelected)
            {
                App.Current.Exit();
            }
        }
        private void OnNavigated(object sender, NavigationEventArgs e)
        {
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = !string.IsNullOrEmpty(mainFrame.CurrentSourcePageType.ToString()) ?
                AppViewBackButtonVisibility.Visible : AppViewBackButtonVisibility.Collapsed;
        }
        private void BackRequested(object sender, BackRequestedEventArgs e)
        {
            if (mainFrame == null)
                return;
            if (e.Handled == false)
            {                
                if (mainFrame.CanGoBack)
                {
                    mainFrame.GoBack();
                    e.Handled = true;
                }                   
                else if(!mainFrame.CanGoBack)   
                {
                    this.Frame.Navigate(typeof(MainPage));  //问题：执行一次后，mainFrame的CanGoBack属性定为False
                    e.Handled = true;
                }
            }
        }

        private void alpha_Toggled(object sender, RoutedEventArgs e)
        {
           
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(typeof(HomePage));
        }
        List<string> myItems = new List<string>
            {
                "分组视图",
                "缩放分组",
                "列表",
                "控件",
                "导航",
                "绑定",
                "应用数据",
                "基本动画",
                "通知",
                "地图",
                "自适应布局",
                "转换器",
                "Get请求",
                "其他",
                "音乐",
                "PDF阅读器",
                "Rss阅读器"
            };
        private void SearchBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            char[] input = SearchBox.Text.ToCharArray();
            List<string> temp = new List<string>();
            foreach (var item in myItems)
            {
                foreach (var a in input)
                {
                    if (item.Contains(a.ToString()) && !temp.Contains(item)) 
                        temp.Add(item);
                }
            }
            if (temp.Count == 0)
                temp.Add("没有搜索结果");
            SearchBox.ItemsSource = temp;
            //分组
        }

        private void SearchBox_SuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
        {
            if (args.SelectedItem.ToString() != "没有搜索结果") 
                i0.IsSelected = false;
            SearchBox.Text = args.SelectedItem.ToString();
            switch(SearchBox.Text)
            {
                case "Rss阅读器":mainFrame.Navigate(typeof(Pages.Web.RSS));break;
                case "Get请求": mainFrame.Navigate(typeof(Pages.Web.WebGet)); break;
                case "转换器": mainFrame.Navigate(typeof(Pages.Utilities.Convert)); break;
                case "自适应布局": mainFrame.Navigate(typeof(Pages.Utilities.AutoFit)); break;
                case "基本动画": mainFrame.Navigate(typeof(Pages.Utilities.Animation)); break;
                case "通知": mainFrame.Navigate(typeof(Pages.Utilities.Toast)); break;
                case "地图": mainFrame.Navigate(typeof(Pages.Utilities.Map)); break;
                case "应用数据": mainFrame.Navigate(typeof(Pages.Services.AppData)); break;
                case "绑定": mainFrame.Navigate(typeof(Pages.Services.Binding)); break;
                case "导航": mainFrame.Navigate(typeof(Pages.Services.Navigate)); break;
                case "列表": mainFrame.Navigate(typeof(Pages.Controls.Lists)); break;
                case "控件": mainFrame.Navigate(typeof(Pages.Controls.Ctrls)); break;
                case "缩放分组": mainFrame.Navigate(typeof(Pages.Viewpanels.Zoom)); break;
                case "分组视图": mainFrame.Navigate(typeof(Pages.Viewpanels.Group)); break;
                case "其他": mainFrame.Navigate(typeof(Pages.Services.AppLink)); break;
                case "音乐": mainFrame.Navigate(typeof(Pages.Utilities.Music)); break;
                case "PDF阅读器": mainFrame.Navigate(typeof(Pages.Utilities.PDFReader)); break;
                default:SearchBox.Text = "";break;
            }
        }

        private void full_Toggled(object sender, RoutedEventArgs e)
        {
            var view = ApplicationView.GetForCurrentView();
            _appSettings.Values["_isfull"] = full.IsOn.ToString();
            if (_appSettings.Values["_isfull"].ToString() == "True") 
            {
                view.TryEnterFullScreenMode();
            }
            else
            {
                view.ExitFullScreenMode();
            }
        }

        private void appback_Toggled(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(appback.IsOn.ToString()))
            {
                _appSettings.Values["_appback"] = appback.IsOn.ToString();             
            }
        }
    }
}

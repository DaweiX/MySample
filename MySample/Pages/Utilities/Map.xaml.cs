using System;
using Windows.Devices.Geolocation;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上有介绍

namespace MySample.Pages.Utilities
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class Map : Page
    {
        Geolocator geolocator = null;
        public Map()
        {
            this.InitializeComponent();
            NavigationCacheMode = NavigationCacheMode.Required;
            geolocator = new Geolocator();
            //myMap.MapServiceToken = "";
        }

        private async void getlocation_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Geoposition pos = await geolocator.GetGeopositionAsync();
                myMap.Center = pos.Coordinate.Point;
                myMap.ZoomLevel = 5;
                txt_Latitude.Text = "经度：" + pos.Coordinate.Point.Position.Latitude;
                txt_Longitude.Text = "纬度：" + pos.Coordinate.Point.Position.Longitude;
                txt_Accuracy.Text = "准确性：" + pos.Coordinate.Accuracy + "m";
            }
            catch(Exception ex)
            {
                await new MessageDialog(ex.Message.ToString()).ShowAsync();
            }
        }
    }
}

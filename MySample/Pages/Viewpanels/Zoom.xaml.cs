using System.Linq;
using Windows.UI.Xaml.Controls;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上有介绍

namespace MySample.Pages.Viewpanels
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class Zoom : Page
    {
        public Zoom()
        {
            this.InitializeComponent();
            string[] data = {"A1","A2","A3","A4","A5","A6",
                             "B1","B2","B3","B4","B5","B6",
                             "C1","C2","C3","C4","C5","C6",
                             "D1","D2","D3","D4","D5","D6"};
            var groups = from s in data group s by s[0];
            this.cvsData.Source = groups;
        }
    }
}

using System;
using System.IO;
using System.Net;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上有介绍

namespace MySample.Pages.Web
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class WebGet : Page
    {
        public WebGet()
        {
            this.InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            //ring.IsActive = true;
            if (string.IsNullOrEmpty(sitename.Text.ToString()) == true)
            {
                await new MessageDialog("网址不能为空！").ShowAsync();
            } 
            else
            {
                WebRequest rq = WebRequest.Create(sitename.Text.ToString());
                rq.Method = "GET";
                rq.BeginGetResponse(RqCallBack, rq);
            }

        }

        private async void RqCallBack(IAsyncResult rs)
        {
            string info = null;
            try
            {
                HttpWebRequest httpWebRequest = (HttpWebRequest)rs.AsyncState;
                WebResponse webResponse = httpWebRequest.EndGetResponse(rs);
                using (Stream st = webResponse.GetResponseStream())
                using (StreamReader sr = new StreamReader(st))
                {
                    string content = sr.ReadToEnd();
                    await new MessageDialog(content).ShowAsync();
                }
            }
            catch(Exception e)
            {
                info = e.Message.ToString();
            }
            finally
            {
                if (!string.IsNullOrEmpty(info) && info != null) 
                    await new MessageDialog(info).ShowAsync();
            }
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            sitename.Text = "http://";
        }

        private async void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (txtInput.Text.Length == 0) return;
            Button btn = sender as Button;
            btn.IsEnabled = false;
            PhoneAdSV.MobileCodeWSSoapClient client = new PhoneAdSV.MobileCodeWSSoapClient();
            string rs = await client.getMobileCodeInfoAsync(txtInput.Text,"");
            txtResult.Text = rs;
            btn.IsEnabled = true;
        }

        private async void Button_Click_3(object sender, RoutedEventArgs e)
        {
            txtResult2.Text = "";
            if (txt1.Text.Length == 0) return;
            Button btn = sender as Button;
            btn.IsEnabled = false;
            TrainSV.TrainTimeWebServiceSoapClient client = new TrainSV.TrainTimeWebServiceSoapClient();
            string[] rs = await client.getStationAndTimeByTrainCodeAsync(txt1.Text,"");
            names.Text = Environment.NewLine +
                        "车次：" + Environment.NewLine +
                        "始发站：" + Environment.NewLine +
                        "终点站：" + Environment.NewLine +
                        "发车站：" + Environment.NewLine +
                        "发车时间：" + Environment.NewLine +
                        "到达站：" + Environment.NewLine +
                        "到达时间：" + Environment.NewLine +
                        "里程：" + Environment.NewLine +
                        "历时：" + Environment.NewLine;
            for (int i = 0; i < 9; i++)
            {
                txtResult2.Text += Environment.NewLine + rs[i].ToString();
            }
            btn.IsEnabled = true;
        }
        //无法关闭进度环，与异步操作的进程冲突有关。
    }
}

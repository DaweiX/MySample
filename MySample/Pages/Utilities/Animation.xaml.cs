using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上有介绍

namespace MySample.Pages.Utilities
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class Animation : Page
    {
        public Animation()
        {
            this.InitializeComponent();
            this.DataContext = this;
        }
        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            Storyboard Sty = new Storyboard();
            DoubleAnimation da = new DoubleAnimation();
            da.Duration = TimeSpan.FromSeconds(1);
            da.From = 1;
            da.To = 0;
            da.AutoReverse = true;
            Storyboard.SetTarget(da, sampleGrid);
            Storyboard.SetTargetProperty(da, "Opacity");
            Sty.Children.Add(da);
            Sty.Begin();
        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            sampleGrid.RenderTransform = new RotateTransform() { CenterX = 100, CenterY = 100 };
            Storyboard Sty = new Storyboard();
            DoubleAnimation da = new DoubleAnimation();
            da.Duration = TimeSpan.FromSeconds(1);
            da.By = 270;
            Storyboard.SetTarget(da, sampleGrid.RenderTransform);
            Storyboard.SetTargetProperty(da, "Angle");
            Sty.Children.Add(da);
            Sty.Begin();
        }

        private void Button3_Click(object sender, RoutedEventArgs e)
        {
            sampleGrid.RenderTransform = new ScaleTransform() { };
            Storyboard Sty = new Storyboard();
            DoubleAnimation da = new DoubleAnimation();
            da.Duration = TimeSpan.FromSeconds(0.4);
            da.By = -0.5;
            da.AutoReverse = true;
            Storyboard.SetTarget(da, sampleGrid.RenderTransform);
            Storyboard.SetTargetProperty(da, "ScaleX");
            Sty.Children.Add(da);
            Sty.Begin();
        }

        private void Button4_Click(object sender, RoutedEventArgs e)
        {
            sampleGrid.RenderTransform = new TranslateTransform();
            Storyboard Sty = new Storyboard();
            DoubleAnimation da = new DoubleAnimation();
            da.Duration = TimeSpan.FromSeconds(0.4);
            da.By = -50;
            da.AutoReverse = true;
            Storyboard.SetTarget(da, sampleGrid.RenderTransform);
            Storyboard.SetTargetProperty(da, "Y");
            Sty.Children.Add(da);
            DoubleAnimation da2 = new DoubleAnimation();
            da2.Duration = TimeSpan.FromSeconds(0.4);
            da2.By = -50;
            da2.AutoReverse = true;
            Storyboard.SetTarget(da2, sampleGrid.RenderTransform);
            Storyboard.SetTargetProperty(da2, "X");
            Sty.Children.Add(da2);
            Sty.Begin();
        }

        private void Button5_Click(object sender, RoutedEventArgs e)
        {
            sampleGrid.RenderTransform = new ScaleTransform() { };
            Storyboard Sty = new Storyboard();
            DoubleAnimation da = new DoubleAnimation();
            da.Duration = TimeSpan.FromSeconds(0.4);
            da.By = 0.5;
            da.AutoReverse = true;
            Storyboard.SetTarget(da, sampleGrid.RenderTransform);
            Storyboard.SetTargetProperty(da, "ScaleY");
            Sty.Children.Add(da);
            Sty.Begin();
        }

        private void Button6_Click(object sender, RoutedEventArgs e)
        {
            sampleGrid.RenderTransform = new SkewTransform { };
            Storyboard Sty = new Storyboard();
            DoubleAnimation da = new DoubleAnimation();
            da.Duration = TimeSpan.FromSeconds(0.4);
            da.By = -20;
            da.AutoReverse = true;
            Storyboard.SetTarget(da, sampleGrid.RenderTransform);
            Storyboard.SetTargetProperty(da, "AngleY");
            Sty.Children.Add(da);
            Sty.Begin();
        }
    }
}

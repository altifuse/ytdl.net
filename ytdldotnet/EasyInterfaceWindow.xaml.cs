using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace ytdldotnet
{
    /// <summary>
    /// Interaction logic for EasyInterfaceWindow.xaml
    /// </summary>
    public partial class EasyInterfaceWindow
    {
        DispatcherTimer urlBoxTimer;

        TextBox urlBox;
        ProgressRing ytdlInitialFetchProgressRing;
        
        public EasyInterfaceWindow()
        {
            InitializeComponent();

            // creates the URL textbox
            this.urlBox = new TextBox()
            {
                HorizontalAlignment = HorizontalAlignment.Left,
                Height = 32,
                Width = 256,
                Margin = new Thickness(16, 16, 16, 16),
                TextWrapping = TextWrapping.NoWrap,
                VerticalAlignment = VerticalAlignment.Top,
                BorderThickness = new Thickness(1)
            };
            this.urlBox.SetResourceReference(Control.BorderBrushProperty, "AccentColorBrush");
            TextBoxHelper.SetWatermark(this.urlBox, "enter a YouTube URL...");
            this.urlBox.TextChanged += UrlBox_TextChanged;
            Grid.SetColumn(this.urlBox, 0);
            Grid.SetRow(this.urlBox, 0);
            MainGrid.Children.Add(this.urlBox);
        }
        
        private void UrlBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // waits until user has stopped typing to actually do something
            if(this.urlBoxTimer == null)
            {
                this.urlBoxTimer = new DispatcherTimer();
                this.urlBoxTimer.Tick += new EventHandler(this.FetchUrlInfo);
                this.urlBoxTimer.Interval = TimeSpan.FromMilliseconds(1000);
            }
            this.urlBoxTimer.Stop();
            this.urlBoxTimer.Tag = (sender as TextBox).Text;
            this.urlBoxTimer.Start();
        }

        private void FetchUrlInfo(object sender, EventArgs e)
        {
            var timer = sender as DispatcherTimer;
            if(timer == null)
            {
                return;
            }

            // resizes window
            this.Resize(80); // 64 height + 16 margin

            // adds spinner
            MainGrid.Children.RemoveRange(1, 2);
            this.ytdlInitialFetchProgressRing = new ProgressRing()
            {
                Height = 64,
                Width = 64,
                Margin = new Thickness(16, 16, 16, 16),
                IsActive = true
            };
            this.ytdlInitialFetchProgressRing.SetResourceReference(Control.BorderBrushProperty, "AccentColorBrush");
            Grid.SetColumn(this.ytdlInitialFetchProgressRing, 0);
            Grid.SetRow(this.ytdlInitialFetchProgressRing, 1);
            MainGrid.Children.Add(this.ytdlInitialFetchProgressRing);
            
            // TO-DO: call youtube-dl

            // stops textbox delay timer
            timer.Stop();
        }

        private async void Resize(double increment)
        {
            await Task.Delay(10);
            double initialHeight = this.Height;
            Trace.WriteLine(initialHeight);
            double newHeight = initialHeight + increment;
            double heightInterval = newHeight - initialHeight;
            double heightStep = heightInterval / 10;

            double initialTop = this.Top;
            double newTop = (SystemParameters.WorkArea.Height - newHeight) / 2;
            double topInterval = newTop - initialTop;
            double topStep = topInterval / 10;

            for (int i = 0; i < 10; i++)
            {
                // this.Height += heightStep;
                this.Top += topStep;
                await Task.Delay(10);
            }
        }
    }
}

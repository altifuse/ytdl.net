using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
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
using ytdldotnet.externalSoftware;
using ytdldotnet.Util;

namespace ytdldotnet
{
    /// <summary>
    /// Interaction logic for EasyInterfaceWindow.xaml
    /// </summary>
    public partial class EasyInterfaceWindow
    {
        DispatcherTimer urlBoxTimer;

        Dictionary<UrlTypes, String> urlTypesText = new Dictionary<UrlTypes, string>()
        {
            { UrlTypes.YoutubeVideo, "video" },
            { UrlTypes.YoutubePlaylist, "playlist" },
            { UrlTypes.YoutubeChannel, "channel" },
            { UrlTypes.Page, "page" }
        };

        public EasyInterfaceWindow()
        {
            InitializeComponent();

            // creates the URL textbox
            TextBox urlBox = new TextBox()
            {
                HorizontalAlignment = HorizontalAlignment.Left,
                Height = 32,
                Width = 256,
                Margin = new Thickness(16, 16, 16, 16),
                TextWrapping = TextWrapping.NoWrap,
                VerticalAlignment = VerticalAlignment.Top,
                BorderThickness = new Thickness(1)
            };
            urlBox.SetResourceReference(Control.BorderBrushProperty, "AccentColorBrush");
            TextBoxHelper.SetWatermark(urlBox, "enter a YouTube URL...");
            urlBox.TextChanged += UrlBox_TextChanged;
            Grid.SetColumn(urlBox, 0);
            Grid.SetRow(urlBox, 0);
            MainGrid.Children.Add(urlBox);
        }
        
        private void UrlBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // waits until user has stopped typing to actually do something
            if(urlBoxTimer == null)
            {
                urlBoxTimer = new DispatcherTimer();
                urlBoxTimer.Tick += new EventHandler(this.FetchUrlInfo);
                urlBoxTimer.Interval = TimeSpan.FromMilliseconds(1000);
            }
            urlBoxTimer.Stop();
            urlBoxTimer.Tag = ((TextBox)sender).Text;
            urlBoxTimer.Start();
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

            string url = (String)timer.Tag;

            // stops textbox delay timer
            timer.Stop();

            // TO-DO: call youtube-dl

            if(GetUrlType(url) == UrlTypes.YoutubeChannel || GetUrlType(url) == UrlTypes.YoutubePlaylist)
            {
                // TO-DO: this thread must NOT return here; instead, I think it needs to trigger an event upon ending that will call another method supposed to continue after the spinner
                YtdlManager ytdlManager = new YtdlManager();
                string[] info = null;
                var thread = new Thread(
                    () =>
                    {
                        info = ytdlManager.GetPlaylistNameAndLength(url, GetUrlType(url));
                    });
                thread.Start();
                thread.Join();
                Trace.WriteLine(info[0] + " " + info[1]);
            }

            // adds spinner and initial loading message
            MainGrid.Children.RemoveRange(1, 2);
            ProgressRing ytdlInitialFetchProgressRing = new ProgressRing()
            {
                Height = 64,
                Width = 64,
                Margin = new Thickness(16, 16, 16, 16),
                IsActive = true
            };
            ytdlInitialFetchProgressRing.SetResourceReference(Control.BorderBrushProperty, "AccentColorBrush");
            Grid.SetColumn(ytdlInitialFetchProgressRing, 0);
            Grid.SetRow(ytdlInitialFetchProgressRing, 1);
            MainGrid.Children.Add(ytdlInitialFetchProgressRing);
            Label initialFetchLabel = new Label()
            {
                Content = "Loading " + urlTypesText[GetUrlType(url)] + " info...",
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };
            initialFetchLabel.SetResourceReference(Control.BorderBrushProperty, "AccentColorBrush");
            Grid.SetColumn(initialFetchLabel, 0);
            Grid.SetRow(initialFetchLabel, 1);
            MainGrid.Children.Add(initialFetchLabel);
        }

        private UrlTypes GetUrlType(string url)
        {
            if (url.Contains("channel/") || url.Contains("user/"))
            {
                return UrlTypes.YoutubeChannel;
            }
            else if (url.Contains("list="))
            {
                return UrlTypes.YoutubePlaylist;
            }
            else if (url.Contains("v=") || url.Contains("youtu.be"))
            {
                return UrlTypes.YoutubeVideo;
            }
            return UrlTypes.Page;
        }

        private async void Resize(double increment)
        {
            await Task.Delay(10);
            double initialHeight = this.Height;
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

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
using ytdldotnet.Data;
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

        // controls
        TextBox urlBox;
        Label initialFetchInfoLabel;
        ProgressRing initialFetchProgressRing;

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
            urlBox = CustomControls.GetUrlTextbox();
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
            if (timer == null)
            {
                return;
            }

            string url = "\"" + (String)timer.Tag + "\"";

            // adds spinner and initial loading message
            MainGrid.Children.RemoveRange(1, 2);
            initialFetchProgressRing = CustomControls.GetProgressRing();
            initialFetchProgressRing.SetResourceReference(Control.BorderBrushProperty, "AccentColorBrush");
            Grid.SetColumn(initialFetchProgressRing, 0);
            Grid.SetRow(initialFetchProgressRing, 1);
            MainGrid.Children.Add(initialFetchProgressRing);
            initialFetchInfoLabel = CustomControls.GetLoadingLabel("Loading " + urlTypesText[GetUrlType(url)] + " info...");
            initialFetchInfoLabel.SetResourceReference(Control.BorderBrushProperty, "AccentColorBrush");
            Grid.SetColumn(initialFetchInfoLabel, 0);
            Grid.SetRow(initialFetchInfoLabel, 1);
            MainGrid.Children.Add(initialFetchInfoLabel);

            // stops textbox delay timer
            timer.Stop();

            // calls new thread for handling youtube-dl
            new Thread(() => FetchYtdlInfo(url, GetUrlType(url))).Start();
        }

        private async void FetchYtdlInfo(string url, UrlTypes urlType)
        {
            YtdlManager ytdlManager = new YtdlManager();
            if (urlType == UrlTypes.YoutubeChannel || urlType == UrlTypes.YoutubePlaylist)
            {
                PlaylistInfo playlistInfo = await ytdlManager.GetPlaylistNameAndLength(url, urlType);
                this.Dispatcher.Invoke(() =>
                {
                    if(urlType == UrlTypes.YoutubeChannel)
                    {
                        initialFetchInfoLabel.Content = "Channel: " + playlistInfo.Name + " | " + playlistInfo.NumberOfVideos + " videos";
                    }
                    else
                    {
                        initialFetchInfoLabel.Content = "Playlist: " + playlistInfo.Name + " | " + playlistInfo.NumberOfVideos + " videos";
                    }

                    Grid.SetRow(initialFetchProgressRing, 2);

                    
                });

                playlistInfo.Videos = await ytdlManager.GetPlaylistHead(url);

                //TO-DO: use info from playlist head
            }
            else if (urlType == UrlTypes.YoutubeVideo)
            {
                //TO-DO
            }
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

        // handles (or at least should do so) animations when controls are changed
        private async void Recenter()
        {
            await Task.Delay(10);

            double initialTop = this.Top;
            double newTop = (SystemParameters.WorkArea.Height - this.Height) / 2;
            double topInterval = newTop - initialTop;
            double topStep = topInterval / 10;

            for (int i = 0; i < 10; i++)
            {
                this.Top += topStep;
                await Task.Delay(10);
            }
        }

        //private async void Resize(double increment)
        //{
        //    await Task.Delay(10);
        //    double initialHeight = this.Height;
        //    double newHeight = initialHeight + increment;
        //    double heightInterval = newHeight - initialHeight;
        //    double heightStep = heightInterval / 10;

        //    double initialTop = this.Top;
        //    double newTop = (SystemParameters.WorkArea.Height - newHeight) / 2;
        //    double topInterval = newTop - initialTop;
        //    double topStep = topInterval / 10;

        //    for (int i = 0; i < 10; i++)
        //    {
        //        // this.Height += heightStep;
        //        this.Top += topStep;
        //        await Task.Delay(10);
        //    }
        //}

        private async void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Recenter();
        }
    }
}

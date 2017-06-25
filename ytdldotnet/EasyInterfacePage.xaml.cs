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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using ytdldotnet.Data;
using ytdldotnet.externalSoftware;
using ytdldotnet.Util;

namespace ytdldotnet
{
    /// <summary>
    /// Interaction logic for EasyInterfacePage.xaml
    /// </summary>
    public partial class EasyInterfacePage : Page, INotifyPropertyChanged
    {
        // control vars
        DispatcherTimer urlBoxTimer;

        // data bindings
        string url;
        string statusText;
        string localPath;
        ConversionFormats conversionOption;

        // data
        UrlTypes urlType;

        // control bindings
        bool isStatusTextVisible;
        bool isConversionFormatVisible;
        bool canConvert;
        bool isLocalPathBoxVisible;
        bool isDownloadButtonVisible;
        bool isLeftProgressRingVisible;
        bool isRightProgressRingVisible;

        public EasyInterfacePage()
        {
            InitializeComponent();
            this.DataContext = this;

            IsStatusTextVisible = false;
            IsConversionFormatVisible = false;
            CanConvert = Properties.Settings.Default.CanConvert;
            IsLocalPathBoxVisible = false;
            IsDownloadButtonVisible = false;
            IsLeftProgressRingVisible = false;
            IsRightProgressRingVisible = false;
        }

        // PROPERTIES

        private void NotifyPropertyChanged(string info)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(info));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public string Url { get => url; set => url = value; }
        public string StatusText
        {
            get => statusText;
            set
            {
                statusText = value;
                NotifyPropertyChanged("StatusText");
            }
        }
        public string LocalPath { get => localPath; set => localPath = value; }
        public bool IsStatusTextVisible
        {
            get => isStatusTextVisible;
            set
            {
                isStatusTextVisible = value;
                NotifyPropertyChanged("IsStatusTextVisible");
            }
        }
        public bool IsConversionFormatVisible
        {
            get => isConversionFormatVisible;
            set
            {
                isConversionFormatVisible = value;
                NotifyPropertyChanged("IsConversionFormatVisible");
            }
        }
        public bool CanConvert { get => canConvert; set => canConvert = value; }
        public bool IsLocalPathBoxVisible
        {
            get => isLocalPathBoxVisible;
            set
            {
                isLocalPathBoxVisible = value;
                NotifyPropertyChanged("IsLocalPathBoxVisible");
            }
        }
        public bool IsDownloadButtonVisible
        {
            get => isDownloadButtonVisible;
            set
            {
                isDownloadButtonVisible = value;
                NotifyPropertyChanged("IsDownloadButtonVisible");
            }
        }
        public bool IsLeftProgressRingVisible
        {
            get => isLeftProgressRingVisible;
            set
            {
                isLeftProgressRingVisible = value;
                NotifyPropertyChanged("IsLeftProgressRingVisible");
            }
        }
        public bool IsRightProgressRingVisible
        {
            get => isRightProgressRingVisible;
            set
            {
                isRightProgressRingVisible = value;
                NotifyPropertyChanged("IsRightProgressRingVisible");
            }
        }

        public ConversionFormats ConversionOption
        {
            get => conversionOption;
            set
            {
                conversionOption = value;
                NotifyPropertyChanged("ConversionOption");
            }
        }

        // HANDLERS

        private void DownloadButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void UrlBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // waits until user has stopped typing to actually do something
            if (urlBoxTimer == null)
            {
                urlBoxTimer = new DispatcherTimer();
                urlBoxTimer.Tick += new EventHandler(this.FetchUrlInfo);
                urlBoxTimer.Interval = TimeSpan.FromMilliseconds(1000);
            }
            urlBoxTimer.Stop();
            urlBoxTimer.Start();
        }

        private void FetchUrlInfo(object sender, EventArgs e)
        {
            var timer = sender as DispatcherTimer;
            if (timer == null)
            {
                return;
            }
            timer.Stop();

            // change interface

            // fetch ytdl info
            Trace.WriteLine(url);
            urlType = GetUrlType(url);
            StatusText = "Fetching " + EnumTexts.UrlTypesText(urlType) + " info...";
            IsStatusTextVisible = true;
            IsLeftProgressRingVisible = true;
            new Thread(() => FetchYtdlInfo()).Start();
        }

        private async void FetchYtdlInfo()
        {
            YtdlManager ytdlManager = new YtdlManager();
            if (urlType == UrlTypes.YoutubeChannel || urlType == UrlTypes.YoutubePlaylist)
            {
                PlaylistInfo playlistInfo = await ytdlManager.GetPlaylistNameAndLength(url, urlType);
                this.Dispatcher.Invoke(() =>
                {
                    if (urlType == UrlTypes.YoutubeChannel)
                    {
                        StatusText = "Channel: " + playlistInfo.Name + " | " + playlistInfo.NumberOfVideos + " videos";
                    }
                    else
                    {
                        StatusText = "Playlist: " + playlistInfo.Name + " | " + playlistInfo.NumberOfVideos + " videos";
                    }
                    IsLeftProgressRingVisible = false;
                    IsConversionFormatVisible = true;
                    IsLocalPathBoxVisible = true;
                    IsDownloadButtonVisible = true;
                    IsRightProgressRingVisible = true;
                });
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
    }
}

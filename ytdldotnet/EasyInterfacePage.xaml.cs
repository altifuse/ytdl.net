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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ytdldotnet
{
    /// <summary>
    /// Interaction logic for EasyInterfacePage.xaml
    /// </summary>
    public partial class EasyInterfacePage : Page, INotifyPropertyChanged
    {
        // data bindings
        string url;
        string statusText;
        string localPath;

        // control bindings
        bool isStatusTextVisible;
        bool isConversionFormatVisible;
        bool canConvert;
        bool isLocalPathBoxVisible;

        public EasyInterfacePage()
        {
            InitializeComponent();
            this.DataContext = this;

            IsStatusTextVisible = false;
            IsConversionFormatVisible = false;
            CanConvert = Properties.Settings.Default.CanConvert;
            IsLocalPathBoxVisible = false;
        }

        // PROPERTIES

        private void NotifyPropertyChanged(string info)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(info));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public string Url { get => url; set => url = value; }
        public string StatusText { get => statusText; set => statusText = value; }
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

    }
}

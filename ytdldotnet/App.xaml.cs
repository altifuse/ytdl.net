using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using ytdldotnet.external;

namespace ytdldotnet
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            YtdlManager ytdlManager = new YtdlManager();
            // Trace.WriteLine("exists: " + ytdlManager.Exists());
            // ytdlManager.Download();
            Trace.WriteLine("exists: " + ytdlManager.Exists());
            Trace.WriteLine("is up to date: " + ytdlManager.IsUpToDate());
        }
    }
}

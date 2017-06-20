using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using ytdldotnet.externalSoftware;
using ytdldotnet.Properties;

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
            ExternalSoftwareManager ytdlManager = new YtdlManager();
            ExternalSoftwareManager ffmpegManager = new FfmpegManager();
            
            // check if youtube-dl is available
            if(!ytdlManager.Exists())
            {
                // TO-DO: prompt user to download or exit
                // placeholder
                ytdlManager.Download();
            }

            // check if ffmpeg is available
            if(ffmpegManager.Exists())
            {
                Settings.Default.CanConvert = true;
            }

            // number of cores for the Easy Interface multithreading
            Settings.Default.ProcessorCount = Environment.ProcessorCount;

            Trace.WriteLine(Settings.Default.CanConvert);
            Trace.WriteLine(Settings.Default.ProcessorCount);
        }
    }
}

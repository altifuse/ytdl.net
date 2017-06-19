using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ytdldotnet.external
{
    class YtdlManager: ExternalSoftwareManager
    {
        public YtdlManager()
        {
            string name = "ytdl";
            string downloadedFile = "youtube-dl.exe";
            string mainExecutable = "youtube-dl.exe";
            string downloadUrl = "https://yt-dl.org/latest/youtube-dl.exe";
            string checksumUrl = "https://yt-dl.org/latest/MD5SUMS";
            this.Info = new SWInfo(name, downloadedFile, mainExecutable, downloadUrl, checksumUrl);
        }
    }
}

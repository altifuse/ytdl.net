using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ytdldotnet.external
{
    class YtdlManager: ExternalSoftware
    {
        public YtdlManager()
        {
            string name = "ytdl";
            string localFile = "youtube-dl.exe";
            string downloadUrl = "https://yt-dl.org/latest/youtube-dl.exe";
            string checksumUrl = "https://yt-dl.org/latest/MD5SUMS";
            this.Info = new SWInfo(name, localFile, downloadUrl, checksumUrl);
        }
    }
}

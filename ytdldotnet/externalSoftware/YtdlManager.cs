using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ytdldotnet.Util;

namespace ytdldotnet.externalSoftware
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

        public Process GetNewYtdlProcess(string arguments)
        {
            return new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = this.Info.MainExecutable,
                    Arguments = arguments,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true
                }
            };
        }

        public string[] GetPlaylistNameAndLength(string url, UrlTypes urlType)
        {
            // youtube-dl.exe -s --flat-playlist <url>
            // if playlist or channel: look for the line that begins with "[youtube:playlist] playlist"
            // else pass
            Process process = GetNewYtdlProcess("-s --flat-playlist " + url);

            string line = "";
            string name;
            string length;
            process.Start();
            while(!process.StandardOutput.EndOfStream)
            {
                string _line = process.StandardOutput.ReadLine();
                if(_line.StartsWith("[youtube:playlist] playlist"))
                {
                    line = _line;
                    process.Kill();
                    break;
                }
            };

            string[] aux = line.Split(new string[] { ": Downloading " }, StringSplitOptions.RemoveEmptyEntries);
            length = aux[aux.Length - 1].Split(new string[] { " videos" }, StringSplitOptions.RemoveEmptyEntries)[0]; //ok
            if(urlType == UrlTypes.YoutubePlaylist)
            {
                name = line.Substring(28);
                name = name.PadRight(21 + length.Length);
            }
            else
            {
                name = line.Substring(41);
                name = name.PadRight(21 + length.Length);
            }
            return new string[] { name, length };
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Compression;
using System.IO;
using System.Diagnostics;
using System.Net;

namespace ytdldotnet.externalSoftware
{
    class FfmpegManager: ExternalSoftwareManager
    {
        public FfmpegManager()
        {
            string name = "ffmpeg";
            string downloadedFile = "ffmpeg-latest-win64-static.zip";
            string mainExecutable = "ffmpeg.exe";
            string downloadUrl = "http://ffmpeg.zeranoe.com/builds/win64/static/ffmpeg-latest-win64-static.zip";
            string checksumUrl = "";
            this.Info = new SWInfo(name, downloadedFile, mainExecutable, downloadUrl, checksumUrl);
        }

        public override void Download()
        {
            base.Download();
            using (ZipArchive archive = ZipFile.OpenRead(this.Info.DownloadedFile))
            {
                Trace.WriteLine(archive);
                foreach (ZipArchiveEntry entry in archive.Entries)
                {
                    Trace.WriteLine(entry.FullName);
                    if (entry.FullName.EndsWith(this.Info.MainExecutable))
                    {
                        entry.ExtractToFile(this.Info.MainExecutable);
                    }
                }
            }

            // TO-DO: remove .zip
        }

        public override bool IsUpToDate()
        {
            // TO-DO
            return true;
        }
    }
}

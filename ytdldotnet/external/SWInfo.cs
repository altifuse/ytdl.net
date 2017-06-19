using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ytdldotnet.external
{
    class SWInfo
    {
        string name;
        string downloadedFile;
        string mainExecutable;
        string downloadUrl;
        string checksumUrl;

        public SWInfo(string _name, string _downloadedFile, string _mainExecutable, string _downloadUrl, string _checksumUrl)
        {
            this.Name = _name;
            this.DownloadedFile = _downloadedFile;
            this.MainExecutable = _mainExecutable;
            this.DownloadUrl = _downloadUrl;
            this.ChecksumUrl = _checksumUrl;
        }

        public string Name { get => name; set => name = value; }
        public string DownloadedFile { get => downloadedFile; set => downloadedFile = value; }
        public string DownloadUrl { get => downloadUrl; set => downloadUrl = value; }
        public string ChecksumUrl { get => checksumUrl; set => checksumUrl = value; }
        public string MainExecutable { get => mainExecutable; set => mainExecutable = value; }
    }
}

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
        string localFile;
        string downloadUrl;
        string checksumUrl;

        public SWInfo(string _name, string _localFile, string _downloadUrl, string _checksumUrl)
        {
            this.Name = _name;
            this.LocalFile = _localFile;
            this.DownloadUrl = _downloadUrl;
            this.ChecksumUrl = _checksumUrl;
        }

        public string Name { get => name; set => name = value; }
        public string LocalFile { get => localFile; set => localFile = value; }
        public string DownloadUrl { get => downloadUrl; set => downloadUrl = value; }
        public string ChecksumUrl { get => checksumUrl; set => checksumUrl = value; }
    }
}

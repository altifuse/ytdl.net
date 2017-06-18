using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ytdldotnet.external
{
    class ExternalSoftware
    {
        SWInfo info;
        WebClient client;
        string appBaseDir = AppDomain.CurrentDomain.BaseDirectory;

        internal SWInfo Info { get => info; set => info = value; }
        public string AppBaseDir { get => appBaseDir; set => appBaseDir = value; }

        public bool Exists()
        {
            if (File.Exists(AppBaseDir + this.Info.LocalFile))
            {
                return true;
            }
            return false;
        }

        public void Download()
        {
            this.client = new WebClient();
            this.client.DownloadFile(this.Info.DownloadUrl, this.Info.LocalFile);
        }

        public bool IsUpToDate() //defaults to MD5SUMS file
        {
            this.client = new WebClient();
            this.client.DownloadFile(this.Info.ChecksumUrl, "_tempMD5SUMS");
            string referenceMD5;
            string localMD5;
            string reference = File.ReadAllText("_tempMD5SUMS");
            referenceMD5 = Regex.Match(reference.ToString(), ".*(?=" + this.Info.LocalFile + ")").ToString();
            referenceMD5 = Regex.Replace(referenceMD5, @"\s+", "");
            Trace.WriteLine(referenceMD5);

            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(this.Info.LocalFile))
                {
                    localMD5 = BitConverter.ToString(md5.ComputeHash(stream)).Replace("-", "‌​").ToLower();
                    localMD5 = Regex.Replace(localMD5, @"\s+", "");
                    Trace.WriteLine(localMD5);
                }
            }

            if(String.Equals(referenceMD5, localMD5, StringComparison.InvariantCultureIgnoreCase))
            {
                return true;
            }
            return false;
        }
    }
}

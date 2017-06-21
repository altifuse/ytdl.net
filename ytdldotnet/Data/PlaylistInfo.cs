using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ytdldotnet.Data
{
    class PlaylistInfo
    {
        string name;
        string numberOfVideos;
        List<VideoInfo> videos;
        
        public PlaylistInfo(string name, string length)
        {
            this.Name = name;
            this.NumberOfVideos = length;
            this.Videos = new List<VideoInfo>();
        }

        public string Name { get => name; set => name = value; }
        public string NumberOfVideos { get => numberOfVideos; set => numberOfVideos = value; }
        public List<VideoInfo> Videos { get => videos; set => videos = value; }
    }
}

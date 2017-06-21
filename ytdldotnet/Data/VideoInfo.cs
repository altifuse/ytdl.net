using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ytdldotnet.Data
{
    class VideoInfo
    {
        string title;
        string duration;
        string thumbnailUrl;

        public VideoInfo(string title, string duration, string thumbnailUrl)
        {
            this.Title = title;
            this.Duration = duration;
            this.ThumbnailUrl = thumbnailUrl;
        }

        public string Title { get => title; set => title = value; }
        public string Duration { get => duration; set => duration = value; }
        public string ThumbnailUrl { get => thumbnailUrl; set => thumbnailUrl = value; }
    }
}

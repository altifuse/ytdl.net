using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ytdldotnet.Util
{
    public enum UrlTypes
    {
        YoutubeVideo,
        YoutubePlaylist,
        YoutubeChannel,
        Page
    }

    public enum ConversionFormats
    {
        DDL,
        MP3,
        OGG,
        AVI,
        MP4,
        WMV
    }

    public static class EnumTexts
    {
        public static string UrlTypesText(UrlTypes urlType)
        {
            switch(urlType)
            {
                case UrlTypes.YoutubeVideo: return "video";
                case UrlTypes.YoutubePlaylist: return "playlist";
                case UrlTypes.YoutubeChannel: return "channel";
            }
            return "page";
        }

        public static string ConversionParameters(ConversionFormats conversionFormat)
        {
            switch(conversionFormat)
            {
                case ConversionFormats.DDL: return "";
                case ConversionFormats.MP3: return "--extract-audio --audio-format mp3 --audio-quality 0";
                case ConversionFormats.OGG: return "--extract-audio --audio-format vorbis --audio-quality 0";
                case ConversionFormats.AVI: return ""; // TO-DO
                case ConversionFormats.MP4: return ""; // TO-DO
                case ConversionFormats.WMV: return ""; // TO-DO
            }
            return "";
        }
    }
}

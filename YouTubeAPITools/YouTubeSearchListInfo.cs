using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YouTubeAPI
{
    [Flags]
    public enum SearchType : int
    {
        Videos = 0x1,
        Channels = 0x2,
        Playlists = 0x4,
        All = 0x7
    }
    
    public abstract class YoutubeSearchResult
    {
        public string Title { get; set; }
    }

    public class VideoResult : YoutubeSearchResult
    {
        public string Description { get; set; }
        public DateTime? PublishedAt { get; set; }
    }

    public class ChannelResult : YoutubeSearchResult
    {
        public string ChannelId { get; set; }
    }

    public class PlaylistInfo : YoutubeSearchResult
    {
        public string Description { get; set; }
    }
}

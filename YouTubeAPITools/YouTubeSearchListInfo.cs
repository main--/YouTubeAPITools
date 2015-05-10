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

    public class YouTubeSearchListInfo
    {
        public List<VideoInfo> videos = new List<VideoInfo>();
        public List<ChannelInfo> channels = new List<ChannelInfo>();
        public List<PlaylistInfo> playlists = new List<PlaylistInfo>();
    }

    public class VideoInfo
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? PublishedAt { get; set; }

        public VideoInfo(string title, string desc, DateTime? publishedAt)
        {
            Title = title;
            Description = desc;
            PublishedAt = publishedAt;
        }
    }

    public class ChannelInfo
    {
        public string ChannelId { get; set; }
        public string ChannelTitle { get; set; }
        public ChannelInfo(string channelId, string channelTitle)
        {
            ChannelId = channelId;
            ChannelTitle = channelTitle;
        }
    }

    public class PlaylistInfo
    {
        public string Title { get; set; }
        public string Description { get; set; }

        public PlaylistInfo(string title, string desc)
        {
            Title = title;
            Description = desc;
        }
    }
}

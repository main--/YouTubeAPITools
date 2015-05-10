using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace YouTubeAPI
{
    public class YouTubeSearch
    {
        private readonly BaseClientService.Initializer APIInitializer;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="apiKey">YouTube APIKey for authoriziation purposes</param>
        /// <param name="applicationName">Is needed for static purposes</param>
        public YouTubeSearch(string apiKey, string applicationName)
        {
            APIInitializer = new BaseClientService.Initializer()
            {
                ApiKey = apiKey,
                ApplicationName = applicationName
            };
        }

        /// <summary>
        /// Search for videos, channels and playlists
        /// </summary>
        /// <param name="searchQuery">What are you searching for?</param>
        /// <param name="maxResults">Specify how many results will be returned</param>
        /// <param name="type">Type of the search. E.g videos or playlists only</param>
        /// <returns>Returns a list of videos, channels and playlists for a given search query</returns>
        public async Task<IEnumerable<YouTubeSearchResult>> RetreiveList(string searchQuery, long maxResults, SearchType type)
        {
            if (String.IsNullOrWhiteSpace(searchQuery))
            {
                throw new ArgumentException("searchQuery");
            }

            if (maxResults <= 0)
            {
                throw new ArgumentOutOfRangeException("The amount of maximum results can not be zero or lower");
            }

            var youtubeService = new YouTubeService(APIInitializer);

            var searchListRequest = youtubeService.Search.List("snippet");
            searchListRequest.Q = searchQuery;
            searchListRequest.MaxResults = maxResults;

            // Call the search.list method to retrieve results matching the specified query term.
            var searchListResponse = await searchListRequest.ExecuteAsync();

            return searchListResponse.Items.Select(searchResult => {
                if (searchResult.Id.Kind == "youtube#video" && type.HasFlag(SearchType.Videos))
                    return new VideoResult { Title = searchResult.Snippet.Title, Description = searchResult.Snippet.Description };
                if (searchResult.Id.Kind == "youtube#channel" && type.HasFlag(SearchType.Channels))
                    return new ChannelResult { Title = searchResult.Snippet.ChannelTitle, ChannelId = searchResult.Snippet.ChannelTitle };
                if (searchResult.Id.Kind == "youtube#playlist" && type.HasFlag(SearchType.Playlist))
                    return new PlaylistResult { Title = searchResult.Snippet.Title, Description = searchResult.Snippet.Description };
                return null;
            }).Where(x => x != null);
        }
    }
}

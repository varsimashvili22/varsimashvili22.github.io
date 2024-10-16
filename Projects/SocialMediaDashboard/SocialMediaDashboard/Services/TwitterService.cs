using Newtonsoft.Json;
using RestSharp;
using SocialMediaDashboard.Models;
using System.Threading.Tasks;

namespace SocialMediaDashboard.Services
{
    public class TwitterService
    {
        private readonly string _bearerToken;
        private readonly RestClient _client;

        public TwitterService(string bearerToken)
        {
            _bearerToken = bearerToken;
            _client = new RestClient("https://api.twitter.com/2");
        }

        public async Task<List<Tweet>> GetLatestTweets(string username)
        {
            var request = new RestRequest($"/tweets?username={username}", Method.Get);
            request.AddHeader("Authorization", $"Bearer {_bearerToken}");

            var response = await _client.ExecuteAsync(request);
            if (response.IsSuccessful)
            {
                // Deserialize the JSON response into a list of Tweet objects
                var tweets = JsonConvert.DeserializeObject<List<Tweet>>(response.Content);
                return tweets;
            }

            return new List<Tweet>();
        }
    }
}
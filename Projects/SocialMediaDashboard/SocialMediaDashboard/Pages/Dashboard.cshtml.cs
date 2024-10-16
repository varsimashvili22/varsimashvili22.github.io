using Microsoft.AspNetCore.Mvc.RazorPages;
using SocialMediaDashboard.Models;
using SocialMediaDashboard.Services;

namespace SocialMediaDashboard.Pages
{
    public class DashboardModel : PageModel
    {
        private readonly TwitterService _twitterService;

        public List<Tweet> Tweets { get; private set; }
        public string Username { get; set; } = "@elonmusk"; 

        public DashboardModel(TwitterService twitterService)
        {
            _twitterService = twitterService;
        }

        public async Task OnGet()
        {
            Tweets = await _twitterService.GetLatestTweets(Username);
        }
    }
}

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SocialMediaDashboard.Services;

var builder = WebApplication.CreateBuilder(args);

// Retrieve the Bearer Token from environment variables
var bearerToken = Environment.GetEnvironmentVariable("TWITTER_BEARER_TOKEN");

// Add services to the container, including the TwitterService
builder.Services.AddSingleton(new TwitterService(bearerToken));
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

// Middleware to run before app starts
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var twitterService = services.GetRequiredService<TwitterService>();

    // Run the async method
    var tweets = await twitterService.GetLatestTweets("galaxymeekYT");  // Replace with a valid Twitter username
    Console.WriteLine(tweets);  // This will output the tweets in the console for debugging
}

// Configure other middleware
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapRazorPages();

app.Run();

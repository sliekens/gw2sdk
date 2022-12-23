using System.Globalization;
using System.Text;
using GuildWars2;
using GuildWars2.Achievements;
using GuildWars2.Items;


Console.OutputEncoding = Encoding.UTF8;
CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("en");
CultureInfo.CurrentUICulture = CultureInfo.GetCultureInfo("en");

// HttpClient has a fully customizable pipeline, but defaults are fine too
using var httpClient = new HttpClient();
var gw2 = new Gw2Client(httpClient);

// Use Gw2Client to fetch daily achievements
var dailyAchievements = await gw2.Achievements.GetDailyAchievements();

// The result is a Replica<DailyAchievementGroup> object that contains the achievements
// and also some response headers such as Date
Console.WriteLine("Daily achievements of {0:D}\n", dailyAchievements.Date);

// The actual achievements are available in the Value property
// This data is highly normalized, it's necessary to make additional requests
// to fetch the achievement names and item rewards
foreach (var dailyFractal in dailyAchievements.Value.Fractals)
{
    // Get the fractal achievement details by the achievement ID
    // By the way, you can cast a 'Replica<T>' to 'T' if you don't care about the response headers
    var achievement = (Achievement)await gw2.Achievements.GetAchievementById(dailyFractal.Id);

    Console.WriteLine(achievement.Name);
    Console.WriteLine(achievement.Requirement);

    foreach (var reward in achievement.Rewards ?? Enumerable.Empty<AchievementReward>())
    {
        // Get the item details by the ID of the item reward
        if (reward is ItemReward itemReward)
        {
            // By the way, you can assign 'Replica<T>' to 'T' without casting
            // although some say this makes the code less clear
            Item item = await gw2.Items.GetItemById(itemReward.Id);

            Console.WriteLine("Rewards {0} ({1})", item.Name, itemReward.Count);
            Console.WriteLine(item.Description);
        }
    }

    Console.WriteLine();
}

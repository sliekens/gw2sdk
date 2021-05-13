using System;
using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Achievements.Dailies.Http;
using GW2SDK.Annotations;
using GW2SDK.Http;

namespace GW2SDK.Achievements.Dailies
{
    [PublicAPI]
    public sealed class DailyAchievementService
    {
        private readonly IDailyAchievementReader _dailyAchievementReader;
        private readonly HttpClient _http;

        public DailyAchievementService(HttpClient http, IDailyAchievementReader dailyAchievementReader)
        {
            _http = http ?? throw new ArgumentNullException(nameof(http));
            _dailyAchievementReader = dailyAchievementReader ?? throw new ArgumentNullException(nameof(dailyAchievementReader));
        }

        public async Task<DailyAchievementGroup> GetDailyAchievements(Day day = Day.Today)
        {
            var request = new DailyAchievementsRequest(day);
            using var response = await _http.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using var json = await response.Content.ReadAsJsonAsync().ConfigureAwait(false);
            return _dailyAchievementReader.Read(json);
        }
    }
}

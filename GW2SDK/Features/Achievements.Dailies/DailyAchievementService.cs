using System;
using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Achievements.Dailies.Impl;
using GW2SDK.Annotations;
using GW2SDK.Impl.JsonConverters;
using Newtonsoft.Json;

namespace GW2SDK.Achievements.Dailies
{
    [PublicAPI]
    public sealed class DailyAchievementService
    {
        private readonly HttpClient _http;

        public DailyAchievementService(HttpClient http)
        {
            _http = http ?? throw new ArgumentNullException(nameof(http));
        }

        public async Task<DailyAchievementGroup?> GetDailyAchievements()
        {
            var request = new DailyAchievementsRequest();
            using var response = await _http.SendAsync(request).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            return JsonConvert.DeserializeObject<DailyAchievementGroup>(json, Json.DefaultJsonSerializerSettings);
        }
    }
}

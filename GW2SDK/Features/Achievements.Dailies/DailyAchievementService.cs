using System;
using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Achievements.Dailies.Http;
using GW2SDK.Http;
using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Achievements.Dailies
{
    [PublicAPI]
    public sealed class DailyAchievementService
    {
        private readonly IDailyAchievementReader dailyAchievementReader;
        private readonly HttpClient http;
        private readonly MissingMemberBehavior missingMemberBehavior;

        public DailyAchievementService(HttpClient http, IDailyAchievementReader dailyAchievementReader,
            MissingMemberBehavior missingMemberBehavior
        )
        {
            this.http = http ?? throw new ArgumentNullException(nameof(http));
            this.dailyAchievementReader =
                dailyAchievementReader ?? throw new ArgumentNullException(nameof(dailyAchievementReader));
            this.missingMemberBehavior = missingMemberBehavior;
        }

        public async Task<IReplica<DailyAchievementGroup>> GetDailyAchievements(Day day = Day.Today)
        {
            var request = new DailyAchievementsRequest(day);
            return await http.GetResource(request, json => dailyAchievementReader.Read(json, missingMemberBehavior))
                .ConfigureAwait(false);
        }
    }
}

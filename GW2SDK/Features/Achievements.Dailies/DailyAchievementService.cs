using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GW2SDK.Achievements.Dailies.Http;
using GW2SDK.Achievements.Dailies.Json;
using GW2SDK.Http;
using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Achievements.Dailies
{
    [PublicAPI]
    public sealed class DailyAchievementService
    {
        private readonly HttpClient http;

        public DailyAchievementService(HttpClient http)
        {
            this.http = http.WithDefaults() ?? throw new ArgumentNullException(nameof(http));
        }

        public async Task<IReplica<DailyAchievementGroup>> GetDailyAchievements(
            Day day = Day.Today,
            MissingMemberBehavior missingMemberBehavior = default,
            CancellationToken cancellationToken = default
        )
        {
            var request = new DailyAchievementsRequest(day);
            return await http.GetResource(request,
                    json => DailyAchievementReader.Read(json.RootElement, missingMemberBehavior),
                    cancellationToken)
                .ConfigureAwait(false);
        }
    }
}

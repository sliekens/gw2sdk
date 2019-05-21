using System;
using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Features.Accounts.Achievements;

namespace GW2SDK.Infrastructure.Accounts.Achievements
{
    public sealed class AchievementJsonService : IAchievementJsonService
    {
        private readonly HttpClient _http;

        public AchievementJsonService([NotNull] HttpClient http)
        {
            _http = http ?? throw new ArgumentNullException(nameof(http));
        }

        public async Task<HttpResponseMessage> GetAchievements()
        {
            var resource = new UriBuilder(_http.BaseAddress)
            {
                Path = "/v2/account/achievements"
            };
            return await _http.GetAsync(resource.Uri, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
        }
    }
}

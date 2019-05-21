using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GW2SDK.Infrastructure;
using Newtonsoft.Json;

namespace GW2SDK.Features.Accounts.Achievements
{
    public sealed class AchievementService
    {
        private readonly IAchievementJsonService _api;

        public AchievementService([NotNull] IAchievementJsonService api)
        {
            _api = api ?? throw new ArgumentNullException(nameof(api));
        }

        public async Task<IReadOnlyList<Achievement>> GetAchievements(
            [CanBeNull] JsonSerializerSettings settings = null)
        {
            var response = await _api.GetAchievements().ConfigureAwait(false);

            // TODO: check authorization
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var list = new List<Achievement>();
            JsonConvert.PopulateObject(json, list, settings ?? Json.DefaultJsonSerializerSettings);
            return list.AsReadOnly();
        }
    }
}

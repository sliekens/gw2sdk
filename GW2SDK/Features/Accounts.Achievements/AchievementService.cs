using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using GW2SDK.Features.Common;
using GW2SDK.Infrastructure;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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
            var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            if (response.StatusCode == HttpStatusCode.Forbidden)
            {
                var text = JObject.Parse(json)["text"].ToString();
                throw new UnauthorizedOperationException(text);
            }

            response.EnsureSuccessStatusCode();
            var list = new List<Achievement>();
            JsonConvert.PopulateObject(json, list, settings ?? Json.DefaultJsonSerializerSettings);
            return list.AsReadOnly();
        }
    }
}

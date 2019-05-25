using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Features.Common;
using GW2SDK.Infrastructure;
using GW2SDK.Infrastructure.Accounts.Achievements;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GW2SDK.Features.Accounts.Achievements
{
    public sealed class AchievementService
    {
        private readonly HttpClient _http;

        public AchievementService([NotNull] HttpClient http)
        {
            _http = http ?? throw new ArgumentNullException(nameof(http));
        }

        // TODO: return as IDataTransferList<Achievement>
        [Scope(Permission.Progression)]
        public async Task<IReadOnlyList<Achievement>> GetAchievements(
            [CanBeNull] JsonSerializerSettings settings = null)
        {
            using (var request = new GetAchievementsRequest())
            using (var response = await _http.SendAsync(request, HttpCompletionOption.ResponseHeadersRead)
                .ConfigureAwait(false))
            {
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
}

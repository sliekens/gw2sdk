using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Achievements.Http;
using GW2SDK.Annotations;
using GW2SDK.Http;

namespace GW2SDK.Achievements
{
    [PublicAPI]
    public sealed class AchievementService
    {
        private readonly HttpClient _http;

        private readonly IAchievementReader _achievementReader;

        public AchievementService(HttpClient http, IAchievementReader achievementReader)
        {
            _http = http ?? throw new ArgumentNullException(nameof(http));
            _achievementReader = achievementReader ?? throw new ArgumentNullException(nameof(achievementReader));
        }

        public async Task<IDataTransferCollection<int>> GetAchievementsIndex()
        {
            var request = new AchievementsIndexRequest();
            using var response = await _http.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using var json = await response.Content.ReadAsJsonAsync().ConfigureAwait(false);
            var context = response.Headers.GetCollectionContext();
            var list = new List<int>(context.ResultCount);
            list.AddRange(_achievementReader.Id.ReadArray(json));
            return new DataTransferCollection<int>(list, context);
        }

        public async Task<Achievement> GetAchievementById(int achievementId)
        {
            var request = new AchievementByIdRequest(achievementId);
            using var response = await _http.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using var json = await response.Content.ReadAsJsonAsync().ConfigureAwait(false);
            return _achievementReader.Read(json);
        }

        public async Task<IDataTransferCollection<Achievement>> GetAchievementsByIds(IReadOnlyCollection<int> achievementIds)
        {
            if (achievementIds is null)
            {
                throw new ArgumentNullException(nameof(achievementIds));
            }

            if (achievementIds.Count == 0)
            {
                throw new ArgumentException("Achievement IDs cannot be an empty collection.", nameof(achievementIds));
            }

            var request = new AchievementsByIdsRequest(achievementIds);
            using var response = await _http.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using var json = await response.Content.ReadAsJsonAsync().ConfigureAwait(false);
            var context = response.Headers.GetCollectionContext();
            var list = new List<Achievement>(context.ResultCount);
            list.AddRange(_achievementReader.ReadArray(json));
            return new DataTransferCollection<Achievement>(list, context);
        }

        public async Task<IDataTransferPage<Achievement>> GetAchievementsByPage(int pageIndex, int? pageSize = null)
        {
            var request = new AchievementsByPageRequest(pageIndex, pageSize);
            using var response = await _http.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            using var json = await response.Content.ReadAsJsonAsync().ConfigureAwait(false);
            var pageContext = response.Headers.GetPageContext();
            var list = new List<Achievement>(pageContext.PageSize);
            list.AddRange(_achievementReader.ReadArray(json));
            return new DataTransferPage<Achievement>(list, pageContext);
        }
    }
}

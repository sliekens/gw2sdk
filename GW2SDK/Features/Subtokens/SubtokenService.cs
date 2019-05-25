using System;
using System.Net;
using System.Threading.Tasks;
using GW2SDK.Features.Common;
using GW2SDK.Infrastructure;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GW2SDK.Features.Subtokens
{
    public sealed class SubtokenService
    {
        private readonly ISubtokenJsonService _api;

        public SubtokenService([NotNull] ISubtokenJsonService api)
        {
            _api = api ?? throw new ArgumentNullException(nameof(api));
        }

        public async Task<CreatedSubtoken> CreateSubtoken([CanBeNull] JsonSerializerSettings settings = null)
        {
            var response = await _api.CreateSubtoken().ConfigureAwait(false);
            var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            if (response.StatusCode == HttpStatusCode.Forbidden)
            {
                var text = JObject.Parse(json)["text"].ToString();
                throw new UnauthorizedOperationException(text);
            }

            response.EnsureSuccessStatusCode();
            var dto = new CreatedSubtoken();
            JsonConvert.PopulateObject(json, dto, settings ?? Json.DefaultJsonSerializerSettings);
            return dto;
        }
    }
}

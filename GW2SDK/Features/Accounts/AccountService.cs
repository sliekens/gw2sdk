using System;
using System.Net;
using System.Threading.Tasks;
using GW2SDK.Features.Common;
using GW2SDK.Infrastructure;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GW2SDK.Features.Accounts
{
    public sealed class AccountService
    {
        private readonly IAccountJsonService _api;

        public AccountService([NotNull] IAccountJsonService api)
        {
            _api = api ?? throw new ArgumentNullException(nameof(api));
        }

        public async Task<Account> GetAccount([CanBeNull] JsonSerializerSettings settings = null)
        {
            var response = await _api.GetAccount().ConfigureAwait(false);
            var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            if (response.StatusCode == HttpStatusCode.Forbidden)
            {
                var text = JObject.Parse(json)["text"].ToString();
                throw new UnauthorizedOperationException(text);
            }

            response.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<Account>(json, settings ?? Json.DefaultJsonSerializerSettings);
        }
    }
}

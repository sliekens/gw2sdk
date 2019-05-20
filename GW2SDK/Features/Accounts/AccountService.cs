using System;
using System.Threading.Tasks;
using GW2SDK.Infrastructure;
using Newtonsoft.Json;

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

            // TODO: check authorization
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            return JsonConvert.DeserializeObject<Account>(json, settings ?? Json.DefaultJsonSerializerSettings);
        }
    }
}

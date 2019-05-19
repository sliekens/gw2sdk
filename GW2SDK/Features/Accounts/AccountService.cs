using System;
using System.Threading.Tasks;
using GW2SDK.Features.Accounts.Infrastructure;
using GW2SDK.Infrastructure;
using Newtonsoft.Json;

namespace GW2SDK.Features.Accounts
{
    public sealed class AccountService
    {
        private readonly IJsonAccountsService _api;

        public AccountService([NotNull] IJsonAccountsService api)
        {
            _api = api ?? throw new ArgumentNullException(nameof(api));
        }

        public async Task<Account> GetAccount([CanBeNull] JsonSerializerSettings settings = null)
        {
            var (json, _) = await _api.GetAccount();
            return JsonConvert.DeserializeObject<Account>(json, settings ?? Json.DefaultJsonSerializerSettings);
        }
    }
}

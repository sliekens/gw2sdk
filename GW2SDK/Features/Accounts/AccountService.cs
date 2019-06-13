using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Features.Common;
using GW2SDK.Infrastructure;
using GW2SDK.Infrastructure.Accounts;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GW2SDK.Features.Accounts
{
    public sealed class AccountService
    {
        private readonly HttpClient _http;

        public AccountService([NotNull] HttpClient http)
        {
            _http = http ?? throw new ArgumentNullException(nameof(http));
        }

        public async Task<Account> GetAccount([CanBeNull] JsonSerializerSettings settings = null)
        {
            using (var request = new GetAccountRequest())
            using (var response = await _http.SendAsync(request).ConfigureAwait(false))
            {
                var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    var text = JObject.Parse(json)["text"].ToString();
                    throw new UnauthorizedOperationException(text);
                }

                response.EnsureSuccessStatusCode();
                return JsonConvert.DeserializeObject<Account>(json, settings ?? Json.DefaultJsonSerializerSettings);
            }
        }
    }
}

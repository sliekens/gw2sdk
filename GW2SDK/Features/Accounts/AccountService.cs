﻿using System;
using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Accounts.Impl;
using GW2SDK.Annotations;
using GW2SDK.Impl;
using GW2SDK.Impl.JsonConverters;
using Newtonsoft.Json;

namespace GW2SDK.Accounts
{
    [PublicAPI]
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
                response.EnsureSuccessStatusCode();
                var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                return JsonConvert.DeserializeObject<Account>(json, settings ?? Json.DefaultJsonSerializerSettings);
            }
        }
    }
}

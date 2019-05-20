﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Extensions;
using GW2SDK.Features.Accounts;

namespace GW2SDK.Infrastructure.Accounts
{
    public sealed class AccountJsonService : IAccountJsonService
    {
        private readonly HttpClient _http;

        public AccountJsonService([NotNull] HttpClient http)
        {
            _http = http ?? throw new ArgumentNullException(nameof(http));
        }

        public async Task<(string Json, Dictionary<string, string> MetaData)> GetAccount()
        {
            var resource = new UriBuilder(_http.BaseAddress)
            {
                Path = "/v2/account"
            };
            return await _http.GetStringWithContextAsync(resource.Uri).ConfigureAwait(false);
        }
    }
}

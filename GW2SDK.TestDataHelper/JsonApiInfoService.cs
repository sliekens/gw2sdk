﻿using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Impl.V2.Impl;

namespace GW2SDK.TestDataHelper
{
    public class JsonApiInfoService
    {
        private readonly HttpClient _http;

        public JsonApiInfoService(HttpClient http)
        {
            _http = http;
        }

        public async Task<string> GetJsonApiInfo()
        {
            var request = new ApiInfoRequest();
            using var response = await _http.SendAsync(request).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
    }
}
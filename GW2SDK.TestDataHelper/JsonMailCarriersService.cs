using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Http;
using GW2SDK.MailCarriers.Http;

namespace GW2SDK.TestDataHelper
{
    public class JsonMailCarriersService
    {
        private readonly HttpClient _http;

        public JsonMailCarriersService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<string>> GetAllJsonMailCarriers()
        {
            var request = new MailCarriersRequest();
            using var response = await _http.SendAsync(request, HttpCompletionOption.ResponseHeadersRead)
                .ConfigureAwait(false);
            response.EnsureSuccessStatusCode();

            // API returns a JSON array but we want a List of JSON objects instead
            using var json = await response.Content.ReadAsJsonAsync()
                .ConfigureAwait(false);
            return json.Indent(false)
                .RootElement.EnumerateArray()
                .Select(item =>
                    item.ToString() ?? throw new InvalidOperationException("Unexpected null in JSON array."))
                .ToList();
        }
    }
}

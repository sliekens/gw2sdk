using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Continents.Http;
using GW2SDK.Http;

namespace GW2SDK.TestDataHelper
{
    public class JsonFloorService
    {
        private readonly HttpClient _http;

        public JsonFloorService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<string>> GetAllJsonFloors(int continentId, bool indented)
        {
            var request = new FloorsRequest(continentId);
            using var response = await _http.SendAsync(request, HttpCompletionOption.ResponseHeadersRead)
                .ConfigureAwait(false);
            response.EnsureSuccessStatusCode();

            // API returns a JSON array but we want a List of JSON objects instead
            using var json = await response.Content.ReadAsJsonAsync().ConfigureAwait(false);
            return json.Indent(indented)
                .RootElement.EnumerateArray()
                .Select(item =>
                    item.ToString() ?? throw new InvalidOperationException("Unexpected null in JSON array."))
                .ToList();
        }
    }
}

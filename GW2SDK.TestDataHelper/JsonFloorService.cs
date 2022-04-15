using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GW2SDK.Http;
using GW2SDK.Maps.Http;

namespace GW2SDK.TestDataHelper;

public class JsonFloorService
{
    private readonly HttpClient http;

    public JsonFloorService(HttpClient http)
    {
        this.http = http;
    }

    public async Task<List<string>> GetAllJsonFloors(int continentId)
    {
        var request = new FloorsRequest(continentId, default);
        using var response = await http.SendAsync(request, HttpCompletionOption.ResponseHeadersRead)
            .ConfigureAwait(false);
        response.EnsureSuccessStatusCode();

        // API returns a JSON array but we want a List of JSON objects instead
        using var json = await response.Content.ReadAsJsonAsync(CancellationToken.None)
            .ConfigureAwait(false);
        return json.Indent(false)
            .RootElement.EnumerateArray()
            .Select(item => item.ToString() ?? throw new InvalidOperationException("Unexpected null in JSON array."))
            .ToList();
    }
}

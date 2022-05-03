using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

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
        var request = new BulkRequest($"/v2/continents/{continentId}/floors");
        var json = await request.SendAsync(http, CancellationToken.None).ConfigureAwait(false);
        return json.Indent(false)
            .RootElement.EnumerateArray()
            .Select(
                item => item.ToString()
                    ?? throw new InvalidOperationException("Unexpected null in JSON array.")
            )
            .ToList();
    }
}

using GW2SDK;
using GW2SDK.Meta;

using HttpClient httpClient = new();

// Gw2Client uses MetaQuery in the basic usage example
// You can create your own instances of this class
async Task<ApiVersion> GetApiVersionExample1()
{
    MetaQuery metaQuery = new(httpClient);
    var apiVersion = await metaQuery.GetApiVersion();
    return apiVersion.Value;
}

// MetaQuery in turn uses ApiVersionRequest to get the API version data
// Again, you can create your own instances of this class
async Task<ApiVersion> GetApiVersionExample2()
{
    ApiVersionRequest request = new("v2");
    var apiVersion = await request.SendAsync(httpClient, CancellationToken.None);
    return apiVersion.Value;
}

// Debug this code to see more details
var example1 = await GetApiVersionExample1();
var example2 = await GetApiVersionExample2();

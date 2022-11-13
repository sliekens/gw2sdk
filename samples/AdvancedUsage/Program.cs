using GW2SDK.Meta;

using HttpClient httpClient = new();

// Debug this code to see more details

// Gw2Client uses MetaQuery in the basic usage example
// You can create your own instances of this class
MetaQuery metaQuery = new(httpClient);
var result1 = await metaQuery.GetApiVersion();
Console.WriteLine(result1.Value.SchemaVersions.Last().Version);

// MetaQuery in turn uses ApiVersionRequest to get the API version data
// Again, you can create your own instances of this class
ApiVersionRequest request = new("v2");
var result2 = await request.SendAsync(httpClient, CancellationToken.None);
Console.WriteLine(result2.Value.SchemaVersions.Last().Version);

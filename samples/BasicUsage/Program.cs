using GuildWars2;

// HttpClient has a fully customizable pipeline, but defaults are fine too
using HttpClient httpClient = new();
Gw2Client gw2Client = new(httpClient);
var apiVersion = await gw2Client.Meta.GetApiVersion();

Console.WriteLine("Output date: {0}", apiVersion.Date);
Console.WriteLine("Output last modified: {0}", apiVersion.LastModified);
Console.WriteLine("Output expires: {0}", apiVersion.Expires);
foreach (var route in apiVersion.Value.Routes)
{
    Console.WriteLine(route.Path);
}

// Output date: 4/30/2022 1:18:27 PM +00:00
// Output last modified: 
// Output expires: 
// /v2/account
// /v2/account/achievements
// /v2/account/bank
// /v2/account/buildstorage
// /v2/account/dailycrafting
// /v2/account/dungeons
// ...

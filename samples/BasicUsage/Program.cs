using GuildWars2;

// HttpClient has a fully customizable pipeline, but defaults are fine too
using var httpClient = new HttpClient();
var gw2 = new Gw2Client(httpClient);

// Use Gw2Client to fetch data
var apiVersion = await gw2.Meta.GetApiVersion();

// The result is a Replica object that contains the value
// and also some response headers such as Date
Console.WriteLine("Output date: {0}", apiVersion.Date);

// The actual data is available in the Value property
foreach (var route in apiVersion.Value.Routes)
{
    Console.WriteLine(route.Path);
}

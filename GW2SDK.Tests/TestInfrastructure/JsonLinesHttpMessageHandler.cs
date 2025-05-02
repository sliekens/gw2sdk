using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Web;

namespace GuildWars2.Tests.TestInfrastructure;

internal class JsonLinesHttpMessageHandler(string path) : HttpMessageHandler
{
    private readonly Dictionary<int, JsonElement> Entries = JsonLinesReader.Read(path)
        .Select(json =>
            {
                using var document = JsonDocument.Parse(json);
                return document.RootElement.Clone();
            }
        )
        .ToDictionary(node => node.GetProperty("id").GetInt32());

    protected override Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken
    )
    {
        var results = new JsonArray();
        var query = HttpUtility.ParseQueryString(request.RequestUri!.Query);
        var keys = query.Get("ids");
        if (keys is null)
        {
            foreach (var key in Entries.Keys)
            {
                results.Add(key);
            }
        }
        else
        {
            foreach (var key in keys.Split(',').Select(int.Parse))
            {
                if (Entries.TryGetValue(key, out var entry))
                {
                    results.Add(entry);
                }
            }
        }

        return Task.FromResult(
            new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(
                    results.ToString(),
                    Encoding.UTF8,
                    "application/json"
                )
            }
        );
    }
}

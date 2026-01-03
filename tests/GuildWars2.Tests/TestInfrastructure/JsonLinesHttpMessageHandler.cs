using System.Collections.Specialized;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Web;

namespace GuildWars2.Tests.TestInfrastructure;

internal sealed class JsonLinesHttpMessageHandler(string path) : HttpMessageHandler
{
    private readonly Dictionary<int, JsonElement> entries = JsonLinesReader.Read(path)
        .Select(json =>
            {
                using JsonDocument document = JsonDocument.Parse(json);
                return document.RootElement.Clone();
            }
        )
        .ToDictionary(node => node.GetProperty("id").GetInt32());

    protected override Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken
    )
    {
        List<int> ids = [];
        List<JsonElement> results = [];
        NameValueCollection query = HttpUtility.ParseQueryString(request.RequestUri!.Query);
        string? keys = query.Get("ids");
        if (keys is null)
        {
            ids = [.. entries.Keys];
        }
        else
        {
            foreach (int key in keys.Split(',').Select(int.Parse))
            {
                if (entries.TryGetValue(key, out JsonElement entry))
                {
                    results.Add(entry);
                }
            }
        }
        // Serialize the heterogenous list: ints or JsonElements.
        using MemoryStream buffer = new();
        using (Utf8JsonWriter writer = new(buffer))
        {
            writer.WriteStartArray();
            if (results.Count > 0)
            {
                foreach (JsonElement result in results)
                {
                    result.WriteTo(writer);
                }
            }
            else
            {
                foreach (int id in ids)
                {
                    writer.WriteNumberValue(id);
                }
            }

            writer.WriteEndArray();
        }
        string payload = Encoding.UTF8.GetString(buffer.ToArray());
        return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent(payload, Encoding.UTF8, "application/json")
        });
    }
}

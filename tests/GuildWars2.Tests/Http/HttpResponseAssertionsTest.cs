using System.Net;

using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Http;

public class HttpResponseAssertionsTest
{
    [Test]
    public async Task Json_lines_handlers_return_successful_json_responses()
    {
        using JsonLinesHttpMessageHandler handler = new("Data/items.jsonl.gz");
        using HttpClient httpClient = new(handler);

        HttpResponseMessage response = await httpClient.GetAsync(
            new Uri("https://example.com/"),
            TestContext.Current!.Execution.CancellationToken);

        await Assert.That(response).HasStatusCode(HttpStatusCode.OK);
        await Assert.That(response).HasContentType("application/json");
    }
}

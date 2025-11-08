using GuildWars2.Pve.Home.Decorations;
using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Pve.Home.Decorations;

public class Decorations
{
    [Test]
    public async Task Can_be_enumerated()
    {
        // The JsonLinesHttpMessageHandler simulates the behavior of the real API
        // because bulk enumeration quickly exhausts the API rate limit
        using JsonLinesHttpMessageHandler handler = new("Data/decorations.jsonl.gz");
        using HttpClient httpClient = new(handler);
        Gw2Client sut = new(httpClient);
        await foreach ((Decoration actual, MessageContext context) in sut.Pve.Home.GetDecorationsBulk(cancellationToken: TestContext.Current!.Execution.CancellationToken))
        {
            Assert.NotNull(context);
            Assert.True(actual.Id > 0);
            Assert.NotEmpty(actual.Name);
            Assert.NotNull(actual.Description);
            // Some decorations are not linked to categories
            Assert.NotNull(actual.CategoryIds);
            Assert.All(actual.CategoryIds, categoryId => Assert.True(categoryId > 0));
            Assert.True(actual.MaxCount > 0);
            Assert.True(actual.IconUrl is null || actual.IconUrl.IsAbsoluteUri);
        }
    }
}

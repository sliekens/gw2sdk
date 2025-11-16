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
            await Assert.That(context).IsNotNull();
            await Assert.That(actual.Id > 0).IsTrue();
            await Assert.That(actual.Name).IsNotEmpty();
            await Assert.That(actual.Description).IsNotNull();
            // Some decorations are not linked to categories
            await Assert.That(actual.CategoryIds).IsNotNull();
            foreach (int categoryId in actual.CategoryIds)
            {
                await Assert.That(categoryId > 0).IsTrue();
            }
            await Assert.That(actual.MaxCount > 0).IsTrue();
            await Assert.That(actual.IconUrl is null || actual.IconUrl.IsAbsoluteUri).IsTrue();
        }
    }
}

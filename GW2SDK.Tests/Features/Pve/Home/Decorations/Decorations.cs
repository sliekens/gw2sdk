using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Pve.Home.Decorations;

public class Decorations
{
    [Fact]
    public async Task Can_be_enumerated()
    {
        // The JsonLinesHttpMessageHandler simulates the behavior of the real API
        // because bulk enumeration quickly exhausts the API rate limit
        using var httpClient =
            new HttpClient(new JsonLinesHttpMessageHandler("Data/decorations.jsonl.gz"));
        var sut = new Gw2Client(httpClient);
        await foreach (var (actual, context) in sut.Pve.Home.GetDecorationsBulk(
                cancellationToken: TestContext.Current.CancellationToken
            ))
        {
            Assert.NotNull(context);
            Assert.True(actual.Id > 0);
            Assert.NotEmpty(actual.Name);
            Assert.NotNull(actual.Description);
            if (actual.Id is 13
                or 28
                or 73
                or 161
                or 170
                or 379
                or 396
                or 435
                or 438
                or 458
                or 496
                or 499
                or 528
                or 574
                or 593
                or 599
                or 617
                or 624
                or 631
                or 677
                or 714
                or 741) // Some SAB decorations are not linked to its category
            {
                Assert.Empty(actual.CategoryIds);
            }
            else
            {
                Assert.NotEmpty(actual.CategoryIds);
            }

            Assert.All(actual.CategoryIds, categoryId => Assert.True(categoryId > 0));
            Assert.True(actual.MaxCount > 0);
            Assert.NotEmpty(actual.IconHref);
        }
    }
}

using GuildWars2.Chat;
using GuildWars2.Hero.Equipment.Wardrobe;
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
            new HttpClient(new JsonLinesHttpMessageHandler("Data/decorations.json.gz"));
        var sut = new Gw2Client(httpClient);
        await foreach (var (actual, context) in sut.Pve.Home.GetDecorationsBulk())
        {
            Assert.NotNull(context);
            Assert.True(actual.Id > 0);
            Assert.NotEmpty(actual.Name);
            Assert.NotNull(actual.Description);
            Assert.NotEmpty(actual.CategoryIds);
            Assert.All(actual.CategoryIds, categoryId => Assert.True(categoryId > 0));
            Assert.True(actual.MaxCount > 0);
            Assert.NotEmpty(actual.IconHref);
        }
    }
}

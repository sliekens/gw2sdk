using System.Text.Json;

using GuildWars2.Hero.Equipment.Templates;
using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Equipment.Templates;

public class BoundLegendaryItems
{
    [Fact]
    public async Task Can_be_listed()
    {
        var sut = Composer.Resolve<Gw2Client>();
        var accessToken = TestConfiguration.ApiKey;

        var (actual, context) = await sut.Hero.Equipment.Templates.GetBoundLegendaryItems(
            accessToken.Key,
            cancellationToken: TestContext.Current.CancellationToken
        );

        Assert.NotNull(context);
        Assert.NotEmpty(actual);
        Assert.All(
            actual,
            entry =>
            {
                Assert.True(entry.Id > 0);
                Assert.True(entry.Count > 0);

                var json = JsonSerializer.Serialize(entry);
                var roundtrip = JsonSerializer.Deserialize<BoundLegendaryItem>(json);
                Assert.Equal(entry, roundtrip);
            }
        );
    }
}

using System.Text.Json;
using GuildWars2.Hero.Equipment.Miniatures;
using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Equipment.Miniatures;

public class Miniatures
{
    [Fact]
    public async Task Can_be_listed()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var (actual, context) =
            await sut.Hero.Equipment.Miniatures.GetMiniatures(
                cancellationToken: TestContext.Current.CancellationToken
            );

        Assert.NotEmpty(actual);
        Assert.Equal(context.ResultCount, actual.Count);
        Assert.Equal(context.ResultTotal, actual.Count);
        Assert.All(
            actual,
            entry =>
            {
                Assert.True(entry.Id > 0);
                Assert.NotEmpty(entry.Name);
                Assert.True(entry.IconUrl is null || entry.IconUrl.IsAbsoluteUri);
                Assert.True(entry.Order >= 0);
                Assert.True(entry.ItemId >= 0);

                var json = JsonSerializer.Serialize(entry);
                var roundtrip = JsonSerializer.Deserialize<Miniature>(json);
                Assert.Equal(entry, roundtrip);
            }
        );
    }
}

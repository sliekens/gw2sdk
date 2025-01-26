using System.Text.Json;
using GuildWars2.Hero.Equipment.Skiffs;
using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Equipment.Skiffs;

public class SkiffSkins
{
    [Fact]
    public async Task Can_be_listed()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var (actual, context) = await sut.Hero.Equipment.Skiffs.GetSkiffSkins(
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
                Assert.NotEmpty(entry.IconHref);
                Assert.NotNull(entry.DyeSlots);
                Assert.All(
                    entry.DyeSlots,
                    dyeSlot =>
                    {
                        Assert.True(dyeSlot.Material.IsDefined());
                        Assert.True(dyeSlot.ColorId > 0);
                    }
                );

                var json = JsonSerializer.Serialize(entry);
                var roundtrip = JsonSerializer.Deserialize<SkiffSkin>(json);
                Assert.Equal(entry, roundtrip);
            }
        );
    }
}

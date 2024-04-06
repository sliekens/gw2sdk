using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Equipment.Gliders;

public class GliderSkins
{
    [Fact]
    public async Task Can_be_listed()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var (actual, context) = await sut.Hero.Equipment.Gliders.GetGliderSkins();

        Assert.NotEmpty(actual);
        Assert.Equal(context.ResultCount, actual.Count);
        Assert.Equal(context.ResultTotal, actual.Count);
        Assert.All(
            actual,
            entry =>
            {
                Assert.True(entry.Id > 0);
                Assert.NotNull(entry.UnlockItemIds);
                Assert.True(entry.Order >= 0);
                Assert.NotEmpty(entry.IconHref);
                Assert.NotEmpty(entry.Name);
                Assert.NotNull(entry.Description);
                Assert.NotNull(entry.DefaultDyeColorIds);
            }
        );
    }
}

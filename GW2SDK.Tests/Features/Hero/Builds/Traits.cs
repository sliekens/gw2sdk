using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Builds;

public class Traits
{
    [Fact]
    public async Task Can_be_listed()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var (actual, context) = await sut.Hero.Builds.GetTraits();

        Assert.Equal(context.ResultTotal, actual.Count);
        Assert.All(
            actual,
            trait =>
            {
                Assert.True(trait.Id >= 1);
                Assert.InRange(trait.Tier, 0, 4);
                Assert.True(trait.Order >= 0);
                Assert.NotEmpty(trait.Name);
                Assert.NotNull(trait.Description);
                Assert.True(trait.Slot.IsDefined());
            }
        );
    }
}

using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Builds;

public class Traits
{
    [Fact]
    public async Task Can_be_listed()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var (actual, context) = await sut.Hero.Builds.GetTraits();

        Assert.Equal(actual.Count, context.ResultTotal);
        Assert.All(
            actual,
            trait =>
            {
                trait.Id_is_positive();
            }
        );
    }
}

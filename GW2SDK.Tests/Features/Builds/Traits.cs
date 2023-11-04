using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Builds;

public class Traits
{
    [Fact]
    public async Task Can_be_listed()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var actual = await sut.Builds.GetTraits();

        Assert.NotNull(actual.Context.ResultContext);
        Assert.Equal(actual.Context.ResultContext.ResultTotal, actual.Value.Count);
        Assert.All(
            actual.Value,
            trait =>
            {
                trait.Id_is_positive();
            }
        );
    }
}

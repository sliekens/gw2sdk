using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Races;

public class Races
{
    [Fact]
    public async Task Can_be_listed()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var actual = await sut.Races.GetRaces();

        Assert.NotNull(actual.Context.ResultContext);
        Assert.Equal(actual.Context.ResultContext.ResultTotal, actual.Value.Count);
    }
}

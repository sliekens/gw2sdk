using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Exploration.Floors;

public class FloorsIndex
{
    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    public async Task Can_be_listed(int continentId)
    {
        var sut = Composer.Resolve<Gw2Client>();

        var (actual, context) = await sut.Exploration.GetFloorsIndex(continentId);

        Assert.NotEmpty(actual);
        Assert.Equal(actual.Count, context.ResultCount);
        Assert.Equal(actual.Count, context.ResultTotal);
    }
}

using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Masteries;

public class MasteriesIndex
{
    [Fact]
    public async Task Masteries_index_Can_be_listed()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var actual = await sut.Masteries.GetMasteriesIndex();

        Assert.NotEmpty(actual.Value);
        Assert.NotNull(actual.Context.ResultContext);
        Assert.Equal(actual.Value.Count, actual.Context.ResultContext.ResultCount);
        Assert.Equal(actual.Value.Count, actual.Context.ResultContext.ResultTotal);
    }
}

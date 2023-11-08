using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Guilds.Emblems;

public class BackgroundEmblemsIndex
{
    [Fact]
    public async Task Can_be_listed()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var (actual, context) = await sut.Guilds.GetBackgroundEmblemsIndex();

        Assert.NotEmpty(actual);
        Assert.NotNull(context.ResultContext);
        Assert.Equal(actual.Count, context.ResultContext.ResultCount);
        Assert.Equal(actual.Count, context.ResultContext.ResultTotal);
    }

}

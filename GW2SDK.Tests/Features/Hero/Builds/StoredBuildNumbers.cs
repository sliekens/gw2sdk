using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Builds;

public class StoredBuildNumbers
{
    [Fact]
    public async Task Can_be_listed()
    {
        var sut = Composer.Resolve<Gw2Client>();
        var accessToken = Composer.Resolve<ApiKey>();

        var (actual, context) = await sut.Hero.Builds.GetStoredBuildNumbers(accessToken.Key);

        Assert.NotEmpty(actual);
        Assert.NotNull(context.ResultContext);
        Assert.Equal(actual.Count, context.ResultContext.ResultCount);
        Assert.Equal(actual.Count, context.ResultContext.ResultTotal);
    }
}

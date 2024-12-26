using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Builds;

public class BuildNumbers
{
    [Fact]
    public async Task Can_be_listed()
    {
        var sut = Composer.Resolve<Gw2Client>();
        var character = TestConfiguration.TestCharacter;
        var accessToken = TestConfiguration.ApiKey;

        var (actual, context) =
            await sut.Hero.Builds.GetBuildNumbers(character.Name, accessToken.Key, cancellationToken: TestContext.Current.CancellationToken);

        Assert.NotEmpty(actual);
        Assert.Equal(context.ResultCount, actual.Count);
        Assert.Equal(context.ResultTotal, actual.Count);
    }
}

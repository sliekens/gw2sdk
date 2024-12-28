using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Exploration.HeroChallenges;

public class CompletedHeroChallenges
{
    [Fact]
    public async Task Can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();
        var character = TestConfiguration.TestCharacter;
        var accessToken = TestConfiguration.ApiKey;

        var (actual, _) = await sut.Exploration.GetCompletedHeroChallenges(
            character.Name,
            accessToken.Key,
            TestContext.Current.CancellationToken
        );

        // BUG: currently this data is unavailable :(
        // Change this back to Assert.NotEmpty once fixed
        // https://github.com/gw2-api/issues/issues/56
        Assert.Empty(actual);
    }
}

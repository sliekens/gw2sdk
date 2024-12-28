using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Wvw.Matches.Stats;

public class MatchStatsByWorldId
{
    [Fact]
    public async Task Can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();

        const int worldId = 2006;

        var (actual, _) = await sut.Wvw.GetMatchStatsByWorldId(
            worldId,
            cancellationToken: TestContext.Current.CancellationToken
        );

        Assert.NotNull(actual);
    }
}

using GuildWars2.Tests.TestInfrastructure;
using GuildWars2.Wvw.Matches;

namespace GuildWars2.Tests.Features.Wvw.Matches;

public class MatchByWorldId
{
    [Fact]
    public async Task Can_be_found()
    {
        Gw2Client sut = Composer.Resolve<Gw2Client>();

        const int worldId = 2006;

        (Match actual, _) = await sut.Wvw.GetMatchByWorldId(
            worldId,
            cancellationToken: TestContext.Current.CancellationToken
        );

        Assert.NotNull(actual);
    }
}

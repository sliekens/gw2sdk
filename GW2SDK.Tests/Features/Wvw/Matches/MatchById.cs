using GuildWars2.Tests.TestInfrastructure;
using GuildWars2.Wvw.Matches;

namespace GuildWars2.Tests.Features.Wvw.Matches;

public class MatchById
{
    [Fact]
    public async Task Can_be_found()
    {
        Gw2Client sut = Composer.Resolve<Gw2Client>();

        const string id = "1-1";

        (Match actual, MessageContext context) = await sut.Wvw.GetMatchById(
            id,
            cancellationToken: TestContext.Current.CancellationToken
        );

        Assert.NotNull(context);
        Assert.Equal(id, actual.Id);
    }
}

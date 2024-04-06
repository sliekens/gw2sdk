using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Wvw.Matches;

public class MatchById
{
    [Fact]
    public async Task Can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();

        const string id = "1-1";

        var (actual, context) = await sut.Wvw.GetMatchById(id);

        Assert.NotNull(context);
        Assert.Equal(id, actual.Id);
    }
}

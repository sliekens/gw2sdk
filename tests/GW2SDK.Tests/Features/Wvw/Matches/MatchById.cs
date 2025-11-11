using GuildWars2.Tests.TestInfrastructure.Composition;
using GuildWars2.Wvw.Matches;

namespace GuildWars2.Tests.Features.Wvw.Matches;

[ServiceDataSource]
public class MatchById(Gw2Client sut)
{
    [Test]
    public async Task Can_be_found()
    {
        const string id = "1-1";
        (Match actual, MessageContext context) = await sut.Wvw.GetMatchById(id, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        Assert.NotNull(context);
        Assert.Equal(id, actual.Id);
    }
}

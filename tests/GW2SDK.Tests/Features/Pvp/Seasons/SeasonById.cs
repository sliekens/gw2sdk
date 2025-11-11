using GuildWars2.Pvp.Seasons;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Pvp.Seasons;

[ServiceDataSource]
public class SeasonById(Gw2Client sut)
{
    [Test]
    public async Task Can_be_found()
    {
        const string id = "2B2E80D3-0A74-424F-B0EA-E221500B323C";
        (Season actual, MessageContext context) = await sut.Pvp.GetSeasonById(id, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        Assert.NotNull(context);
        Assert.Equal(id, actual.Id);
    }
}

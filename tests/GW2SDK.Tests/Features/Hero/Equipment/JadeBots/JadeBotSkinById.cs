using GuildWars2.Hero.Equipment.JadeBots;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Hero.Equipment.JadeBots;

[ServiceDataSource]
public class JadeBotSkinById(Gw2Client sut)
{
    [Test]
    public async Task Can_be_found()
    {
        const int id = 2;
        (JadeBotSkin actual, MessageContext context) = await sut.Hero.Equipment.JadeBots.GetJadeBotSkinById(id, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        Assert.NotNull(context);
        Assert.Equal(id, actual.Id);
    }
}

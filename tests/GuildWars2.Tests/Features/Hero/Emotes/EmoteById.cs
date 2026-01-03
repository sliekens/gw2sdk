using GuildWars2.Hero.Emotes;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Hero.Emotes;

[ServiceDataSource]
public class EmoteById(Gw2Client sut)
{
    [Test]
    public async Task Can_be_found()
    {
        const string id = "rockout";
        (Emote actual, MessageContext context) = await sut.Hero.Emotes.GetEmoteById(id, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        await Assert.That(context).IsNotNull();
        await Assert.That(actual.Id).IsEqualTo(id);
    }
}

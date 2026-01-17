using GuildWars2.Hero.Emotes;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Hero.Emotes;

[ServiceDataSource]
public class Emotes(Gw2Client sut)
{
    [Test]
    public async Task Can_be_listed()
    {
        (IImmutableValueSet<Emote> actual, MessageContext context) = await sut.Hero.Emotes.GetEmotes(cancellationToken: TestContext.Current!.Execution.CancellationToken);
        await Assert.That(context.ResultCount).IsEqualTo(actual.Count);
        await Assert.That(context.ResultTotal).IsEqualTo(actual.Count);
        await Assert.That(actual).IsNotEmpty();
        foreach (Emote entry in actual)
        {
            await Assert.That(entry.Id).IsNotEmpty();
            await Assert.That(entry.Commands).IsNotEmpty();
            await Assert.That(entry.UnlockItemIds).IsNotEmpty();
        }
    }
}

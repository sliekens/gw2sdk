using GuildWars2.Hero.Emotes;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Hero.Emotes;

[ServiceDataSource]
public class EmotesByFilter(Gw2Client sut)
{
    [Test]
    public async Task Can_be_filtered_by_id()
    {
        HashSet<string> ids = ["geargrind", "playdead", "rockout"];
        (HashSet<Emote> actual, MessageContext context) = await sut.Hero.Emotes.GetEmotesByIds(ids, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        await Assert.That(context.ResultCount).IsEqualTo(ids.Count);
        await Assert.That(context.ResultTotal > ids.Count).IsTrue();
        await Assert.That(actual.Count).IsEqualTo(ids.Count);
        foreach (string id in ids)
        {
            await Assert.That(actual).Contains(found => found.Id == id);
        }
    }
}

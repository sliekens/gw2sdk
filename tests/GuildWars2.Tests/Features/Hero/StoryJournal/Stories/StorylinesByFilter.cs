using GuildWars2.Hero.StoryJournal.Stories;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Hero.StoryJournal.Stories;

[ServiceDataSource]
public class StorylinesByFilter(Gw2Client sut)
{
    [Test]
    public async Task Can_be_filtered_by_id()
    {
        HashSet<string> ids = ["09766A86-D88D-4DF2-9385-259E9A8CA583", "A515A1D3-4BD7-4594-AE30-2C5D05FF5960", "215AAA0F-CDAC-4F93-86DA-C155A99B5784"];
        (IImmutableValueSet<Storyline> actual, MessageContext context) = await sut.Hero.StoryJournal.GetStorylinesByIds(ids, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        await Assert.That(context.ResultCount).IsEqualTo(ids.Count);
        await Assert.That(context.ResultTotal > ids.Count).IsTrue();
        await Assert.That(actual.Count).IsEqualTo(ids.Count);
        foreach (string id in ids)
        {
            await Assert.That(actual).Contains(found => found.Id == id);
        }
    }
}

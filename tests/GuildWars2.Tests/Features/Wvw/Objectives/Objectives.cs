using GuildWars2.Chat;
using GuildWars2.Tests.TestInfrastructure.Composition;
using GuildWars2.Wvw.Objectives;

namespace GuildWars2.Tests.Features.Wvw.Objectives;

[ServiceDataSource]
public class Objectives(Gw2Client sut)
{
    [Test]
    public async Task Can_be_listed()
    {
        (IImmutableValueSet<Objective> actual, MessageContext context) = await sut.Wvw.GetObjectives(cancellationToken: TestContext.Current!.Execution.CancellationToken);
        await Assert.That(actual).IsNotEmpty();
        await Assert.That(context).Member(c => c.ResultCount, m => m.IsEqualTo(actual.Count));
        await Assert.That(context).Member(c => c.ResultTotal, m => m.IsEqualTo(actual.Count));
        foreach (Objective entry in actual)
        {
            await Assert.That(entry.Id).IsNotEmpty();
            await Assert.That(entry.Name).IsNotEmpty();
            await Assert.That(entry.SectorId > 0).IsTrue();
            await Assert.That(entry.MapId > 0).IsTrue();
            await Assert.That(entry.MapKind.IsDefined()).IsTrue();
            await Assert.That(entry.MarkerIconUrl is null or { IsAbsoluteUri: true }).IsTrue();
            ObjectiveLink chatLink = entry.GetChatLink();
            await Assert.That(entry.ChatLink).IsNotEmpty();
            await Assert.That(chatLink.ToString()).IsEqualTo(entry.ChatLink);
            await Assert.That(chatLink.MapId).IsEqualTo(entry.MapId);
            await Assert.That($"{chatLink.MapId}-{chatLink.ObjectiveId}").IsEqualTo(entry.Id);
            ObjectiveLink chatLinkRoundtrip = ObjectiveLink.Parse(chatLink.ToString());
            await Assert.That(chatLinkRoundtrip.ToString()).IsEqualTo(chatLink.ToString());
        }
    }
}

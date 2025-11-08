using GuildWars2.Chat;
using GuildWars2.Tests.TestInfrastructure;
using GuildWars2.Wvw.Objectives;

namespace GuildWars2.Tests.Features.Wvw.Objectives;

public class Objectives
{
    [Test]
    public async Task Can_be_listed()
    {
        Gw2Client sut = Composer.Resolve<Gw2Client>();
        (HashSet<Objective> actual, MessageContext context) = await sut.Wvw.GetObjectives(cancellationToken: TestContext.Current!.Execution.CancellationToken);
        Assert.NotEmpty(actual);
        Assert.Equal(context.ResultCount, actual.Count);
        Assert.Equal(context.ResultTotal, actual.Count);
        Assert.All(actual, entry =>
        {
            Assert.NotEmpty(entry.Id);
            Assert.NotEmpty(entry.Name);
            Assert.True(entry.SectorId > 0);
            Assert.True(entry.MapId > 0);
            Assert.True(entry.MapKind.IsDefined());
            Assert.True(entry.MarkerIconUrl is null or { IsAbsoluteUri: true });
            ObjectiveLink chatLink = entry.GetChatLink();
            Assert.NotEmpty(entry.ChatLink);
            Assert.Equal(entry.ChatLink, chatLink.ToString());
            Assert.Equal(entry.MapId, chatLink.MapId);
            Assert.Equal(entry.Id, $"{chatLink.MapId}-{chatLink.ObjectiveId}");
            ObjectiveLink chatLinkRoundtrip = ObjectiveLink.Parse(chatLink.ToString());
            Assert.Equal(chatLink.ToString(), chatLinkRoundtrip.ToString());
        });
    }
}

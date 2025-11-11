using GuildWars2.Hero.Emotes;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Hero.Emotes;

[ServiceDataSource]
public class Emotes(Gw2Client sut)
{
    [Test]
    public async Task Can_be_listed()
    {
        (HashSet<Emote> actual, MessageContext context) = await sut.Hero.Emotes.GetEmotes(cancellationToken: TestContext.Current!.Execution.CancellationToken);
        Assert.Equal(context.ResultCount, actual.Count);
        Assert.Equal(context.ResultTotal, actual.Count);
        Assert.NotEmpty(actual);
        Assert.All(actual, entry =>
        {
            Assert.NotEmpty(entry.Id);
            Assert.NotEmpty(entry.Commands);
            Assert.NotEmpty(entry.UnlockItemIds);
        });
    }
}

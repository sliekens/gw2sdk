using GuildWars2.Pvp.MistChampions;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Pvp.MistChampions;

[ServiceDataSource]
public class MistChampionById(Gw2Client sut)
{
    [Test]
    public async Task Can_be_found()
    {
        using (Assert.Multiple())
        {
            const string id = "115C140F-C2F5-40EB-8EA2-C3773F2AE468";
            (MistChampion actual, MessageContext context) = await sut.Pvp.GetMistChampionById(id, cancellationToken: TestContext.Current!.Execution.CancellationToken);
            await Assert.That(context).IsNotNull();
            await Assert.That(actual.Id).IsEqualTo(id);
        }
    }
}

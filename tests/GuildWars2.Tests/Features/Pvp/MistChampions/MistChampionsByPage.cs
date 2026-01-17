using GuildWars2.Pvp.MistChampions;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Pvp.MistChampions;

[ServiceDataSource]
public class MistChampionsByPage(Gw2Client sut)
{
    [Test]
    public async Task Can_be_filtered_by_page()
    {
        const int pageSize = 3;
        (IImmutableValueSet<MistChampion> actual, MessageContext context) = await sut.Pvp.GetMistChampionByPage(0, pageSize, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        using (Assert.Multiple())
        {
            await Assert.That(context.Links).IsNotNull();
            await Assert.That(context).Member(c => c.PageSize, m => m.IsEqualTo(pageSize));
            await Assert.That(context).Member(c => c.ResultCount, m => m.IsEqualTo(pageSize));
            await Assert.That(context.PageTotal!.Value).IsGreaterThan(0);
            await Assert.That(context.ResultTotal!.Value).IsGreaterThan(0);
            await Assert.That(actual).Count().IsEqualTo(pageSize);
            foreach (MistChampion entry in actual)
            {
                await Assert.That(entry).IsNotNull();
            }
        }
    }
}

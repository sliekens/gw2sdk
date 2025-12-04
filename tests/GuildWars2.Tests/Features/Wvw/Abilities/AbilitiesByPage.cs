using GuildWars2.Tests.TestInfrastructure.Composition;
using GuildWars2.Wvw.Abilities;

namespace GuildWars2.Tests.Features.Wvw.Abilities;

[ServiceDataSource]
public class AbilitiesByPage(Gw2Client sut)
{
    [Test]
    public async Task Can_be_filtered_by_page()
    {
        const int pageSize = 3;
        (HashSet<Ability> actual, MessageContext context) = await sut.Wvw.GetAbilitiesByPage(0, pageSize, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        await Assert.That(context.Links).IsNotNull();
        await Assert.That(context.PageSize).IsEqualTo(pageSize);
        await Assert.That(context.ResultCount).IsEqualTo(pageSize);
        await Assert.That(context.PageTotal > 0).IsTrue();
        await Assert.That(context.ResultTotal > 0).IsTrue();
        await Assert.That(actual.Count).IsEqualTo(pageSize);
        using (Assert.Multiple())
        {
            foreach (Ability item in actual)
            {
                await Assert.That(item).IsNotNull();
            }
        }
    }
}

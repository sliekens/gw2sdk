using GuildWars2.Tests.TestInfrastructure.Composition;
using GuildWars2.Wvw.Abilities;

namespace GuildWars2.Tests.Features.Wvw.Abilities;

[ServiceDataSource]
public class Abilities(Gw2Client sut)
{
    [Test]
    public async Task Can_be_listed()
    {
        (HashSet<Ability> actual, MessageContext context) = await sut.Wvw.GetAbilities(cancellationToken: TestContext.Current!.Execution.CancellationToken);
        await Assert.That(actual).IsNotEmpty();
        await Assert.That(context).Member(c => c.ResultCount, m => m.IsEqualTo(actual.Count));
        await Assert.That(context).Member(c => c.ResultTotal, m => m.IsEqualTo(actual.Count));
        foreach (Ability entry in actual)
        {
            await Assert.That(entry.Id > 0).IsTrue();
            await Assert.That(entry)
                .Member(e => e.Name, name => name.IsNotEmpty())
                .And.Member(e => e.Description, desc => desc.IsNotEmpty());
#pragma warning disable CS0618 // IconHref is obsolete

            await Assert.That(entry.IconHref).IsNotEmpty();
#pragma warning restore CS0618
            await Assert.That(entry)
                .Member(e => e.IconUrl.IsAbsoluteUri, isAbsolute => isAbsolute.IsTrue())
                .And.Member(e => e.Ranks, ranks => ranks.IsNotEmpty());
        }
    }
}

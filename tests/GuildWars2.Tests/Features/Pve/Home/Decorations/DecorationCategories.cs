using GuildWars2.Pve.Home.Decorations;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Pve.Home.Decorations;

[ServiceDataSource]
public class DecorationCategories(Gw2Client sut)
{
    [Test]
    public async Task Can_be_listed()
    {
        (HashSet<DecorationCategory> actual, MessageContext context) = await sut.Pve.Home.GetDecorationCategories(cancellationToken: TestContext.Current!.Execution.CancellationToken);
        await Assert.That(actual).IsNotEmpty();
        await Assert.That(context).Member(c => c.ResultTotal, m => m.IsEqualTo(actual.Count));
        foreach (DecorationCategory category in actual)
        {
            await Assert.That(category).IsNotNull();
            await Assert.That(category.Id > 0).IsTrue();
            await Assert.That(category.Name).IsNotEmpty();
        }
    }
}

using GuildWars2.Pve.Home.Cats;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Pve.Home.Cats;

[ServiceDataSource]
public class Cats(Gw2Client sut)
{
    [Test]
    public async Task Can_be_listed()
    {
        (HashSet<Cat> actual, MessageContext context) = await sut.Pve.Home.GetCats(cancellationToken: TestContext.Current!.Execution.CancellationToken);
        await Assert.That(actual).IsNotEmpty();
        await Assert.That(context).Member(c => c.ResultTotal, m => m.IsEqualTo(actual.Count));
        foreach (Cat cat in actual)
        {
            await Assert.That(cat).IsNotNull();
            await Assert.That(cat.Id > 0).IsTrue();
            await Assert.That(cat.Hint).IsNotEmpty();
        }
    }
}

using GuildWars2.Pve.Pets;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Pve.Pets;

[ServiceDataSource]
public class Pets(Gw2Client sut)
{
    [Test]
    public async Task Can_be_listed()
    {
        (IImmutableValueSet<Pet> actual, MessageContext context) = await sut.Pve.Pets.GetPets(cancellationToken: TestContext.Current!.Execution.CancellationToken);
        await Assert.That(actual).IsNotEmpty();
        await Assert.That(context).Member(c => c.ResultCount, m => m.IsEqualTo(actual.Count));
        await Assert.That(context).Member(c => c.ResultTotal, m => m.IsEqualTo(actual.Count));
        foreach (Pet entry in actual)
        {
            await Assert.That(entry.Id > 0).IsTrue();
            await Assert.That(entry)
                .Member(e => e.Name, name => name.IsNotEmpty())
                .And.Member(e => e.Description, desc => desc.IsNotEmpty())
                .And.Member(e => e.IconUrl, url => url.IsNotNull())
                .And.Member(e => e.Skills, skills => skills.IsNotEmpty());
        }
    }
}

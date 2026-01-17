using GuildWars2.Hero.Training;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Hero.Training;

[ServiceDataSource]
public class Professions(Gw2Client sut)
{
    [Test]
    public async Task Can_be_listed()
    {
        (IImmutableValueSet<Profession> actual, _) = await sut.Hero.Training.GetProfessions(cancellationToken: TestContext.Current!.Execution.CancellationToken);
        await Assert.That(actual.Count).IsEqualTo(Profession.AllProfessions.Count);
        foreach (Profession profession in actual)
        {
            await Assert.That(profession.Id.IsDefined()).IsTrue();
            await Assert.That(profession.Name).IsNotEmpty();
            await Assert.That(profession.IconUrl is null || profession.IconUrl.IsAbsoluteUri).IsTrue();
            await Assert.That(profession.BigIconUrl is null || profession.BigIconUrl.IsAbsoluteUri).IsTrue();
        }
    }
}

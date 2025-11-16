using GuildWars2.Hero;
using GuildWars2.Hero.Training;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Hero.Training;

[ServiceDataSource]
public class ProfessionNames(Gw2Client sut)
{
    [Test]
    public async Task Can_be_listed()
    {
        (HashSet<Extensible<ProfessionName>> actual, _) = await sut.Hero.Training.GetProfessionNames(TestContext.Current!.Execution.CancellationToken);
        await Assert.That(actual.Count).IsEqualTo(Profession.AllProfessions.Count);
        foreach (Extensible<ProfessionName> name in actual)
        {
            await Assert.That(name.IsDefined()).IsTrue();
        }
    }
}

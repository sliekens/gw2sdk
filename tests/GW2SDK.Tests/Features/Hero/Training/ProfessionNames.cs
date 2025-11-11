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
        Assert.Equal(Profession.AllProfessions.Count, actual.Count);
        Assert.All(actual, name => Assert.True(name.IsDefined()));
    }
}

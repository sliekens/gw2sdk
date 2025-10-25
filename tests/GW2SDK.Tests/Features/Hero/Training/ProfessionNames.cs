using GuildWars2.Hero;
using GuildWars2.Hero.Training;
using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Training;

public class ProfessionNames
{
    [Test]
    public async Task Can_be_listed()
    {
        Gw2Client sut = Composer.Resolve<Gw2Client>();
        (HashSet<Extensible<ProfessionName>> actual, _) = await sut.Hero.Training.GetProfessionNames(TestContext.Current!.CancellationToken);
        Assert.Equal(Profession.AllProfessions.Count, actual.Count);
        Assert.All(actual, name => Assert.True(name.IsDefined()));
    }
}

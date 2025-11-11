using GuildWars2.Hero.Training;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Hero.Training;

[ServiceDataSource]
public class Professions(Gw2Client sut)
{
    [Test]
    public async Task Can_be_listed()
    {
        (HashSet<Profession> actual, _) = await sut.Hero.Training.GetProfessions(cancellationToken: TestContext.Current!.Execution.CancellationToken);
        Assert.Equal(Profession.AllProfessions.Count, actual.Count);
        Assert.All(actual, profession =>
        {
            Assert.True(profession.Id.IsDefined());
            Assert.NotEmpty(profession.Name);
            Assert.True(profession.IconUrl is null || profession.IconUrl.IsAbsoluteUri);
            Assert.True(profession.BigIconUrl is null || profession.BigIconUrl.IsAbsoluteUri);
        });
    }
}

using GuildWars2.Tests.TestInfrastructure.Composition;
using GuildWars2.Tests.TestInfrastructure.Configuration;

namespace GuildWars2.Tests.Features.Hero.Builds;

[ServiceDataSource]
public class StoredBuilds(Gw2Client sut)
{
    [Test]
    public async Task Can_be_listed()
    {
        ApiKey accessToken = TestConfiguration.ApiKey;
        (IReadOnlyList<GuildWars2.Hero.Builds.Build> actual, _) = await sut.Hero.Builds.GetStoredBuilds(accessToken.Key, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        await Assert.That(actual).IsNotEmpty();
        foreach (GuildWars2.Hero.Builds.Build space in actual)
        {
            await Assert.That(space.Name).IsNotNull();
            await Assert.That(space.Profession.IsDefined()).IsTrue();
            await Assert.That(space.Specialization1?.Id is null or > 0).IsTrue();
            await Assert.That(space.Specialization2?.Id is null or > 0).IsTrue();
            await Assert.That(space.Specialization3?.Id is null or > 0).IsTrue();
            await Assert.That(space.Skills).IsNotNull();
            await Assert.That(space.AquaticSkills).IsNotNull();
        }
    }
}

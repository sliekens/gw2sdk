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
        Assert.NotEmpty(actual);
        Assert.All(actual, space =>
        {
            Assert.NotNull(space.Name);
            Assert.True(space.Profession.IsDefined());
            Assert.True(space.Specialization1?.Id is null or > 0);
            Assert.True(space.Specialization2?.Id is null or > 0);
            Assert.True(space.Specialization3?.Id is null or > 0);
            Assert.NotNull(space.Skills);
            Assert.NotNull(space.AquaticSkills);
        });
    }
}

using GuildWars2.Tests.TestInfrastructure.Composition;
using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Builds;

[ServiceDataSource]
public class StoredBuild(Gw2Client sut)
{
    [Test]
    public async Task Can_be_found()
    {
        ApiKey accessToken = TestConfiguration.ApiKey;
        const int slotNumber = 3;
        (GuildWars2.Hero.Builds.Build actual, _) = await sut.Hero.Builds.GetStoredBuild(slotNumber, accessToken.Key, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        Assert.NotNull(actual);
        Assert.NotEmpty(actual.Name);
    }
}

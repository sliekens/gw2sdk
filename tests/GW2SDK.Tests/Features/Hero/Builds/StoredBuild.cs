using GuildWars2.Tests.TestInfrastructure;


namespace GuildWars2.Tests.Features.Hero.Builds;

public class StoredBuild
{

    [Test]

    public async Task Can_be_found()
    {

        Gw2Client sut = Composer.Resolve<Gw2Client>();

        ApiKey accessToken = TestConfiguration.ApiKey;

        const int slotNumber = 3;

        (GuildWars2.Hero.Builds.Build actual, _) = await sut.Hero.Builds.GetStoredBuild(slotNumber, accessToken.Key, cancellationToken: TestContext.Current!.CancellationToken);

        Assert.NotNull(actual);

        Assert.NotEmpty(actual.Name);
    }
}

using GuildWars2.Hero.Equipment.Mounts;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Hero.Equipment.Mounts;

[ServiceDataSource]
public class MountByName(Gw2Client sut)
{
    [Retry(3, RetryOnExceptionTypes = new[] { typeof(System.Net.Http.HttpRequestException) })]
    [Test]
    public async Task Can_be_found_by_name()
    {
        const MountName name = MountName.Skyscale;
        (Mount actual, _) = await sut.Hero.Equipment.Mounts.GetMountByName(name, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        await Assert.That(actual.Id).IsEqualTo(name);
    }
}

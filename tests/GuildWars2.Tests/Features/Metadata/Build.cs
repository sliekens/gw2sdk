using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Metadata;

[ServiceDataSource]
public class Build(Gw2Client sut)
{
    [Test]
    public async Task Current_build_can_be_found()
    {
        (GuildWars2.Metadata.Build actual, _) = await sut.Metadata.GetBuild(cancellationToken: TestContext.Current!.Execution.CancellationToken);
        await Assert.That(actual.Id).IsGreaterThan(115267);
    }
}

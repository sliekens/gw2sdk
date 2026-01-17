using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Hero.Equipment.Mounts;

[ServiceDataSource]
public class MountSkinsIndex(Gw2Client sut)
{
    [Test]
    public async Task Can_be_listed()
    {
        (IImmutableValueSet<int> actual, MessageContext context) =
            await sut.Hero.Equipment.Mounts.GetMountSkinsIndex(
                TestContext.Current!.Execution.CancellationToken
            );

        // https://github.com/gw2-api/issues/issues/134
        await Assert.That(context).Member(c => c.ResultCount, resultCount => resultCount.IsEqualTo(actual.Count + 1))
            .And.Member(c => c.ResultTotal, resultTotal => resultTotal.IsEqualTo(actual.Count + 1));
        await Assert.That(actual).IsNotEmpty();
    }
}

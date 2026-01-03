using GuildWars2.Hero.Masteries;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Hero.Masteries;

[ServiceDataSource]
public class MasteryTrackById(Gw2Client sut)
{
    [Test]
    public async Task Can_be_found()
    {
        const int id = 1;
        (MasteryTrack actual, MessageContext context) = await sut.Hero.Masteries.GetMasteryTrackById(id, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        await Assert.That(context).IsNotNull();
        await Assert.That(actual.Id).IsEqualTo(id);
    }
}

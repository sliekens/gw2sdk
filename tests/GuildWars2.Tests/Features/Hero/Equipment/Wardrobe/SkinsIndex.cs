using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Hero.Equipment.Wardrobe;

[ServiceDataSource]
public class SkinsIndex(Gw2Client sut)
{
    [Test]
    public async Task Can_be_listed()
    {
        (IImmutableValueSet<int> actual, MessageContext context) = await sut.Hero.Equipment.Wardrobe.GetSkinsIndex(TestContext.Current!.Execution.CancellationToken);
        await Assert.That(context.ResultCount).IsEqualTo(actual.Count);
        await Assert.That(context.ResultTotal).IsEqualTo(actual.Count);
        await Assert.That(actual).IsNotEmpty();
    }
}

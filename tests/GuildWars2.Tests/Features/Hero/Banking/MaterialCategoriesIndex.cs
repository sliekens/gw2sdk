using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Hero.Banking;

[ServiceDataSource]
public class MaterialCategoriesIndex(Gw2Client sut)
{
    [Test]
    public async Task Can_be_listed()
    {
        (IImmutableValueSet<int> actual, MessageContext context) = await sut.Hero.Bank.GetMaterialCategoriesIndex(TestContext.Current!.Execution.CancellationToken);
        await Assert.That(context.ResultCount).IsEqualTo(actual.Count);
        await Assert.That(context.ResultTotal).IsEqualTo(actual.Count);
        await Assert.That(actual).IsNotEmpty();
    }
}

using GuildWars2.Hero.Banking;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Hero.Banking;

[ServiceDataSource]
public class MaterialCategoryById(Gw2Client sut)
{
    [Test]
    public async Task Can_be_found()
    {
        const int id = 5;
        (MaterialCategory actual, MessageContext context) = await sut.Hero.Bank.GetMaterialCategoryById(id, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        Assert.NotNull(context);
        Assert.Equal(id, actual.Id);
    }
}

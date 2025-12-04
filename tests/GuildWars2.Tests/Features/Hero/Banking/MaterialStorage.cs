using GuildWars2.Hero.Banking;
using GuildWars2.Tests.TestInfrastructure.Composition;
using GuildWars2.Tests.TestInfrastructure.Configuration;

namespace GuildWars2.Tests.Features.Hero.Banking;

[ServiceDataSource]
public class MaterialStorage(Gw2Client sut)
{
    [Test]
    public async Task Can_be_listed()
    {
        ApiKey accessToken = TestConfiguration.ApiKey;
        (GuildWars2.Hero.Banking.MaterialStorage actual, _) = await sut.Hero.Bank.GetMaterialStorage(accessToken.Key, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        await Assert.That(actual.Materials).IsNotEmpty();
        foreach (MaterialSlot entry in actual.Materials)
        {
            await Assert.That(entry.ItemId > 0).IsTrue();
            await Assert.That(entry.CategoryId > 0).IsTrue();
            await Assert.That(entry.Binding.IsDefined()).IsTrue();
            await Assert.That(entry.Count >= 0).IsTrue();
        }
    }
}

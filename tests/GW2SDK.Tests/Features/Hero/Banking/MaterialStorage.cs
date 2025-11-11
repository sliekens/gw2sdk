using GuildWars2.Tests.TestInfrastructure.Composition;
using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Banking;

[ServiceDataSource]
public class MaterialStorage(Gw2Client sut)
{
    [Test]
    public async Task Can_be_listed()
    {
        ApiKey accessToken = TestConfiguration.ApiKey;
        (GuildWars2.Hero.Banking.MaterialStorage actual, _) = await sut.Hero.Bank.GetMaterialStorage(accessToken.Key, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        Assert.NotEmpty(actual.Materials);
        Assert.All(actual.Materials, entry =>
        {
            Assert.True(entry.ItemId > 0);
            Assert.True(entry.CategoryId > 0);
            Assert.True(entry.Binding.IsDefined());
            Assert.True(entry.Count >= 0);
        });
    }
}

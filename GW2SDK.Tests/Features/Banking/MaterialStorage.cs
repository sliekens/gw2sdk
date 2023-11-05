using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Banking;

public class MaterialStorage
{
    [Fact]
    public async Task Can_be_listed()
    {
        var sut = Composer.Resolve<Gw2Client>();
        var accessToken = Composer.Resolve<ApiKey>();

        var (actual, _) = await sut.Bank.GetMaterialStorage(accessToken.Key);

        Assert.NotEmpty(actual.Materials);
        Assert.All(
            actual.Materials,
            entry =>
            {
                Assert.True(entry.ItemId > 0);
                Assert.True(entry.CategoryId > 0);
                Assert.True(entry.Count >= 0);
            }
        );
    }
}

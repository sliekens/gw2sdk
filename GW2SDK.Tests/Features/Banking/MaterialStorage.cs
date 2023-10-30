using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Banking;

public class MaterialStorage
{
    [Fact]
    public async Task Can_be_listed()
    {
        var sut = Composer.Resolve<Gw2Client>();
        var accessToken = Composer.Resolve<ApiKey>();

        var actual = await sut.Bank.GetMaterialStorage(accessToken.Key);

        Assert.NotEmpty(actual.Value.Materials);
        Assert.All(
            actual.Value.Materials,
            entry =>
            {
                Assert.True(entry.ItemId > 0);
                Assert.True(entry.CategoryId > 0);
                Assert.True(entry.Count >= 0);
            }
        );
    }
}

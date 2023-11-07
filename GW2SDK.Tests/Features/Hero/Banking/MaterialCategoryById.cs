using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Banking;

public class MaterialCategoryById
{
    [Fact]
    public async Task Can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();

        const int id = 5;

        var (actual, _) = await sut.Hero.Bank.GetMaterialCategoryById(id);

        Assert.Equal(id, actual.Id);
        actual.Has_name();
        actual.Has_items();
        actual.Has_order();
    }
}

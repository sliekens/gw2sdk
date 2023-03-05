using System.Threading.Tasks;
using GuildWars2.Tests.TestInfrastructure;
using Xunit;

namespace GuildWars2.Tests.Features.Banking;

public class MaterialCategoryById
{
    [Fact]
    public async Task Can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();

        const int materialCategoryId = 5;

        var actual = await sut.Bank.GetMaterialCategoryById(materialCategoryId);

        Assert.Equal(materialCategoryId, actual.Value.Id);
        actual.Value.Has_name();
        actual.Value.Has_items();
        actual.Value.Has_order();
    }
}

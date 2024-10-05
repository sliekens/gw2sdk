using GuildWars2.Chat;
using GuildWars2.Hero.Equipment.Wardrobe;
using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Pve.Home.Decorations;

public class DecorationCategories
{
    [Fact]
    public async Task Can_be_listed()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var (actual, context) = await sut.Pve.Home.GetDecorationCategories();

        Assert.NotEmpty(actual);
        Assert.Equal(context.ResultTotal, actual.Count);
        Assert.All(
            actual,
            category =>
            {
                Assert.NotNull(category);
                Assert.True(category.Id > 0);
                Assert.NotEmpty(category.Name);
            }
        );
    }
}

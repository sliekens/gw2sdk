using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Pve.Home.Cats;

public class CatsByPage
{
    [Fact]
    public async Task Can_be_filtered_by_page()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var (actual, context) = await sut.Pve.Home.GetCatsByPage(0, 3);

        Assert.Equal(3, actual.Count);
        Assert.Equal(3, context.PageSize);
        Assert.All(
            actual,
            cat =>
            {
                Assert.NotNull(cat);
                Assert.True(cat.Id > 0);
                Assert.NotEmpty(cat.Hint);
            }
        );
    }
}

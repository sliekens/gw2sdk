using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Equipment.Dyes;

public class Colors
{
    [Fact]
    public async Task Can_be_listed()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var (actual, context) = await sut.Hero.Equipment.Dyes.GetColors(cancellationToken: TestContext.Current.CancellationToken);

        Assert.Equal(context.ResultTotal, actual.Count);
        Assert.All(
            actual,
            color =>
            {
                Assert.True(color.Id > 0);
                Assert.NotEmpty(color.Name);
                Assert.False(color.BaseRgb.IsEmpty);
                Assert.False(color.Cloth.Rgb.IsEmpty);
                Assert.False(color.Leather.Rgb.IsEmpty);
                Assert.False(color.Metal.Rgb.IsEmpty);

                if (color.Fur is not null)
                {
                    Assert.False(color.Fur.Rgb.IsEmpty);
                }

                Assert.True(color.Hue.IsDefined());
                Assert.True(color.Material.IsDefined());
                Assert.True(color.Set.IsDefined());

                if (color.ItemId.HasValue)
                {
                    var link = color.GetChatLink();
                    Assert.Equal(color.ItemId, link?.ItemId);
                }
            }
        );
    }
}

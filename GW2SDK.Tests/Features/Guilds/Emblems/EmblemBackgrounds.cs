using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Guilds.Emblems;

public class EmblemBackgrounds
{
    [Fact]
    public async Task Can_be_listed()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var (actual, context) = await sut.Guilds.GetEmblemBackgrounds();

        Assert.NotEmpty(actual);
        Assert.NotNull(context.ResultContext);
        Assert.Equal(context.ResultContext.ResultTotal, actual.Count);
        Assert.All(
            actual,
            emblem =>
            {
                Assert.True(emblem.Id > 0);
                Assert.NotEmpty(emblem.Layers);
                Assert.All(emblem.Layers, url => Assert.NotEmpty(url));
            }
        );
    }
}

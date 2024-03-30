using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Pve.Dungeons;

public class Dungeons
{
    [Fact]
    public async Task Can_be_listed()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var (actual, context) = await sut.Pve.Dungeons.GetDungeons();

        Assert.NotEmpty(actual);
        Assert.Equal(context.ResultCount, actual.Count);
        Assert.Equal(context.ResultTotal, actual.Count);
        Assert.All(
            actual,
            entry =>
            {
                Assert.NotEmpty(entry.Id);
                Assert.NotEmpty(entry.Paths);
                Assert.All(
                    entry.Paths,
                    path =>
                    {
                        Assert.NotEmpty(path.Id);
                        Assert.True(path.Kind.IsDefined());
                    }
                );
            }
        );
    }
}

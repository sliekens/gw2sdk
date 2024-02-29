using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Files;

public class Files
{
    [Fact]
    public async Task Can_be_listed()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var (actual, context) = await sut.Files.GetFiles();

        Assert.NotEmpty(actual);
        Assert.Equal(actual.Count, context.ResultCount);
        Assert.Equal(actual.Count, context.ResultTotal);
        Assert.All(
            actual,
            entry =>
            {
                entry.Has_id();
                entry.Has_icon();
            }
        );
    }
}

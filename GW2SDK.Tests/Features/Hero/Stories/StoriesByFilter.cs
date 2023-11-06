using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Stories;

public class StoriesByFilter
{
    [Fact]
    public async Task Can_be_filtered_by_id()
    {
        var sut = Composer.Resolve<Gw2Client>();

        HashSet<int> ids = new()
        {
            9,
            10,
            11
        };

        var (actual, context) = await sut.Hero.Stories.GetStoriesByIds(ids);

        Assert.Equal(ids.Count, actual.Count);
        Assert.NotNull(context.ResultContext);
        Assert.Equal(ids.Count, context.ResultContext.ResultCount);
        Assert.All(
            actual,
            entry =>
            {
                entry.Has_id();
            }
        );
    }
}

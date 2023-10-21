using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Stories;

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

        var actual = await sut.Stories.GetStoriesByIds(ids);

        Assert.Equal(ids.Count, actual.Value.Count);
        Assert.NotNull(actual.ResultContext);
        Assert.Equal(ids.Count, actual.ResultContext.ResultCount);
        Assert.All(
            actual.Value,
            entry =>
            {
                entry.Has_id();
            }
        );
    }
}

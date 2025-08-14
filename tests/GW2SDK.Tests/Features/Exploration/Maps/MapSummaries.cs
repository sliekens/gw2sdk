using System.Drawing;

using GuildWars2.Exploration.Maps;
using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Exploration.Maps;

public class MapSummaries
{
    [Fact]
    public async Task Can_be_listed()
    {
        Gw2Client sut = Composer.Resolve<Gw2Client>();

        (HashSet<MapSummary> actual, MessageContext context) = await sut.Exploration.GetMapSummaries(
            cancellationToken: TestContext.Current.CancellationToken
        );

        Assert.NotEmpty(actual);
        Assert.Equal(context.ResultCount, actual.Count);
        Assert.Equal(context.ResultTotal, actual.Count);
        Assert.All(
            actual,
            entry =>
            {
                Assert.True(entry.Id > 0);
                if (entry.Id == 1150)
                {
                    // Unnamed Salvation Pass (Public) map
                    Assert.Empty(entry.Name);
                }
                else
                {
                    Assert.NotEmpty(entry.Name);
                }

                Assert.True(entry.MinLevel >= 0);
                Assert.True(entry.MaxLevel >= entry.MinLevel);
                Assert.True(entry.Kind.IsDefined());
                Assert.NotEmpty(entry.Floors);
                Assert.NotNull(entry.RegionName);
                Assert.NotNull(entry.ContinentName);
                Assert.NotEqual(Rectangle.Empty, entry.MapRectangle);
                Assert.NotEqual(Rectangle.Empty, entry.ContinentRectangle);
            }
        );
    }
}

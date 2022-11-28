using System.Threading.Tasks;
using GuildWars2.Tests.TestInfrastructure;
using Xunit;

namespace GuildWars2.Tests.Features.Gliders;

public class Gliders
{
    [Fact]
    public async Task Can_be_enumerated()
    {
        await using Composer services = new();
        var sut = services.Resolve<Gw2Client>();

        var actual = await sut.Gliders.GetGliders();

        Assert.NotEmpty(actual);
        Assert.Equal(actual.Count, actual.Context.ResultCount);
        Assert.Equal(actual.Count, actual.Context.ResultTotal);
        Assert.All(
            actual,
            entry =>
            {
                entry.Has_id();
                entry.Has_unlock_items();
                entry.Has_order();
                entry.Has_icon();
                entry.Has_name();
                entry.Has_description();
                entry.Has_default_dyes();
            }
        );
    }
}

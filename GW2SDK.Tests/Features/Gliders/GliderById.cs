using System.Threading.Tasks;
using GuildWars2.Tests.TestInfrastructure;
using Xunit;

namespace GuildWars2.Tests.Features.Gliders;

public class GliderById
{
    [Fact]
    public async Task Can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();

        const int gliderId = 58;

        var actual = await sut.Gliders.GetGliderById(gliderId);

        Assert.Equal(gliderId, actual.Value.Id);
        actual.Value.Has_unlock_items();
        actual.Value.Has_order();
        actual.Value.Has_icon();
        actual.Value.Has_name();
        actual.Value.Has_description();
        actual.Value.Has_default_dyes();
    }
}

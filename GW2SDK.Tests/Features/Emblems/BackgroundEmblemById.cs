using System.Threading.Tasks;
using GuildWars2.Tests.TestInfrastructure;
using Xunit;

namespace GuildWars2.Tests.Features.Emblems;

public class BackgroundEmblemById
{
    [Fact]
    public async Task Can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();

        const int id = 1;

        var actual = await sut.Emblems.GetBackgroundEmblemById(id);

        Assert.Equal(id, actual.Value.Id);
    }
}

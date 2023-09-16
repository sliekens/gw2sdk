using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using GuildWars2.Meta;
using GuildWars2.Tests.TestInfrastructure;
using Xunit;

namespace GuildWars2.Tests.Features.Meta;

public class Build
{
    [Fact]
    public async Task Build_is_stuck()
    {
        using var http = Composer.Resolve<HttpClient>();
        var request = new BuildRequest();
        var response = await request.SendAsync(http, CancellationToken.None);
        var actual = response.Value;
        Assert.Equal(115267, actual.Id);
    }

    [Fact]
    public async Task It_can_get_the_current_build()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var actual = await sut.Meta.GetBuild();

        Assert.True(actual.Value.Id >= 127440);
    }
}
using System.Net.Http;
using System.Threading.Tasks;

using GW2SDK.Builds;
using GW2SDK.Builds.Http;
using GW2SDK.Builds.Json;
using GW2SDK.Http;
using GW2SDK.Json;
using GW2SDK.Tests.TestInfrastructure;

using Xunit;

namespace GW2SDK.Tests.Features.Builds;

public class BuildServiceTest
{
    [Fact]
    public async Task Build_is_stuck()
    {
        await using Composer services = new();
        var http = services.Resolve<HttpClient>();
        using var response = await http.SendAsync(new BuildRequest(), HttpCompletionOption.ResponseHeadersRead);
        using var json = await response.Content.ReadAsJsonAsync(default);
        response.EnsureSuccessStatusCode();

        var actual = BuildReader.Read(json.RootElement, MissingMemberBehavior.Error);

        Assert.Equal(115267, actual.Id);
    }

    [Fact]
    public async Task It_can_get_the_current_build()
    {
        await using Composer services = new();
        var sut = services.Resolve<BuildService>();

        var actual = await sut.GetBuild();

        Assert.True(actual.Value.Id >= 127440);
    }
}
using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Meta;

public class Build
{
    [Fact]
    public async Task Build_is_stuck()
    {
        var builder = new UriBuilder(BaseAddress.DefaultUri)
        {
            Path = "v2/build",
            Query = "v=" + SchemaVersion.Recommended
        };

        // The API has been stuck on build 115267 since at least 2021-05-27
        using var http = Composer.Resolve<HttpClient>();
        var actual = await http.GetStringAsync(builder.Uri);
        Assert.Contains("115267", actual);
    }

    [Fact]
    public async Task Current_build_can_be_found()
    {
        // A undocumented API is used to find the current build
        // The same API is used by the Guild Wars 2 launcher to check for updates
        // So in a sense, this is the "official" way to find the current build
        var sut = Composer.Resolve<Gw2Client>();

        var actual = await sut.Meta.GetBuild();

        Assert.True(actual.Value.Id > 115267);
    }
}

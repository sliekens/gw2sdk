using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Metadata;

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
#if NET
        var actual = await http.GetStringAsync(builder.Uri, TestContext.Current.CancellationToken);
#else
        var actual = await http.GetStringAsync(builder.Uri);
#endif
        Assert.Contains("115267", actual);
    }

    [Fact]
    public async Task Current_build_can_be_found()
    {
        // A undocumented API is used to find the current build
        // The same API is used by the Guild Wars 2 launcher to check for updates
        // So in a sense, this is the "official" way to find the current build
        var sut = Composer.Resolve<Gw2Client>();

        var (actual, _) = await sut.Metadata.GetBuild(cancellationToken: TestContext.Current.CancellationToken);

        Assert.True(actual.Id > 115267);
    }
}

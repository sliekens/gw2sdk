using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Meta;

public class V2
{
    [Fact]
    public async Task Has_api_metadata()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var actual = await sut.Meta.GetApiVersion();

        actual.Value.There_are_no_newer_translations();
        actual.Value.There_are_no_surprise_endpoints();
        actual.Value.There_are_no_newer_schema_versions();
    }
}

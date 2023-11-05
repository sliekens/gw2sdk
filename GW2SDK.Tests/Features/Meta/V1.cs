using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Meta;

public class V1
{
    [Fact]
    public async Task Has_api_metadata()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var (actual, _) = await sut.Meta.GetApiVersion("v1");

        actual.There_are_no_newer_translations();
    }
}

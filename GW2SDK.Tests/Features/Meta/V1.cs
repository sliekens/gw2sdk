using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Meta;

public class V1
{
    [Fact]
    public async Task Has_api_metadata()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var actual = await sut.Meta.GetApiVersion("v1");

        actual.Value.There_are_no_newer_translations();
    }
}

using System.Threading.Tasks;
using GuildWars2.Tests.TestInfrastructure;
using Xunit;

namespace GuildWars2.Tests.Features.Guilds.Search;

public class GuildsByName
{
    [Fact]
    public async Task Is_not_empty()
    {
        await using Composer services = new();
        var sut = services.Resolve<Gw2Client>();

        var actual = await sut.Guilds.GetGuildsByName("noobs");

        var guild = Assert.Single(actual.Value);

        Assert.Equal("EFD3B9E7-CE5C-41B7-BE02-70F07E63BB49", guild);
    }
}

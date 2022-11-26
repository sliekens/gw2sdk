using System.Threading.Tasks;
using GW2SDK.Tests.TestInfrastructure;
using Xunit;

namespace GW2SDK.Tests.Features.Pvp.Heroes;

public class HeroById
{
    [Fact]
    public async Task Can_be_found()
    {
        await using Composer services = new();
        var sut = services.Resolve<Gw2Client>();

        const string id = "115C140F-C2F5-40EB-8EA2-C3773F2AE468";

        var actual = await sut.Pvp.GetHeroById(id);

        Assert.Equal(id, actual.Value.Id);
        actual.Value.Has_name();
    }
}

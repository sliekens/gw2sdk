using System.Threading.Tasks;
using GuildWars2.Tests.TestInfrastructure;
using Xunit;

namespace GuildWars2.Tests.Features.Quaggans;

public class QuagganById
{
    [Fact]
    public async Task Can_be_found()
    {
        await using Composer services = new();
        var sut = services.Resolve<Gw2Client>();

        const string quagganId = "present";

        var actual = await sut.Quaggans.GetQuagganById(quagganId);

        Assert.Equal(quagganId, actual.Value.Id);
        actual.Value.Quaggan_has_picture();
    }
}

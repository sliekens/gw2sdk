using System.Threading.Tasks;
using GuildWars2.Tests.TestInfrastructure;
using Xunit;

namespace GuildWars2.Tests.Features.Quaggans;

public class QuagganById
{
    [Fact]
    public async Task Can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();

        const string id = "present";

        var actual = await sut.Quaggans.GetQuagganById(id);

        Assert.Equal(id, actual.Value.Id);
        actual.Value.Quaggan_has_picture();
    }
}

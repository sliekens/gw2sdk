using System.Threading.Tasks;
using GW2SDK.Tests.TestInfrastructure;
using Xunit;

namespace GW2SDK.Tests.Features.Quaggans;

public class QuaggansByPage
{
    [Fact]
    public async Task Can_be_filtered_by_page()
    {
        await using Composer services = new();
        var sut = services.Resolve<Gw2Client>();

        const int pageSize = 3;
        var actual = await sut.Quaggans.GetQuaggansByPage(0, pageSize);

        Assert.Equal(pageSize, actual.Count);
        Assert.Equal(pageSize, actual.Context.PageSize);
        Assert.Equal(pageSize, actual.Context.ResultCount);
        Assert.All(
            actual,
            entry =>
            {
                entry.Id_is_not_empty();
                entry.Quaggan_has_picture();
            }
        );
    }
}

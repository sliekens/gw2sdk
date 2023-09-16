using System.Threading.Tasks;
using GuildWars2.Tests.TestInfrastructure;
using Xunit;

namespace GuildWars2.Tests.Features.Traits;

public class TraitById
{
    [Fact]
    public async Task Can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();

        const int id = 214;

        var actual = await sut.Traits.GetTraitById(id);

        Assert.Equal(id, actual.Value.Id);
    }
}

using System.Threading.Tasks;
using GuildWars2.Tests.TestInfrastructure;
using Xunit;

namespace GuildWars2.Tests.Features.Professions;

public class ProfessionByName
{
    [Fact]
    public async Task Can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();

        const ProfessionName name = ProfessionName.Engineer;

        var actual = await sut.Professions.GetProfessionByName(name);

        Assert.Equal(name, actual.Value.Id);
    }
}

using System.Threading.Tasks;
using GuildWars2.Tests.TestInfrastructure;
using Xunit;

namespace GuildWars2.Tests.Features.Stories;

public class BackstoryQuestionById
{
    [Fact]
    public async Task Can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();

        const int id = 7;

        var actual = await sut.Stories.GetBackstoryQuestionById(id);

        Assert.Equal(id, actual.Value.Id);
    }
}

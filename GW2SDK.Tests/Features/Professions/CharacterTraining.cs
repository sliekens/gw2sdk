using System.Threading.Tasks;
using GuildWars2.Tests.TestInfrastructure;
using Xunit;

namespace GuildWars2.Tests.Features.Professions;

public class CharacterTraining
{
    [Fact]
    public async Task Can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();
        var character = Composer.Resolve<TestCharacter>();
        var accessToken = Composer.Resolve<ApiKey>();

        var actual = await sut.Professions.GetCharacterTraining(character.Name, accessToken.Key);

        Assert.NotEmpty(actual.Value.Training);
        Assert.All(actual.Value.Training, entry => Assert.NotEqual(0, entry.Spent));
        Assert.All(actual.Value.Training, entry => Assert.True(entry.Done));
    }
}

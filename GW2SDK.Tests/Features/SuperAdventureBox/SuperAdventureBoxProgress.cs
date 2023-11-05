using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.SuperAdventureBox;

public class SuperAdventureBoxProgress
{
    [Fact]
    public async Task Can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();
        var character = Composer.Resolve<TestCharacter>();
        var accessToken = Composer.Resolve<ApiKey>();

        var (actual, _) = await sut.SuperAdventureBox.GetSuperAdventureBoxProgress(
            character.Name,
            accessToken.Key
        );

        Assert.NotEmpty(actual.Zones);
        Assert.NotEmpty(actual.Unlocks);
        Assert.NotEmpty(actual.Songs);
    }
}

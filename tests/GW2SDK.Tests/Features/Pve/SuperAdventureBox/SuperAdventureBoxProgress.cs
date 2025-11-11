using GuildWars2.Tests.TestInfrastructure;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Pve.SuperAdventureBox;

[ServiceDataSource]
public class SuperAdventureBoxProgress(Gw2Client sut)
{
    [Test]
    public async Task Can_be_found()
    {
        TestCharacter character = TestConfiguration.TestCharacter;
        ApiKey accessToken = TestConfiguration.ApiKey;
        (GuildWars2.Pve.SuperAdventureBox.SuperAdventureBoxProgress actual, _) = await sut.Pve.SuperAdventureBox.GetSuperAdventureBoxProgress(character.Name, accessToken.Key, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        Assert.NotEmpty(actual.Zones);
        Assert.All(actual.Zones, zone =>
        {
            Assert.True(zone.Id > 0);
            Assert.True(zone.Mode.IsDefined());
            Assert.True(zone.World > 0);
            Assert.True(zone.Zone > 0);
        });
        Assert.NotEmpty(actual.Unlocks);
        Assert.All(actual.Unlocks, upgrade =>
        {
            Assert.True(upgrade.Id > 0);
            Assert.NotNull(upgrade.Name);
        });
        Assert.NotEmpty(actual.Songs);
        Assert.All(actual.Songs, song =>
        {
            Assert.True(song.Id > 0);
            Assert.NotEmpty(song.Name);
        });
    }
}

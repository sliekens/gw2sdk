using GuildWars2.Tests.TestInfrastructure.Composition;
using GuildWars2.Tests.TestInfrastructure.Configuration;

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
        await Assert.That(actual.Zones).IsNotEmpty();
        foreach (GuildWars2.Pve.SuperAdventureBox.SuperAdventureBoxZone zone in actual.Zones)
        {
            await Assert.That(zone.Id > 0).IsTrue();
            await Assert.That(zone.Mode.IsDefined()).IsTrue();
            await Assert.That(zone.World > 0).IsTrue();
            await Assert.That(zone.Zone > 0).IsTrue();
        }

        await Assert.That(actual.Unlocks).IsNotEmpty();
        foreach (GuildWars2.Pve.SuperAdventureBox.SuperAdventureBoxUpgrade upgrade in actual.Unlocks)
        {
            await Assert.That(upgrade.Id > 0).IsTrue();
            await Assert.That(upgrade.Name).IsNotNull();
        }

        await Assert.That(actual.Songs).IsNotEmpty();
        foreach (GuildWars2.Pve.SuperAdventureBox.SuperAdventureBoxSong song in actual.Songs)
        {
            await Assert.That(song.Id > 0).IsTrue();
            await Assert.That(song.Name).IsNotEmpty();
        }
    }
}

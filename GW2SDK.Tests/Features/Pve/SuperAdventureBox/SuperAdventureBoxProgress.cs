﻿using GuildWars2.Http;
using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Pve.SuperAdventureBox;

public class SuperAdventureBoxProgress
{
    [Fact]
    public async Task ErrInternal()
    {
        var reason = await Assert.ThrowsAsync<BadResponseException>(Can_be_found);

        Assert.Equal("ErrInternal", reason.Message);
    }

    [Fact(Skip = "ErrInternal")]
    public async Task Can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();
        var character = TestConfiguration.TestCharacter;
        var accessToken = TestConfiguration.ApiKey;


        var (actual, _) = await sut.Pve.SuperAdventureBox.GetSuperAdventureBoxProgress(
            character.Name,
            accessToken.Key,
            cancellationToken: TestContext.Current.CancellationToken
        );

        Assert.NotEmpty(actual.Zones);
        Assert.All(
            actual.Zones,
            zone =>
            {
                Assert.True(zone.Id > 0);
                Assert.True(zone.Mode.IsDefined());
                Assert.True(zone.World > 0);
                Assert.True(zone.Zone > 0);
            }
        );

        Assert.NotEmpty(actual.Unlocks);
        Assert.All(
            actual.Unlocks,
            upgrade =>
            {
                Assert.True(upgrade.Id > 0);
                Assert.NotNull(upgrade.Name);
            }
        );

        Assert.NotEmpty(actual.Songs);
        Assert.All(
            actual.Songs,
            song =>
            {
                Assert.True(song.Id > 0);
                Assert.NotEmpty(song.Name);
            }
        );
    }
}

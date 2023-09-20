﻿using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.SuperAdventureBox;

public class SuperAdventureBoxProgress
{
    [Fact]
    public async Task Can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();
        var character = Composer.Resolve<TestCharacter>();
        var accessToken = Composer.Resolve<ApiKey>();

        var actual =
            await sut.SuperAdventureBox.GetSuperAdventureBoxProgress(
                character.Name,
                accessToken.Key
            );

        Assert.NotEmpty(actual.Value.Zones);
        Assert.NotEmpty(actual.Value.Unlocks);
        Assert.NotEmpty(actual.Value.Songs);
    }
}

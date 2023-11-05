﻿using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Professions;

public class ProfessionByName
{
    [Fact]
    public async Task Can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();

        const ProfessionName name = ProfessionName.Engineer;

        var (actual, _) = await sut.Professions.GetProfessionByName(name);

        Assert.Equal(name, actual.Id);
    }
}

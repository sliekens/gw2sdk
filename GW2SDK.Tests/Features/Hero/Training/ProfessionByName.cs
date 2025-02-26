﻿using GuildWars2.Hero;
using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Training;

public class ProfessionByName
{
    [Fact]
    public async Task Can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();

        const ProfessionName name = ProfessionName.Engineer;

        var (actual, _) = await sut.Hero.Training.GetProfessionByName(
            name,
            cancellationToken: TestContext.Current.CancellationToken
        );

        Assert.Equal(name, actual.Id);
    }
}

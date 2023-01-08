﻿using System.Threading.Tasks;
using GuildWars2.Tests.TestInfrastructure;
using Xunit;

namespace GuildWars2.Tests.Features.Exploration.HeroChallenges;

public class CompletedHeroChallenges
{
    [Fact]
    public async Task Can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();
        var character = Composer.Resolve<TestCharacter>();
        var accessToken = Composer.Resolve<ApiKey>();

        var actual = await sut.Maps.GetCompletedHeroChallenges(character.Name, accessToken.Key);

        Assert.NotEmpty(actual.Value);
    }
}

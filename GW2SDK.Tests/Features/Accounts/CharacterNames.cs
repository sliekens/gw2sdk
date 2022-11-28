﻿using System.Threading.Tasks;
using GuildWars2.Tests.TestInfrastructure;
using Xunit;

namespace GuildWars2.Tests.Features.Accounts;

public class CharacterNames
{
    [Fact]
    public async Task Can_be_enumerated()
    {
        await using Composer services = new();
        var sut = services.Resolve<Gw2Client>();
        var accessToken = services.Resolve<ApiKey>();

        var actual = await sut.Accounts.GetCharactersIndex(accessToken.Key);

        var expected = services.Resolve<TestCharacterName>().Name;

        Assert.Contains(expected, actual);
    }
}

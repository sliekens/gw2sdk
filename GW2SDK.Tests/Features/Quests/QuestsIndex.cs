﻿using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Quests;

public class QuestsIndex
{
    [Fact]
    public async Task Can_be_listed()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var actual = await sut.Quests.GetQuestsIndex();

        Assert.NotEmpty(actual.Value);
        Assert.NotNull(actual.ResultContext);
        Assert.Equal(actual.Value.Count, actual.ResultContext.ResultCount);
        Assert.Equal(actual.Value.Count, actual.ResultContext.ResultTotal);
    }
}

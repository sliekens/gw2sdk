﻿using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Stories;

public class BackstoryQuestionsIndex
{
    [Fact]
    public async Task Can_be_listed()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var actual = await sut.Stories.GetBackstoryQuestionsIndex();

        Assert.NotEmpty(actual.Value);
        Assert.NotNull(actual.Context.ResultContext);
        Assert.Equal(actual.Value.Count, actual.Context.ResultContext.ResultCount);
        Assert.Equal(actual.Value.Count, actual.Context.ResultContext.ResultTotal);
    }
}

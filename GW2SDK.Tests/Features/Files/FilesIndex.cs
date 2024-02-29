﻿using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Files;

public class FilesIndex
{
    [Fact]
    public async Task Can_be_listed()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var (actual, context) = await sut.Files.GetFilesIndex();

        Assert.NotEmpty(actual);
        Assert.Equal(actual.Count, context.ResultCount);
        Assert.Equal(actual.Count, context.ResultTotal);
    }
}

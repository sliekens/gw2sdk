﻿using System.Threading.Tasks;
using GuildWars2.Tests.TestInfrastructure;
using Xunit;

namespace GuildWars2.Tests.Features.Finishers;

public class FinisherById
{
    [Fact]
    public async Task Can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();

        const int finisherId = 58;

        var actual = await sut.Finishers.GetFinisherById(finisherId);

        Assert.Equal(finisherId, actual.Value.Id);
        actual.Value.Has_unlock_details();
        actual.Value.Has_unlock_items();
        actual.Value.Has_order();
        actual.Value.Has_icon();
        actual.Value.Has_name();
    }
}

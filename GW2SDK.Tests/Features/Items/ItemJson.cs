﻿using System.Text.Json;
using GuildWars2.Items;

namespace GuildWars2.Tests.Features.Items;

public class ItemJson : IClassFixture<ItemFixture>
{
    public ItemJson(ItemFixture fixture)
    {
        this.fixture = fixture;
    }

    private readonly ItemFixture fixture;

    [Fact]
    public void Items_can_be_created_from_json() =>
        Assert.All(
            fixture.Items,
            json =>
            {
                using var document = JsonDocument.Parse(json);
                var actual = document.RootElement.GetItem(MissingMemberBehavior.Error);
                actual.Validate();
            }
        );
}

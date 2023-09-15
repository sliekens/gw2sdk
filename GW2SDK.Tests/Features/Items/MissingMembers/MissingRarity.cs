using System;
using System.Text.Json;
using GuildWars2.Items;
using Xunit;

namespace GuildWars2.Tests.Features.Items;

public class MissingRarity
{
    [Fact]
    public void Throws_by_default()
    {
        const string text = """
            { 
              "type": "Trophy",
              "rarity": "Epic",
              "id": 0,
              "name": "",
              "level": 0,
              "vendor_value": 0,
              "game_types": [],
              "flags": [],
              "restrictions": [],
              "chat_link": ""
            }
            """;

        using var json = JsonDocument.Parse(text);

        Item Act()
        {
            return json.RootElement.GetItem(default);
        }

        var actual = Assert.ThrowsAny<InvalidOperationException>(Act);

        Assert.Equal("Value for 'rarity' is incompatible.", actual.Message);
        Assert.Equal("Unexpected member 'Epic'.", actual.InnerException!.Message);
    }

    [Fact]
    public void Uses_generated_value_when_MissingMemberBehavior_is_Undefined()
    {
        const string text = """
            { 
              "type": "Trophy",
              "rarity": "Epic",
              "id": 0,
              "name": "",
              "level": 0,
              "vendor_value": 0,
              "game_types": [],
              "flags": [],
              "restrictions": [],
              "chat_link": ""
            }
            """;

        using var json = JsonDocument.Parse(text);

        var actual = json.RootElement.GetItem(MissingMemberBehavior.Undefined);

        var trophy = Assert.IsType<Trophy>(actual);

        const Rarity epic = (Rarity)126655543;
        Assert.Equal(epic, trophy.Rarity);
    }
}

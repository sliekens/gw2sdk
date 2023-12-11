using System.Text.Json;
using GuildWars2.Items;

namespace GuildWars2.Tests.Features.Items.MissingMembers;

public class MissingGatheringTool
{
    [Fact]
    public void Throws_by_default()
    {
        const string text = """
                            {
                              "type": "Gathering",
                              "details": {
                                "type": "Hunting"
                              }
                            }
                            """;

        using var json = JsonDocument.Parse(text);

        Item Act()
        {
            return json.RootElement.GetItem(default);
        }

        var actual = Assert.ThrowsAny<InvalidOperationException>(Act);

        Assert.Equal("Unexpected discriminator value 'Hunting'.", actual.Message);
    }

    [Fact]
    public void Uses_base_type_when_MissingMemberBehavior_is_Undefined()
    {
        const string text = """
                            {
                              "type": "Gathering",
                              "details": {
                                "type": "Hunting"
                              },
                              "id": 0,
                              "name": "",
                              "level": 0,
                              "rarity": "",
                              "vendor_value": 0,
                              "game_types": [],
                              "flags": [],
                              "restrictions": [],
                              "chat_link": ""
                            }
                            """;

        using var json = JsonDocument.Parse(text);

        var actual = json.RootElement.GetItem(MissingMemberBehavior.Undefined);

        Assert.IsType<GatheringTool>(actual);
    }
}

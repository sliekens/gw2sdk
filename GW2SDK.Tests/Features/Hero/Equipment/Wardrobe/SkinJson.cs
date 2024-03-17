using System.Text.Json;
using GuildWars2.Hero.Equipment.Wardrobe;

namespace GuildWars2.Tests.Features.Hero.Equipment.Wardrobe;

public class SkinJson(SkinFixture fixture) : IClassFixture<SkinFixture>
{
    [Fact]
    public void Skins_can_be_created_from_json() =>
        Assert.All(
            fixture.Skins,
            json =>
            {
                using var document = JsonDocument.Parse(json);

                var actual = document.RootElement.GetEquipmentSkin(MissingMemberBehavior.Error);
                var link = actual.GetChatLink();

                actual.Has_id();
                actual.Has_races();
                Assert.Equal(actual.Id, link.SkinId);
            }
        );
}

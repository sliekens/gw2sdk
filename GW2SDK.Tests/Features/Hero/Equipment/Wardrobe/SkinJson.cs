using System.Text.Json;
using GuildWars2.Hero.Equipment.Wardrobe;

namespace GuildWars2.Tests.Features.Hero.Equipment.Wardrobe;

public class SkinJson : IClassFixture<SkinFixture>
{
    public SkinJson(SkinFixture fixture)
    {
        this.fixture = fixture;
    }

    private readonly SkinFixture fixture;

    [Fact]
    public void Skins_can_be_created_from_json() =>
        Assert.All(
            fixture.Skins,
            json =>
            {
                using var document = JsonDocument.Parse(json);

                var actual = document.RootElement.GetSkin(MissingMemberBehavior.Error);

                actual.Has_id();
            }
        );
}

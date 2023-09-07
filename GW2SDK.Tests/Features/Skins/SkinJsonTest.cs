using System.Text.Json;
using GuildWars2.Skins;
using Xunit;

namespace GuildWars2.Tests.Features.Skins;

public class SkinJsonTest : IClassFixture<SkinFixture>
{
    public SkinJsonTest(SkinFixture fixture)
    {
        this.fixture = fixture;
    }

    private readonly SkinFixture fixture;


    [Fact]
    public void Skins_can_be_created_from_json()
    {
        foreach (var json in fixture.Skins)
        {
            using var document = JsonDocument.Parse(json);

            var actual = document.RootElement.GetSkin(MissingMemberBehavior.Error);

            actual.Id_is_positive();
        }
    }
}

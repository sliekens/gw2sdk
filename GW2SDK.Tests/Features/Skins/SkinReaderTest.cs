using System.Text.Json;
using GuildWars2.Skins;
using GuildWars2.Tests.TestInfrastructure;
using Xunit;

namespace GuildWars2.Tests.Features.Skins;

public class SkinReaderTest : IClassFixture<SkinFixture>
{
    public SkinReaderTest(SkinFixture fixture)
    {
        this.fixture = fixture;
    }

    private readonly SkinFixture fixture;

    private static class SkinFact
    {
        public static void Id_is_positive(Skin actual) =>
            Assert.InRange(actual.Id, 1, int.MaxValue);
    }

    [Fact]
    public void Skins_can_be_created_from_json()
    {
        foreach (var json in fixture.Skins)
        {
            using var document = JsonDocument.Parse(json);

            var actual = document.RootElement.GetSkin(MissingMemberBehavior.Error);

            SkinFact.Id_is_positive(actual);
        }
    }
}

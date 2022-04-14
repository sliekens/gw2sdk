using System.Text.Json;

using GW2SDK.Json;
using GW2SDK.Skins;
using GW2SDK.Skins.Json;
using GW2SDK.Skins.Models;
using GW2SDK.Tests.TestInfrastructure;

using Xunit;

namespace GW2SDK.Tests.Features.Skins;

public class SkinReaderTest : IClassFixture<SkinFixture>
{
    public SkinReaderTest(SkinFixture fixture)
    {
        this.fixture = fixture;
    }

    private readonly SkinFixture fixture;

    private static class SkinFact
    {
        public static void Id_is_positive(Skin actual)
        {
            Assert.InRange(actual.Id, 1, int.MaxValue);
        }
    }

    [Fact]
    public void Skins_can_be_created_from_json()
    {
        AssertEx.ForEach(fixture.Skins,
            json =>
            {
                using var document = JsonDocument.Parse(json);

                var actual = SkinReader.Read(document.RootElement, MissingMemberBehavior.Error);

                SkinFact.Id_is_positive(actual);
            });
    }
}
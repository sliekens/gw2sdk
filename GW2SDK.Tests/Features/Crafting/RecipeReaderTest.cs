using System.Text.Json;
using GW2SDK.Crafting;
using GW2SDK.Json;
using Xunit;

namespace GW2SDK.Tests.Features.Crafting;

public class RecipeReaderTest : IClassFixture<RecipeFixture>
{
    public RecipeReaderTest(RecipeFixture fixture)
    {
        this.fixture = fixture;
    }

    private readonly RecipeFixture fixture;

    [Fact]
    public void Recipes_can_be_created_from_json() =>
        Assert.All(
            fixture.Recipes,
            json =>
            {
                using var document = JsonDocument.Parse(json);
                var actual = RecipeReader.Read(document.RootElement, MissingMemberBehavior.Error);

                RecipeFacts.Validate(actual);
            }
            );
}

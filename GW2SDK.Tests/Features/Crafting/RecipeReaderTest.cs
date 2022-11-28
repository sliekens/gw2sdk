﻿using System.Text.Json;
using GuildWars2.Crafting;
using Xunit;

namespace GuildWars2.Tests.Features.Crafting;

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
                var actual = document.RootElement.GetRecipe(MissingMemberBehavior.Error);

                RecipeFacts.Validate(actual);
            }
        );
}

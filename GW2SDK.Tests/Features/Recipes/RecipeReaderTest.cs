﻿using System.Text.Json;
using GW2SDK.Json;
using GW2SDK.Recipes;
using GW2SDK.Tests.TestInfrastructure;
using Xunit;

namespace GW2SDK.Tests.Features.Recipes
{
    public class RecipeReaderTest : IClassFixture<RecipeFixture>
    {
        public RecipeReaderTest(RecipeFixture fixture)
        {
            _fixture = fixture;
        }

        private readonly RecipeFixture _fixture;

        [Fact]
        [Trait("Feature",    "Recipes")]
        [Trait("Category",   "Integration")]
        [Trait("Importance", "Critical")]
        public void Recipes_can_be_created_from_json()
        {
            var sut = new RecipeReader();

            AssertEx.ForEach(_fixture.Recipes,
                json =>
                {
                    using var document = JsonDocument.Parse(json);
                    var actual = sut.Read(document.RootElement, MissingMemberBehavior.Error);

                    RecipeFacts.Validate(actual);
                });
        }
    }
}

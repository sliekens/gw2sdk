using GW2SDK.Features.Recipes;
using GW2SDK.Infrastructure;
using GW2SDK.Tests.Features.Recipes.Fixtures;
using GW2SDK.Tests.Shared;
using Newtonsoft.Json;
using Xunit;
using Xunit.Abstractions;

namespace GW2SDK.Tests.Features.Recipes
{
    [Collection(nameof(RecipeDbCollection))]
    public class RecipeTest
    {
        public RecipeTest(RecipeFixture fixture, ITestOutputHelper output)
        {
            _fixture = fixture;
            _output = output;
        }

        private readonly RecipeFixture _fixture;

        private readonly ITestOutputHelper _output;

        [Fact]
        [Trait("Feature",    "Recipes")]
        [Trait("Category",   "Integration")]
        [Trait("Importance", "Critical")]
        public void Class_ShouldHaveNoMissingMembers()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output))
                                                              .UseMissingMemberHandling(MissingMemberHandling.Error)
                                                              .Build();
            AssertEx.ForEach(_fixture.Db.Recipes,
                json =>
                {
                    // Next statement throws if there are missing members
                    _ = JsonConvert.DeserializeObject<Recipe>(json, settings);
                });
        }

        [Fact]
        [Trait("Feature",  "Recipes")]
        [Trait("Category", "Integration")]
        public void Id_ShouldBePositive()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();
            AssertEx.ForEach(_fixture.Db.Recipes,
                json =>
                {
                    var actual = JsonConvert.DeserializeObject<Recipe>(json, settings);
                    Assert.InRange(actual.Id, 1, int.MaxValue);
                });
        }

        [Fact]
        [Trait("Feature",  "Recipes")]
        [Trait("Category", "Integration")]
        public void OutputItemId_ShouldBePositive()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();
            AssertEx.ForEach(_fixture.Db.Recipes,
                json =>
                {
                    var actual = JsonConvert.DeserializeObject<Recipe>(json, settings);
                    Assert.InRange(actual.OutputItemId, 1, int.MaxValue);
                });
        }

        [Fact]
        [Trait("Feature",  "Recipes")]
        [Trait("Category", "Integration")]
        public void OutputItemCount_ShouldBePositive()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();
            AssertEx.ForEach(_fixture.Db.Recipes,
                json =>
                {
                    var actual = JsonConvert.DeserializeObject<Recipe>(json, settings);
                    Assert.InRange(actual.OutputItemCount, 1, int.MaxValue);
                });
        }

        [Fact]
        [Trait("Feature",  "Recipes")]
        [Trait("Category", "Integration")]
        public void MinRating_ShouldBeBetween0And500()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();
            AssertEx.ForEach(_fixture.Db.Recipes,
                json =>
                {
                    var actual = JsonConvert.DeserializeObject<Recipe>(json, settings);
                    Assert.InRange(actual.MinRating, 0, 500);
                });
        }

        [Fact]
        [Trait("Feature",  "Recipes")]
        [Trait("Category", "Integration")]
        public void TimeToCraft_ShouldBePositive()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();
            AssertEx.ForEach(_fixture.Db.Recipes,
                json =>
                {
                    var actual = JsonConvert.DeserializeObject<Recipe>(json, settings);
                    Assert.InRange(actual.TimeToCraft.Ticks, 1, long.MaxValue);
                });
        }

        [Fact]
        [Trait("Feature",  "Recipes")]
        [Trait("Category", "Integration")]
        public void Disciplines_ShouldNotBeNull()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();
            AssertEx.ForEach(_fixture.Db.Recipes,
                json =>
                {
                    var actual = JsonConvert.DeserializeObject<Recipe>(json, settings);
                    Assert.NotNull(actual.Disciplines);
                });
        }

        [Fact]
        [Trait("Feature", "Recipes")]
        [Trait("Category", "Integration")]
        public void Flags_ShouldNotBeNull()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();
            AssertEx.ForEach(_fixture.Db.Recipes,
                json =>
                {
                    var actual = JsonConvert.DeserializeObject<Recipe>(json, settings);
                    Assert.NotNull(actual.Flags);
                });
        }

        [Fact]
        [Trait("Feature", "Recipes")]
        [Trait("Category", "Integration")]
        public void Ingredients_ShouldNotBeNull()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();
            AssertEx.ForEach(_fixture.Db.Recipes,
                json =>
                {
                    var actual = JsonConvert.DeserializeObject<Recipe>(json, settings);
                    Assert.NotNull(actual.Ingredients);
                });
        }

        [Fact]
        [Trait("Feature",  "Recipes")]
        [Trait("Category", "Integration")]
        public void ChatLink_ShouldNotBeNull()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();
            AssertEx.ForEach(_fixture.Db.Recipes,
                json =>
                {
                    var actual = JsonConvert.DeserializeObject<Recipe>(json, settings);
                    Assert.NotNull(actual.ChatLink);
                });
        }
    }
}

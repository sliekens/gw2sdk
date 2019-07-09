using System.Linq;
using GW2SDK.Impl;
using GW2SDK.Impl.JsonConverters;
using GW2SDK.Skins;
using GW2SDK.Tests.Features.Skins.Fixtures;
using GW2SDK.Tests.TestInfrastructure;
using Newtonsoft.Json;
using Xunit;
using Xunit.Abstractions;

namespace GW2SDK.Tests.Features.Skins
{
    [Collection(nameof(SkinDbCollection))]
    public class SkinTest
    {
        public SkinTest(SkinFixture fixture, ITestOutputHelper output)
        {
            _fixture = fixture;
            _output = output;
        }

        private readonly SkinFixture _fixture;

        private readonly ITestOutputHelper _output;

        [Fact]
        [Trait("Feature",    "Skins")]
        [Trait("Category",   "Integration")]
        [Trait("Importance", "Critical")]
        public void Class_ShouldHaveNoMissingMembers()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output))
                                                              .UseMissingMemberHandling(MissingMemberHandling.Error)
                                                              .Build();
            AssertEx.ForEach(_fixture.Db.Skins,
                json =>
                {
                    // Next statement throws if there are missing members
                    _ = JsonConvert.DeserializeObject<Skin>(json, settings);
                });
        }

        [Fact]
        [Trait("Feature", "Skins")]
        [Trait("Category", "Integration")]
        public void Id_ShouldBePositive()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();
            AssertEx.ForEach(_fixture.Db.Skins,
                json =>
                {
                    var actual = JsonConvert.DeserializeObject<Skin>(json, settings);
                    Assert.InRange(actual.Id, 1, int.MaxValue);
                });
        }

        [Fact]
        [Trait("Feature", "Skins")]
        [Trait("Category", "Integration")]
        public void Name_ShouldNotBeNull()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();
            AssertEx.ForEach(_fixture.Db.Skins,
                json =>
                {
                    var actual = JsonConvert.DeserializeObject<Skin>(json, settings);
                    Assert.NotNull(actual.Name);
                });
        }

        [Fact]
        [Trait("Feature", "Skins")]
        [Trait("Category", "Integration")]
        public void Flags_ShouldNotBeNull()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();
            AssertEx.ForEach(_fixture.Db.Skins,
                json =>
                {
                    var actual = JsonConvert.DeserializeObject<Skin>(json, settings);
                    Assert.NotNull(actual.Flags);
                });
        }

        [Fact]
        [Trait("Feature",  "Skins")]
        [Trait("Category", "Integration")]
        public void Restrictions_ShouldNotBeNull()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();
            AssertEx.ForEach(_fixture.Db.Skins,
                json =>
                {
                    var actual = JsonConvert.DeserializeObject<Skin>(json, settings);
                    Assert.NotNull(actual.Restrictions);
                });
        }
    }
}

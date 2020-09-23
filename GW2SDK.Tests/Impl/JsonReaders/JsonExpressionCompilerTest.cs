using System.Text.Json;
using GW2SDK.Impl.JsonReaders;
using GW2SDK.Impl.JsonReaders.Linq;
using GW2SDK.Impl.JsonReaders.Mappings;
using Xunit;

namespace GW2SDK.Tests.Impl.JsonReaders
{
    public class JsonExpressionCompilerTest
    {
        [Theory]
        [InlineData("3.14", 3.14d)]
        [InlineData("null", null)]
        public void It_can_parse_nullable_double(string input, double? expected)
        {
            var sut = new JsonExpressionCompiler();

            var reader = sut.Compile<double?>
            (
                new JsonValueMapping<double?>
                {
                    ValueKind = JsonValueMappingKind.Double,
                    Significance = MappingSignificance.Optional
                }
            );

            using var json = JsonDocument.Parse(input);

            var actual = reader(json.RootElement, JsonPath.Root);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(MappingSignificance.Required, MappingSignificance.Required)]
        [InlineData(MappingSignificance.Required, MappingSignificance.Optional)]
        [InlineData(MappingSignificance.Optional, MappingSignificance.Optional)]
        [InlineData(MappingSignificance.Optional, MappingSignificance.Required)]
        public void It_can_parse_arrays
            (MappingSignificance significance, MappingSignificance itemSignificance)
        {
            var sut = new JsonExpressionCompiler();

            var reader = sut.Compile<string[]>
            (
                new JsonArrayMapping<string>
                {
                    Significance = significance,
                    ValueMapping = new JsonValueMapping<string>
                    {
                        Significance = itemSignificance,
                        ValueKind = JsonValueMappingKind.String
                    }
                }
            );

            using var json = JsonDocument.Parse("[\"1\", \"2\", \"3\"]");

            var actual = reader(json.RootElement, JsonPath.Root);

            Assert.Equal(new[] { "1", "2", "3" }, actual);
        }

        [Fact]
        public void It_can_parse_string()
        {
            var sut = new JsonExpressionCompiler();

            var reader = sut.Compile<string>
            (
                new JsonValueMapping<string>
                {
                    ValueKind = JsonValueMappingKind.String
                }
            );

            using var json = JsonDocument.Parse("\"it works\"");

            var actual = reader(json.RootElement, JsonPath.Root);

            Assert.Equal("it works", actual);
        }

        [Fact]
        public void It_can_parse_double()
        {
            var sut = new JsonExpressionCompiler();

            var reader = sut.Compile<double>
            (
                new JsonValueMapping<double>
                {
                    ValueKind = JsonValueMappingKind.Double
                }
            );

            using var json = JsonDocument.Parse("3.14");

            var actual = reader(json.RootElement, JsonPath.Root);

            Assert.Equal(3.14d, actual);
        }
    }
}

using System.Text.Json;
using GW2SDK.Impl.JsonReaders;
using Xunit;

namespace GW2SDK.Tests.Impl
{
    public class ExpressionFactoryTest
    {
        
        public sealed class Build
        {
            public int Id { get; set; }

            public Other Other { get; set; }
        }

        
        public sealed class Other
        {
            public bool Value { get; set; }

            public int[] Numbers { get; set; }
        }

        [Fact]
        public void It_can_create_build_parser()
        {
            var sut = new JsonObjectReader<Build>();
            var otherReader = new JsonObjectReader<Other>();
            sut.Map("id", build => build.Id);
            sut.Map("other", build => build.Other, otherReader);
            otherReader.Map("value", other => other.Value);
            //otherReader.Require("nums", other => other.Numbers);

            var json = JsonDocument.Parse("{\"id\": 12345, \"other\": { \"value\": true }}");

            for (int i = 0; i < 1000000; i++)
            {
                var actual = sut.Read(json);
                Assert.Equal(12345, actual.Id);
                Assert.True(actual.Other.Value);
            }
        }
    }
}

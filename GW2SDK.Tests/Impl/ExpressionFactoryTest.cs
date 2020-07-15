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
        }

        [Fact]
        public void It_can_create_build_parser()
        {
            var sut = new JsonReader<Build>();
            sut.Require("id", build => build.Id);

            var f = sut.Compile();
            var json = JsonDocument.Parse("{\"id\": 12345}");

            var actual = f(json.RootElement);
            Assert.Equal(12345, actual.Id);
        }
    }
}

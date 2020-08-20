using GW2SDK.Impl.JsonReaders;
using Xunit;

namespace GW2SDK.Tests.Impl.JsonReaders
{
    public class JsonPathTest
    {
        [Fact]
        public void Root_path_is_dollar_sign()
        {
            var actual = JsonPath.Root;

            Assert.Equal("$", actual.ToString());
        }

        [Fact]
        public void It_can_create_a_property_path()
        {
            var sut = JsonPath.Root;

            var actual = sut.AccessProperty("property");

            Assert.Equal("$.property", actual.ToString());
        }

        [Fact]
        public void It_can_create_an_array_path()
        {
            var sut = JsonPath.Root;

            var actual = sut.AccessArrayIndex(3);

            Assert.Equal("$[3]", actual.ToString());
        }

        [Fact]
        public void It_can_chain_member_accessors()
        {
            var sut = JsonPath.Root;

            var actual = sut.AccessProperty("store")
                .AccessProperty("book")
                .AccessArrayIndex(0)
                .AccessProperty("title");

            Assert.Equal("$.store.book[0].title", actual.ToString());
        }
    }
}

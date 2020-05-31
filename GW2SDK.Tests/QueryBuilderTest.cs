using GW2SDK.Impl;
using Xunit;

namespace GW2SDK.Tests
{
    public class QueryBuilderTest
    {
        [Fact]
        public void The_default_state_is_an_empty_query()
        {
            var sut = new QueryBuilder();
            var result = sut.Build();
            Assert.Equal("", result);
        }
    }
}

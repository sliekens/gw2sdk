using System;
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

        [Fact]
        public void Query_builder_can_build_id_queries()
        {
            var id = 123;
            var sut = new QueryBuilder();
            sut.WithId(id);
            var result = sut.Build();
            Assert.Equal("id=123", result);
        }

        [Fact]
        public void Query_builder_can_build_ids_queries()
        {
            var ids = new [] { 12, 34, 56};
            var sut = new QueryBuilder();
            sut.WithIds(ids);
            var result = sut.Build();
            Assert.Equal("ids=12,34,56", result);
        }

        [Fact]
        public void Query_builder_throws_when_ids_is_null_or_empty()
        {
            var sut = new QueryBuilder();
            Assert.Throws<ArgumentNullException>("ids", () => sut.WithIds(null!));
            Assert.Throws<ArgumentException>("ids", () => sut.WithIds(new int[0]));
        }
        
        [Fact]
        public void Query_builder_can_build_all_ids_queries()
        {
            var sut = new QueryBuilder();
            sut.WithIdsAll();
            var result = sut.Build();
            Assert.Equal("ids=all", result);
        }

        [Fact]
        public void Query_builder_can_build_page_queries()
        {
            var sut = new QueryBuilder();
            sut.WithPageIndex(1);
            sut.WithPageSize(200);
            var result = sut.Build();
            Assert.Equal("page=1&page_size=200", result);
        }
    }
}

using GW2SDK.Http;
using Xunit;

namespace GW2SDK.Tests.Http
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
        public void It_can_create_query_strings_with_one_string_argument()
        {
            var sut = new QueryBuilder();

            sut.Add("key", "value");

            var actual = sut.Build();

            Assert.Equal("key=value", actual);
        }

        [Fact]
        public void It_can_create_query_strings_with_one_int_argument()
        {
            var sut = new QueryBuilder();

            sut.Add("key", 42);

            var actual = sut.Build();

            Assert.Equal("key=42", actual);
        }

        [Fact]
        public void It_can_create_query_strings_with_many_string_arguments()
        {
            var sut = new QueryBuilder();

            sut.Add("key1", "first");
            sut.Add("key2", "second");
            sut.Add("key3", "third");

            var actual = sut.Build();

            Assert.Equal("key1=first&key2=second&key3=third", actual);
        }

        [Fact]
        public void It_can_create_query_strings_with_many_int_arguments()
        {
            var sut = new QueryBuilder();

            sut.Add("key1", 1);
            sut.Add("key2", 2);
            sut.Add("key3", 3);

            var actual = sut.Build();

            Assert.Equal("key1=1&key2=2&key3=3", actual);
        }

        [Fact]
        public void It_can_create_query_strings_with_repeated_arguments()
        {
            var sut = new QueryBuilder();

            sut.Add("key", "first");
            sut.Add("key", "second");
            sut.Add("key", "third");

            var actual = sut.Build();

            Assert.Equal("key=first&key=second&key=third", actual);
        }

        [Fact]
        public void It_can_create_query_strings_with_csv_string_arguments()
        {
            var sut = new QueryBuilder();

            sut.Add("key", new[] { "first", "second", "third" });

            var actual = sut.Build();

            Assert.Equal("key=first,second,third", actual);
        }

        [Fact]
        public void It_can_create_query_strings_with_csv_int_arguments()
        {
            var sut = new QueryBuilder();

            sut.Add("key", new[] { 1, 2, 3 });

            var actual = sut.Build();

            Assert.Equal("key=1,2,3", actual);
        }
    }
}

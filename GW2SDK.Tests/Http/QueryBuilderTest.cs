using GuildWars2.Http;

namespace GuildWars2.Tests.Http;

public class QueryBuilderTest
{
    [Fact]
    public void The_default_state_is_an_empty_query()
    {
        var sut = new QueryBuilder();

        var actual = sut.Build();

        Assert.Equal("", actual);
    }

    [Fact]
    public void Build_query_with_one_key()
    {
        var sut = new QueryBuilder { { "key", "value" } };

        var actual = sut.Build();

        Assert.Equal("?key=value", actual);
    }

    [Fact]
    public void Build_query_with_multiple_keys()
    {
        var sut = new QueryBuilder
        {
            { "key1", "first" },
            { "key2", "second" },
            { "key3", "third" }
        };

        var actual = sut.Build();

        Assert.Equal("?key1=first&key2=second&key3=third", actual);
    }

    [Fact]
    public void Build_query_with_repeating_keys()
    {
        var sut = new QueryBuilder
        {
            { "key", "first" },
            { "key", "second" },
            { "key", "third" }
        };

        var actual = sut.Build();

        Assert.Equal("?key=first&key=second&key=third", actual);
    }
}

using GuildWars2.Http;

namespace GuildWars2.Tests.Http;

public class QueryBuilderTest
{
    [Fact]
    public void The_default_state_is_an_empty_query()
    {
        QueryBuilder sut = [];

        string actual = sut.Build();

        Assert.Equal("", actual);
    }

    [Fact]
    public void Build_query_with_one_key()
    {
        QueryBuilder sut = new() { { "key", "value" } };

        string actual = sut.Build();

        Assert.Equal("?key=value", actual);
    }

    [Fact]
    public void Build_query_with_multiple_keys()
    {
        QueryBuilder sut = new()
        {
            { "key1", "first" },
            { "key2", "second" },
            { "key3", "third" }
        };

        string actual = sut.Build();

        Assert.Equal("?key1=first&key2=second&key3=third", actual);
    }

    [Fact]
    public void Build_query_with_repeating_keys()
    {
        QueryBuilder sut = new()
        {
            { "key", "first" },
            { "key", "second" },
            { "key", "third" }
        };

        string actual = sut.Build();

        Assert.Equal("?key=first&key=second&key=third", actual);
    }
}

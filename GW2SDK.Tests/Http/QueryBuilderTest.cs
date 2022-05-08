using System;
using GW2SDK.Http;
using Xunit;

namespace GW2SDK.Tests.Http;

public class QueryBuilderTest
{
    [Fact]
    public void The_default_state_is_an_empty_query()
    {
        var sut = new QueryBuilder();

        var actual = sut.Build();

        Assert.Equal(0, sut.Count);
        Assert.Equal("", actual);
    }

    [Fact]
    public void A_frozen_builder_is_immutable()
    {
        var sut = new QueryBuilder();

        sut.Freeze();

        Assert.ThrowsAny<InvalidOperationException>(() => sut.Add("key", "value"));
    }

    [Fact]
    public void A_cloned_builder_is_mutable()
    {
        var immutable = new QueryBuilder();

        immutable.Freeze();

        var sut = immutable.Clone();

        sut.Add("key", "value");

        Assert.Equal("?key=value", sut.Build());
    }

    [Fact]
    public void Value_type_can_be_string()
    {
        var sut = new QueryBuilder { { "key", "value" } };

        var actual = sut.Build();

        Assert.Equal("?key=value", actual);
    }

    [Fact]
    public void Value_type_can_be_int()
    {
        var sut = new QueryBuilder { { "key", 42 } };

        var actual = sut.Build();

        Assert.Equal("?key=42", actual);
    }

    [Fact]
    public void Multiple_arguments_are_delimited_by_ampersand()
    {
        var sut = new QueryBuilder
        {
            { "key1", "first" },
            { "key2", "second" },
            { "key3", "third" }
        };

        var actual = sut.Build();

        Assert.Equal(3, sut.Count);
        Assert.Equal("?key1=first&key2=second&key3=third", actual);
    }

    [Fact]
    public void Keys_can_have_multiple_occurrences()
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

    [Fact]
    public void String_collections_are_converted_to_csv()
    {
        var sut = new QueryBuilder
        {
            {
                "key", new[]
                {
                    "first",
                    "second",
                    "third"
                }
            }
        };

        var actual = sut.Build();

        Assert.Equal("?key=first,second,third", actual);
    }

    [Fact]
    public void Int_collections_are_converted_to_csv()
    {
        var sut = new QueryBuilder
        {
            {
                "key", new[]
                {
                    1,
                    2,
                    3
                }
            }
        };

        var actual = sut.Build();

        Assert.Equal("?key=1,2,3", actual);
    }
}

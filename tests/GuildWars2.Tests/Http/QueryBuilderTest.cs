using GuildWars2.Http;

namespace GuildWars2.Tests.Http;

public class QueryBuilderTest
{
    [Test]
    public async Task The_default_state_is_an_empty_query()
    {
        QueryBuilder sut = [];
        string actual = sut.Build();
        await Assert.That(actual).IsEmpty();
    }

    [Test]
    public async Task Build_query_with_one_key()
    {
        QueryBuilder sut = new()
        {
            {
                "key",
                "value"
            }
        };
        string actual = sut.Build();
        await Assert.That(actual).IsEqualTo("?key=value");
    }

    [Test]
    public async Task Build_query_with_multiple_keys()
    {
        QueryBuilder sut = new()
        {
            {
                "key1",
                "first"
            },
            {
                "key2",
                "second"
            },
            {
                "key3",
                "third"
            }
        };
        string actual = sut.Build();
        await Assert.That(actual).IsEqualTo("?key1=first&key2=second&key3=third");
    }

    [Test]
    public async Task Build_query_with_repeating_keys()
    {
        QueryBuilder sut = new()
        {
            {
                "key",
                "first"
            },
            {
                "key",
                "second"
            },
            {
                "key",
                "third"
            }
        };
        string actual = sut.Build();
        await Assert.That(actual).IsEqualTo("?key=first&key=second&key=third");
    }
}

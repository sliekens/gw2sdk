using GuildWars2.Http;
using Xunit;

namespace GuildWars2.Tests.Http;

public class LinkHeaderTest
{
    private static class LinkFact
    {
        public static void IsLink(LinkHeaderValue actual, string rel, string href)
        {
            Assert.Equal(rel, actual.Rel);
            Assert.Equal(href, actual.Href);
        }
    }

    [Fact]
    public void It_can_parse_link_headers()
    {
        const string input =
            "</v2/colors?page=117&page_size=5>; rel=previous, </v2/colors?page=119&page_size=5>; rel=next, </v2/colors?page=118&page_size=5>; rel=self, </v2/colors?page=0&page_size=5>; rel=first, </v2/colors?page=119&page_size=5>; rel=last";
        var actual = LinkHeader.Parse(input);

        Assert.Collection(
            actual.Links,
            next => LinkFact.IsLink(next, "previous", "/v2/colors?page=117&page_size=5"),
            next => LinkFact.IsLink(next, "next", "/v2/colors?page=119&page_size=5"),
            next => LinkFact.IsLink(next, "self", "/v2/colors?page=118&page_size=5"),
            next => LinkFact.IsLink(next, "first", "/v2/colors?page=0&page_size=5"),
            next => LinkFact.IsLink(next, "last", "/v2/colors?page=119&page_size=5")
        );
    }

    [Fact]
    public void It_can_serialize_round_trip()
    {
        const string input =
            "</v2/colors?page=117&page_size=5>; rel=previous, </v2/colors?page=119&page_size=5>; rel=next, </v2/colors?page=118&page_size=5>; rel=self, </v2/colors?page=0&page_size=5>; rel=first, </v2/colors?page=119&page_size=5>; rel=last";
        var sut = LinkHeader.Parse(input);

        var actual = sut.ToString();

        Assert.Equal(input, actual);
    }
}

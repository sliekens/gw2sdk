using GuildWars2.Http.WebLinks;

namespace GuildWars2.Tests.Http.WebLinks;

public class LinkHeaderTest
{
    [Fact]
    public void It_can_parse_link_headers()
    {
        const string input =
            "</v2/colors?page=117&page_size=5>; rel=previous, </v2/colors?page=119&page_size=5>; rel=next, </v2/colors?page=118&page_size=5>; rel=self, </v2/colors?page=0&page_size=5>; rel=first, </v2/colors?page=119&page_size=5>; rel=last";
        LinkHeader actual = LinkHeader.Parse(input);

        Assert.Collection(
            actual.Links,
            rel => IsLink(rel, "previous", "/v2/colors?page=117&page_size=5"),
            rel => IsLink(rel, "next", "/v2/colors?page=119&page_size=5"),
            rel => IsLink(rel, "self", "/v2/colors?page=118&page_size=5"),
            rel => IsLink(rel, "first", "/v2/colors?page=0&page_size=5"),
            rel => IsLink(rel, "last", "/v2/colors?page=119&page_size=5")
        );

        static void IsLink(LinkValue actual, string relationType, string target)
        {
            Assert.Equal(relationType, actual.RelationType);
            Assert.Equal(target, actual.TargetUrl.ToString());
        }
    }

    [Fact]
    public void It_can_serialize_round_trip()
    {
        const string input =
            "</v2/colors?page=117&page_size=5>; rel=previous, </v2/colors?page=119&page_size=5>; rel=next, </v2/colors?page=118&page_size=5>; rel=self, </v2/colors?page=0&page_size=5>; rel=first, </v2/colors?page=119&page_size=5>; rel=last";
        LinkHeader sut = LinkHeader.Parse(input);

        var actual = sut.ToString();

        Assert.Equal(input, actual);
    }
}

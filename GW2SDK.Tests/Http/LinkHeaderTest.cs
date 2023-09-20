﻿using GuildWars2.Http;

namespace GuildWars2.Tests.Http;

public class LinkHeaderTest
{
    [Fact]
    public void It_can_parse_link_headers()
    {
        const string input =
            "</v2/colors?page=117&page_size=5>; rel=previous, </v2/colors?page=119&page_size=5>; rel=next, </v2/colors?page=118&page_size=5>; rel=self, </v2/colors?page=0&page_size=5>; rel=first, </v2/colors?page=119&page_size=5>; rel=last";
        var actual = LinkHeader.Parse(input);

        Assert.Collection(
            actual.Links,
            rel => rel.IsLink("previous", "/v2/colors?page=117&page_size=5"),
            rel => rel.IsLink("next", "/v2/colors?page=119&page_size=5"),
            rel => rel.IsLink("self", "/v2/colors?page=118&page_size=5"),
            rel => rel.IsLink("first", "/v2/colors?page=0&page_size=5"),
            rel => rel.IsLink("last", "/v2/colors?page=119&page_size=5")
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

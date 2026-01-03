using GuildWars2.Http.WebLinks;

namespace GuildWars2.Tests.Http.WebLinks;

public class LinkHeaderTest
{
    [Test]
    public async Task It_can_parse_link_headers()
    {
        const string input = "</v2/colors?page=117&page_size=5>; rel=previous, </v2/colors?page=119&page_size=5>; rel=next, </v2/colors?page=118&page_size=5>; rel=self, </v2/colors?page=0&page_size=5>; rel=first, </v2/colors?page=119&page_size=5>; rel=last";
        LinkHeader actual = LinkHeader.Parse(input);
        List<LinkValue> links = [.. actual.Links];
        await Assert.That(links).Count().IsEqualTo(5);
        await Assert.That(links[0]).Member(l => l.RelationType, rt => rt.IsEqualTo("previous"))
            .And.Member(l => l.TargetUrl.ToString(), url => url.IsEqualTo("/v2/colors?page=117&page_size=5"));
        await Assert.That(links[1]).Member(l => l.RelationType, rt => rt.IsEqualTo("next"))
            .And.Member(l => l.TargetUrl.ToString(), url => url.IsEqualTo("/v2/colors?page=119&page_size=5"));
        await Assert.That(links[2]).Member(l => l.RelationType, rt => rt.IsEqualTo("self"))
            .And.Member(l => l.TargetUrl.ToString(), url => url.IsEqualTo("/v2/colors?page=118&page_size=5"));
        await Assert.That(links[3]).Member(l => l.RelationType, rt => rt.IsEqualTo("first"))
            .And.Member(l => l.TargetUrl.ToString(), url => url.IsEqualTo("/v2/colors?page=0&page_size=5"));
        await Assert.That(links[4]).Member(l => l.RelationType, rt => rt.IsEqualTo("last"))
            .And.Member(l => l.TargetUrl.ToString(), url => url.IsEqualTo("/v2/colors?page=119&page_size=5"));
    }

    [Test]
    public async Task It_can_serialize_round_trip()
    {
        const string input = "</v2/colors?page=117&page_size=5>; rel=previous, </v2/colors?page=119&page_size=5>; rel=next, </v2/colors?page=118&page_size=5>; rel=self, </v2/colors?page=0&page_size=5>; rel=first, </v2/colors?page=119&page_size=5>; rel=last";
        LinkHeader sut = LinkHeader.Parse(input);
        string actual = sut.ToString();
        await Assert.That(actual).IsEqualTo(input);
    }
}

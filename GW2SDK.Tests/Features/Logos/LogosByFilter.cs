using GuildWars2.Logos;
using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Logos;

public class LogosByFilter
{
    [Fact]
    public async Task Can_be_filtered_by_id()
    {
        var sut = Composer.Resolve<Gw2Client>();

        HashSet<string> ids =
        [
            "Guild-Wars-2-logo-en",
            "Guild-Wars-2-logo-es",
            "Guild-Wars-2-logo-de",
        ];

        (HashSet<Logo> actual, MessageContext context) = await sut.Logos.GetLogosByIds(
            ids,
            cancellationToken: TestContext.Current.CancellationToken
        );

        Assert.Equal(ids.Count, context.ResultCount);
        Assert.True(context.ResultTotal > ids.Count);
        Assert.Equal(ids.Count, actual.Count);
        Assert.Collection(
            ids,
            first => Assert.Contains(actual, found => found.Id == first),
            second => Assert.Contains(actual, found => found.Id == second),
            third => Assert.Contains(actual, found => found.Id == third)
        );
    }
}

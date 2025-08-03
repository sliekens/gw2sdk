using GuildWars2.Pvp.MistChampions;
using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Pvp.MistChampions;

public class MistChampionById
{
    [Fact]
    public async Task Can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();

        const string id = "115C140F-C2F5-40EB-8EA2-C3773F2AE468";

        (MistChampion actual, MessageContext context) = await sut.Pvp.GetMistChampionById(
            id,
            cancellationToken: TestContext.Current.CancellationToken
        );

        Assert.NotNull(context);
        Assert.Equal(id, actual.Id);
    }
}

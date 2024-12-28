using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Pvp.MistChampions;

public class MistChampionByFilter

{
    [Fact]
    public async Task Can_be_filtered_by_id()
    {
        var sut = Composer.Resolve<Gw2Client>();

        HashSet<string> ids =
        [
            "115C140F-C2F5-40EB-8EA2-C3773F2AE468", "B7EA9889-5F16-4636-9705-4FCAF8B39ECD",
            "BEA79596-CA8B-4D46-9B9C-EA1B606BCF42"
        ];

        var (actual, context) = await sut.Pvp.GetMistChampionsByIds(
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

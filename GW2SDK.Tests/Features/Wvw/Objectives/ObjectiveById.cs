using GuildWars2.Tests.TestInfrastructure;
using GuildWars2.Wvw.Objectives;

namespace GuildWars2.Tests.Features.Wvw.Objectives;

public class ObjectiveById
{
    [Fact]
    public async Task Can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();

        const string id = "1099-99";

        (Objective actual, MessageContext context) = await sut.Wvw.GetObjectiveById(
            id,
            cancellationToken: TestContext.Current.CancellationToken
        );

        Assert.NotNull(context);
        Assert.Equal(id, actual.Id);
    }
}

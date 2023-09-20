using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Banking;

public class AccountBank
{
    [Fact]
    public async Task Contents_can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();
        var accessToken = Composer.Resolve<ApiKey>();

        var actual = await sut.Bank.GetBank(accessToken.Key);

        actual.Value.Not_empty();
        actual.Value.Has_multiple_of_30_slots();

        Assert.All(
            actual.Value,
            slot =>
            {
                slot?.Has_id();
                slot?.Has_count();
            }
        );
    }
}

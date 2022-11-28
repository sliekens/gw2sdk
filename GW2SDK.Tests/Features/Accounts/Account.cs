using System.Threading.Tasks;
using GuildWars2.Tests.TestInfrastructure;
using Xunit;

namespace GuildWars2.Tests.Features.Accounts;

public class Account
{
    [Fact]
    public async Task Basic_summary_with_any_access_token()
    {
        await using Composer services = new();
        var sut = services.Resolve<Gw2Client>();
        var accessToken = services.Resolve<ApiKeyBasic>();

        var actual = await sut.Accounts.GetSummary(accessToken.Key);

        actual.Value.Name_is_never_empty();
        actual.Value.Access_is_never_empty();
        actual.Value.GuildLeader_requires_guilds_scope();
        actual.Value.Access_is_never_none();
        actual.Value.Age_is_never_zero();
        actual.Value.FractalLevel_requires_progression_scope();
        actual.Value.DailyAp_requires_progression_scope();
        actual.Value.MonthlyAp_requires_progression_scope();
        actual.Value.WvwRank_requires_progression_scope();
    }

    [Fact]
    public async Task Full_summary_with_high_trust_access_token()
    {
        await using Composer services = new();
        var sut = services.Resolve<Gw2Client>();
        var accessToken = services.Resolve<ApiKey>();

        var actual = await sut.Accounts.GetSummary(accessToken.Key);

        actual.Value.Name_is_never_empty();
        actual.Value.Access_is_never_empty();
        actual.Value.Access_is_never_none();
        actual.Value.GuildLeader_is_included_by_guilds_scope();
        actual.Value.Age_is_never_zero();
        actual.Value.Created_ShouldNotBeDefaultValue();
        actual.Value.FractalLevel_is_included_by_progression_scope();
        actual.Value.DailyAp_is_included_by_progression_scope();
        actual.Value.MonthlyAp_is_included_by_progression_scope();
        actual.Value.WvwRank_is_included_by_progression_scope();
    }
}

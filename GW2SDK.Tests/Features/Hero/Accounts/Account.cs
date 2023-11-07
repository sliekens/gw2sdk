using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Accounts;

public class Account
{
    [Fact]
    public async Task Basic_summary_with_any_access_token()
    {
        var sut = Composer.Resolve<Gw2Client>();
        var accessToken = Composer.Resolve<ApiKeyBasic>();

        var (actual, _) = await sut.Hero.Account.GetSummary(accessToken.Key);

        actual.Name_is_never_empty();
        actual.Access_is_never_empty();
        actual.GuildLeader_requires_guilds_scope();
        actual.Access_is_never_none();
        actual.Age_is_never_zero();
        actual.FractalLevel_requires_progression_scope();
        actual.DailyAp_requires_progression_scope();
        actual.MonthlyAp_requires_progression_scope();
        actual.WvwRank_requires_progression_scope();
    }

    [Fact]
    public async Task Full_summary_with_high_trust_access_token()
    {
        var sut = Composer.Resolve<Gw2Client>();
        var accessToken = Composer.Resolve<ApiKey>();

        var (actual, _) = await sut.Hero.Account.GetSummary(accessToken.Key);

        actual.Name_is_never_empty();
        actual.Access_is_never_empty();
        actual.Access_is_never_none();
        actual.GuildLeader_is_included_by_guilds_scope();
        actual.Age_is_never_zero();
        actual.Created_ShouldNotBeDefaultValue();
        actual.FractalLevel_is_included_by_progression_scope();
        actual.DailyAp_is_included_by_progression_scope();
        actual.MonthlyAp_is_included_by_progression_scope();
        actual.WvwRank_is_included_by_progression_scope();
    }
}

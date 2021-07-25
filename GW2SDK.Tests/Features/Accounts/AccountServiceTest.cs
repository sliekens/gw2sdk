using System;
using System.Threading.Tasks;
using GW2SDK.Accounts;
using GW2SDK.Tests.TestInfrastructure;
using Xunit;

namespace GW2SDK.Tests.Features.Accounts
{
    public class AccountServiceTest
    {
        private static class BasicAccountFact
        {
            public static void Name_is_never_empty(Account actual) => Assert.NotEmpty(actual.Name);

            public static void Access_is_never_empty(Account actual) => Assert.NotEmpty(actual.Access);

            public static void GuildLeader_requires_guilds_scope(Account actual) => Assert.Null(actual.GuildLeader);

            public static void Access_is_never_none(Account actual) => Assert.DoesNotContain(ProductName.None, actual.Access);

            public static void Age_is_never_zero(Account actual) => Assert.NotEqual(TimeSpan.Zero, actual.Age);

            public static void FractalLevel_requires_progression_scope(Account actual) => Assert.Null(actual.FractalLevel);

            public static void DailyAp_requires_progression_scope(Account actual) => Assert.Null(actual.DailyAp);

            public static void MonthlyAp_requires_progression_scope(Account actual) => Assert.Null(actual.MonthlyAp);

            public static void WvwRank_requires_progression_scope(Account actual) => Assert.Null(actual.WvwRank);
        }

        private static class FullAccountFact
        {
            public static void Name_is_never_empty(Account actual) => Assert.NotEmpty(actual.Name);

            public static void Access_is_never_empty(Account actual) => Assert.NotEmpty(actual.Access);

            public static void Access_is_never_none(Account actual) => Assert.DoesNotContain(ProductName.None, actual.Access);

            public static void GuildLeader_is_included_by_guilds_scope(Account actual) => Assert.NotNull(actual.GuildLeader);

            public static void Age_is_never_zero(Account actual) => Assert.NotEqual(TimeSpan.Zero, actual.Age);

            public static void Created_ShouldNotBeDefaultValue(Account actual) => Assert.NotEqual(default, actual.Created);

            public static void FractalLevel_is_included_by_progression_scope(Account actual) => Assert.NotNull(actual.FractalLevel);

            public static void DailyAp_is_included_by_progression_scope(Account actual) => Assert.NotNull(actual.DailyAp);

            public static void MonthlyAp_is_included_by_progression_scope(Account actual) => Assert.NotNull(actual.MonthlyAp);

            public static void WvwRank_is_included_by_progression_scope(Account actual) => Assert.NotNull(actual.WvwRank);
        }

        [Fact]
        [Trait("Feature",  "Accounts")]
        [Trait("Category", "Integration")]
        public async Task It_can_get_basic_account_info_with_any_access_token()
        {
            await using var services = new Composer();
            var sut = services.Resolve<AccountService>();

            var actual = await sut.GetAccount(ConfigurationManager.Instance.ApiKeyBasic);
            
            BasicAccountFact.Name_is_never_empty(actual.Value);
            BasicAccountFact.Access_is_never_empty(actual.Value);
            BasicAccountFact.GuildLeader_requires_guilds_scope(actual.Value);
            BasicAccountFact.Access_is_never_none(actual.Value);
            BasicAccountFact.Age_is_never_zero(actual.Value);
            BasicAccountFact.FractalLevel_requires_progression_scope(actual.Value);
            BasicAccountFact.DailyAp_requires_progression_scope(actual.Value);
            BasicAccountFact.MonthlyAp_requires_progression_scope(actual.Value);
            BasicAccountFact.WvwRank_requires_progression_scope(actual.Value);
        }

        [Fact]
        [Trait("Feature",  "Accounts")]
        [Trait("Category", "Integration")]
        public async Task It_can_get_full_details_when_authorized_by_scopes()
        {
            await using var services = new Composer();
            var sut = services.Resolve<AccountService>();

            var actual = await sut.GetAccount(ConfigurationManager.Instance.ApiKeyFull);
            
            FullAccountFact.Name_is_never_empty(actual.Value);
            FullAccountFact.Access_is_never_empty(actual.Value);
            FullAccountFact.Access_is_never_none(actual.Value);
            FullAccountFact.GuildLeader_is_included_by_guilds_scope(actual.Value);
            FullAccountFact.Age_is_never_zero(actual.Value);
            FullAccountFact.Created_ShouldNotBeDefaultValue(actual.Value);
            FullAccountFact.FractalLevel_is_included_by_progression_scope(actual.Value);
            FullAccountFact.DailyAp_is_included_by_progression_scope(actual.Value);
            FullAccountFact.MonthlyAp_is_included_by_progression_scope(actual.Value);
            FullAccountFact.WvwRank_is_included_by_progression_scope(actual.Value);
        }
    }
}

using System;
using System.Text.Json;
using GW2SDK.Accounts;
using GW2SDK.Json;
using GW2SDK.Tests.Features.Accounts.Fixtures;
using Xunit;

namespace GW2SDK.Tests.Features.Accounts
{
    public class AccountReaderTest : IClassFixture<AccountFixture>
    {
        public AccountReaderTest(AccountFixture fixture)
        {
            _fixture = fixture;
        }

        private readonly AccountFixture _fixture;

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

        public static class FullAccountFact
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
        [Trait("Feature",    "Accounts")]
        [Trait("Category",   "Integration")]
        [Trait("Importance", "Critical")]
        public void Account_can_be_partially_created_from_json_with_limited_scopes()
        {
            var sut = new AccountReader();

            using var document = JsonDocument.Parse(_fixture.Db.BasicAccount);

            var actual = sut.Read(document.RootElement, MissingMemberBehavior.Error);

            BasicAccountFact.Name_is_never_empty(actual);
            BasicAccountFact.Access_is_never_empty(actual);
            BasicAccountFact.GuildLeader_requires_guilds_scope(actual);
            BasicAccountFact.Access_is_never_none(actual);
            BasicAccountFact.Age_is_never_zero(actual);
            BasicAccountFact.FractalLevel_requires_progression_scope(actual);
            BasicAccountFact.DailyAp_requires_progression_scope(actual);
            BasicAccountFact.MonthlyAp_requires_progression_scope(actual);
            BasicAccountFact.WvwRank_requires_progression_scope(actual);
        }

        [Fact]
        [Trait("Feature",    "Accounts")]
        [Trait("Category",   "Integration")]
        [Trait("Importance", "Critical")]
        public void Account_can_be_fully_created_from_json_with_all_scopes()
        {
            var sut = new AccountReader();

            using var document = JsonDocument.Parse(_fixture.Db.FullAccount);

            var actual = sut.Read(document.RootElement, MissingMemberBehavior.Error);

            FullAccountFact.Name_is_never_empty(actual);
            FullAccountFact.Access_is_never_empty(actual);
            FullAccountFact.Access_is_never_none(actual);
            FullAccountFact.GuildLeader_is_included_by_guilds_scope(actual);
            FullAccountFact.Age_is_never_zero(actual);
            FullAccountFact.Created_ShouldNotBeDefaultValue(actual);
            FullAccountFact.FractalLevel_is_included_by_progression_scope(actual);
            FullAccountFact.DailyAp_is_included_by_progression_scope(actual);
            FullAccountFact.MonthlyAp_is_included_by_progression_scope(actual);
            FullAccountFact.WvwRank_is_included_by_progression_scope(actual);
        }
    }
}

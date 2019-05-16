using GW2SDK.Features.Accounts;
using GW2SDK.Infrastructure;
using GW2SDK.Tests.Features.Accounts.Fixtures;
using Newtonsoft.Json;
using Xunit;
using Xunit.Abstractions;

namespace GW2SDK.Tests.Features.Accounts
{
    public class AccountTest : IClassFixture<AccountFixture>
    {
        public AccountTest(AccountFixture fixture, ITestOutputHelper output)
        {
            _fixture = fixture;
            _output = output;
        }

        private readonly AccountFixture _fixture;

        private readonly ITestOutputHelper _output;

        [Fact]
        [Trait("Feature", "Accounts")]
        [Trait("Category", "Integration")]
        public void Account_ShouldHaveNoMissingMembers()
        {
            _output.WriteLine(_fixture.JsonAccount);

            var actual = new Account();

            var serializerSettings = Json.DefaultJsonSerializerSettings;
            serializerSettings.MissingMemberHandling = MissingMemberHandling.Error;

            JsonConvert.PopulateObject(_fixture.JsonAccount, actual, serializerSettings);
        }


        [Fact]
        [Trait("Feature", "Accounts")]
        [Trait("Category", "Integration")]
        public void Account_Id_ShouldNotBeDefaultValue()
        {
            var actual = new Account();

            JsonConvert.PopulateObject(_fixture.JsonAccount, actual, Json.DefaultJsonSerializerSettings);

            Assert.NotEqual(default, actual.Id);
        }

        [Fact]
        [Trait("Feature", "Accounts")]
        [Trait("Category", "Integration")]
        public void Account_Name_ShouldNotBeEmpty()
        {
            var actual = new Account();

            JsonConvert.PopulateObject(_fixture.JsonAccount, actual, Json.DefaultJsonSerializerSettings);

            Assert.NotEmpty(actual.Name);
        }

        [Fact]
        [Trait("Feature", "Accounts")]
        [Trait("Category", "Integration")]
        public void Account_Access_ShouldNotBeEmpty()
        {
            var actual = new Account();

            JsonConvert.PopulateObject(_fixture.JsonAccount, actual, Json.DefaultJsonSerializerSettings);

            Assert.NotEmpty(actual.Access);
        }

        [Fact]
        [Trait("Feature", "Accounts")]
        [Trait("Category", "Integration")]
        public void Account_Access_ShouldNotContainNone()
        {
            var actual = new Account();

            JsonConvert.PopulateObject(_fixture.JsonAccount, actual, Json.DefaultJsonSerializerSettings);

            Assert.DoesNotContain(GameAccess.None, actual.Access);
        }

        [Fact]
        [Trait("Feature", "Accounts")]
        [Trait("Category", "Integration")]
        public void Account_Age_ShouldNotBeDefaultValue()
        {
            var actual = new Account();

            JsonConvert.PopulateObject(_fixture.JsonAccount, actual, Json.DefaultJsonSerializerSettings);

            Assert.NotEqual(default, actual.Age);
        }

        [Fact]
        [Trait("Feature", "Accounts")]
        [Trait("Category", "Integration")]
        public void Account_LastModified_ShouldNotBeDefaultValue()
        {
            var actual = new Account();

            JsonConvert.PopulateObject(_fixture.JsonAccount, actual, Json.DefaultJsonSerializerSettings);

            Assert.NotEqual(default, actual.LastModified);
        }

        [Fact]
        [Trait("Feature", "Accounts")]
        [Trait("Category", "Integration")]
        public void Account_World_ShouldBePositiveNumber()
        {
            var actual = new Account();

            JsonConvert.PopulateObject(_fixture.JsonAccount, actual, Json.DefaultJsonSerializerSettings);

            // TODO: update with /v2/worlds comparison once implemented
            Assert.NotEqual(default, actual.LastModified);
        }

        [Fact]
        [Trait("Feature", "Accounts")]
        [Trait("Category", "Integration")]
        public void Account_Guilds_ShouldNotBeNull()
        {
            var actual = new Account();

            JsonConvert.PopulateObject(_fixture.JsonAccount, actual, Json.DefaultJsonSerializerSettings);

            Assert.NotNull(actual.Guilds);
        }

        [Fact]
        [Trait("Feature", "Accounts")]
        [Trait("Category", "Integration")]
        public void Account_GuildLeader_ShouldNotBeNull()
        {
            var actual = new Account();

            JsonConvert.PopulateObject(_fixture.JsonAccount, actual, Json.DefaultJsonSerializerSettings);

            Assert.NotNull(actual.GuildLeader);
        }

        [Fact]
        [Trait("Feature", "Accounts")]
        [Trait("Category", "Integration")]
        public void Account_Created_ShouldNotBeDefaultValue()
        {
            var actual = new Account();

            JsonConvert.PopulateObject(_fixture.JsonAccount, actual, Json.DefaultJsonSerializerSettings);

            Assert.NotEqual(default, actual.Created);
        }
        
        [Fact]
        [Trait("Feature", "Accounts")]
        [Trait("Category", "Integration")]
        public void Account_FractalLevel_ShouldNotBeNull()
        {
            var actual = new Account();

            JsonConvert.PopulateObject(_fixture.JsonAccount, actual, Json.DefaultJsonSerializerSettings);

            // Can be null if token is missing Progression permission
            Assert.NotNull(actual.FractalLevel);
        }
        
        [Fact]
        [Trait("Feature", "Accounts")]
        [Trait("Category", "Integration")]
        public void Account_DailyAp_ShouldNotBeNull()
        {
            var actual = new Account();

            JsonConvert.PopulateObject(_fixture.JsonAccount, actual, Json.DefaultJsonSerializerSettings);

            // Can be null if token is missing Progression permission
            Assert.NotNull(actual.DailyAp);
        }

        [Fact]
        [Trait("Feature", "Accounts")]
        [Trait("Category", "Integration")]
        public void Account_MonthlyAp_ShouldNotBeNull()
        {
            var actual = new Account();

            JsonConvert.PopulateObject(_fixture.JsonAccount, actual, Json.DefaultJsonSerializerSettings);

            // Can be null if token is missing Progression permission
            Assert.NotNull(actual.MonthlyAp);
        }

        [Fact]
        [Trait("Feature", "Accounts")]
        [Trait("Category", "Integration")]
        public void Account_WvwRank_ShouldNotBeNull()
        {
            var actual = new Account();

            JsonConvert.PopulateObject(_fixture.JsonAccount, actual, Json.DefaultJsonSerializerSettings);

            // Can be null if token is missing Progression permission
            Assert.NotNull(actual.WvwRank);
        }
    }
}

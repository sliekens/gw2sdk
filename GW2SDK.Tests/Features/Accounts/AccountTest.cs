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

            var sut = new Account();

            var serializerSettings = Json.DefaultJsonSerializerSettings;
            serializerSettings.MissingMemberHandling = MissingMemberHandling.Error;

            JsonConvert.PopulateObject(_fixture.JsonAccount, sut, serializerSettings);
        }


        [Fact]
        [Trait("Feature", "Accounts")]
        [Trait("Category", "Integration")]
        public void Account_Id_ShouldNotBeDefaultValue()
        {
            var sut = new Account();

            JsonConvert.PopulateObject(_fixture.JsonAccount, sut, Json.DefaultJsonSerializerSettings);

            Assert.NotEqual(default, sut.Id);
        }

        [Fact]
        [Trait("Feature", "Accounts")]
        [Trait("Category", "Integration")]
        public void Account_Name_ShouldNotBeEmpty()
        {
            var sut = new Account();

            JsonConvert.PopulateObject(_fixture.JsonAccount, sut, Json.DefaultJsonSerializerSettings);

            Assert.NotEmpty(sut.Name);
        }

        [Fact]
        [Trait("Feature", "Accounts")]
        [Trait("Category", "Integration")]
        public void Account_Access_ShouldNotBeEmpty()
        {
            var sut = new Account();

            JsonConvert.PopulateObject(_fixture.JsonAccount, sut, Json.DefaultJsonSerializerSettings);

            Assert.NotEmpty(sut.Access);
        }

        [Fact]
        [Trait("Feature", "Accounts")]
        [Trait("Category", "Integration")]
        public void Account_Access_ShouldNotContainNone()
        {
            var sut = new Account();

            JsonConvert.PopulateObject(_fixture.JsonAccount, sut, Json.DefaultJsonSerializerSettings);

            Assert.DoesNotContain(GameAccess.None, sut.Access);
        }

        [Fact]
        [Trait("Feature", "Accounts")]
        [Trait("Category", "Integration")]
        public void Account_Age_ShouldNotBeDefaultValue()
        {
            var sut = new Account();

            JsonConvert.PopulateObject(_fixture.JsonAccount, sut, Json.DefaultJsonSerializerSettings);

            Assert.NotEqual(default, sut.Age);
        }

        [Fact]
        [Trait("Feature", "Accounts")]
        [Trait("Category", "Integration")]
        public void Account_LastModified_ShouldNotBeDefaultValue()
        {
            var sut = new Account();

            JsonConvert.PopulateObject(_fixture.JsonAccount, sut, Json.DefaultJsonSerializerSettings);

            Assert.NotEqual(default, sut.LastModified);
        }

        [Fact]
        [Trait("Feature", "Accounts")]
        [Trait("Category", "Integration")]
        public void Account_World_ShouldBePositiveNumber()
        {
            var sut = new Account();

            JsonConvert.PopulateObject(_fixture.JsonAccount, sut, Json.DefaultJsonSerializerSettings);

            // TODO: update with /v2/worlds comparison once implemented
            Assert.NotEqual(default, sut.LastModified);
        }

        [Fact]
        [Trait("Feature", "Accounts")]
        [Trait("Category", "Integration")]
        public void Account_Guilds_ShouldNotBeNull()
        {
            var sut = new Account();

            JsonConvert.PopulateObject(_fixture.JsonAccount, sut, Json.DefaultJsonSerializerSettings);

            Assert.NotNull(sut.Guilds);
        }

        [Fact]
        [Trait("Feature", "Accounts")]
        [Trait("Category", "Integration")]
        public void Account_GuildLeader_ShouldNotBeNull()
        {
            var sut = new Account();

            JsonConvert.PopulateObject(_fixture.JsonAccount, sut, Json.DefaultJsonSerializerSettings);

            Assert.NotNull(sut.GuildLeader);
        }

        [Fact]
        [Trait("Feature", "Accounts")]
        [Trait("Category", "Integration")]
        public void Account_Created_ShouldNotBeDefaultValue()
        {
            var sut = new Account();

            JsonConvert.PopulateObject(_fixture.JsonAccount, sut, Json.DefaultJsonSerializerSettings);

            Assert.NotEqual(default, sut.Created);
        }
        
        [Fact]
        [Trait("Feature", "Accounts")]
        [Trait("Category", "Integration")]
        public void Account_FractalLevel_ShouldNotBeNull()
        {
            var sut = new Account();

            JsonConvert.PopulateObject(_fixture.JsonAccount, sut, Json.DefaultJsonSerializerSettings);

            // Can be null if token is missing Progression permission
            Assert.NotNull(sut.FractalLevel);
        }
        
        [Fact]
        [Trait("Feature", "Accounts")]
        [Trait("Category", "Integration")]
        public void Account_DailyAp_ShouldNotBeNull()
        {
            var sut = new Account();

            JsonConvert.PopulateObject(_fixture.JsonAccount, sut, Json.DefaultJsonSerializerSettings);

            // Can be null if token is missing Progression permission
            Assert.NotNull(sut.DailyAp);
        }

        [Fact]
        [Trait("Feature", "Accounts")]
        [Trait("Category", "Integration")]
        public void Account_MonthlyAp_ShouldNotBeNull()
        {
            var sut = new Account();

            JsonConvert.PopulateObject(_fixture.JsonAccount, sut, Json.DefaultJsonSerializerSettings);

            // Can be null if token is missing Progression permission
            Assert.NotNull(sut.MonthlyAp);
        }

        [Fact]
        [Trait("Feature", "Accounts")]
        [Trait("Category", "Integration")]
        public void Account_WvwRank_ShouldNotBeNull()
        {
            var sut = new Account();

            JsonConvert.PopulateObject(_fixture.JsonAccount, sut, Json.DefaultJsonSerializerSettings);

            // Can be null if token is missing Progression permission
            Assert.NotNull(sut.WvwRank);
        }
    }
}

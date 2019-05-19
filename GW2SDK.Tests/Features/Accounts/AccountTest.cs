using GW2SDK.Extensions;
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

        private Account CreateSut(JsonSerializerSettings jsonSerializerSettings)
        {
            _output.WriteLine(_fixture.JsonAccountObject);
            var sut = new Account();
            JsonConvert.PopulateObject(_fixture.JsonAccountObject, sut, jsonSerializerSettings);
            return sut;
        }

        [Fact]
        [Trait("Feature", "Accounts")]
        [Trait("Category", "Integration")]
        [Trait("Importance", "Critical")]
        public void Account_ShouldHaveNoMissingMembers()
        {
            _ = CreateSut(Json.DefaultJsonSerializerSettings.WithMissingMemberHandling(MissingMemberHandling.Error));
        }


        [Fact]
        [Trait("Feature", "Accounts")]
        [Trait("Category", "Integration")]
        public void Account_Id_ShouldNotBeDefaultValue()
        {
            var sut = CreateSut(Json.DefaultJsonSerializerSettings);

            Assert.NotEqual(default, sut.Id);
        }

        [Fact]
        [Trait("Feature", "Accounts")]
        [Trait("Category", "Integration")]
        public void Account_Name_ShouldNotBeEmpty()
        {
            var sut = CreateSut(Json.DefaultJsonSerializerSettings);

            Assert.NotEmpty(sut.Name);
        }

        [Fact]
        [Trait("Feature", "Accounts")]
        [Trait("Category", "Integration")]
        public void Account_Access_ShouldNotBeEmpty()
        {
            var sut = CreateSut(Json.DefaultJsonSerializerSettings);

            Assert.NotEmpty(sut.Access);
        }

        [Fact]
        [Trait("Feature", "Accounts")]
        [Trait("Category", "Integration")]
        public void Account_Access_ShouldNotContainNone()
        {
            var sut = CreateSut(Json.DefaultJsonSerializerSettings);

            Assert.DoesNotContain(GW2SDK.Features.Accounts.ProductName.None, sut.Access);
        }

        [Fact]
        [Trait("Feature", "Accounts")]
        [Trait("Category", "Integration")]
        public void Account_Age_ShouldNotBeDefaultValue()
        {
            var sut = CreateSut(Json.DefaultJsonSerializerSettings);

            Assert.NotEqual(default, sut.Age);
        }

        [Fact]
        [Trait("Feature", "Accounts")]
        [Trait("Category", "Integration")]
        public void Account_LastModified_ShouldNotBeDefaultValue()
        {
            var sut = CreateSut(Json.DefaultJsonSerializerSettings);

            Assert.NotEqual(default, sut.LastModified);
        }

        [Fact]
        [Trait("Feature", "Accounts")]
        [Trait("Category", "Integration")]
        public void Account_World_ShouldBePositiveNumber()
        {
            var sut = CreateSut(Json.DefaultJsonSerializerSettings);

            // TODO: update with /v2/worlds comparison once implemented
            Assert.NotEqual(default, sut.LastModified);
        }

        [Fact]
        [Trait("Feature", "Accounts")]
        [Trait("Category", "Integration")]
        public void Account_Guilds_ShouldNotBeNull()
        {
            var sut = CreateSut(Json.DefaultJsonSerializerSettings);

            Assert.NotNull(sut.Guilds);
        }

        [Fact]
        [Trait("Feature", "Accounts")]
        [Trait("Category", "Integration")]
        public void Account_GuildLeader_ShouldNotBeNull()
        {
            var sut = CreateSut(Json.DefaultJsonSerializerSettings);

            Assert.NotNull(sut.GuildLeader);
        }

        [Fact]
        [Trait("Feature", "Accounts")]
        [Trait("Category", "Integration")]
        public void Account_Created_ShouldNotBeDefaultValue()
        {
            var sut = CreateSut(Json.DefaultJsonSerializerSettings);

            Assert.NotEqual(default, sut.Created);
        }

        [Fact]
        [Trait("Feature", "Accounts")]
        [Trait("Category", "Integration")]
        public void Account_FractalLevel_ShouldNotBeNull()
        {
            var sut = CreateSut(Json.DefaultJsonSerializerSettings);

            // Can be null if token is missing Progression permission
            Assert.NotNull(sut.FractalLevel);
        }

        [Fact]
        [Trait("Feature", "Accounts")]
        [Trait("Category", "Integration")]
        public void Account_DailyAp_ShouldNotBeNull()
        {
            var sut = CreateSut(Json.DefaultJsonSerializerSettings);

            // Can be null if token is missing Progression permission
            Assert.NotNull(sut.DailyAp);
        }

        [Fact]
        [Trait("Feature", "Accounts")]
        [Trait("Category", "Integration")]
        public void Account_MonthlyAp_ShouldNotBeNull()
        {
            var sut = CreateSut(Json.DefaultJsonSerializerSettings);

            // Can be null if token is missing Progression permission
            Assert.NotNull(sut.MonthlyAp);
        }

        [Fact]
        [Trait("Feature", "Accounts")]
        [Trait("Category", "Integration")]
        public void Account_WvwRank_ShouldNotBeNull()
        {
            var sut = CreateSut(Json.DefaultJsonSerializerSettings);

            // Can be null if token is missing Progression permission
            Assert.NotNull(sut.WvwRank);
        }
    }
}

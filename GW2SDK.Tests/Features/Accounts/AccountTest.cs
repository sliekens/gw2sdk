using GW2SDK.Features.Accounts;
using GW2SDK.Infrastructure;
using GW2SDK.Tests.Features.Accounts.Fixtures;
using GW2SDK.Tests.Features.Worlds.Fixtures;
using GW2SDK.Tests.Shared;
using Newtonsoft.Json;
using Xunit;
using Xunit.Abstractions;

namespace GW2SDK.Tests.Features.Accounts
{
    public class AccountTest : IClassFixture<AccountFixture>, IClassFixture<WorldFixture>
    {
        public AccountTest(AccountFixture fixture, WorldFixture worlds, ITestOutputHelper output)
        {
            _fixture = fixture;
            _worlds = worlds;
            _output = output;
        }

        private readonly AccountFixture _fixture;

        private readonly WorldFixture _worlds;

        private readonly ITestOutputHelper _output;

        [Fact]
        [Trait("Feature",    "Accounts")]
        [Trait("Category",   "Integration")]
        [Trait("Importance", "Critical")]
        public void Basic_Account_ShouldHaveNoMissingMembers()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output))
                                                              .UseMissingMemberHandling(MissingMemberHandling.Error)
                                                              .Build();

            // Next statement throws if there are missing members
            _ = JsonConvert.DeserializeObject<Account>(_fixture.Db.BasicAccount, settings);
        }

        [Fact]
        [Trait("Feature",    "Accounts")]
        [Trait("Category",   "Integration")]
        [Trait("Importance", "Critical")]
        public void Full_Account_ShouldHaveNoMissingMembers()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output))
                                                              .UseMissingMemberHandling(MissingMemberHandling.Error)
                                                              .Build();

            // Next statement throws if there are missing members
            _ = JsonConvert.DeserializeObject<Account>(_fixture.Db.FullAccount, settings);
        }

        [Fact]
        [Trait("Feature",  "Accounts")]
        [Trait("Category", "Integration")]
        public void Basic_Account_Id_ShouldNotBeDefaultValue()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();

            var actual = JsonConvert.DeserializeObject<Account>(_fixture.Db.BasicAccount, settings);

            Assert.NotEqual(default, actual.Id);
        }

        [Fact]
        [Trait("Feature",  "Accounts")]
        [Trait("Category", "Integration")]
        public void Full_Account_Id_ShouldNotBeDefaultValue()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();

            var actual = JsonConvert.DeserializeObject<Account>(_fixture.Db.FullAccount, settings);

            Assert.NotEqual(default, actual.Id);
        }

        [Fact]
        [Trait("Feature",  "Accounts")]
        [Trait("Category", "Integration")]
        public void Basic_Account_Name_ShouldNotBeEmpty()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();

            var actual = JsonConvert.DeserializeObject<Account>(_fixture.Db.BasicAccount, settings);

            Assert.NotEmpty(actual.Name);
        }

        [Fact]
        [Trait("Feature",  "Accounts")]
        [Trait("Category", "Integration")]
        public void Full_Account_Name_ShouldNotBeEmpty()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();

            var actual = JsonConvert.DeserializeObject<Account>(_fixture.Db.FullAccount, settings);

            Assert.NotEmpty(actual.Name);
        }

        [Fact]
        [Trait("Feature",  "Accounts")]
        [Trait("Category", "Integration")]
        public void Basic_Account_Access_ShouldNotBeEmpty()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();

            var actual = JsonConvert.DeserializeObject<Account>(_fixture.Db.BasicAccount, settings);

            Assert.NotEmpty(actual.Access);
        }

        [Fact]
        [Trait("Feature",  "Accounts")]
        [Trait("Category", "Integration")]
        public void Full_Account_Access_ShouldNotBeEmpty()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();

            var actual = JsonConvert.DeserializeObject<Account>(_fixture.Db.FullAccount, settings);

            Assert.NotEmpty(actual.Access);
        }

        [Fact]
        [Trait("Feature",  "Accounts")]
        [Trait("Category", "Integration")]
        public void Basic_Account_Access_ShouldNotContainNone()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();

            var actual = JsonConvert.DeserializeObject<Account>(_fixture.Db.BasicAccount, settings);

            Assert.DoesNotContain(ProductName.None, actual.Access);
        }

        [Fact]
        [Trait("Feature",  "Accounts")]
        [Trait("Category", "Integration")]
        public void Full_Account_Access_ShouldNotContainNone()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();

            var actual = JsonConvert.DeserializeObject<Account>(_fixture.Db.FullAccount, settings);

            Assert.DoesNotContain(ProductName.None, actual.Access);
        }

        [Fact]
        [Trait("Feature",  "Accounts")]
        [Trait("Category", "Integration")]
        public void Basic_Account_Age_ShouldNotBeDefaultValue()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();

            var actual = JsonConvert.DeserializeObject<Account>(_fixture.Db.BasicAccount, settings);

            Assert.NotEqual(default, actual.Age);
        }

        [Fact]
        [Trait("Feature",  "Accounts")]
        [Trait("Category", "Integration")]
        public void Full_Account_Age_ShouldNotBeDefaultValue()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();

            var actual = JsonConvert.DeserializeObject<Account>(_fixture.Db.FullAccount, settings);

            Assert.NotEqual(default, actual.Age);
        }

        [Fact]
        [Trait("Feature",  "Accounts")]
        [Trait("Category", "Integration")]
        public void Basic_Account_LastModified_ShouldNotBeDefaultValue()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();

            var actual = JsonConvert.DeserializeObject<Account>(_fixture.Db.BasicAccount, settings);

            Assert.NotEqual(default, actual.LastModified);
        }

        [Fact]
        [Trait("Feature",  "Accounts")]
        [Trait("Category", "Integration")]
        public void Full_Account_LastModified_ShouldNotBeDefaultValue()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();

            var actual = JsonConvert.DeserializeObject<Account>(_fixture.Db.FullAccount, settings);

            Assert.NotEqual(default, actual.LastModified);
        }

        [Fact]
        [Trait("Feature",  "Accounts")]
        [Trait("Category", "Integration")]
        public void Basic_Account_World_ShouldBeValidId()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();

            var actual = JsonConvert.DeserializeObject<Account>(_fixture.Db.BasicAccount, settings);

            Assert.Contains(actual.World, _worlds.Db.Index);
        }

        [Fact]
        [Trait("Feature",  "Accounts")]
        [Trait("Category", "Integration")]
        public void Full_Account_World_ShouldBeValidId()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();

            var actual = JsonConvert.DeserializeObject<Account>(_fixture.Db.FullAccount, settings);

            Assert.Contains(actual.World, _worlds.Db.Index);
        }

        [Fact]
        [Trait("Feature",  "Accounts")]
        [Trait("Category", "Integration")]
        public void Basic_Account_Guilds_ShouldNotBeNull()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();

            var actual = JsonConvert.DeserializeObject<Account>(_fixture.Db.BasicAccount, settings);

            Assert.NotNull(actual.Guilds);
        }

        [Fact]
        [Trait("Feature",  "Accounts")]
        [Trait("Category", "Integration")]
        public void Full_Account_Guilds_ShouldNotBeNull()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();

            var actual = JsonConvert.DeserializeObject<Account>(_fixture.Db.FullAccount, settings);

            Assert.NotNull(actual.Guilds);
        }

        [Fact]
        [Trait("Feature",  "Accounts")]
        [Trait("Category", "Integration")]
        public void Basic_Account_GuildLeader_ShouldBeNull()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();

            var actual = JsonConvert.DeserializeObject<Account>(_fixture.Db.BasicAccount, settings);

            Assert.Null(actual.GuildLeader);
        }

        [Fact]
        [Trait("Feature",  "Accounts")]
        [Trait("Category", "Integration")]
        public void Full_Account_GuildLeader_ShouldNotBeNull()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();

            var actual = JsonConvert.DeserializeObject<Account>(_fixture.Db.FullAccount, settings);

            Assert.NotNull(actual.GuildLeader);
        }

        [Fact]
        [Trait("Feature",  "Accounts")]
        [Trait("Category", "Integration")]
        public void Basic_Account_Created_ShouldNotBeDefaultValue()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();

            var actual = JsonConvert.DeserializeObject<Account>(_fixture.Db.BasicAccount, settings);

            Assert.NotEqual(default, actual.Created);
        }

        [Fact]
        [Trait("Feature",  "Accounts")]
        [Trait("Category", "Integration")]
        public void Full_Account_Created_ShouldNotBeDefaultValue()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();

            var actual = JsonConvert.DeserializeObject<Account>(_fixture.Db.FullAccount, settings);

            Assert.NotEqual(default, actual.Created);
        }

        [Fact]
        [Trait("Feature",  "Accounts")]
        [Trait("Category", "Integration")]
        public void Basic_Account_FractalLevel_ShouldBeNull()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();

            var actual = JsonConvert.DeserializeObject<Account>(_fixture.Db.BasicAccount, settings);

            Assert.Null(actual.FractalLevel);
        }

        [Fact]
        [Trait("Feature",  "Accounts")]
        [Trait("Category", "Integration")]
        public void Full_Account_FractalLevel_ShouldNotBeNull()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();

            var actual = JsonConvert.DeserializeObject<Account>(_fixture.Db.FullAccount, settings);

            Assert.NotNull(actual.FractalLevel);
        }

        [Fact]
        [Trait("Feature",  "Accounts")]
        [Trait("Category", "Integration")]
        public void Basic_Account_DailyAp_ShouldBeNull()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();

            var actual = JsonConvert.DeserializeObject<Account>(_fixture.Db.BasicAccount, settings);

            Assert.Null(actual.DailyAp);
        }

        [Fact]
        [Trait("Feature",  "Accounts")]
        [Trait("Category", "Integration")]
        public void Full_Account_DailyAp_ShouldNotBeNull()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();

            var actual = JsonConvert.DeserializeObject<Account>(_fixture.Db.FullAccount, settings);

            Assert.NotNull(actual.DailyAp);
        }

        [Fact]
        [Trait("Feature",  "Accounts")]
        [Trait("Category", "Integration")]
        public void Basic_Account_MonthlyAp_ShouldBeNull()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();

            var actual = JsonConvert.DeserializeObject<Account>(_fixture.Db.BasicAccount, settings);

            Assert.Null(actual.MonthlyAp);
        }

        [Fact]
        [Trait("Feature",  "Accounts")]
        [Trait("Category", "Integration")]
        public void Full_Account_MonthlyAp_ShouldNotBeNull()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();

            var actual = JsonConvert.DeserializeObject<Account>(_fixture.Db.FullAccount, settings);

            Assert.NotNull(actual.MonthlyAp);
        }

        [Fact]
        [Trait("Feature",  "Accounts")]
        [Trait("Category", "Integration")]
        public void Basic_Account_WvwRank_ShouldBeNull()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();

            var actual = JsonConvert.DeserializeObject<Account>(_fixture.Db.BasicAccount, settings);

            Assert.Null(actual.WvwRank);
        }

        [Fact]
        [Trait("Feature",  "Accounts")]
        [Trait("Category", "Integration")]
        public void Full_Account_WvwRank_ShouldNotBeNull()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();

            var actual = JsonConvert.DeserializeObject<Account>(_fixture.Db.FullAccount, settings);

            Assert.NotNull(actual.WvwRank);
        }
    }
}

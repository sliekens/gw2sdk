using GW2SDK.Accounts;
using GW2SDK.Enums;
using GW2SDK.Impl.JsonConverters;
using GW2SDK.Tests.Features.Accounts.Fixtures;
using GW2SDK.Tests.TestInfrastructure;
using Newtonsoft.Json;
using Xunit;
using Xunit.Abstractions;

namespace GW2SDK.Tests.Features.Accounts
{
    public class FullAccountTest : IClassFixture<AccountFixture>
    {
        public FullAccountTest(AccountFixture fixture, ITestOutputHelper output)
        {
            _fixture = fixture;
            _output = output;
        }

        private readonly AccountFixture _fixture;

        private readonly ITestOutputHelper _output;

        [Fact]
        [Trait("Feature",    "Accounts")]
        [Trait("Category",   "Integration")]
        [Trait("Importance", "Critical")]
        public void Class_ShouldHaveNoMissingMembers()
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
        public void Id_ShouldNotBeNull()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();

            var actual = JsonConvert.DeserializeObject<Account>(_fixture.Db.FullAccount, settings);

            Assert.NotEmpty(actual.Id);
        }

        [Fact]
        [Trait("Feature",  "Accounts")]
        [Trait("Category", "Integration")]
        public void Name_ShouldNotBeEmpty()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();

            var actual = JsonConvert.DeserializeObject<Account>(_fixture.Db.FullAccount, settings);

            Assert.NotEmpty(actual.Name);
        }

        [Fact]
        [Trait("Feature",  "Accounts")]
        [Trait("Category", "Integration")]
        public void Access_ShouldNotBeEmpty()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();

            var actual = JsonConvert.DeserializeObject<Account>(_fixture.Db.FullAccount, settings);

            Assert.NotEmpty(actual.Access);
        }

        [Fact]
        [Trait("Feature",  "Accounts")]
        [Trait("Category", "Integration")]
        public void Access_ShouldNotContainNone()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();

            var actual = JsonConvert.DeserializeObject<Account>(_fixture.Db.FullAccount, settings);

            Assert.DoesNotContain(ProductName.None, actual.Access);
        }

        [Fact]
        [Trait("Feature",  "Accounts")]
        [Trait("Category", "Integration")]
        public void Age_ShouldNotBeDefaultValue()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();

            var actual = JsonConvert.DeserializeObject<Account>(_fixture.Db.FullAccount, settings);

            Assert.NotEqual(default, actual.Age);
        }

        [Fact]
        [Trait("Feature",  "Accounts")]
        [Trait("Category", "Integration")]
        public void LastModified_ShouldNotBeDefaultValue()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();

            var actual = JsonConvert.DeserializeObject<Account>(_fixture.Db.FullAccount, settings);

            Assert.NotEqual(default, actual.LastModified);
        }

        [Fact]
        [Trait("Feature",  "Accounts")]
        [Trait("Category", "Integration")]
        public void Guilds_ShouldNotBeNull()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();

            var actual = JsonConvert.DeserializeObject<Account>(_fixture.Db.FullAccount, settings);

            Assert.NotNull(actual.Guilds);
        }

        [Fact]
        [Trait("Feature",  "Accounts")]
        [Trait("Category", "Integration")]
        public void Guilds_ShouldNotContainEmpty()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();

            var actual = JsonConvert.DeserializeObject<Account>(_fixture.Db.FullAccount, settings);

            Assert.All(actual.Guilds, Assert.NotEmpty);
        }

        [Fact]
        [Trait("Feature",  "Accounts")]
        [Trait("Category", "Integration")]
        public void GuildLeader_ShouldNotBeNull()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();

            var actual = JsonConvert.DeserializeObject<Account>(_fixture.Db.FullAccount, settings);

            Assert.NotNull(actual.GuildLeader);
        }

        [Fact]
        [Trait("Feature",  "Accounts")]
        [Trait("Category", "Integration")]
        public void GuildLeader_ShouldNotContainEmpty()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();

            var actual = JsonConvert.DeserializeObject<Account>(_fixture.Db.FullAccount, settings);

            Assert.All(actual.GuildLeader, Assert.NotEmpty);
        }

        [Fact]
        [Trait("Feature",  "Accounts")]
        [Trait("Category", "Integration")]
        public void Created_ShouldNotBeDefaultValue()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();

            var actual = JsonConvert.DeserializeObject<Account>(_fixture.Db.FullAccount, settings);

            Assert.NotEqual(default, actual.Created);
        }

        [Fact]
        [Trait("Feature",  "Accounts")]
        [Trait("Category", "Integration")]
        public void FractalLevel_ShouldNotBeNull()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();

            var actual = JsonConvert.DeserializeObject<Account>(_fixture.Db.FullAccount, settings);

            Assert.NotNull(actual.FractalLevel);
        }

        [Fact]
        [Trait("Feature",  "Accounts")]
        [Trait("Category", "Integration")]
        public void DailyAp_ShouldNotBeNull()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();

            var actual = JsonConvert.DeserializeObject<Account>(_fixture.Db.FullAccount, settings);

            Assert.NotNull(actual.DailyAp);
        }

        [Fact]
        [Trait("Feature",  "Accounts")]
        [Trait("Category", "Integration")]
        public void MonthlyAp_ShouldNotBeNull()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();

            var actual = JsonConvert.DeserializeObject<Account>(_fixture.Db.FullAccount, settings);

            Assert.NotNull(actual.MonthlyAp);
        }

        [Fact]
        [Trait("Feature",  "Accounts")]
        [Trait("Category", "Integration")]
        public void WvwRank_ShouldNotBeNull()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();

            var actual = JsonConvert.DeserializeObject<Account>(_fixture.Db.FullAccount, settings);

            Assert.NotNull(actual.WvwRank);
        }
    }
}

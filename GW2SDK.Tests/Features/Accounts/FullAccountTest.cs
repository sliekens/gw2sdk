using System;
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
        public void Account_can_be_fully_serialized_from_json_with_all_scopes()
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
        public void Name_is_never_empty()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();

            var actual = JsonConvert.DeserializeObject<Account>(_fixture.Db.FullAccount, settings);

            Assert.NotEmpty(actual.Name);
        }

        [Fact]
        [Trait("Feature",  "Accounts")]
        [Trait("Category", "Integration")]
        public void Access_is_never_empty()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();

            var actual = JsonConvert.DeserializeObject<Account>(_fixture.Db.FullAccount, settings);

            Assert.NotEmpty(actual.Access);
        }

        [Fact]
        [Trait("Feature",  "Accounts")]
        [Trait("Category", "Integration")]
        public void GuildLeader_is_included_by_guilds_scope()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();

            var actual = JsonConvert.DeserializeObject<Account>(_fixture.Db.FullAccount, settings);

            Assert.NotNull(actual.GuildLeader);
        }

        [Fact]
        [Trait("Feature",  "Accounts")]
        [Trait("Category", "Integration")]
        public void Access_is_never_none()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();

            var actual = JsonConvert.DeserializeObject<Account>(_fixture.Db.FullAccount, settings);

            Assert.DoesNotContain(ProductName.None, actual.Access);
        }

        [Fact]
        [Trait("Feature",  "Accounts")]
        [Trait("Category", "Integration")]
        public void Age_is_never_zero()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();

            var actual = JsonConvert.DeserializeObject<Account>(_fixture.Db.FullAccount, settings);

            Assert.NotEqual(TimeSpan.Zero, actual.Age);
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
        public void FractalLevel_is_included_by_progression_scope()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();

            var actual = JsonConvert.DeserializeObject<Account>(_fixture.Db.FullAccount, settings);

            Assert.NotNull(actual.FractalLevel);
        }

        [Fact]
        [Trait("Feature",  "Accounts")]
        [Trait("Category", "Integration")]
        public void DailyAp_is_included_by_progression_scope()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();

            var actual = JsonConvert.DeserializeObject<Account>(_fixture.Db.FullAccount, settings);

            Assert.NotNull(actual.DailyAp);
        }

        [Fact]
        [Trait("Feature",  "Accounts")]
        [Trait("Category", "Integration")]
        public void MonthlyAp_is_included_by_progression_scope()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();

            var actual = JsonConvert.DeserializeObject<Account>(_fixture.Db.FullAccount, settings);

            Assert.NotNull(actual.MonthlyAp);
        }

        [Fact]
        [Trait("Feature",  "Accounts")]
        [Trait("Category", "Integration")]
        public void WvwRank_is_included_by_progression_scope()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();

            var actual = JsonConvert.DeserializeObject<Account>(_fixture.Db.FullAccount, settings);

            Assert.NotNull(actual.WvwRank);
        }
    }
}

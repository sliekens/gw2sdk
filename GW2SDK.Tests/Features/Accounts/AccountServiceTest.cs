using System.Threading.Tasks;
using GW2SDK.Features.Accounts;
using GW2SDK.Features.Worlds;
using GW2SDK.Infrastructure;
using GW2SDK.Tests.Shared;
using GW2SDK.Tests.Shared.Fixtures;
using Newtonsoft.Json;
using Xunit;
using Xunit.Abstractions;

namespace GW2SDK.Tests.Features.Accounts
{
    public class AccountServiceTest : IClassFixture<HttpFixture>
    {
        public AccountServiceTest(HttpFixture http, ITestOutputHelper output)
        {
            _http = http;
            _output = output;
        }

        private readonly HttpFixture _http;

        private readonly ITestOutputHelper _output;

        [Fact]
        [Trait("Feature", "Accounts")]
        [Trait("Category", "Integration")]
        public async Task GetAccount_ShouldReturnAccount()
        {
            var sut = new AccountService(_http.HttpFullAccess);

            var actual = await sut.GetAccount();

            Assert.IsType<Account>(actual);
        }

        [Fact]
        [Trait("Feature", "Accounts")]
        [Trait("Category", "Integration")]
        [Trait("Importance", "Critical")]
        public async Task Basic_Account_ShouldHaveNoMissingMembers()
        {
            var sut = new AccountService(_http.HttpBasicAccess);

            var settings = new JsonSerializerSettingsBuilder()
                .UseMissingMemberHandling(MissingMemberHandling.Error)
                .UseTraceWriter(new XunitTraceWriter(_output))
                .Build();

            // Next statement throws if there are missing members
            _ = await sut.GetAccount(settings);
        }

        [Fact]
        [Trait("Feature", "Accounts")]
        [Trait("Category", "Integration")]
        [Trait("Importance", "Critical")]
        public async Task Full_Account_ShouldHaveNoMissingMembers()
        {
            var sut = new AccountService(_http.HttpFullAccess);

            var settings = new JsonSerializerSettingsBuilder()
                .UseMissingMemberHandling(MissingMemberHandling.Error)
                .UseTraceWriter(new XunitTraceWriter(_output))
                .Build();

            // Next statement throws if there are missing members
            _ = await sut.GetAccount(settings);
        }
        
        [Fact]
        [Trait("Feature", "Accounts")]
        [Trait("Category", "Integration")]
        public async Task Basic_Account_Id_ShouldNotBeDefaultValue()
        {
            var sut = new AccountService(_http.HttpBasicAccess);

            var settings = new JsonSerializerSettingsBuilder()
                .UseMissingMemberHandling(MissingMemberHandling.Error)
                .UseTraceWriter(new XunitTraceWriter(_output))
                .Build();

            var actual = await sut.GetAccount(settings);

            Assert.NotEqual(default, actual.Id);
        }
        
        [Fact]
        [Trait("Feature", "Accounts")]
        [Trait("Category", "Integration")]
        public async Task Full_Account_Id_ShouldNotBeDefaultValue()
        {
            var sut = new AccountService(_http.HttpFullAccess);

            var settings = new JsonSerializerSettingsBuilder()
                .UseMissingMemberHandling(MissingMemberHandling.Error)
                .UseTraceWriter(new XunitTraceWriter(_output))
                .Build();

            var actual = await sut.GetAccount(settings);

            Assert.NotEqual(default, actual.Id);
        }

        [Fact]
        [Trait("Feature", "Accounts")]
        [Trait("Category", "Integration")]
        public async Task Basic_Account_Name_ShouldNotBeEmpty()
        {
            var sut = new AccountService(_http.HttpBasicAccess);

            var settings = new JsonSerializerSettingsBuilder()
                .UseMissingMemberHandling(MissingMemberHandling.Error)
                .UseTraceWriter(new XunitTraceWriter(_output))
                .Build();

            var actual = await sut.GetAccount(settings);

            Assert.NotEmpty(actual.Name);
        }
        
        [Fact]
        [Trait("Feature", "Accounts")]
        [Trait("Category", "Integration")]
        public async Task Full_Account_Name_ShouldNotBeEmpty()
        {
            var sut = new AccountService(_http.HttpFullAccess);

            var settings = new JsonSerializerSettingsBuilder()
                .UseMissingMemberHandling(MissingMemberHandling.Error)
                .UseTraceWriter(new XunitTraceWriter(_output))
                .Build();

            var actual = await sut.GetAccount(settings);

            Assert.NotEmpty(actual.Name);
        }

        [Fact]
        [Trait("Feature", "Accounts")]
        [Trait("Category", "Integration")]
        public async Task Basic_Account_Access_ShouldNotBeEmpty()
        {
            var sut = new AccountService(_http.HttpBasicAccess);

            var settings = new JsonSerializerSettingsBuilder()
                .UseMissingMemberHandling(MissingMemberHandling.Error)
                .UseTraceWriter(new XunitTraceWriter(_output))
                .Build();

            var actual = await sut.GetAccount(settings);

            Assert.NotEmpty(actual.Access);
        }
        
        [Fact]
        [Trait("Feature", "Accounts")]
        [Trait("Category", "Integration")]
        public async Task Full_Account_Access_ShouldNotBeEmpty()
        {
            var sut = new AccountService(_http.HttpFullAccess);

            var settings = new JsonSerializerSettingsBuilder()
                .UseMissingMemberHandling(MissingMemberHandling.Error)
                .UseTraceWriter(new XunitTraceWriter(_output))
                .Build();

            var actual = await sut.GetAccount(settings);

            Assert.NotEmpty(actual.Access);
        }

        [Fact]
        [Trait("Feature", "Accounts")]
        [Trait("Category", "Integration")]
        public async Task Basic_Account_Access_ShouldNotContainNone()
        {
            var sut = new AccountService(_http.HttpBasicAccess);

            var settings = new JsonSerializerSettingsBuilder()
                .UseMissingMemberHandling(MissingMemberHandling.Error)
                .UseTraceWriter(new XunitTraceWriter(_output))
                .Build();

            var actual = await sut.GetAccount(settings);

            Assert.DoesNotContain(ProductName.None, actual.Access);
        }
        
        [Fact]
        [Trait("Feature", "Accounts")]
        [Trait("Category", "Integration")]
        public async Task Full_Account_Access_ShouldNotContainNone()
        {
            var sut = new AccountService(_http.HttpFullAccess);

            var settings = new JsonSerializerSettingsBuilder()
                .UseMissingMemberHandling(MissingMemberHandling.Error)
                .UseTraceWriter(new XunitTraceWriter(_output))
                .Build();

            var actual = await sut.GetAccount(settings);

            Assert.DoesNotContain(ProductName.None, actual.Access);
        }

        [Fact]
        [Trait("Feature", "Accounts")]
        [Trait("Category", "Integration")]
        public async Task Basic_Account_Age_ShouldNotBeDefaultValue()
        {
            var sut = new AccountService(_http.HttpBasicAccess);

            var settings = new JsonSerializerSettingsBuilder()
                .UseMissingMemberHandling(MissingMemberHandling.Error)
                .UseTraceWriter(new XunitTraceWriter(_output))
                .Build();

            var actual = await sut.GetAccount(settings);

            Assert.NotEqual(default, actual.Age);
        }
        
        [Fact]
        [Trait("Feature", "Accounts")]
        [Trait("Category", "Integration")]
        public async Task Full_Account_Age_ShouldNotBeDefaultValue()
        {
            var sut = new AccountService(_http.HttpFullAccess);

            var settings = new JsonSerializerSettingsBuilder()
                .UseMissingMemberHandling(MissingMemberHandling.Error)
                .UseTraceWriter(new XunitTraceWriter(_output))
                .Build();

            var actual = await sut.GetAccount(settings);

            Assert.NotEqual(default, actual.Age);
        }
        
        [Fact]
        [Trait("Feature", "Accounts")]
        [Trait("Category", "Integration")]
        public async Task Basic_Account_LastModified_ShouldNotBeDefaultValue()
        {
            var sut = new AccountService(_http.HttpBasicAccess);

            var settings = new JsonSerializerSettingsBuilder()
                .UseMissingMemberHandling(MissingMemberHandling.Error)
                .UseTraceWriter(new XunitTraceWriter(_output))
                .Build();

            var actual = await sut.GetAccount(settings);

            Assert.NotEqual(default, actual.LastModified);
        }

        [Fact]
        [Trait("Feature", "Accounts")]
        [Trait("Category", "Integration")]
        public async Task Full_Account_LastModified_ShouldNotBeDefaultValue()
        {
            var sut = new AccountService(_http.HttpFullAccess);

            var settings = new JsonSerializerSettingsBuilder()
                .UseMissingMemberHandling(MissingMemberHandling.Error)
                .UseTraceWriter(new XunitTraceWriter(_output))
                .Build();

            var actual = await sut.GetAccount(settings);

            Assert.NotEqual(default, actual.LastModified);
        }
        
        [Fact]
        [Trait("Feature", "Accounts")]
        [Trait("Category", "Integration")]
        public async Task Basic_Account_World_ShouldBeValidId()
        {
            var sut = new AccountService(_http.HttpBasicAccess);

            var settings = new JsonSerializerSettingsBuilder()
                .UseMissingMemberHandling(MissingMemberHandling.Error)
                .UseTraceWriter(new XunitTraceWriter(_output))
                .Build();

            var actual = await sut.GetAccount(settings);

            var worldIds = await new WorldService(_http.Http).GetWorldIds();

            Assert.Contains(actual.World, worldIds);
        }

        [Fact]
        [Trait("Feature", "Accounts")]
        [Trait("Category", "Integration")]
        public async Task Full_Account_World_ShouldBeValidId()
        {
            var sut = new AccountService(_http.HttpFullAccess);

            var settings = new JsonSerializerSettingsBuilder()
                .UseMissingMemberHandling(MissingMemberHandling.Error)
                .UseTraceWriter(new XunitTraceWriter(_output))
                .Build();

            var actual = await sut.GetAccount(settings);

            var worldIds = await new WorldService(_http.Http).GetWorldIds();

            Assert.Contains(actual.World, worldIds);
        }

        [Fact]
        [Trait("Feature", "Accounts")]
        [Trait("Category", "Integration")]
        public async Task Basic_Account_Guilds_ShouldNotBeNull()
        {
            var sut = new AccountService(_http.HttpBasicAccess);

            var settings = new JsonSerializerSettingsBuilder()
                .UseMissingMemberHandling(MissingMemberHandling.Error)
                .UseTraceWriter(new XunitTraceWriter(_output))
                .Build();

            var actual = await sut.GetAccount(settings);

            Assert.NotNull(actual.Guilds);
        }

        [Fact]
        [Trait("Feature", "Accounts")]
        [Trait("Category", "Integration")]
        public async Task Full_Account_Guilds_ShouldNotBeNull()
        {
            var sut = new AccountService(_http.HttpFullAccess);

            var settings = new JsonSerializerSettingsBuilder()
                .UseMissingMemberHandling(MissingMemberHandling.Error)
                .UseTraceWriter(new XunitTraceWriter(_output))
                .Build();

            var actual = await sut.GetAccount(settings);

            Assert.NotNull(actual.Guilds);
        }
        
        [Fact]
        [Trait("Feature", "Accounts")]
        [Trait("Category", "Integration")]
        public async Task Basic_Account_GuildLeader_ShouldBeNull()
        {
            var sut = new AccountService(_http.HttpBasicAccess);

            var settings = new JsonSerializerSettingsBuilder()
                .UseMissingMemberHandling(MissingMemberHandling.Error)
                .UseTraceWriter(new XunitTraceWriter(_output))
                .Build();

            var actual = await sut.GetAccount(settings);

            Assert.Null(actual.GuildLeader);
        }
        
        [Fact]
        [Trait("Feature", "Accounts")]
        [Trait("Category", "Integration")]
        public async Task Full_Account_GuildLeader_ShouldNotBeNull()
        {
            var sut = new AccountService(_http.HttpFullAccess);

            var settings = new JsonSerializerSettingsBuilder()
                .UseMissingMemberHandling(MissingMemberHandling.Error)
                .UseTraceWriter(new XunitTraceWriter(_output))
                .Build();

            var actual = await sut.GetAccount(settings);

            Assert.NotNull(actual.GuildLeader);
        }
        
        [Fact]
        [Trait("Feature", "Accounts")]
        [Trait("Category", "Integration")]
        public async Task Basic_Account_Created_ShouldNotBeDefaultValue()
        {
            var sut = new AccountService(_http.HttpBasicAccess);

            var settings = new JsonSerializerSettingsBuilder()
                .UseMissingMemberHandling(MissingMemberHandling.Error)
                .UseTraceWriter(new XunitTraceWriter(_output))
                .Build();

            var actual = await sut.GetAccount(settings);

            Assert.NotEqual(default, actual.Created);
        }
        
        [Fact]
        [Trait("Feature", "Accounts")]
        [Trait("Category", "Integration")]
        public async Task Full_Account_Created_ShouldNotBeDefaultValue()
        {
            var sut = new AccountService(_http.HttpFullAccess);

            var settings = new JsonSerializerSettingsBuilder()
                .UseMissingMemberHandling(MissingMemberHandling.Error)
                .UseTraceWriter(new XunitTraceWriter(_output))
                .Build();

            var actual = await sut.GetAccount(settings);

            Assert.NotEqual(default, actual.Created);
        }

        [Fact]
        [Trait("Feature", "Accounts")]
        [Trait("Category", "Integration")]
        public async Task Basic_Account_FractalLevel_ShouldBeNull()
        {
            var sut = new AccountService(_http.HttpBasicAccess);

            var settings = new JsonSerializerSettingsBuilder()
                .UseMissingMemberHandling(MissingMemberHandling.Error)
                .UseTraceWriter(new XunitTraceWriter(_output))
                .Build();

            var actual = await sut.GetAccount(settings);

            Assert.Null(actual.FractalLevel);
        }
        
        [Fact]
        [Trait("Feature", "Accounts")]
        [Trait("Category", "Integration")]
        public async Task Full_Account_FractalLevel_ShouldNotBeNull()
        {
            var sut = new AccountService(_http.HttpFullAccess);

            var settings = new JsonSerializerSettingsBuilder()
                .UseMissingMemberHandling(MissingMemberHandling.Error)
                .UseTraceWriter(new XunitTraceWriter(_output))
                .Build();

            var actual = await sut.GetAccount(settings);

            Assert.NotNull(actual.FractalLevel);
        }
        
        [Fact]
        [Trait("Feature", "Accounts")]
        [Trait("Category", "Integration")]
        public async Task Basic_Account_DailyAp_ShouldBeNull()
        {
            var sut = new AccountService(_http.HttpBasicAccess);

            var settings = new JsonSerializerSettingsBuilder()
                .UseMissingMemberHandling(MissingMemberHandling.Error)
                .UseTraceWriter(new XunitTraceWriter(_output))
                .Build();

            var actual = await sut.GetAccount(settings);

            Assert.Null(actual.DailyAp);
        }
        
        [Fact]
        [Trait("Feature", "Accounts")]
        [Trait("Category", "Integration")]
        public async Task Full_Account_DailyAp_ShouldNotBeNull()
        {
            var sut = new AccountService(_http.HttpFullAccess);

            var settings = new JsonSerializerSettingsBuilder()
                .UseMissingMemberHandling(MissingMemberHandling.Error)
                .UseTraceWriter(new XunitTraceWriter(_output))
                .Build();

            var actual = await sut.GetAccount(settings);

            Assert.NotNull(actual.DailyAp);
        }
        
        [Fact]
        [Trait("Feature", "Accounts")]
        [Trait("Category", "Integration")]
        public async Task Basic_Account_MonthlyAp_ShouldBeNull()
        {
            var sut = new AccountService(_http.HttpBasicAccess);

            var settings = new JsonSerializerSettingsBuilder()
                .UseMissingMemberHandling(MissingMemberHandling.Error)
                .UseTraceWriter(new XunitTraceWriter(_output))
                .Build();

            var actual = await sut.GetAccount(settings);

            Assert.Null(actual.MonthlyAp);
        }
        
        [Fact]
        [Trait("Feature", "Accounts")]
        [Trait("Category", "Integration")]
        public async Task Full_Account_MonthlyAp_ShouldNotBeNull()
        {
            var sut = new AccountService(_http.HttpFullAccess);

            var settings = new JsonSerializerSettingsBuilder()
                .UseMissingMemberHandling(MissingMemberHandling.Error)
                .UseTraceWriter(new XunitTraceWriter(_output))
                .Build();

            var actual = await sut.GetAccount(settings);

            Assert.NotNull(actual.MonthlyAp);
        }
        
        [Fact]
        [Trait("Feature", "Accounts")]
        [Trait("Category", "Integration")]
        public async Task Basic_Account_WvwRank_ShouldBeNull()
        {
            var sut = new AccountService(_http.HttpBasicAccess);

            var settings = new JsonSerializerSettingsBuilder()
                .UseMissingMemberHandling(MissingMemberHandling.Error)
                .UseTraceWriter(new XunitTraceWriter(_output))
                .Build();

            var actual = await sut.GetAccount(settings);

            Assert.Null(actual.WvwRank);
        }
        
        [Fact]
        [Trait("Feature", "Accounts")]
        [Trait("Category", "Integration")]
        public async Task Full_Account_WvwRank_ShouldNotBeNull()
        {
            var sut = new AccountService(_http.HttpFullAccess);

            var settings = new JsonSerializerSettingsBuilder()
                .UseMissingMemberHandling(MissingMemberHandling.Error)
                .UseTraceWriter(new XunitTraceWriter(_output))
                .Build();

            var actual = await sut.GetAccount(settings);

            Assert.NotNull(actual.WvwRank);
        }
    }
}

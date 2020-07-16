using System;
using System.Diagnostics;
using GW2SDK.Annotations;
using GW2SDK.Enums;
using GW2SDK.Impl.JsonConverters;
using GW2SDK.Impl.JsonReaders;
using Newtonsoft.Json;

namespace GW2SDK.Accounts
{
    internal sealed class AccountJsonReader : JsonObjectReader<Account>
    {
        public AccountJsonReader()
        {
            Map("id", account => account.Id);
            Map("name", account => account.Name);
            Map("age", account => account.Age, SecondsJsonReader.Instance);
            Map("last_modified", account => account.LastModified);
            Map("world", account => account.World);
            Map("guilds", account => account.Guilds);
            Map("guild_leader", account => account.GuildLeader, PropertySignificance.Optional);
            Map("created", account => account.Created);
            Map("access", account => account.Access, new JsonStringEnumReader<ProductName>());
            Map("commander", account => account.Commander);
            Map("fractal_level", account => account.FractalLevel);
            Map("daily_ap", account => account.DailyAp);
            Map("monthly_ap", account => account.MonthlyAp);
            Map("wvw_rank", account => account.WvwRank);
            Map("build_storage_slots", account => account.BuildStorageSlots);
            Compile();
        }
    }

    [PublicAPI]
    [DebuggerDisplay("{Name,nq}")]
    [DataTransferObject]
    public sealed class Account
    {
        [JsonProperty(Required = Required.Always)]
        public string Id { get; set; } = "";

        [JsonProperty(Required = Required.Always)]
        public string Name { get; set; } = "";

        [JsonProperty(Required = Required.Always)]
        [JsonConverter(typeof(SecondsConverter))]
        public TimeSpan Age { get; set; }

        [JsonProperty(Required = Required.Always)]
        public DateTimeOffset LastModified { get; set; }

        [JsonProperty(Required = Required.Always)]
        public int World { get; set; }

        [JsonProperty(Required = Required.Always)]
        public string[] Guilds { get; set; } = new string[0];

        [Scope(Permission.Guilds)]
        [JsonProperty(Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string[]? GuildLeader { get; set; }

        [JsonProperty(Required = Required.Always)]
        public DateTimeOffset Created { get; set; }

        [JsonProperty(Required = Required.Always)]
        public ProductName[] Access { get; set; } = new ProductName[0];

        [JsonProperty(Required = Required.Always)]
        public bool Commander { get; set; }

        [Scope(Permission.Progression)]
        [JsonProperty(Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public int? FractalLevel { get; set; }

        [Scope(Permission.Progression)]
        [JsonProperty(Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public int? DailyAp { get; set; }

        [Scope(Permission.Progression)]
        [JsonProperty(Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public int? MonthlyAp { get; set; }

        [Scope(Permission.Progression)]
        [JsonProperty(Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public int? WvwRank { get; set; }

        [Scope(Permission.Builds)]
        [JsonProperty(Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public int? BuildStorageSlots { get; set; }
    }
}

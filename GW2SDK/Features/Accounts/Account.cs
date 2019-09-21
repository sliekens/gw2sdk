using System;
using System.Diagnostics;
using GW2SDK.Annotations;
using GW2SDK.Enums;
using GW2SDK.Impl.JsonConverters;
using Newtonsoft.Json;

namespace GW2SDK.Accounts
{
    [PublicAPI]
    [DebuggerDisplay("{Name,nq}")]
    [DataTransferObject]
    public sealed class Account
    {
        [NotNull]
        [JsonProperty(Required = Required.Always)]
        public string Id { get; set; }

        [JsonProperty(Required = Required.Always)]
        public string Name { get; set; }

        [JsonProperty(Required = Required.Always)]
        [JsonConverter(typeof(SecondsConverter))]
        public TimeSpan Age { get; set; }

        [JsonProperty(Required = Required.Always)]
        public DateTimeOffset LastModified { get; set; }

        [JsonProperty(Required = Required.Always)]
        public int World { get; set; }

        [NotNull]
        [ItemNotNull]
        [JsonProperty(Required = Required.Always)]
        public string[] Guilds { get; set; }

        [Scope(Permission.Guilds)]
        [CanBeNull]
        [ItemNotNull]
        [JsonProperty(Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string[] GuildLeader { get; set; }

        [JsonProperty(Required = Required.Always)]
        public DateTimeOffset Created { get; set; }

        [JsonProperty(Required = Required.Always)]
        public ProductName[] Access { get; set; }

        [JsonProperty(Required = Required.Always)]
        public bool Commander { get; set; }

        [Scope(Permission.Progression)]
        [CanBeNull]
        [JsonProperty(Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public int? FractalLevel { get; set; }

        [Scope(Permission.Progression)]
        [CanBeNull]
        [JsonProperty(Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public int? DailyAp { get; set; }

        [Scope(Permission.Progression)]
        [CanBeNull]
        [JsonProperty(Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public int? MonthlyAp { get; set; }

        [Scope(Permission.Progression)]
        [CanBeNull]
        [JsonProperty(Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public int? WvwRank { get; set; }
    }
}

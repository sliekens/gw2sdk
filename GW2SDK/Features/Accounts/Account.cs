using System;
using System.Diagnostics;
using GW2SDK.Annotations;
using GW2SDK.Enums;
using GW2SDK.Impl;
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
        public string Id { get; set; }

        public string Name { get; set; }

        [JsonConverter(typeof(SecondsConverter))]
        public TimeSpan Age { get; set; }

        public DateTimeOffset LastModified { get; set; }

        public int World { get; set; }

        [NotNull]
        [ItemNotNull]
        public string[] Guilds { get; set; }

        [CanBeNull]
        [ItemNotNull]
        [Scope(Permission.Guilds)]
        public string[] GuildLeader { get; set; }

        public DateTimeOffset Created { get; set; }

        public ProductName[] Access { get; set; }

        public bool Commander { get; set; }

        [CanBeNull]
        [Scope(Permission.Progression)]
        public int? FractalLevel { get; set; }

        [CanBeNull]
        [Scope(Permission.Progression)]
        public int? DailyAp { get; set; }

        [CanBeNull]
        [Scope(Permission.Progression)]
        public int? MonthlyAp { get; set; }

        [CanBeNull]
        [Scope(Permission.Progression)]
        public int? WvwRank { get; set; }
    }
}

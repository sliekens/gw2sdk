using System;
using System.Diagnostics;
using GW2SDK.Features.Common;
using GW2SDK.Infrastructure;
using Newtonsoft.Json;

namespace GW2SDK.Features.Accounts
{
    [DebuggerDisplay("{Name,nq}")]
    [DataTransferObject]
    public sealed class Account
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        [JsonConverter(typeof(SecondsConverter))]
        public TimeSpan Age { get; set; }

        public DateTimeOffset LastModified { get; set; }

        public int World { get; set; }

        public Guid[] Guilds { get; set; }

        [CanBeNull]
        [Scope(Permission.Guilds)]
        public Guid[] GuildLeader { get; set; }

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

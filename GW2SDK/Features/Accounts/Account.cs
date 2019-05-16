using System;
using GW2SDK.Features.Tokens;
using GW2SDK.Infrastructure;
using Newtonsoft.Json;

namespace GW2SDK.Features.Accounts
{
    public sealed class Account
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        [JsonConverter(typeof(SecondsConverter))]
        public TimeSpan Age { get; set; }

        public DateTimeOffset LastModified { get; set; }

        public int World { get; set; }

        public Guid[] Guilds { get; set; }

        public Guid[] GuildLeader { get; set; }

        public DateTimeOffset Created { get; set; }

        public GameAccess[] Access { get; set; }

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

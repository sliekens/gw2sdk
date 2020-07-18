using System;
using System.Diagnostics;
using GW2SDK.Annotations;
using GW2SDK.Enums;

namespace GW2SDK.Accounts
{
    [PublicAPI]
    [DebuggerDisplay("{Name,nq}")]
    [DataTransferObject]
    public sealed class Account
    {
        public string Id { get; set; } = "";

        public string Name { get; set; } = "";

        public TimeSpan Age { get; set; }

        public DateTimeOffset LastModified { get; set; }

        public int World { get; set; }

        public string[] Guilds { get; set; } = new string[0];

        [Scope(Permission.Guilds)]
        public string[]? GuildLeader { get; set; }

        public DateTimeOffset Created { get; set; }

        public ProductName[] Access { get; set; } = new ProductName[0];

        public bool Commander { get; set; }

        [Scope(Permission.Progression)]
        public int? FractalLevel { get; set; }

        [Scope(Permission.Progression)]
        public int? DailyAp { get; set; }

        [Scope(Permission.Progression)]
        public int? MonthlyAp { get; set; }

        [Scope(Permission.Progression)]
        public int? WvwRank { get; set; }

        [Scope(Permission.Builds)]
        public int? BuildStorageSlots { get; set; }
    }
}

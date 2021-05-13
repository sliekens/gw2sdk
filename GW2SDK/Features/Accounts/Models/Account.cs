using System;
using GW2SDK.Annotations;

namespace GW2SDK.Accounts
{
    [PublicAPI]
    [DataTransferObject]
    public sealed record Account
    {
        public string Id { get; init; } = "";

        public string Name { get; init; } = "";

        public TimeSpan Age { get; init; }

        public DateTimeOffset LastModified { get; init; }

        public int World { get; init; }

        public string[] Guilds { get; init; } = new string[0];

        [Scope(Permission.Guilds)]
        public string[]? GuildLeader { get; init; }

        public DateTimeOffset Created { get; init; }

        public ProductName[] Access { get; init; } = new ProductName[0];

        public bool Commander { get; init; }

        [Scope(Permission.Progression)]
        public int? FractalLevel { get; init; }

        [Scope(Permission.Progression)]
        public int? DailyAp { get; init; }

        [Scope(Permission.Progression)]
        public int? MonthlyAp { get; init; }

        [Scope(Permission.Progression)]
        public int? WvwRank { get; init; }

        [Scope(Permission.Builds)]
        public int? BuildStorageSlots { get; init; }
    }
}

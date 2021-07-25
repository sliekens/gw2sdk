using System;
using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Masteries
{
    [PublicAPI]
    [DataTransferObject]
    public sealed record Mastery
    {
        public int Id { get; init; }

        public string Name { get; init; } = "";

        /// <remarks>Can be empty.</remarks>
        public string Requirement { get; init; } = "";

        public int Order { get; init; }

        public string Background { get; init; } = "";

        public MasteryRegionName Region { get; init; }

        public MasteryLevel[] Levels { get; init; } = Array.Empty<MasteryLevel>();
    }
}

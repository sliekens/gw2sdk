using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Backstories.Answers
{
    [PublicAPI]
    [DataTransferObject]
    public sealed record BackstoryAnswer
    {
        public string Id { get; init; } = "";

        public string Title { get; init; } = "";

        public string Description { get; init; } = "";

        public string Journal { get; init; } = "";

        public int Question { get; init; }

        public Race[]? Races { get; init; }

        public Profession[]? Professions { get; init; }
    }
}

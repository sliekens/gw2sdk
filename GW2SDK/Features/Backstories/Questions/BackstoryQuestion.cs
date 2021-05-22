using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Backstories.Questions
{
    [PublicAPI]
    [DataTransferObject]
    public sealed record BackstoryQuestion
    {
        public int Id { get; init; }

        public string Title { get; init; } = "";

        public string Description { get; init; } = "";

        public string[] Answers { get; init; } = new string[0];

        public int Order { get; init; }

        public Race[]? Races { get; init; }

        public ProfessionName[]? Professions { get; init; }
    }
}

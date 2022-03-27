using System;
using System.Collections.Generic;
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

        public IReadOnlyCollection<string> Answers { get; init; } = Array.Empty<string>();

        public int Order { get; init; }

        public IReadOnlyCollection<Race>? Races { get; init; }

        public IReadOnlyCollection<ProfessionName>? Professions { get; init; }
    }
}

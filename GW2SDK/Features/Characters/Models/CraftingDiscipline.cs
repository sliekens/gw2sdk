using JetBrains.Annotations;

namespace GW2SDK.Characters
{
    [PublicAPI]
    public sealed record CraftingDiscipline
    {
        public CraftingDisciplineName Discipline { get; init; }

        public int Rating { get; init; }

        public bool Active { get; init; }
    }
}

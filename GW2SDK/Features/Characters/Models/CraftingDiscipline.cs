namespace GW2SDK.Characters
{
    public sealed record CraftingDiscipline
    {
        public CraftingDisciplineName Discipline { get; init; }

        public int Rating { get; init; }

        public bool Active { get; init; }
    }
}

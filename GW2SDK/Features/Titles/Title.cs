using GW2SDK.Annotations;

namespace GW2SDK.Titles
{
    [PublicAPI]
    [DataTransferObject]
    public sealed record Title
    {
        public int Id { get; init; }

        public string Name { get; init; } = "";

        public int[]? Achievements { get; init; }

        public int? AchievementPointsRequired { get; init; }
    }
}

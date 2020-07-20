using System.Diagnostics;
using GW2SDK.Annotations;

namespace GW2SDK.Titles
{
    [PublicAPI]
    [DebuggerDisplay("{Name,nq}")]
    [DataTransferObject]
    public sealed class Title
    {
        public int Id { get; set; }

        public string Name { get; set; } = "";

        public int[]? Achievements { get; set; }

        public int? AchievementPointsRequired { get; set; }
    }
}

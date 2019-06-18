using System.Diagnostics;
using GW2SDK.Infrastructure;

namespace GW2SDK.Features.Achievements.Categories
{
    [PublicAPI]
    [DebuggerDisplay("{Name,nq}")]
    [DataTransferObject]
    public sealed class AchievementCategory
    {
        public int Id { get; set; }

        [NotNull]
        public string Name { get; set; }

        [NotNull]
        public string Description { get; set; }

        public int Order { get; set; }

        [NotNull]
        public string Icon { get; set; }

        [NotNull]
        public int[] Achievements { get; set; }
    }
}

using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Achievements
{
    [PublicAPI]
    [Inheritable]
    [DataTransferObject(RootObject = false)]
    public record AchievementBit;
}

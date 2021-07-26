using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Mounts
{
    [PublicAPI]
    [DataTransferObject(RootObject = false)]
    public sealed record SkillReference
    {
        public int Id { get; init; }

        public SkillSlot Slot { get; init; }
    }
}

using GW2SDK.Infrastructure;

namespace GW2SDK.Features.Items
{
    [PublicAPI]
    [DataTransferObject(RootObject = false)]
    public sealed class Buff
    {
        public int SkillId { get; set; }

        [CanBeNull]
        public string Description { get; set; }
    }
}
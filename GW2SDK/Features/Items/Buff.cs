using GW2SDK.Annotations;

namespace GW2SDK.Items
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
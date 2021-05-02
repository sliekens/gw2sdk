using GW2SDK.Annotations;

namespace GW2SDK.Items
{
    [PublicAPI]
    [DataTransferObject(RootObject = false)]
    public sealed record Buff
    {
        public int SkillId { get; init; }

        public string Description { get; init; } = "";
    }
}

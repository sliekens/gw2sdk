using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Skills
{
    [PublicAPI]
    [Inheritable]
    [DataTransferObject(RootObject = false)]
    public record SkillFact
    {
        public string Text { get; init; } = "";

        public string Icon { get; init; } = "";
    }
}
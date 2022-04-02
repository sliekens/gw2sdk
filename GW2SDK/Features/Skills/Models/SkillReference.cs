using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Skills
{
    [PublicAPI]
    [DataTransferObject(RootObject = false)]
    public sealed record SkillReference
    {
        public int Id { get; init; }

        public Attunement? Attunement { get; init; }

        public Transformation? Form { get; init; }
    }
}

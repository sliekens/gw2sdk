using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Traits
{
    [PublicAPI]
    [DataTransferObject(RootObject = false)]
    public sealed record CompoundTraitFact
    {
        /// <summary>The ID of the trait that activates this combination.</summary>
        public int RequiresTrait { get; init; }

        /// <summary>The index of the fact that is replaced by this <see cref="Fact" />, or <c>null</c> if it is to be appended to
        /// the existing facts.</summary>
        public int? Overrides { get; init; }

        public TraitFact Fact { get; init; } = new();
    }
}

using System;
using GW2SDK.Annotations;

namespace GW2SDK.Traits
{
    [PublicAPI]
    [Inheritable]
    [DataTransferObject(RootObject = false)]
    public class BuffTraitFact : TraitFact
    {
        /// <summary>The duration of the effect applied by the trait, or null when the effect is removed by the trait.</summary>
        public TimeSpan? Duration { get; set; }

        public string Status { get; set; } = "";

        public string? Description { get; set; }

        /// <summary>The number of stacks applied by the trait, or null when the effect is removed by the trait.</summary>
        public int? ApplyCount { get; set; }
    }
}

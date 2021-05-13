using System;
using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Traits
{
    [PublicAPI]
    [DataTransferObject(RootObject = false)]
    public sealed record TimeTraitFact : TraitFact
    {
        public TimeSpan Duration { get; init; }
    }
}

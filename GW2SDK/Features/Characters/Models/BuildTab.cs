using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Characters
{
    [PublicAPI]
    [DataTransferObject]
    public sealed record BuildTab
    {
        /// <summary>The number of the current tab.</summary>
        public int Tab { get; init; }

        /// <summary>The selected skills and traits on the current build tab.</summary>
        public Build Build { get; init; } = new();
    }
}

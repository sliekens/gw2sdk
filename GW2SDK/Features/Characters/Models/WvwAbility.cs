using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Characters
{
    [PublicAPI]
    [DataTransferObject]
    public sealed record WvwAbility
    {
        /// <summary>The ID of the current ability.</summary>
        public int Id { get; init; }

        /// <summary>The current rank of the ability.</summary>
        public int Rank { get; init; }
    }
}

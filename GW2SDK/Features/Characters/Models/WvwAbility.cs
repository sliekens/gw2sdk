using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Characters
{
    [PublicAPI]
    [DataTransferObject(RootObject = false)]
    public sealed record WvwAbility
    {
        public int Id { get; init; }

        public int Rank { get; init; }
    }
}
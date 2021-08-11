using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Characters
{
    [PublicAPI]
    [DataTransferObject(RootObject = false)]
    public sealed record BuildTab
    {
        public int Tab { get; init; }

        public Build Build { get; init; } = new();
    }
}

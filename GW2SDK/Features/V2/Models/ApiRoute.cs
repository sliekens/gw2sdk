using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.V2
{
    [PublicAPI]
    [DataTransferObject(RootObject = false)]
    public sealed record ApiRoute
    {
        public string Path { get; init; } = "";

        public bool Multilingual { get; init; }

        public bool RequiresAuthorization { get; init; }

        public bool Active { get; init; }
    }
}

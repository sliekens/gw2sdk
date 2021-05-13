using GW2SDK.Annotations;

namespace GW2SDK.V2
{
    [PublicAPI]
    [DataTransferObject(RootObject = false)]
    public sealed record ApiVersion
    {
        public string Version { get; init; } = "latest";

        public string Description { get; init; } = "";
    }
}

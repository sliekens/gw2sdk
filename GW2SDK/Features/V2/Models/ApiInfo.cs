using GW2SDK.Annotations;

namespace GW2SDK.V2
{
    [PublicAPI]
    [DataTransferObject]
    public sealed record ApiInfo
    {
        public string[] Languages { get; init; } = new string[0];

        public ApiRoute[] Routes { get; init; } = new ApiRoute[0];

        public ApiVersion[] SchemaVersions { get; init; } = new ApiVersion[0];
    }
}

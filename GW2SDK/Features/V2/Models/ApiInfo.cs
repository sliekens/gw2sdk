using System;
using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.V2
{
    [PublicAPI]
    [DataTransferObject]
    public sealed record ApiInfo
    {
        public string[] Languages { get; init; } = Array.Empty<string>();

        public ApiRoute[] Routes { get; init; } = Array.Empty<ApiRoute>();

        public ApiVersion[] SchemaVersions { get; init; } = Array.Empty<ApiVersion>();
    }
}

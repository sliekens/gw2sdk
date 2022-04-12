using System;
using System.Collections.Generic;

using GW2SDK.Annotations;

using JetBrains.Annotations;

namespace GW2SDK.V2;

[PublicAPI]
[DataTransferObject]
public sealed record ApiInfo
{
    public IReadOnlyCollection<string> Languages { get; init; } = Array.Empty<string>();

    public IReadOnlyCollection<ApiRoute> Routes { get; init; } = Array.Empty<ApiRoute>();

    public IReadOnlyCollection<ApiVersion> SchemaVersions { get; init; } = Array.Empty<ApiVersion>();
}
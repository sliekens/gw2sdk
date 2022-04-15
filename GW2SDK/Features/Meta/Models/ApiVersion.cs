using System;
using System.Collections.Generic;
using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Meta.Models;

[PublicAPI]
[DataTransferObject]
public sealed record ApiVersion
{
    public IReadOnlyCollection<string> Languages { get; init; } = Array.Empty<string>();

    public IReadOnlyCollection<Route> Routes { get; init; } = Array.Empty<Route>();

    public IReadOnlyCollection<Schema> SchemaVersions { get; init; } = Array.Empty<Schema>();
}

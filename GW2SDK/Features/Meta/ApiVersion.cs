using System;
using System.Collections.Generic;
using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Meta;

[PublicAPI]
[DataTransferObject]
public sealed record ApiVersion
{
    public required IReadOnlyCollection<string> Languages { get; init; }

    public required IReadOnlyCollection<Route> Routes { get; init; }

    public required IReadOnlyCollection<Schema> SchemaVersions { get; init; }
}

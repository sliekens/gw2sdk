using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace GW2SDK.Banking;

[PublicAPI]
public sealed record MaterialCategory
{
    public required int Id { get; init; }

    public required string Name { get; init; }

    public required IEnumerable<int> Items { get; init; }

    public required int Order { get; init; }
}

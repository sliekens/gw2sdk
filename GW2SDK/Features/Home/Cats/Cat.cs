using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Home.Cats;

[PublicAPI]
[DataTransferObject]
public sealed record Cat
{
    public required int Id { get; init; }

    public required string Hint { get; init; }
}

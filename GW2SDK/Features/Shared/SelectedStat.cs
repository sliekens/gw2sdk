using GW2SDK.Annotations;

using JetBrains.Annotations;

namespace GW2SDK;

/// <summary>A reference to a named set of item attributes, used for customizable items such as legendary weapons. The Id
/// can be used to retrieve more information.</summary>
[PublicAPI]
[DataTransferObject]
public sealed record SelectedStat
{
    public int Id { get; init; }

    public SelectedModification Attributes { get; init; } = new();
}
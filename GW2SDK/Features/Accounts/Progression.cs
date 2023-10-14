namespace GW2SDK.Features.Accounts;

[PublicAPI]
public sealed record Progression
{
    /// <summary>
    /// The progression kind. See <see cref="ProgressionKind"/> for known values.
    /// </summary>
    public required string Id { get; init; }

    /// <summary>
    /// The amount of progression.
    /// </summary>
    public required int Value { get; init; }
}
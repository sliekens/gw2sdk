namespace GuildWars2.BuildStorage;

[PublicAPI]
[DataTransferObject]
public sealed record Specialization
{
    public required int? Id { get; init; }

    // Always length 3
    public required IReadOnlyCollection<int?> Traits { get; init; }
}

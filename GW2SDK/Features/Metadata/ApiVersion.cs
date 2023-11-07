namespace GuildWars2.Metadata;

[PublicAPI]
[DataTransferObject]
public sealed record ApiVersion
{
    public required IReadOnlyCollection<string> Languages { get; init; }

    public required IReadOnlyCollection<Route> Routes { get; init; }

    public required IReadOnlyCollection<Schema> SchemaVersions { get; init; }
}

namespace GuildWars2.Meta;

[PublicAPI]
[DataTransferObject]
public sealed record ApiVersion
{
    public required IReadOnlyCollection<string> Languages { get; init; }

    public required IReadOnlyCollection<Route> Routes { get; init; }

    public required IReadOnlyCollection<Schema> SchemaVersions { get; init; }
}

namespace GuildWars2.Traits;

[PublicAPI]
[DataTransferObject]
public sealed record ComboFieldTraitFact : TraitFact
{
    public required ComboFieldName Field { get; init; }
}

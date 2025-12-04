namespace GuildWars2.Items;

/// <summary>Information about a container item. This type is the base type for all container items. Cast objects of this
/// type to a more specific type to access more properties.</summary>
[Inheritable]
public record Container : Item;

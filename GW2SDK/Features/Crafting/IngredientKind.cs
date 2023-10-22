using System.ComponentModel;

namespace GuildWars2.Crafting;

[PublicAPI]
[DefaultValue(Item)]
public enum IngredientKind
{
    Item,

    Currency,

    GuildUpgrade
}

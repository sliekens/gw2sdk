using System.ComponentModel;

namespace GuildWars2.Hero.Crafting;

[PublicAPI]
[DefaultValue(Item)]
public enum IngredientKind
{
    Item,

    Currency,

    GuildUpgrade
}

using System.ComponentModel;
using JetBrains.Annotations;

namespace GuildWars2.Crafting;

[PublicAPI]
[DefaultValue(Item)]
public enum IngredientKind
{
    Item,

    Currency,

    GuildUpgrade
}

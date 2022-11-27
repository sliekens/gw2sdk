using System.ComponentModel;
using JetBrains.Annotations;

namespace GW2SDK.Crafting;

[PublicAPI]
[DefaultValue(Item)]
public enum IngredientKind
{
    Item,

    Currency,

    GuildUpgrade
}

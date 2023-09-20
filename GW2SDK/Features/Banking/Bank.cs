using System.Collections.ObjectModel;
using GuildWars2.Inventories;

namespace GuildWars2.Banking;

/// <summary>The current account's bank, sorted by in-game order. Enumerated values can contain <c>null</c> when some item
/// slots are empty.</summary>
[PublicAPI]
public sealed class Bank : ReadOnlyCollection<ItemSlot?>
{
    public Bank(IList<ItemSlot?> list)
        : base(list)
    {
    }
}

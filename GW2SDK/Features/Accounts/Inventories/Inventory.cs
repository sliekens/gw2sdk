using System.Collections.Generic;
using System.Collections.ObjectModel;
using JetBrains.Annotations;

namespace GW2SDK.Accounts.Inventories;

/// <summary>An inventory, sorted by in-game order. Enumerated values can contain <c>null</c> when some item slots are
/// empty.</summary>
[PublicAPI]
public sealed class Inventory : ReadOnlyCollection<ItemSlot?>
{
    public Inventory(IList<ItemSlot?> list)
        : base(list)
    {
    }
}

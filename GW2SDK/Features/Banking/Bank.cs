using System.Collections.Generic;
using System.Collections.ObjectModel;
using GW2SDK.Inventories;
using JetBrains.Annotations;

namespace GW2SDK.Banking;

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

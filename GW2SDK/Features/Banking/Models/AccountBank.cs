using System.Collections.Generic;
using System.Collections.ObjectModel;
using JetBrains.Annotations;

namespace GW2SDK.Banking.Models;

/// <summary>The current account's bank, sorted by in-game order. Enumerated values can contain <c>null</c> when some item
/// slots are empty.</summary>
[PublicAPI]
public sealed class AccountBank : ReadOnlyCollection<BankSlot?>
{
    public AccountBank(IList<BankSlot?> list)
        : base(list)
    {
    }
}

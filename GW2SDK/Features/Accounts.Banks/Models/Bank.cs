﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using JetBrains.Annotations;

namespace GW2SDK.Accounts.Banks
{
    /// <summary>The current account's bank, sorted by in-game order. Enumerated values can contain <c>null</c> when some item
    /// slots are empty.</summary>
    [PublicAPI]
    public sealed class Bank : ReadOnlyCollection<BankSlot?>
    {
        public Bank(IList<BankSlot?> list)
            : base(list)
        {
        }
    }
}

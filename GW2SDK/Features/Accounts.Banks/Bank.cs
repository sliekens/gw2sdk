using System.Collections.Generic;
using System.Collections.ObjectModel;
using GW2SDK.Annotations;

namespace GW2SDK.Accounts.Banks
{
    [PublicAPI]
    public sealed class Bank : ReadOnlyCollection<BankSlot?>
    {
        public Bank(IList<BankSlot?> list)
            : base(list)
        {
        }
    }
}

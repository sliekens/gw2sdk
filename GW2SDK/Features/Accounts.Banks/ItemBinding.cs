using System.ComponentModel;
using GW2SDK.Annotations;

namespace GW2SDK.Accounts.Banks
{
    [PublicAPI]
    [DefaultValue(None)]
    public enum ItemBinding
    {
        None,

        Account,

        Character
    }
}

using System.ComponentModel;
using GW2SDK.Annotations;

namespace GW2SDK
{
    [PublicAPI]
    [DefaultValue(Today)]
    public enum Day
    {
        Today,

        Tomorrow
    }
}

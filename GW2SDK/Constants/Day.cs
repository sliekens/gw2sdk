using System.ComponentModel;
using JetBrains.Annotations;

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

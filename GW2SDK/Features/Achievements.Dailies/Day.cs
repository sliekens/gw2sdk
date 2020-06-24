using System.ComponentModel;
using GW2SDK.Annotations;

namespace GW2SDK.Achievements.Dailies
{
    [PublicAPI]
    [DefaultValue(Today)]
    public enum Day
    {
        Today,

        Tomorrow
    }
}

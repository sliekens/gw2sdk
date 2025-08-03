using System.Text.Json;

using GuildWars2.Json;

namespace GuildWars2.Wvw.Timers;

internal static class WvwTimerJson
{
    public static WvwTimer GetWvwTimer(this in JsonElement json)
    {
        RequiredMember na = "na";
        RequiredMember eu = "eu";

        foreach (JsonProperty member in json.EnumerateObject())
        {
            if (na.Match(member))
            {
                na = member;
            }
            else if (eu.Match(member))
            {
                eu = member;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new WvwTimer
        {
            NorthAmerica = na.Map(static (in JsonElement value) => value.GetDateTimeOffset()),
            Europe = eu.Map(static (in JsonElement value) => value.GetDateTimeOffset())
        };
    }
}

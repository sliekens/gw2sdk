using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Hero.Inventories;

internal static class BaggageJson
{
    public static Baggage GetBaggage(this JsonElement json)
    {
        RequiredMember bags = "bags";

        foreach (var member in json.EnumerateObject())
        {
            if (bags.Match(member))
            {
                bags = member;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new Baggage
        {
            Bags = bags.Map(static values => values.GetList(static value => value.GetBag()))
        };
    }
}

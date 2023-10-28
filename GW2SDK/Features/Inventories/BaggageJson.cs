using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Inventories;

internal static class BaggageJson
{
    public static Baggage GetBaggage(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember bags = "bags";

        foreach (var member in json.EnumerateObject())
        {
            if (member.Name == bags.Name)
            {
                bags = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Baggage
        {
            Bags = bags.Map(
                values => values.GetList(value => value.GetBag(missingMemberBehavior))
            )
        };
    }
}

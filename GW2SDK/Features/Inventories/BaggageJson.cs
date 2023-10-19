using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Inventories;

[PublicAPI]
public static class BaggageJson
{
    public static Baggage GetBaggage(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember bags = new("bags");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(bags.Name))
            {
                bags.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Baggage { Bags = bags.SelectMany(value => value.GetBag(missingMemberBehavior)) };
    }
}

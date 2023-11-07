using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Hero.Finishers;

internal static class UnlockedFinisherJson
{
    public static UnlockedFinisher GetUnlockedFinisher(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember id = "id";
        RequiredMember permanent = "permanent";
        NullableMember quantity = "quantity";

        foreach (var member in json.EnumerateObject())
        {
            if (member.Name == id.Name)
            {
                id = member;
            }
            else if (member.Name == permanent.Name)
            {
                permanent = member;
            }
            else if (member.Name == quantity.Name)
            {
                quantity = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new UnlockedFinisher
        {
            Id = id.Map(value => value.GetInt32()),
            Permanent = permanent.Map(value => value.GetBoolean()),
            Quantity = quantity.Map(value => value.GetInt32())
        };
    }
}

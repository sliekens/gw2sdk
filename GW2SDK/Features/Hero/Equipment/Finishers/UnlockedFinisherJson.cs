using System.Text.Json;

using GuildWars2.Json;

namespace GuildWars2.Hero.Equipment.Finishers;

internal static class UnlockedFinisherJson
{
    public static UnlockedFinisher GetUnlockedFinisher(this in JsonElement json)
    {
        RequiredMember id = "id";
        RequiredMember permanent = "permanent";
        NullableMember quantity = "quantity";

        foreach (JsonProperty member in json.EnumerateObject())
        {
            if (id.Match(member))
            {
                id = member;
            }
            else if (permanent.Match(member))
            {
                permanent = member;
            }
            else if (quantity.Match(member))
            {
                quantity = member;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new UnlockedFinisher
        {
            Id = id.Map(static (in JsonElement value) => value.GetInt32()),
            Permanent = permanent.Map(static (in JsonElement value) => value.GetBoolean()),
            Quantity = quantity.Map(static (in JsonElement value) => value.GetInt32())
        };
    }
}

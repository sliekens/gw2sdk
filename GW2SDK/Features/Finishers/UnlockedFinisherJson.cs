using System;
using System.Text.Json;
using GuildWars2.Json;
using JetBrains.Annotations;

namespace GuildWars2.Finishers;

[PublicAPI]
public static class UnlockedFinisherJson
{
    public static UnlockedFinisher GetUnlockedFinisher(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<int> id = new("id");
        RequiredMember<bool> permanent = new("permanent");
        NullableMember<int> quantity = new("quantity");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(id.Name))
            {
                id.Value = member.Value;
            }
            else if (member.NameEquals(permanent.Name))
            {
                permanent.Value = member.Value;
            }
            else if (member.NameEquals(quantity.Name))
            {
                quantity.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new UnlockedFinisher
        {
            Id = id.GetValue(),
            Permanent = permanent.GetValue(),
            Quantity = quantity.GetValue()
        };
    }
}

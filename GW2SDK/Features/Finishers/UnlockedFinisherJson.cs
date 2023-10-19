﻿using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Finishers;

[PublicAPI]
public static class UnlockedFinisherJson
{
    public static UnlockedFinisher GetUnlockedFinisher(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember id = new("id");
        RequiredMember permanent = new("permanent");
        NullableMember quantity = new("quantity");

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
            Id = id.Select(value => value.GetInt32()),
            Permanent = permanent.Select(value => value.GetBoolean()),
            Quantity = quantity.Select(value => value.GetInt32())
        };
    }
}

﻿using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Guilds.Upgrades;

internal static class GuildUpgradeCostJson
{
    public static GuildUpgradeCost GetGuildUpgradeCost(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember kind = "type";
        OptionalMember name = "name";
        RequiredMember count = "count";
        NullableMember itemId = "item_id";

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(kind.Name))
            {
                kind = member;
            }
            else if (member.NameEquals(name.Name))
            {
                name = member;
            }
            else if (member.NameEquals(count.Name))
            {
                count = member;
            }
            else if (member.NameEquals(itemId.Name))
            {
                itemId = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new GuildUpgradeCost
        {
            Kind =
                kind.Map(value => value.GetEnum<GuildUpgradeCostKind>(missingMemberBehavior)),
            Name = name.Map(value => value.GetString()) ?? "",
            Count = count.Map(value => value.GetInt32()),
            ItemId = itemId.Map(value => value.GetInt32())
        };
    }
}
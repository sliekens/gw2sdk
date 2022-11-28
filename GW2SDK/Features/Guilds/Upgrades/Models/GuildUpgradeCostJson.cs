using System;
using System.Text.Json;
using GuildWars2.Json;
using JetBrains.Annotations;

namespace GuildWars2.Guilds.Upgrades;

[PublicAPI]
public static class GuildUpgradeCostJson
{
    public static GuildUpgradeCost GetGuildUpgradeCost(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<GuildUpgradeCostKind> kind = new("type");
        OptionalMember<string> name = new("name");
        RequiredMember<int> count = new("count");
        NullableMember<int> itemId = new("item_id");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(kind.Name))
            {
                kind.Value = member.Value;
            }
            else if (member.NameEquals(name.Name))
            {
                name.Value = member.Value;
            }
            else if (member.NameEquals(count.Name))
            {
                count.Value = member.Value;
            }
            else if (member.NameEquals(itemId.Name))
            {
                itemId.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new GuildUpgradeCost
        {
            Kind = kind.GetValue(missingMemberBehavior),
            Name = name.GetValueOrEmpty(),
            Count = count.GetValue(),
            ItemId = itemId.GetValue()
        };
    }
}

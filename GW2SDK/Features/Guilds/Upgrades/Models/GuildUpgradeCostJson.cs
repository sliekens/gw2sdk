using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Guilds.Upgrades;

[PublicAPI]
public static class GuildUpgradeCostJson
{
    public static GuildUpgradeCost GetGuildUpgradeCost(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember kind = new("type");
        OptionalMember name = new("name");
        RequiredMember count = new("count");
        NullableMember itemId = new("item_id");

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
            Kind = kind.Select(value => value.GetEnum<GuildUpgradeCostKind>(missingMemberBehavior)),
            Name = name.Select(value => value.GetString()) ?? "",
            Count = count.Select(value => value.GetInt32()),
            ItemId = itemId.Select(value => value.GetInt32())
        };
    }
}

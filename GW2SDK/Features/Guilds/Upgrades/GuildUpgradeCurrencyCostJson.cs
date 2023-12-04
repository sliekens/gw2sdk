using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Guilds.Upgrades;

internal static class GuildUpgradeCurrencyCostJson
{
    public static GuildUpgradeCurrencyCost GetGuildUpgradeCurrencyCost(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember name = "name";
        RequiredMember count = "count";

        foreach (var member in json.EnumerateObject())
        {
            if (member.Name == "type")
            {
                if (!member.Value.ValueEquals("Currency"))
                {
                    throw new InvalidOperationException(
                        Strings.InvalidDiscriminator(member.Value.GetString())
                    );
                }
            }
            else if (member.Name == name.Name)
            {
                name = member;
            }
            else if (member.Name == count.Name)
            {
                count = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new GuildUpgradeCurrencyCost
        {
            Name = name.Map(value => value.GetString()) ?? "",
            Count = count.Map(value => value.GetInt32())
        };
    }
}

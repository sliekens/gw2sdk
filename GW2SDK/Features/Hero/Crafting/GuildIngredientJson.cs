using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Hero.Crafting;

internal static class GuildIngredientJson
{
    public static GuildIngredient GetGuildIngredient(this JsonElement json)
    {
        RequiredMember upgradeId = "upgrade_id";
        RequiredMember count = "count";
        foreach (var member in json.EnumerateObject())
        {
            if (upgradeId.Match(member))
            {
                upgradeId = member;
            }
            else if (count.Match(member))
            {
                count = member;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new GuildIngredient
        {
            UpgradeId = upgradeId.Map(static value => value.GetInt32()),
            Count = count.Map(static value => value.GetInt32())
        };
    }
}

using System.Text.Json;

using GuildWars2.Json;

namespace GuildWars2.Hero.Crafting;

internal static class GuildIngredientJson
{
    public static GuildIngredient GetGuildIngredient(this in JsonElement json)
    {
        RequiredMember upgradeId = "upgrade_id";
        RequiredMember count = "count";
        foreach (JsonProperty member in json.EnumerateObject())
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
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new GuildIngredient
        {
            UpgradeId = upgradeId.Map(static (in JsonElement value) => value.GetInt32()),
            Count = count.Map(static (in JsonElement value) => value.GetInt32())
        };
    }
}

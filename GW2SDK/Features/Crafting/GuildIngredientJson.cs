using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Crafting;

internal static class GuildIngredientJson
{
    public static GuildIngredient GetGuildIngredient(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember upgradeId = "upgrade_id";
        RequiredMember count = "count";
        foreach (var member in json.EnumerateObject())
        {
            if (member.Name == upgradeId.Name)
            {
                upgradeId = member;
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

        return new GuildIngredient
        {
            UpgradeId = upgradeId.Map(value => value.GetInt32()),
            Count = count.Map(value => value.GetInt32())
        };
    }
}

using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Guilds.Logs;

internal static class GuildUpgradeActivityJson
{
    public static GuildUpgradeActivity GetGuildUpgradeActivity(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember id = "id";
        RequiredMember time = "time";
        OptionalMember user = "user";
        RequiredMember action = "action";
        RequiredMember upgradeId = "upgrade_id";
        NullableMember recipeId = "recipe_id";
        NullableMember itemId = "item_id";
        NullableMember count = "count";

        foreach (var member in json.EnumerateObject())
        {
            if (member.Name == "type")
            {
                if (!member.Value.ValueEquals("upgrade"))
                {
                    throw new InvalidOperationException(
                        Strings.InvalidDiscriminator(member.Value.GetString())
                    );
                }
            }
            else if (id.Match(member))
            {
                id = member;
            }
            else if (time.Match(member))
            {
                time = member;
            }
            else if (user.Match(member))
            {
                user = member;
            }
            else if (action.Match(member))
            {
                action = member;
            }
            else if (upgradeId.Match(member))
            {
                upgradeId = member;
            }
            else if (recipeId.Match(member))
            {
                recipeId = member;
            }
            else if (itemId.Match(member))
            {
                itemId = member;
            }
            else if (count.Match(member))
            {
                count = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new GuildUpgradeActivity
        {
            Id = id.Map(value => value.GetInt32()),
            Time = time.Map(value => value.GetDateTimeOffset()),
            User = user.Map(value => value.GetString()) ?? "",
            Action = action.Map(value => value.GetEnum<GuildUpgradeAction>(missingMemberBehavior)),
            UpgradeId = upgradeId.Map(value => value.GetInt32()),
            RecipeId = recipeId.Map(value => value.GetInt32()),
            ItemId = itemId.Map(value => value.GetInt32()),
            Count = count.Map(value => value.GetInt32())
        };
    }
}

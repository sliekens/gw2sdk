﻿using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Guilds.Logs;

internal static class GuildUpgradeActivityJson
{
    public static GuildUpgradeActivity GetGuildUpgradeActivity(this in JsonElement json)
    {
        RequiredMember id = "id";
        RequiredMember time = "time";
        OptionalMember user = "user";
        RequiredMember action = "action";
        OptionalMember upgradeId = "upgrade_id";
        NullableMember recipeId = "recipe_id";
        NullableMember itemId = "item_id";
        NullableMember count = "count";

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("upgrade"))
                {
                    ThrowHelper.ThrowInvalidDiscriminator(member.Value.GetString());
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
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new GuildUpgradeActivity
        {
            Id = id.Map(static (in JsonElement value) => value.GetInt32()),
            Time = time.Map(static (in JsonElement value) => value.GetDateTimeOffset()),
            User = user.Map(static (in JsonElement value) => value.GetString()) ?? "",
            Action =
                action.Map(static (in JsonElement value) => value.ValueEquals("complete")
                    ? GuildUpgradeAction.Completed
                    : value.GetEnum<GuildUpgradeAction>()
                ),
            UpgradeId = upgradeId.Map(static (in JsonElement value) => value.GetInt32()),
            RecipeId = recipeId.Map(static (in JsonElement value) => value.GetInt32()),
            ItemId = itemId.Map(static (in JsonElement value) => value.GetInt32()),
            Count = count.Map(static (in JsonElement value) => value.GetInt32())
        };
    }
}

using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Guilds.Logs;

[PublicAPI]
public static class GuildUpgradeActivityJson
{
    public static GuildUpgradeActivity GetGuildUpgradeActivity(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember id = new("id");
        RequiredMember time = new("time");
        OptionalMember user = new("user");
        RequiredMember action = new("action");
        RequiredMember upgradeId = new("upgrade_id");
        NullableMember recipeId = new("recipe_id");
        NullableMember itemId = new("item_id");
        NullableMember count = new("count");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("upgrade"))
                {
                    throw new InvalidOperationException(
                        Strings.InvalidDiscriminator(member.Value.GetString())
                    );
                }
            }
            else if (member.NameEquals(id.Name))
            {
                id.Value = member.Value;
            }
            else if (member.NameEquals(time.Name))
            {
                time.Value = member.Value;
            }
            else if (member.NameEquals(user.Name))
            {
                user.Value = member.Value;
            }
            else if (member.NameEquals(action.Name))
            {
                action.Value = member.Value;
            }
            else if (member.NameEquals(upgradeId.Name))
            {
				upgradeId.Value = member.Value;
            }
            else if (member.NameEquals(recipeId.Name))
            {
                recipeId.Value = member.Value;
            }
            else if (member.NameEquals(itemId.Name))
            {
                itemId.Value = member.Value;
            }
            else if (member.NameEquals(count.Name))
            {
                count.Value = member.Value;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new GuildUpgradeActivity
        {
            Id = id.Select(value => value.GetInt32()),
            Time = time.Select(value => value.GetDateTimeOffset()),
            User = user.Select(value => value.GetString()) ?? "",
            Action = action.Select(value => value.GetEnum<GuildUpgradeAction>(missingMemberBehavior)),
            UpgradeId = upgradeId.Select(value => value.GetInt32()),
            RecipeId = recipeId.Select(value => value.GetInt32()),
            ItemId = itemId.Select(value => value.GetInt32()),
            Count = count.Select(value => value.GetInt32())
        };
    }
}

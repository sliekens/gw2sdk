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
        RequiredMember<int> id = new("id");
        RequiredMember<DateTimeOffset> time = new("time");
        OptionalMember<string> user = new("user");
        RequiredMember<GuildUpgradeAction> action = new("action");
        RequiredMember<int> upgradeId = new("upgrade_id");
        NullableMember<int> recipeId = new("recipe_id");
        NullableMember<int> itemId = new("item_id");
        NullableMember<int> count = new("count");

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
            Id = id.GetValue(),
            Time = time.GetValue(),
            User = user.GetValueOrEmpty(),
            Action = action.GetValue(missingMemberBehavior),
            UpgradeId = upgradeId.GetValue(),
            RecipeId = recipeId.GetValue(),
            ItemId = itemId.GetValue(),
            Count = count.GetValue()
        };
    }
}

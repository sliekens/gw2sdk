using System.Text.Json;

using GuildWars2.Json;

namespace GuildWars2.Hero.Achievements.Rewards;

internal static class ItemRewardJson
{
    public static ItemReward GetItemReward(this in JsonElement json)
    {
        RequiredMember id = "id";
        RequiredMember count = "count";
        foreach (JsonProperty member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("Item"))
                {
                    ThrowHelper.ThrowInvalidDiscriminator(member.Value.GetString());
                }
            }
            else if (id.Match(member))
            {
                id = member;
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

        return new ItemReward
        {
            Id = id.Map(static (in value) => value.GetInt32()),
            Count = count.Map(static (in value) => value.GetInt32())
        };
    }
}

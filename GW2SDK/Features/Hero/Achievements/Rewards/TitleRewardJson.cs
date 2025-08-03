using System.Text.Json;

using GuildWars2.Json;

namespace GuildWars2.Hero.Achievements.Rewards;

internal static class TitleRewardJson
{
    public static TitleReward GetTitleReward(this in JsonElement json)
    {
        RequiredMember id = "id";
        foreach (JsonProperty member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("Title"))
                {
                    ThrowHelper.ThrowInvalidDiscriminator(member.Value.GetString());
                }
            }
            else if (id.Match(member))
            {
                id = member;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new TitleReward { Id = id.Map(static (in JsonElement value) => value.GetInt32()) };
    }
}

using System.Text.Json;

using GuildWars2.Json;

namespace GuildWars2.Hero.Achievements.Bits;

internal static class AchievementMiniatureBitJson
{
    public static AchievementMiniatureBit GetAchievementMiniatureBit(this in JsonElement json)
    {
        RequiredMember id = "id";
        OptionalMember text = "text";
        foreach (JsonProperty member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("Minipet"))
                {
                    ThrowHelper.ThrowInvalidDiscriminator(member.Value.GetString());
                }
            }
            else if (id.Match(member))
            {
                id = member;
            }
            else if (text.Match(member))
            {
                text = member;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new AchievementMiniatureBit
        {
            Id = id.Map(static (in value) => value.GetInt32()),
            Text = text.Map(static (in value) => value.GetString()) ?? ""
        };
    }
}

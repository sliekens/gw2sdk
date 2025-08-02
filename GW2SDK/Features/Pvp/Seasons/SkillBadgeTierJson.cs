using System.Text.Json;

using GuildWars2.Json;

namespace GuildWars2.Pvp.Seasons;

internal static class SkillBadgeTierJson
{
    public static SkillBadgeTier GetSkillBadgeTier(this in JsonElement json)
    {
        RequiredMember rating = "rating";

        foreach (var member in json.EnumerateObject())
        {
            if (rating.Match(member))
            {
                rating = member;
            }
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new SkillBadgeTier { Rating = rating.Map(static (in JsonElement value) => value.GetInt32()) };
    }
}

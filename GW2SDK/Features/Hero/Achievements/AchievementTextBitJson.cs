using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Hero.Achievements;

internal static class AchievementTextBitJson
{
    public static AchievementTextBit GetAchievementTextBit(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember text = "text";
        foreach (var member in json.EnumerateObject())
        {
            if (member.Name == "type")
            {
                if (!member.Value.ValueEquals("Text"))
                {
                    throw new InvalidOperationException(
                        Strings.InvalidDiscriminator(member.Value.GetString())
                    );
                }
            }
            else if (member.Name == text.Name)
            {
                text = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new AchievementTextBit { Text = text.Map(value => value.GetStringRequired()) };
    }
}

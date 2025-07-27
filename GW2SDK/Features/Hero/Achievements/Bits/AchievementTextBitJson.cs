using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Hero.Achievements.Bits;

internal static class AchievementTextBitJson
{
    public static AchievementTextBit GetAchievementTextBit(this in JsonElement json)
    {
        RequiredMember text = "text";
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("Text"))
                {
                    ThrowHelper.ThrowInvalidDiscriminator(member.Value.GetString());
                }
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

        return new AchievementTextBit
        {
            Text = text.Map(static (in JsonElement value) => value.GetStringRequired())
        };
    }
}

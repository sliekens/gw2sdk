using System.Text.Json;
using GuildWars2.Hero.Achievements.Bits;

namespace GuildWars2.Hero.Achievements;

internal static class AchievementBitJson
{
    public static AchievementBit GetAchievementBit(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        // BUG: some achievement bits don't have a type property, see https://github.com/arenanet/api-cdi/issues/670
        // Hopefully this will get fixed and then TryGetProperty can be replaced by GetProperty
        if (json.TryGetProperty("type", out var type))
        {
            switch (type.GetString())
            {
                case "Text":
                    return json.GetAchievementTextBit(missingMemberBehavior);
                case "Minipet":
                    return json.GetAchievementMiniatureBit(missingMemberBehavior);
                case "Item":
                    return json.GetAchievementItemBit(missingMemberBehavior);
                case "Skin":
                    return json.GetAchievementSkinBit(missingMemberBehavior);
            }
        }

        // ReSharper disable once ForeachCanBePartlyConvertedToQueryUsingAnotherGetEnumerator
        foreach (var member in json.EnumerateObject())
        {
            if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new AchievementBit();
    }
}

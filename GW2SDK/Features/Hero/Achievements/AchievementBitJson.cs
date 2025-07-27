using System.Text.Json;
using GuildWars2.Hero.Achievements.Bits;
using GuildWars2.Json;

namespace GuildWars2.Hero.Achievements;

internal static class AchievementBitJson
{
    public static AchievementBit GetAchievementBit(this in JsonElement json)
    {
        // BUG: some achievement bits don't have a type property, see https://github.com/arenanet/api-cdi/issues/670
        // Hopefully this will get fixed and then TryGetProperty can be replaced by GetProperty
        if (json.TryGetProperty("type", out var type))
        {
            switch (type.GetString())
            {
                case "Text":
                    return json.GetAchievementTextBit();
                case "Minipet":
                    return json.GetAchievementMiniatureBit();
                case "Item":
                    return json.GetAchievementItemBit();
                case "Skin":
                    return json.GetAchievementSkinBit();
            }
        }

        // ReSharper disable once ForeachCanBePartlyConvertedToQueryUsingAnotherGetEnumerator
        foreach (var member in json.EnumerateObject())
        {
            if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new AchievementBit();
    }
}

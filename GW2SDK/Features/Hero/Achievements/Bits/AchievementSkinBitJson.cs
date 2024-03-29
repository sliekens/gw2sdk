﻿using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Hero.Achievements.Bits;

internal static class AchievementSkinBitJson
{
    public static AchievementSkinBit GetAchievementSkinBit(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember id = "id";
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("Skin"))
                {
                    throw new InvalidOperationException(
                        Strings.InvalidDiscriminator(member.Value.GetString())
                    );
                }
            }
            else if (id.Match(member))
            {
                id = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new AchievementSkinBit { Id = id.Map(value => value.GetInt32()) };
    }
}

﻿using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Hero.Achievements.Bits;

internal static class AchievementMinipetBitJson
{
    public static AchievementMinipetBit GetAchievementMinipetBit(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember id = "id";
        foreach (var member in json.EnumerateObject())
        {
            if (member.Name == "type")
            {
                if (!member.Value.ValueEquals("Minipet"))
                {
                    throw new InvalidOperationException(
                        Strings.InvalidDiscriminator(member.Value.GetString())
                    );
                }
            }
            else if (member.Name == id.Name)
            {
                id = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new AchievementMinipetBit { Id = id.Map(value => value.GetInt32()) };
    }
}
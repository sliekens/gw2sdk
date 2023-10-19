﻿using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Achievements.Categories;

[PublicAPI]
public static class AchievementRefJson
{
    public static AchievementRef GetAchievementRef(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember id = "id";
        OptionalMember requiredAccess = "required_access";
        OptionalMember flags = "flags";
        OptionalMember level = "level";

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(id.Name))
            {
                id = member;
            }
            else if (member.NameEquals(requiredAccess.Name))
            {
                requiredAccess = member;
            }
            else if (member.NameEquals(flags.Name))
            {
                flags = member;
            }
            else if (member.NameEquals(level.Name))
            {
                level = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new AchievementRef
        {
            Id = id.Select(value => value.GetInt32()),
            RequiredAccess =
                requiredAccess.Select(value => value.GetProductRequirement(missingMemberBehavior)),
            Flags = flags.SelectMany(value => value.GetEnum<AchievementFlag>(missingMemberBehavior)),
            Level = level.Select(value => value.GetLevelRequirement(missingMemberBehavior))
        };
    }
}

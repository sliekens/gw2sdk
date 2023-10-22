﻿using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Masteries;

internal static class MasteryJson
{
    public static Mastery GetMastery(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember id = "id";
        RequiredMember name = "name";
        RequiredMember requirement = "requirement";
        RequiredMember order = "order";
        RequiredMember background = "background";
        RequiredMember region = "region";
        RequiredMember levels = "levels";

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(id.Name))
            {
                id = member;
            }
            else if (member.NameEquals(name.Name))
            {
                name = member;
            }
            else if (member.NameEquals(requirement.Name))
            {
                requirement = member;
            }
            else if (member.NameEquals(order.Name))
            {
                order = member;
            }
            else if (member.NameEquals(background.Name))
            {
                background = member;
            }
            else if (member.NameEquals(region.Name))
            {
                region = member;
            }
            else if (member.NameEquals(levels.Name))
            {
                levels = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Mastery
        {
            Id = id.Map(value => value.GetInt32()),
            Name = name.Map(value => value.GetStringRequired()),
            Requirement = requirement.Map(value => value.GetStringRequired()),
            Order = order.Map(value => value.GetInt32()),
            Background = background.Map(value => value.GetStringRequired()),
            Region = region.Map(value => value.GetEnum<MasteryRegionName>(missingMemberBehavior)),
            Levels = levels.Map(
                values => values.GetList(value => value.GetMasteryLevel(missingMemberBehavior))
            )
        };
    }
}

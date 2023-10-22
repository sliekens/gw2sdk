﻿using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Crafting;

internal static class LearnedCraftingDisciplinesJson
{
    public static LearnedCraftingDisciplines GetLearnedCraftingDisciplines(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember crafting = "crafting";

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(crafting.Name))
            {
                crafting = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new LearnedCraftingDisciplines
        {
            Disciplines = crafting.Map(
                values => values.GetList(
                    value => value.GetCraftingDiscipline(missingMemberBehavior)
                )
            )
        };
    }
}
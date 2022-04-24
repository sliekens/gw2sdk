﻿using System;
using System.Collections.Generic;
using System.Text.Json;
using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Accounts.Characters.Crafting;

[PublicAPI]
public static class LearnedRecipesReader
{
    public static HashSet<int> GetLearnedRecipes(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<int> recipes = new("recipes");

        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(recipes.Name))
            {
                recipes = recipes.From(member.Value);
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new HashSet<int>(recipes.SelectMany(value => value.GetInt32()));
    }
}
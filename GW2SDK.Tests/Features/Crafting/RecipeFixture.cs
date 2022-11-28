﻿using System.Collections.Generic;
using System.Linq;
using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Crafting;

// ReSharper disable once ClassNeverInstantiated.Global
public class RecipeFixture
{
    public RecipeFixture()
    {
        Recipes = FlatFileReader.Read("Data/recipes.json.gz").ToList().AsReadOnly();
    }

    public IReadOnlyCollection<string> Recipes { get; }
}

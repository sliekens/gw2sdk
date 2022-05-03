using System;
using System.Text.Json;
using GW2SDK.Json;
using JetBrains.Annotations;

namespace GW2SDK.Crafting;

[PublicAPI]
public static class RecipeReader
{
    public static Recipe GetRecipe(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        switch (json.GetProperty("type").GetString())
        {
            case "Amulet":
                return ReadAmuletRecipe(json, missingMemberBehavior);
            case "Axe":
                return ReadAxeRecipe(json, missingMemberBehavior);
            case "Backpack":
                return ReadBackpackRecipe(json, missingMemberBehavior);
            case "Bag":
                return ReadBagRecipe(json, missingMemberBehavior);
            case "Boots":
                return ReadBootsRecipe(json, missingMemberBehavior);
            case "Bulk":
                return ReadBulkRecipe(json, missingMemberBehavior);
            case "Coat":
                return ReadCoatRecipe(json, missingMemberBehavior);
            case "Component":
                return ReadComponentRecipe(json, missingMemberBehavior);
            case "Consumable":
                return ReadConsumableRecipe(json, missingMemberBehavior);
            case "Dagger":
                return ReadDaggerRecipe(json, missingMemberBehavior);
            case "Dessert":
                return ReadDessertRecipe(json, missingMemberBehavior);
            case "Dye":
                return ReadDyeRecipe(json, missingMemberBehavior);
            case "Earring":
                return ReadEarringRecipe(json, missingMemberBehavior);
            case "Feast":
                return ReadFeastRecipe(json, missingMemberBehavior);
            case "Focus":
                return ReadFocusRecipe(json, missingMemberBehavior);
            case "Food":
                return ReadFoodRecipe(json, missingMemberBehavior);
            case "Gloves":
                return ReadGlovesRecipe(json, missingMemberBehavior);
            case "Greatsword":
                return ReadGreatswordRecipe(json, missingMemberBehavior);
            case "GuildConsumable":
                return ReadGuildConsumableRecipe(json, missingMemberBehavior);
            case "GuildConsumableWvw":
                return ReadGuildConsumableWvwRecipe(json, missingMemberBehavior);
            case "GuildDecoration":
                return ReadGuildDecorationRecipe(json, missingMemberBehavior);
            case "Hammer":
                return ReadHammerRecipe(json, missingMemberBehavior);
            case "Harpoon":
                return ReadSpearRecipe(json, missingMemberBehavior);
            case "Helm":
                return ReadHelmRecipe(json, missingMemberBehavior);
            case "IngredientCooking":
                return ReadIngredientCookingRecipe(json, missingMemberBehavior);
            case "Inscription":
                return ReadInscriptionRecipe(json, missingMemberBehavior);
            case "Insignia":
                return ReadInsigniaRecipe(json, missingMemberBehavior);
            case "LegendaryComponent":
                return ReadLegendaryComponentRecipe(json, missingMemberBehavior);
            case "Leggings":
                return ReadLeggingsRecipe(json, missingMemberBehavior);
            case "LongBow":
                return ReadLongbowRecipe(json, missingMemberBehavior);
            case "Mace":
                return ReadMaceRecipe(json, missingMemberBehavior);
            case "Meal":
                return ReadMealRecipe(json, missingMemberBehavior);
            case "Pistol":
                return ReadPistolRecipe(json, missingMemberBehavior);
            case "Potion":
                return ReadPotionRecipe(json, missingMemberBehavior);
            case "Refinement":
                return ReadRefinementRecipe(json, missingMemberBehavior);
            case "RefinementEctoplasm":
                return ReadRefinementEctoplasmRecipe(json, missingMemberBehavior);
            case "RefinementObsidian":
                return ReadRefinementObsidianRecipe(json, missingMemberBehavior);
            case "Rifle":
                return ReadRifleRecipe(json, missingMemberBehavior);
            case "Ring":
                return ReadRingRecipe(json, missingMemberBehavior);
            case "Scepter":
                return ReadScepterRecipe(json, missingMemberBehavior);
            case "Seasoning":
                return ReadSeasoningRecipe(json, missingMemberBehavior);
            case "Shield":
                return ReadShieldRecipe(json, missingMemberBehavior);
            case "ShortBow":
                return ReadShortbowRecipe(json, missingMemberBehavior);
            case "Shoulders":
                return ReadShouldersRecipe(json, missingMemberBehavior);
            case "Snack":
                return ReadSnackRecipe(json, missingMemberBehavior);
            case "Soup":
                return ReadSoupRecipe(json, missingMemberBehavior);
            case "Speargun":
                return ReadHarpoonGunRecipe(json, missingMemberBehavior);
            case "Staff":
                return ReadStaffRecipe(json, missingMemberBehavior);
            case "Sword":
                return ReadSwordRecipe(json, missingMemberBehavior);
            case "Torch":
                return ReadTorchRecipe(json, missingMemberBehavior);
            case "Trident":
                return ReadTridentRecipe(json, missingMemberBehavior);
            case "UpgradeComponent":
                return ReadUpgradeComponentRecipe(json, missingMemberBehavior);
            case "Warhorn":
                return ReadWarhornRecipe(json, missingMemberBehavior);
        }

        RequiredMember<int> outputItemId = new("output_item_id");
        RequiredMember<int> outputItemCount = new("output_item_count");
        RequiredMember<int> minRating = new("min_rating");
        RequiredMember<TimeSpan> timeToCraft = new("time_to_craft_ms");
        RequiredMember<CraftingDisciplineName> disciplines = new("disciplines");
        RequiredMember<RecipeFlag> flags = new("flags");
        RequiredMember<Ingredient> ingredients = new("ingredients");
        RequiredMember<int> id = new("id");
        RequiredMember<string> chatLink = new("chat_link");
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (missingMemberBehavior == MissingMemberBehavior.Error)
                {
                    throw new InvalidOperationException(
                        Strings.UnexpectedDiscriminator(member.Value.GetString())
                    );
                }
            }
            else if (member.NameEquals(outputItemId.Name))
            {
                outputItemId = outputItemId.From(member.Value);
            }
            else if (member.NameEquals(outputItemCount.Name))
            {
                outputItemCount = outputItemCount.From(member.Value);
            }
            else if (member.NameEquals(minRating.Name))
            {
                minRating = minRating.From(member.Value);
            }
            else if (member.NameEquals(minRating.Name))
            {
                minRating = minRating.From(member.Value);
            }
            else if (member.NameEquals(timeToCraft.Name))
            {
                timeToCraft = timeToCraft.From(member.Value);
            }
            else if (member.NameEquals(disciplines.Name))
            {
                disciplines = disciplines.From(member.Value);
            }
            else if (member.NameEquals(flags.Name))
            {
                flags = flags.From(member.Value);
            }
            else if (member.NameEquals(ingredients.Name))
            {
                ingredients = ingredients.From(member.Value);
            }
            else if (member.NameEquals(id.Name))
            {
                id = id.From(member.Value);
            }
            else if (member.NameEquals(chatLink.Name))
            {
                chatLink = chatLink.From(member.Value);
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Recipe
        {
            Id = id.GetValue(),
            OutputItemId = outputItemId.GetValue(),
            OutputItemCount = outputItemCount.GetValue(),
            MinRating = minRating.GetValue(),
            TimeToCraft = timeToCraft.Select(value => TimeSpan.FromMilliseconds(value.GetDouble())),
            Disciplines = disciplines.GetValues(missingMemberBehavior),
            Flags = flags.GetValues(missingMemberBehavior),
            Ingredients =
                ingredients.SelectMany(value => ReadIngredient(value, missingMemberBehavior)),
            ChatLink = chatLink.GetValue()
        };
    }

    private static AmuletRecipe ReadAmuletRecipe(
        JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<int> outputItemId = new("output_item_id");
        RequiredMember<int> outputItemCount = new("output_item_count");
        RequiredMember<int> minRating = new("min_rating");
        RequiredMember<TimeSpan> timeToCraft = new("time_to_craft_ms");
        RequiredMember<CraftingDisciplineName> disciplines = new("disciplines");
        RequiredMember<RecipeFlag> flags = new("flags");
        RequiredMember<Ingredient> ingredients = new("ingredients");
        RequiredMember<int> id = new("id");
        RequiredMember<string> chatLink = new("chat_link");
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("Amulet"))
                {
                    throw new InvalidOperationException(
                        Strings.InvalidDiscriminator(member.Value.GetString())
                    );
                }
            }
            else if (member.NameEquals(outputItemId.Name))
            {
                outputItemId = outputItemId.From(member.Value);
            }
            else if (member.NameEquals(outputItemCount.Name))
            {
                outputItemCount = outputItemCount.From(member.Value);
            }
            else if (member.NameEquals(minRating.Name))
            {
                minRating = minRating.From(member.Value);
            }
            else if (member.NameEquals(minRating.Name))
            {
                minRating = minRating.From(member.Value);
            }
            else if (member.NameEquals(timeToCraft.Name))
            {
                timeToCraft = timeToCraft.From(member.Value);
            }
            else if (member.NameEquals(disciplines.Name))
            {
                disciplines = disciplines.From(member.Value);
            }
            else if (member.NameEquals(flags.Name))
            {
                flags = flags.From(member.Value);
            }
            else if (member.NameEquals(ingredients.Name))
            {
                ingredients = ingredients.From(member.Value);
            }
            else if (member.NameEquals(id.Name))
            {
                id = id.From(member.Value);
            }
            else if (member.NameEquals(chatLink.Name))
            {
                chatLink = chatLink.From(member.Value);
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new AmuletRecipe
        {
            Id = id.GetValue(),
            OutputItemId = outputItemId.GetValue(),
            OutputItemCount = outputItemCount.GetValue(),
            MinRating = minRating.GetValue(),
            TimeToCraft = timeToCraft.Select(value => TimeSpan.FromMilliseconds(value.GetDouble())),
            Disciplines = disciplines.GetValues(missingMemberBehavior),
            Flags = flags.GetValues(missingMemberBehavior),
            Ingredients =
                ingredients.SelectMany(value => ReadIngredient(value, missingMemberBehavior)),
            ChatLink = chatLink.GetValue()
        };
    }

    private static AxeRecipe ReadAxeRecipe(
        JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<int> outputItemId = new("output_item_id");
        RequiredMember<int> outputItemCount = new("output_item_count");
        RequiredMember<int> minRating = new("min_rating");
        RequiredMember<TimeSpan> timeToCraft = new("time_to_craft_ms");
        RequiredMember<CraftingDisciplineName> disciplines = new("disciplines");
        RequiredMember<RecipeFlag> flags = new("flags");
        RequiredMember<Ingredient> ingredients = new("ingredients");
        RequiredMember<int> id = new("id");
        RequiredMember<string> chatLink = new("chat_link");
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("Axe"))
                {
                    throw new InvalidOperationException(
                        Strings.InvalidDiscriminator(member.Value.GetString())
                    );
                }
            }
            else if (member.NameEquals(outputItemId.Name))
            {
                outputItemId = outputItemId.From(member.Value);
            }
            else if (member.NameEquals(outputItemCount.Name))
            {
                outputItemCount = outputItemCount.From(member.Value);
            }
            else if (member.NameEquals(minRating.Name))
            {
                minRating = minRating.From(member.Value);
            }
            else if (member.NameEquals(minRating.Name))
            {
                minRating = minRating.From(member.Value);
            }
            else if (member.NameEquals(timeToCraft.Name))
            {
                timeToCraft = timeToCraft.From(member.Value);
            }
            else if (member.NameEquals(disciplines.Name))
            {
                disciplines = disciplines.From(member.Value);
            }
            else if (member.NameEquals(flags.Name))
            {
                flags = flags.From(member.Value);
            }
            else if (member.NameEquals(ingredients.Name))
            {
                ingredients = ingredients.From(member.Value);
            }
            else if (member.NameEquals(id.Name))
            {
                id = id.From(member.Value);
            }
            else if (member.NameEquals(chatLink.Name))
            {
                chatLink = chatLink.From(member.Value);
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new AxeRecipe
        {
            Id = id.GetValue(),
            OutputItemId = outputItemId.GetValue(),
            OutputItemCount = outputItemCount.GetValue(),
            MinRating = minRating.GetValue(),
            TimeToCraft = timeToCraft.Select(value => TimeSpan.FromMilliseconds(value.GetDouble())),
            Disciplines = disciplines.GetValues(missingMemberBehavior),
            Flags = flags.GetValues(missingMemberBehavior),
            Ingredients =
                ingredients.SelectMany(value => ReadIngredient(value, missingMemberBehavior)),
            ChatLink = chatLink.GetValue()
        };
    }

    private static BackpackRecipe ReadBackpackRecipe(
        JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<int> outputItemId = new("output_item_id");
        RequiredMember<int> outputItemCount = new("output_item_count");
        RequiredMember<int> minRating = new("min_rating");
        RequiredMember<TimeSpan> timeToCraft = new("time_to_craft_ms");
        RequiredMember<CraftingDisciplineName> disciplines = new("disciplines");
        RequiredMember<RecipeFlag> flags = new("flags");
        RequiredMember<Ingredient> ingredients = new("ingredients");
        RequiredMember<int> id = new("id");
        RequiredMember<string> chatLink = new("chat_link");
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("Backpack"))
                {
                    throw new InvalidOperationException(
                        Strings.InvalidDiscriminator(member.Value.GetString())
                    );
                }
            }
            else if (member.NameEquals(outputItemId.Name))
            {
                outputItemId = outputItemId.From(member.Value);
            }
            else if (member.NameEquals(outputItemCount.Name))
            {
                outputItemCount = outputItemCount.From(member.Value);
            }
            else if (member.NameEquals(minRating.Name))
            {
                minRating = minRating.From(member.Value);
            }
            else if (member.NameEquals(minRating.Name))
            {
                minRating = minRating.From(member.Value);
            }
            else if (member.NameEquals(timeToCraft.Name))
            {
                timeToCraft = timeToCraft.From(member.Value);
            }
            else if (member.NameEquals(disciplines.Name))
            {
                disciplines = disciplines.From(member.Value);
            }
            else if (member.NameEquals(flags.Name))
            {
                flags = flags.From(member.Value);
            }
            else if (member.NameEquals(ingredients.Name))
            {
                ingredients = ingredients.From(member.Value);
            }
            else if (member.NameEquals(id.Name))
            {
                id = id.From(member.Value);
            }
            else if (member.NameEquals(chatLink.Name))
            {
                chatLink = chatLink.From(member.Value);
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new BackpackRecipe
        {
            Id = id.GetValue(),
            OutputItemId = outputItemId.GetValue(),
            OutputItemCount = outputItemCount.GetValue(),
            MinRating = minRating.GetValue(),
            TimeToCraft = timeToCraft.Select(value => TimeSpan.FromMilliseconds(value.GetDouble())),
            Disciplines = disciplines.GetValues(missingMemberBehavior),
            Flags = flags.GetValues(missingMemberBehavior),
            Ingredients =
                ingredients.SelectMany(value => ReadIngredient(value, missingMemberBehavior)),
            ChatLink = chatLink.GetValue()
        };
    }

    private static BagRecipe ReadBagRecipe(
        JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<int> outputItemId = new("output_item_id");
        RequiredMember<int> outputItemCount = new("output_item_count");
        RequiredMember<int> minRating = new("min_rating");
        RequiredMember<TimeSpan> timeToCraft = new("time_to_craft_ms");
        RequiredMember<CraftingDisciplineName> disciplines = new("disciplines");
        RequiredMember<RecipeFlag> flags = new("flags");
        RequiredMember<Ingredient> ingredients = new("ingredients");
        RequiredMember<int> id = new("id");
        RequiredMember<string> chatLink = new("chat_link");
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("Bag"))
                {
                    throw new InvalidOperationException(
                        Strings.InvalidDiscriminator(member.Value.GetString())
                    );
                }
            }
            else if (member.NameEquals(outputItemId.Name))
            {
                outputItemId = outputItemId.From(member.Value);
            }
            else if (member.NameEquals(outputItemCount.Name))
            {
                outputItemCount = outputItemCount.From(member.Value);
            }
            else if (member.NameEquals(minRating.Name))
            {
                minRating = minRating.From(member.Value);
            }
            else if (member.NameEquals(minRating.Name))
            {
                minRating = minRating.From(member.Value);
            }
            else if (member.NameEquals(timeToCraft.Name))
            {
                timeToCraft = timeToCraft.From(member.Value);
            }
            else if (member.NameEquals(disciplines.Name))
            {
                disciplines = disciplines.From(member.Value);
            }
            else if (member.NameEquals(flags.Name))
            {
                flags = flags.From(member.Value);
            }
            else if (member.NameEquals(ingredients.Name))
            {
                ingredients = ingredients.From(member.Value);
            }
            else if (member.NameEquals(id.Name))
            {
                id = id.From(member.Value);
            }
            else if (member.NameEquals(chatLink.Name))
            {
                chatLink = chatLink.From(member.Value);
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new BagRecipe
        {
            Id = id.GetValue(),
            OutputItemId = outputItemId.GetValue(),
            OutputItemCount = outputItemCount.GetValue(),
            MinRating = minRating.GetValue(),
            TimeToCraft = timeToCraft.Select(value => TimeSpan.FromMilliseconds(value.GetDouble())),
            Disciplines = disciplines.GetValues(missingMemberBehavior),
            Flags = flags.GetValues(missingMemberBehavior),
            Ingredients =
                ingredients.SelectMany(value => ReadIngredient(value, missingMemberBehavior)),
            ChatLink = chatLink.GetValue()
        };
    }

    private static BootsRecipe ReadBootsRecipe(
        JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<int> outputItemId = new("output_item_id");
        RequiredMember<int> outputItemCount = new("output_item_count");
        RequiredMember<int> minRating = new("min_rating");
        RequiredMember<TimeSpan> timeToCraft = new("time_to_craft_ms");
        RequiredMember<CraftingDisciplineName> disciplines = new("disciplines");
        RequiredMember<RecipeFlag> flags = new("flags");
        RequiredMember<Ingredient> ingredients = new("ingredients");
        RequiredMember<int> id = new("id");
        RequiredMember<string> chatLink = new("chat_link");
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("Boots"))
                {
                    throw new InvalidOperationException(
                        Strings.InvalidDiscriminator(member.Value.GetString())
                    );
                }
            }
            else if (member.NameEquals(outputItemId.Name))
            {
                outputItemId = outputItemId.From(member.Value);
            }
            else if (member.NameEquals(outputItemCount.Name))
            {
                outputItemCount = outputItemCount.From(member.Value);
            }
            else if (member.NameEquals(minRating.Name))
            {
                minRating = minRating.From(member.Value);
            }
            else if (member.NameEquals(minRating.Name))
            {
                minRating = minRating.From(member.Value);
            }
            else if (member.NameEquals(timeToCraft.Name))
            {
                timeToCraft = timeToCraft.From(member.Value);
            }
            else if (member.NameEquals(disciplines.Name))
            {
                disciplines = disciplines.From(member.Value);
            }
            else if (member.NameEquals(flags.Name))
            {
                flags = flags.From(member.Value);
            }
            else if (member.NameEquals(ingredients.Name))
            {
                ingredients = ingredients.From(member.Value);
            }
            else if (member.NameEquals(id.Name))
            {
                id = id.From(member.Value);
            }
            else if (member.NameEquals(chatLink.Name))
            {
                chatLink = chatLink.From(member.Value);
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new BootsRecipe
        {
            Id = id.GetValue(),
            OutputItemId = outputItemId.GetValue(),
            OutputItemCount = outputItemCount.GetValue(),
            MinRating = minRating.GetValue(),
            TimeToCraft = timeToCraft.Select(value => TimeSpan.FromMilliseconds(value.GetDouble())),
            Disciplines = disciplines.GetValues(missingMemberBehavior),
            Flags = flags.GetValues(missingMemberBehavior),
            Ingredients =
                ingredients.SelectMany(value => ReadIngredient(value, missingMemberBehavior)),
            ChatLink = chatLink.GetValue()
        };
    }

    private static BulkRecipe ReadBulkRecipe(
        JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<int> outputItemId = new("output_item_id");
        RequiredMember<int> outputItemCount = new("output_item_count");
        RequiredMember<int> minRating = new("min_rating");
        RequiredMember<TimeSpan> timeToCraft = new("time_to_craft_ms");
        RequiredMember<CraftingDisciplineName> disciplines = new("disciplines");
        RequiredMember<RecipeFlag> flags = new("flags");
        RequiredMember<Ingredient> ingredients = new("ingredients");
        RequiredMember<int> id = new("id");
        RequiredMember<string> chatLink = new("chat_link");
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("Bulk"))
                {
                    throw new InvalidOperationException(
                        Strings.InvalidDiscriminator(member.Value.GetString())
                    );
                }
            }
            else if (member.NameEquals(outputItemId.Name))
            {
                outputItemId = outputItemId.From(member.Value);
            }
            else if (member.NameEquals(outputItemCount.Name))
            {
                outputItemCount = outputItemCount.From(member.Value);
            }
            else if (member.NameEquals(minRating.Name))
            {
                minRating = minRating.From(member.Value);
            }
            else if (member.NameEquals(minRating.Name))
            {
                minRating = minRating.From(member.Value);
            }
            else if (member.NameEquals(timeToCraft.Name))
            {
                timeToCraft = timeToCraft.From(member.Value);
            }
            else if (member.NameEquals(disciplines.Name))
            {
                disciplines = disciplines.From(member.Value);
            }
            else if (member.NameEquals(flags.Name))
            {
                flags = flags.From(member.Value);
            }
            else if (member.NameEquals(ingredients.Name))
            {
                ingredients = ingredients.From(member.Value);
            }
            else if (member.NameEquals(id.Name))
            {
                id = id.From(member.Value);
            }
            else if (member.NameEquals(chatLink.Name))
            {
                chatLink = chatLink.From(member.Value);
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new BulkRecipe
        {
            Id = id.GetValue(),
            OutputItemId = outputItemId.GetValue(),
            OutputItemCount = outputItemCount.GetValue(),
            MinRating = minRating.GetValue(),
            TimeToCraft = timeToCraft.Select(value => TimeSpan.FromMilliseconds(value.GetDouble())),
            Disciplines = disciplines.GetValues(missingMemberBehavior),
            Flags = flags.GetValues(missingMemberBehavior),
            Ingredients =
                ingredients.SelectMany(value => ReadIngredient(value, missingMemberBehavior)),
            ChatLink = chatLink.GetValue()
        };
    }

    private static CoatRecipe ReadCoatRecipe(
        JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<int> outputItemId = new("output_item_id");
        RequiredMember<int> outputItemCount = new("output_item_count");
        RequiredMember<int> minRating = new("min_rating");
        RequiredMember<TimeSpan> timeToCraft = new("time_to_craft_ms");
        RequiredMember<CraftingDisciplineName> disciplines = new("disciplines");
        RequiredMember<RecipeFlag> flags = new("flags");
        RequiredMember<Ingredient> ingredients = new("ingredients");
        RequiredMember<int> id = new("id");
        RequiredMember<string> chatLink = new("chat_link");
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("Coat"))
                {
                    throw new InvalidOperationException(
                        Strings.InvalidDiscriminator(member.Value.GetString())
                    );
                }
            }
            else if (member.NameEquals(outputItemId.Name))
            {
                outputItemId = outputItemId.From(member.Value);
            }
            else if (member.NameEquals(outputItemCount.Name))
            {
                outputItemCount = outputItemCount.From(member.Value);
            }
            else if (member.NameEquals(minRating.Name))
            {
                minRating = minRating.From(member.Value);
            }
            else if (member.NameEquals(minRating.Name))
            {
                minRating = minRating.From(member.Value);
            }
            else if (member.NameEquals(timeToCraft.Name))
            {
                timeToCraft = timeToCraft.From(member.Value);
            }
            else if (member.NameEquals(disciplines.Name))
            {
                disciplines = disciplines.From(member.Value);
            }
            else if (member.NameEquals(flags.Name))
            {
                flags = flags.From(member.Value);
            }
            else if (member.NameEquals(ingredients.Name))
            {
                ingredients = ingredients.From(member.Value);
            }
            else if (member.NameEquals(id.Name))
            {
                id = id.From(member.Value);
            }
            else if (member.NameEquals(chatLink.Name))
            {
                chatLink = chatLink.From(member.Value);
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new CoatRecipe
        {
            Id = id.GetValue(),
            OutputItemId = outputItemId.GetValue(),
            OutputItemCount = outputItemCount.GetValue(),
            MinRating = minRating.GetValue(),
            TimeToCraft = timeToCraft.Select(value => TimeSpan.FromMilliseconds(value.GetDouble())),
            Disciplines = disciplines.GetValues(missingMemberBehavior),
            Flags = flags.GetValues(missingMemberBehavior),
            Ingredients =
                ingredients.SelectMany(value => ReadIngredient(value, missingMemberBehavior)),
            ChatLink = chatLink.GetValue()
        };
    }

    private static ComponentRecipe ReadComponentRecipe(
        JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<int> outputItemId = new("output_item_id");
        RequiredMember<int> outputItemCount = new("output_item_count");
        RequiredMember<int> minRating = new("min_rating");
        RequiredMember<TimeSpan> timeToCraft = new("time_to_craft_ms");
        RequiredMember<CraftingDisciplineName> disciplines = new("disciplines");
        RequiredMember<RecipeFlag> flags = new("flags");
        RequiredMember<Ingredient> ingredients = new("ingredients");
        RequiredMember<int> id = new("id");
        RequiredMember<string> chatLink = new("chat_link");
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("Component"))
                {
                    throw new InvalidOperationException(
                        Strings.InvalidDiscriminator(member.Value.GetString())
                    );
                }
            }
            else if (member.NameEquals(outputItemId.Name))
            {
                outputItemId = outputItemId.From(member.Value);
            }
            else if (member.NameEquals(outputItemCount.Name))
            {
                outputItemCount = outputItemCount.From(member.Value);
            }
            else if (member.NameEquals(minRating.Name))
            {
                minRating = minRating.From(member.Value);
            }
            else if (member.NameEquals(minRating.Name))
            {
                minRating = minRating.From(member.Value);
            }
            else if (member.NameEquals(timeToCraft.Name))
            {
                timeToCraft = timeToCraft.From(member.Value);
            }
            else if (member.NameEquals(disciplines.Name))
            {
                disciplines = disciplines.From(member.Value);
            }
            else if (member.NameEquals(flags.Name))
            {
                flags = flags.From(member.Value);
            }
            else if (member.NameEquals(ingredients.Name))
            {
                ingredients = ingredients.From(member.Value);
            }
            else if (member.NameEquals(id.Name))
            {
                id = id.From(member.Value);
            }
            else if (member.NameEquals(chatLink.Name))
            {
                chatLink = chatLink.From(member.Value);
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new ComponentRecipe
        {
            Id = id.GetValue(),
            OutputItemId = outputItemId.GetValue(),
            OutputItemCount = outputItemCount.GetValue(),
            MinRating = minRating.GetValue(),
            TimeToCraft = timeToCraft.Select(value => TimeSpan.FromMilliseconds(value.GetDouble())),
            Disciplines = disciplines.GetValues(missingMemberBehavior),
            Flags = flags.GetValues(missingMemberBehavior),
            Ingredients =
                ingredients.SelectMany(value => ReadIngredient(value, missingMemberBehavior)),
            ChatLink = chatLink.GetValue()
        };
    }

    private static ConsumableRecipe ReadConsumableRecipe(
        JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<int> outputItemId = new("output_item_id");
        RequiredMember<int> outputItemCount = new("output_item_count");
        RequiredMember<int> minRating = new("min_rating");
        RequiredMember<TimeSpan> timeToCraft = new("time_to_craft_ms");
        RequiredMember<CraftingDisciplineName> disciplines = new("disciplines");
        RequiredMember<RecipeFlag> flags = new("flags");
        RequiredMember<Ingredient> ingredients = new("ingredients");
        RequiredMember<int> id = new("id");
        RequiredMember<string> chatLink = new("chat_link");
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("Consumable"))
                {
                    throw new InvalidOperationException(
                        Strings.InvalidDiscriminator(member.Value.GetString())
                    );
                }
            }
            else if (member.NameEquals(outputItemId.Name))
            {
                outputItemId = outputItemId.From(member.Value);
            }
            else if (member.NameEquals(outputItemCount.Name))
            {
                outputItemCount = outputItemCount.From(member.Value);
            }
            else if (member.NameEquals(minRating.Name))
            {
                minRating = minRating.From(member.Value);
            }
            else if (member.NameEquals(minRating.Name))
            {
                minRating = minRating.From(member.Value);
            }
            else if (member.NameEquals(timeToCraft.Name))
            {
                timeToCraft = timeToCraft.From(member.Value);
            }
            else if (member.NameEquals(disciplines.Name))
            {
                disciplines = disciplines.From(member.Value);
            }
            else if (member.NameEquals(flags.Name))
            {
                flags = flags.From(member.Value);
            }
            else if (member.NameEquals(ingredients.Name))
            {
                ingredients = ingredients.From(member.Value);
            }
            else if (member.NameEquals(id.Name))
            {
                id = id.From(member.Value);
            }
            else if (member.NameEquals(chatLink.Name))
            {
                chatLink = chatLink.From(member.Value);
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new ConsumableRecipe
        {
            Id = id.GetValue(),
            OutputItemId = outputItemId.GetValue(),
            OutputItemCount = outputItemCount.GetValue(),
            MinRating = minRating.GetValue(),
            TimeToCraft = timeToCraft.Select(value => TimeSpan.FromMilliseconds(value.GetDouble())),
            Disciplines = disciplines.GetValues(missingMemberBehavior),
            Flags = flags.GetValues(missingMemberBehavior),
            Ingredients =
                ingredients.SelectMany(value => ReadIngredient(value, missingMemberBehavior)),
            ChatLink = chatLink.GetValue()
        };
    }

    private static DaggerRecipe ReadDaggerRecipe(
        JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<int> outputItemId = new("output_item_id");
        RequiredMember<int> outputItemCount = new("output_item_count");
        RequiredMember<int> minRating = new("min_rating");
        RequiredMember<TimeSpan> timeToCraft = new("time_to_craft_ms");
        RequiredMember<CraftingDisciplineName> disciplines = new("disciplines");
        RequiredMember<RecipeFlag> flags = new("flags");
        RequiredMember<Ingredient> ingredients = new("ingredients");
        RequiredMember<int> id = new("id");
        RequiredMember<string> chatLink = new("chat_link");
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("Dagger"))
                {
                    throw new InvalidOperationException(
                        Strings.InvalidDiscriminator(member.Value.GetString())
                    );
                }
            }
            else if (member.NameEquals(outputItemId.Name))
            {
                outputItemId = outputItemId.From(member.Value);
            }
            else if (member.NameEquals(outputItemCount.Name))
            {
                outputItemCount = outputItemCount.From(member.Value);
            }
            else if (member.NameEquals(minRating.Name))
            {
                minRating = minRating.From(member.Value);
            }
            else if (member.NameEquals(minRating.Name))
            {
                minRating = minRating.From(member.Value);
            }
            else if (member.NameEquals(timeToCraft.Name))
            {
                timeToCraft = timeToCraft.From(member.Value);
            }
            else if (member.NameEquals(disciplines.Name))
            {
                disciplines = disciplines.From(member.Value);
            }
            else if (member.NameEquals(flags.Name))
            {
                flags = flags.From(member.Value);
            }
            else if (member.NameEquals(ingredients.Name))
            {
                ingredients = ingredients.From(member.Value);
            }
            else if (member.NameEquals(id.Name))
            {
                id = id.From(member.Value);
            }
            else if (member.NameEquals(chatLink.Name))
            {
                chatLink = chatLink.From(member.Value);
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new DaggerRecipe
        {
            Id = id.GetValue(),
            OutputItemId = outputItemId.GetValue(),
            OutputItemCount = outputItemCount.GetValue(),
            MinRating = minRating.GetValue(),
            TimeToCraft = timeToCraft.Select(value => TimeSpan.FromMilliseconds(value.GetDouble())),
            Disciplines = disciplines.GetValues(missingMemberBehavior),
            Flags = flags.GetValues(missingMemberBehavior),
            Ingredients =
                ingredients.SelectMany(value => ReadIngredient(value, missingMemberBehavior)),
            ChatLink = chatLink.GetValue()
        };
    }

    private static DessertRecipe ReadDessertRecipe(
        JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<int> outputItemId = new("output_item_id");
        RequiredMember<int> outputItemCount = new("output_item_count");
        RequiredMember<int> minRating = new("min_rating");
        RequiredMember<TimeSpan> timeToCraft = new("time_to_craft_ms");
        RequiredMember<CraftingDisciplineName> disciplines = new("disciplines");
        RequiredMember<RecipeFlag> flags = new("flags");
        RequiredMember<Ingredient> ingredients = new("ingredients");
        RequiredMember<int> id = new("id");
        RequiredMember<string> chatLink = new("chat_link");
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("Dessert"))
                {
                    throw new InvalidOperationException(
                        Strings.InvalidDiscriminator(member.Value.GetString())
                    );
                }
            }
            else if (member.NameEquals(outputItemId.Name))
            {
                outputItemId = outputItemId.From(member.Value);
            }
            else if (member.NameEquals(outputItemCount.Name))
            {
                outputItemCount = outputItemCount.From(member.Value);
            }
            else if (member.NameEquals(minRating.Name))
            {
                minRating = minRating.From(member.Value);
            }
            else if (member.NameEquals(minRating.Name))
            {
                minRating = minRating.From(member.Value);
            }
            else if (member.NameEquals(timeToCraft.Name))
            {
                timeToCraft = timeToCraft.From(member.Value);
            }
            else if (member.NameEquals(disciplines.Name))
            {
                disciplines = disciplines.From(member.Value);
            }
            else if (member.NameEquals(flags.Name))
            {
                flags = flags.From(member.Value);
            }
            else if (member.NameEquals(ingredients.Name))
            {
                ingredients = ingredients.From(member.Value);
            }
            else if (member.NameEquals(id.Name))
            {
                id = id.From(member.Value);
            }
            else if (member.NameEquals(chatLink.Name))
            {
                chatLink = chatLink.From(member.Value);
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new DessertRecipe
        {
            Id = id.GetValue(),
            OutputItemId = outputItemId.GetValue(),
            OutputItemCount = outputItemCount.GetValue(),
            MinRating = minRating.GetValue(),
            TimeToCraft = timeToCraft.Select(value => TimeSpan.FromMilliseconds(value.GetDouble())),
            Disciplines = disciplines.GetValues(missingMemberBehavior),
            Flags = flags.GetValues(missingMemberBehavior),
            Ingredients =
                ingredients.SelectMany(value => ReadIngredient(value, missingMemberBehavior)),
            ChatLink = chatLink.GetValue()
        };
    }

    private static DyeRecipe ReadDyeRecipe(
        JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<int> outputItemId = new("output_item_id");
        RequiredMember<int> outputItemCount = new("output_item_count");
        RequiredMember<int> minRating = new("min_rating");
        RequiredMember<TimeSpan> timeToCraft = new("time_to_craft_ms");
        RequiredMember<CraftingDisciplineName> disciplines = new("disciplines");
        RequiredMember<RecipeFlag> flags = new("flags");
        RequiredMember<Ingredient> ingredients = new("ingredients");
        RequiredMember<int> id = new("id");
        RequiredMember<string> chatLink = new("chat_link");
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("Dye"))
                {
                    throw new InvalidOperationException(
                        Strings.InvalidDiscriminator(member.Value.GetString())
                    );
                }
            }
            else if (member.NameEquals(outputItemId.Name))
            {
                outputItemId = outputItemId.From(member.Value);
            }
            else if (member.NameEquals(outputItemCount.Name))
            {
                outputItemCount = outputItemCount.From(member.Value);
            }
            else if (member.NameEquals(minRating.Name))
            {
                minRating = minRating.From(member.Value);
            }
            else if (member.NameEquals(minRating.Name))
            {
                minRating = minRating.From(member.Value);
            }
            else if (member.NameEquals(timeToCraft.Name))
            {
                timeToCraft = timeToCraft.From(member.Value);
            }
            else if (member.NameEquals(disciplines.Name))
            {
                disciplines = disciplines.From(member.Value);
            }
            else if (member.NameEquals(flags.Name))
            {
                flags = flags.From(member.Value);
            }
            else if (member.NameEquals(ingredients.Name))
            {
                ingredients = ingredients.From(member.Value);
            }
            else if (member.NameEquals(id.Name))
            {
                id = id.From(member.Value);
            }
            else if (member.NameEquals(chatLink.Name))
            {
                chatLink = chatLink.From(member.Value);
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new DyeRecipe
        {
            Id = id.GetValue(),
            OutputItemId = outputItemId.GetValue(),
            OutputItemCount = outputItemCount.GetValue(),
            MinRating = minRating.GetValue(),
            TimeToCraft = timeToCraft.Select(value => TimeSpan.FromMilliseconds(value.GetDouble())),
            Disciplines = disciplines.GetValues(missingMemberBehavior),
            Flags = flags.GetValues(missingMemberBehavior),
            Ingredients =
                ingredients.SelectMany(value => ReadIngredient(value, missingMemberBehavior)),
            ChatLink = chatLink.GetValue()
        };
    }

    private static EarringRecipe ReadEarringRecipe(
        JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<int> outputItemId = new("output_item_id");
        RequiredMember<int> outputItemCount = new("output_item_count");
        RequiredMember<int> minRating = new("min_rating");
        RequiredMember<TimeSpan> timeToCraft = new("time_to_craft_ms");
        RequiredMember<CraftingDisciplineName> disciplines = new("disciplines");
        RequiredMember<RecipeFlag> flags = new("flags");
        RequiredMember<Ingredient> ingredients = new("ingredients");
        RequiredMember<int> id = new("id");
        RequiredMember<string> chatLink = new("chat_link");
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("Earring"))
                {
                    throw new InvalidOperationException(
                        Strings.InvalidDiscriminator(member.Value.GetString())
                    );
                }
            }
            else if (member.NameEquals(outputItemId.Name))
            {
                outputItemId = outputItemId.From(member.Value);
            }
            else if (member.NameEquals(outputItemCount.Name))
            {
                outputItemCount = outputItemCount.From(member.Value);
            }
            else if (member.NameEquals(minRating.Name))
            {
                minRating = minRating.From(member.Value);
            }
            else if (member.NameEquals(minRating.Name))
            {
                minRating = minRating.From(member.Value);
            }
            else if (member.NameEquals(timeToCraft.Name))
            {
                timeToCraft = timeToCraft.From(member.Value);
            }
            else if (member.NameEquals(disciplines.Name))
            {
                disciplines = disciplines.From(member.Value);
            }
            else if (member.NameEquals(flags.Name))
            {
                flags = flags.From(member.Value);
            }
            else if (member.NameEquals(ingredients.Name))
            {
                ingredients = ingredients.From(member.Value);
            }
            else if (member.NameEquals(id.Name))
            {
                id = id.From(member.Value);
            }
            else if (member.NameEquals(chatLink.Name))
            {
                chatLink = chatLink.From(member.Value);
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new EarringRecipe
        {
            Id = id.GetValue(),
            OutputItemId = outputItemId.GetValue(),
            OutputItemCount = outputItemCount.GetValue(),
            MinRating = minRating.GetValue(),
            TimeToCraft = timeToCraft.Select(value => TimeSpan.FromMilliseconds(value.GetDouble())),
            Disciplines = disciplines.GetValues(missingMemberBehavior),
            Flags = flags.GetValues(missingMemberBehavior),
            Ingredients =
                ingredients.SelectMany(value => ReadIngredient(value, missingMemberBehavior)),
            ChatLink = chatLink.GetValue()
        };
    }

    private static FeastRecipe ReadFeastRecipe(
        JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<int> outputItemId = new("output_item_id");
        RequiredMember<int> outputItemCount = new("output_item_count");
        RequiredMember<int> minRating = new("min_rating");
        RequiredMember<TimeSpan> timeToCraft = new("time_to_craft_ms");
        RequiredMember<CraftingDisciplineName> disciplines = new("disciplines");
        RequiredMember<RecipeFlag> flags = new("flags");
        RequiredMember<Ingredient> ingredients = new("ingredients");
        RequiredMember<int> id = new("id");
        RequiredMember<string> chatLink = new("chat_link");
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("Feast"))
                {
                    throw new InvalidOperationException(
                        Strings.InvalidDiscriminator(member.Value.GetString())
                    );
                }
            }
            else if (member.NameEquals(outputItemId.Name))
            {
                outputItemId = outputItemId.From(member.Value);
            }
            else if (member.NameEquals(outputItemCount.Name))
            {
                outputItemCount = outputItemCount.From(member.Value);
            }
            else if (member.NameEquals(minRating.Name))
            {
                minRating = minRating.From(member.Value);
            }
            else if (member.NameEquals(minRating.Name))
            {
                minRating = minRating.From(member.Value);
            }
            else if (member.NameEquals(timeToCraft.Name))
            {
                timeToCraft = timeToCraft.From(member.Value);
            }
            else if (member.NameEquals(disciplines.Name))
            {
                disciplines = disciplines.From(member.Value);
            }
            else if (member.NameEquals(flags.Name))
            {
                flags = flags.From(member.Value);
            }
            else if (member.NameEquals(ingredients.Name))
            {
                ingredients = ingredients.From(member.Value);
            }
            else if (member.NameEquals(id.Name))
            {
                id = id.From(member.Value);
            }
            else if (member.NameEquals(chatLink.Name))
            {
                chatLink = chatLink.From(member.Value);
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new FeastRecipe
        {
            Id = id.GetValue(),
            OutputItemId = outputItemId.GetValue(),
            OutputItemCount = outputItemCount.GetValue(),
            MinRating = minRating.GetValue(),
            TimeToCraft = timeToCraft.Select(value => TimeSpan.FromMilliseconds(value.GetDouble())),
            Disciplines = disciplines.GetValues(missingMemberBehavior),
            Flags = flags.GetValues(missingMemberBehavior),
            Ingredients =
                ingredients.SelectMany(value => ReadIngredient(value, missingMemberBehavior)),
            ChatLink = chatLink.GetValue()
        };
    }

    private static FocusRecipe ReadFocusRecipe(
        JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<int> outputItemId = new("output_item_id");
        RequiredMember<int> outputItemCount = new("output_item_count");
        RequiredMember<int> minRating = new("min_rating");
        RequiredMember<TimeSpan> timeToCraft = new("time_to_craft_ms");
        RequiredMember<CraftingDisciplineName> disciplines = new("disciplines");
        RequiredMember<RecipeFlag> flags = new("flags");
        RequiredMember<Ingredient> ingredients = new("ingredients");
        RequiredMember<int> id = new("id");
        RequiredMember<string> chatLink = new("chat_link");
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("Focus"))
                {
                    throw new InvalidOperationException(
                        Strings.InvalidDiscriminator(member.Value.GetString())
                    );
                }
            }
            else if (member.NameEquals(outputItemId.Name))
            {
                outputItemId = outputItemId.From(member.Value);
            }
            else if (member.NameEquals(outputItemCount.Name))
            {
                outputItemCount = outputItemCount.From(member.Value);
            }
            else if (member.NameEquals(minRating.Name))
            {
                minRating = minRating.From(member.Value);
            }
            else if (member.NameEquals(minRating.Name))
            {
                minRating = minRating.From(member.Value);
            }
            else if (member.NameEquals(timeToCraft.Name))
            {
                timeToCraft = timeToCraft.From(member.Value);
            }
            else if (member.NameEquals(disciplines.Name))
            {
                disciplines = disciplines.From(member.Value);
            }
            else if (member.NameEquals(flags.Name))
            {
                flags = flags.From(member.Value);
            }
            else if (member.NameEquals(ingredients.Name))
            {
                ingredients = ingredients.From(member.Value);
            }
            else if (member.NameEquals(id.Name))
            {
                id = id.From(member.Value);
            }
            else if (member.NameEquals(chatLink.Name))
            {
                chatLink = chatLink.From(member.Value);
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new FocusRecipe
        {
            Id = id.GetValue(),
            OutputItemId = outputItemId.GetValue(),
            OutputItemCount = outputItemCount.GetValue(),
            MinRating = minRating.GetValue(),
            TimeToCraft = timeToCraft.Select(value => TimeSpan.FromMilliseconds(value.GetDouble())),
            Disciplines = disciplines.GetValues(missingMemberBehavior),
            Flags = flags.GetValues(missingMemberBehavior),
            Ingredients =
                ingredients.SelectMany(value => ReadIngredient(value, missingMemberBehavior)),
            ChatLink = chatLink.GetValue()
        };
    }

    private static FoodRecipe ReadFoodRecipe(
        JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<int> outputItemId = new("output_item_id");
        RequiredMember<int> outputItemCount = new("output_item_count");
        RequiredMember<int> minRating = new("min_rating");
        RequiredMember<TimeSpan> timeToCraft = new("time_to_craft_ms");
        RequiredMember<CraftingDisciplineName> disciplines = new("disciplines");
        RequiredMember<RecipeFlag> flags = new("flags");
        RequiredMember<Ingredient> ingredients = new("ingredients");
        RequiredMember<int> id = new("id");
        RequiredMember<string> chatLink = new("chat_link");
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("Food"))
                {
                    throw new InvalidOperationException(
                        Strings.InvalidDiscriminator(member.Value.GetString())
                    );
                }
            }
            else if (member.NameEquals(outputItemId.Name))
            {
                outputItemId = outputItemId.From(member.Value);
            }
            else if (member.NameEquals(outputItemCount.Name))
            {
                outputItemCount = outputItemCount.From(member.Value);
            }
            else if (member.NameEquals(minRating.Name))
            {
                minRating = minRating.From(member.Value);
            }
            else if (member.NameEquals(minRating.Name))
            {
                minRating = minRating.From(member.Value);
            }
            else if (member.NameEquals(timeToCraft.Name))
            {
                timeToCraft = timeToCraft.From(member.Value);
            }
            else if (member.NameEquals(disciplines.Name))
            {
                disciplines = disciplines.From(member.Value);
            }
            else if (member.NameEquals(flags.Name))
            {
                flags = flags.From(member.Value);
            }
            else if (member.NameEquals(ingredients.Name))
            {
                ingredients = ingredients.From(member.Value);
            }
            else if (member.NameEquals(id.Name))
            {
                id = id.From(member.Value);
            }
            else if (member.NameEquals(chatLink.Name))
            {
                chatLink = chatLink.From(member.Value);
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new FoodRecipe
        {
            Id = id.GetValue(),
            OutputItemId = outputItemId.GetValue(),
            OutputItemCount = outputItemCount.GetValue(),
            MinRating = minRating.GetValue(),
            TimeToCraft = timeToCraft.Select(value => TimeSpan.FromMilliseconds(value.GetDouble())),
            Disciplines = disciplines.GetValues(missingMemberBehavior),
            Flags = flags.GetValues(missingMemberBehavior),
            Ingredients =
                ingredients.SelectMany(value => ReadIngredient(value, missingMemberBehavior)),
            ChatLink = chatLink.GetValue()
        };
    }

    private static GlovesRecipe ReadGlovesRecipe(
        JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<int> outputItemId = new("output_item_id");
        RequiredMember<int> outputItemCount = new("output_item_count");
        RequiredMember<int> minRating = new("min_rating");
        RequiredMember<TimeSpan> timeToCraft = new("time_to_craft_ms");
        RequiredMember<CraftingDisciplineName> disciplines = new("disciplines");
        RequiredMember<RecipeFlag> flags = new("flags");
        RequiredMember<Ingredient> ingredients = new("ingredients");
        RequiredMember<int> id = new("id");
        RequiredMember<string> chatLink = new("chat_link");
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("Gloves"))
                {
                    throw new InvalidOperationException(
                        Strings.InvalidDiscriminator(member.Value.GetString())
                    );
                }
            }
            else if (member.NameEquals(outputItemId.Name))
            {
                outputItemId = outputItemId.From(member.Value);
            }
            else if (member.NameEquals(outputItemCount.Name))
            {
                outputItemCount = outputItemCount.From(member.Value);
            }
            else if (member.NameEquals(minRating.Name))
            {
                minRating = minRating.From(member.Value);
            }
            else if (member.NameEquals(minRating.Name))
            {
                minRating = minRating.From(member.Value);
            }
            else if (member.NameEquals(timeToCraft.Name))
            {
                timeToCraft = timeToCraft.From(member.Value);
            }
            else if (member.NameEquals(disciplines.Name))
            {
                disciplines = disciplines.From(member.Value);
            }
            else if (member.NameEquals(flags.Name))
            {
                flags = flags.From(member.Value);
            }
            else if (member.NameEquals(ingredients.Name))
            {
                ingredients = ingredients.From(member.Value);
            }
            else if (member.NameEquals(id.Name))
            {
                id = id.From(member.Value);
            }
            else if (member.NameEquals(chatLink.Name))
            {
                chatLink = chatLink.From(member.Value);
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new GlovesRecipe
        {
            Id = id.GetValue(),
            OutputItemId = outputItemId.GetValue(),
            OutputItemCount = outputItemCount.GetValue(),
            MinRating = minRating.GetValue(),
            TimeToCraft = timeToCraft.Select(value => TimeSpan.FromMilliseconds(value.GetDouble())),
            Disciplines = disciplines.GetValues(missingMemberBehavior),
            Flags = flags.GetValues(missingMemberBehavior),
            Ingredients =
                ingredients.SelectMany(value => ReadIngredient(value, missingMemberBehavior)),
            ChatLink = chatLink.GetValue()
        };
    }

    private static GreatswordRecipe ReadGreatswordRecipe(
        JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<int> outputItemId = new("output_item_id");
        RequiredMember<int> outputItemCount = new("output_item_count");
        RequiredMember<int> minRating = new("min_rating");
        RequiredMember<TimeSpan> timeToCraft = new("time_to_craft_ms");
        RequiredMember<CraftingDisciplineName> disciplines = new("disciplines");
        RequiredMember<RecipeFlag> flags = new("flags");
        RequiredMember<Ingredient> ingredients = new("ingredients");
        RequiredMember<int> id = new("id");
        RequiredMember<string> chatLink = new("chat_link");
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("Greatsword"))
                {
                    throw new InvalidOperationException(
                        Strings.InvalidDiscriminator(member.Value.GetString())
                    );
                }
            }
            else if (member.NameEquals(outputItemId.Name))
            {
                outputItemId = outputItemId.From(member.Value);
            }
            else if (member.NameEquals(outputItemCount.Name))
            {
                outputItemCount = outputItemCount.From(member.Value);
            }
            else if (member.NameEquals(minRating.Name))
            {
                minRating = minRating.From(member.Value);
            }
            else if (member.NameEquals(minRating.Name))
            {
                minRating = minRating.From(member.Value);
            }
            else if (member.NameEquals(timeToCraft.Name))
            {
                timeToCraft = timeToCraft.From(member.Value);
            }
            else if (member.NameEquals(disciplines.Name))
            {
                disciplines = disciplines.From(member.Value);
            }
            else if (member.NameEquals(flags.Name))
            {
                flags = flags.From(member.Value);
            }
            else if (member.NameEquals(ingredients.Name))
            {
                ingredients = ingredients.From(member.Value);
            }
            else if (member.NameEquals(id.Name))
            {
                id = id.From(member.Value);
            }
            else if (member.NameEquals(chatLink.Name))
            {
                chatLink = chatLink.From(member.Value);
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new GreatswordRecipe
        {
            Id = id.GetValue(),
            OutputItemId = outputItemId.GetValue(),
            OutputItemCount = outputItemCount.GetValue(),
            MinRating = minRating.GetValue(),
            TimeToCraft = timeToCraft.Select(value => TimeSpan.FromMilliseconds(value.GetDouble())),
            Disciplines = disciplines.GetValues(missingMemberBehavior),
            Flags = flags.GetValues(missingMemberBehavior),
            Ingredients =
                ingredients.SelectMany(value => ReadIngredient(value, missingMemberBehavior)),
            ChatLink = chatLink.GetValue()
        };
    }

    private static GuildConsumableRecipe ReadGuildConsumableRecipe(
        JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<int> outputItemId = new("output_item_id");
        RequiredMember<int> outputItemCount = new("output_item_count");
        RequiredMember<int> minRating = new("min_rating");
        RequiredMember<TimeSpan> timeToCraft = new("time_to_craft_ms");
        RequiredMember<CraftingDisciplineName> disciplines = new("disciplines");
        RequiredMember<RecipeFlag> flags = new("flags");
        RequiredMember<Ingredient> ingredients = new("ingredients");
        OptionalMember<GuildIngredient> guildIngredients = new("guild_ingredients");
        RequiredMember<int> outputUpgradeId = new("output_upgrade_id");
        RequiredMember<int> id = new("id");
        RequiredMember<string> chatLink = new("chat_link");
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("GuildConsumable"))
                {
                    throw new InvalidOperationException(
                        Strings.InvalidDiscriminator(member.Value.GetString())
                    );
                }
            }
            else if (member.NameEquals(outputItemId.Name))
            {
                outputItemId = outputItemId.From(member.Value);
            }
            else if (member.NameEquals(outputItemCount.Name))
            {
                outputItemCount = outputItemCount.From(member.Value);
            }
            else if (member.NameEquals(minRating.Name))
            {
                minRating = minRating.From(member.Value);
            }
            else if (member.NameEquals(minRating.Name))
            {
                minRating = minRating.From(member.Value);
            }
            else if (member.NameEquals(timeToCraft.Name))
            {
                timeToCraft = timeToCraft.From(member.Value);
            }
            else if (member.NameEquals(disciplines.Name))
            {
                disciplines = disciplines.From(member.Value);
            }
            else if (member.NameEquals(flags.Name))
            {
                flags = flags.From(member.Value);
            }
            else if (member.NameEquals(ingredients.Name))
            {
                ingredients = ingredients.From(member.Value);
            }
            else if (member.NameEquals(guildIngredients.Name))
            {
                guildIngredients = guildIngredients.From(member.Value);
            }
            else if (member.NameEquals(outputUpgradeId.Name))
            {
                outputUpgradeId = outputUpgradeId.From(member.Value);
            }
            else if (member.NameEquals(id.Name))
            {
                id = id.From(member.Value);
            }
            else if (member.NameEquals(chatLink.Name))
            {
                chatLink = chatLink.From(member.Value);
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new GuildConsumableRecipe
        {
            Id = id.GetValue(),
            OutputItemId = outputItemId.GetValue(),
            OutputItemCount = outputItemCount.GetValue(),
            MinRating = minRating.GetValue(),
            TimeToCraft = timeToCraft.Select(value => TimeSpan.FromMilliseconds(value.GetDouble())),
            Disciplines = disciplines.GetValues(missingMemberBehavior),
            Flags = flags.GetValues(missingMemberBehavior),
            Ingredients =
                ingredients.SelectMany(value => ReadIngredient(value, missingMemberBehavior)),
            GuildIngredients =
                guildIngredients.SelectMany(
                    value => ReadGuildIngredient(value, missingMemberBehavior)
                ),
            OutputUpgradeId = outputUpgradeId.GetValue(),
            ChatLink = chatLink.GetValue()
        };
    }

    private static GuildConsumableWvwRecipe ReadGuildConsumableWvwRecipe(
        JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<int> outputItemId = new("output_item_id");
        RequiredMember<int> outputItemCount = new("output_item_count");
        RequiredMember<int> minRating = new("min_rating");
        RequiredMember<TimeSpan> timeToCraft = new("time_to_craft_ms");
        RequiredMember<CraftingDisciplineName> disciplines = new("disciplines");
        RequiredMember<RecipeFlag> flags = new("flags");
        RequiredMember<Ingredient> ingredients = new("ingredients");
        NullableMember<int> outputUpgradeId = new("output_upgrade_id");
        RequiredMember<int> id = new("id");
        RequiredMember<string> chatLink = new("chat_link");
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("GuildConsumableWvw"))
                {
                    throw new InvalidOperationException(
                        Strings.InvalidDiscriminator(member.Value.GetString())
                    );
                }
            }
            else if (member.NameEquals(outputItemId.Name))
            {
                outputItemId = outputItemId.From(member.Value);
            }
            else if (member.NameEquals(outputItemCount.Name))
            {
                outputItemCount = outputItemCount.From(member.Value);
            }
            else if (member.NameEquals(minRating.Name))
            {
                minRating = minRating.From(member.Value);
            }
            else if (member.NameEquals(minRating.Name))
            {
                minRating = minRating.From(member.Value);
            }
            else if (member.NameEquals(timeToCraft.Name))
            {
                timeToCraft = timeToCraft.From(member.Value);
            }
            else if (member.NameEquals(disciplines.Name))
            {
                disciplines = disciplines.From(member.Value);
            }
            else if (member.NameEquals(flags.Name))
            {
                flags = flags.From(member.Value);
            }
            else if (member.NameEquals(ingredients.Name))
            {
                ingredients = ingredients.From(member.Value);
            }
            else if (member.NameEquals(outputUpgradeId.Name))
            {
                outputUpgradeId = outputUpgradeId.From(member.Value);
            }
            else if (member.NameEquals(id.Name))
            {
                id = id.From(member.Value);
            }
            else if (member.NameEquals(chatLink.Name))
            {
                chatLink = chatLink.From(member.Value);
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new GuildConsumableWvwRecipe
        {
            Id = id.GetValue(),
            OutputItemId = outputItemId.GetValue(),
            OutputItemCount = outputItemCount.GetValue(),
            MinRating = minRating.GetValue(),
            TimeToCraft = timeToCraft.Select(value => TimeSpan.FromMilliseconds(value.GetDouble())),
            Disciplines = disciplines.GetValues(missingMemberBehavior),
            Flags = flags.GetValues(missingMemberBehavior),
            Ingredients =
                ingredients.SelectMany(value => ReadIngredient(value, missingMemberBehavior)),
            OutputUpgradeId = outputUpgradeId.GetValue(),
            ChatLink = chatLink.GetValue()
        };
    }

    private static GuildDecorationRecipe ReadGuildDecorationRecipe(
        JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<int> outputItemId = new("output_item_id");
        RequiredMember<int> outputItemCount = new("output_item_count");
        RequiredMember<int> minRating = new("min_rating");
        RequiredMember<TimeSpan> timeToCraft = new("time_to_craft_ms");
        RequiredMember<CraftingDisciplineName> disciplines = new("disciplines");
        RequiredMember<RecipeFlag> flags = new("flags");
        RequiredMember<Ingredient> ingredients = new("ingredients");
        OptionalMember<GuildIngredient> guildIngredients = new("guild_ingredients");
        RequiredMember<int> outputUpgradeId = new("output_upgrade_id");
        RequiredMember<int> id = new("id");
        RequiredMember<string> chatLink = new("chat_link");
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("GuildDecoration"))
                {
                    throw new InvalidOperationException(
                        Strings.InvalidDiscriminator(member.Value.GetString())
                    );
                }
            }
            else if (member.NameEquals(outputItemId.Name))
            {
                outputItemId = outputItemId.From(member.Value);
            }
            else if (member.NameEquals(outputItemCount.Name))
            {
                outputItemCount = outputItemCount.From(member.Value);
            }
            else if (member.NameEquals(minRating.Name))
            {
                minRating = minRating.From(member.Value);
            }
            else if (member.NameEquals(minRating.Name))
            {
                minRating = minRating.From(member.Value);
            }
            else if (member.NameEquals(timeToCraft.Name))
            {
                timeToCraft = timeToCraft.From(member.Value);
            }
            else if (member.NameEquals(disciplines.Name))
            {
                disciplines = disciplines.From(member.Value);
            }
            else if (member.NameEquals(flags.Name))
            {
                flags = flags.From(member.Value);
            }
            else if (member.NameEquals(ingredients.Name))
            {
                ingredients = ingredients.From(member.Value);
            }
            else if (member.NameEquals(guildIngredients.Name))
            {
                guildIngredients = guildIngredients.From(member.Value);
            }
            else if (member.NameEquals(outputUpgradeId.Name))
            {
                outputUpgradeId = outputUpgradeId.From(member.Value);
            }
            else if (member.NameEquals(id.Name))
            {
                id = id.From(member.Value);
            }
            else if (member.NameEquals(chatLink.Name))
            {
                chatLink = chatLink.From(member.Value);
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new GuildDecorationRecipe
        {
            Id = id.GetValue(),
            OutputItemId = outputItemId.GetValue(),
            OutputItemCount = outputItemCount.GetValue(),
            MinRating = minRating.GetValue(),
            TimeToCraft = timeToCraft.Select(value => TimeSpan.FromMilliseconds(value.GetDouble())),
            Disciplines = disciplines.GetValues(missingMemberBehavior),
            Flags = flags.GetValues(missingMemberBehavior),
            Ingredients =
                ingredients.SelectMany(value => ReadIngredient(value, missingMemberBehavior)),
            GuildIngredients =
                guildIngredients.SelectMany(
                    value => ReadGuildIngredient(value, missingMemberBehavior)
                ),
            OutputUpgradeId = outputUpgradeId.GetValue(),
            ChatLink = chatLink.GetValue()
        };
    }

    private static HammerRecipe ReadHammerRecipe(
        JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<int> outputItemId = new("output_item_id");
        RequiredMember<int> outputItemCount = new("output_item_count");
        RequiredMember<int> minRating = new("min_rating");
        RequiredMember<TimeSpan> timeToCraft = new("time_to_craft_ms");
        RequiredMember<CraftingDisciplineName> disciplines = new("disciplines");
        RequiredMember<RecipeFlag> flags = new("flags");
        RequiredMember<Ingredient> ingredients = new("ingredients");
        RequiredMember<int> id = new("id");
        RequiredMember<string> chatLink = new("chat_link");
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("Hammer"))
                {
                    throw new InvalidOperationException(
                        Strings.InvalidDiscriminator(member.Value.GetString())
                    );
                }
            }
            else if (member.NameEquals(outputItemId.Name))
            {
                outputItemId = outputItemId.From(member.Value);
            }
            else if (member.NameEquals(outputItemCount.Name))
            {
                outputItemCount = outputItemCount.From(member.Value);
            }
            else if (member.NameEquals(minRating.Name))
            {
                minRating = minRating.From(member.Value);
            }
            else if (member.NameEquals(minRating.Name))
            {
                minRating = minRating.From(member.Value);
            }
            else if (member.NameEquals(timeToCraft.Name))
            {
                timeToCraft = timeToCraft.From(member.Value);
            }
            else if (member.NameEquals(disciplines.Name))
            {
                disciplines = disciplines.From(member.Value);
            }
            else if (member.NameEquals(flags.Name))
            {
                flags = flags.From(member.Value);
            }
            else if (member.NameEquals(ingredients.Name))
            {
                ingredients = ingredients.From(member.Value);
            }
            else if (member.NameEquals(id.Name))
            {
                id = id.From(member.Value);
            }
            else if (member.NameEquals(chatLink.Name))
            {
                chatLink = chatLink.From(member.Value);
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new HammerRecipe
        {
            Id = id.GetValue(),
            OutputItemId = outputItemId.GetValue(),
            OutputItemCount = outputItemCount.GetValue(),
            MinRating = minRating.GetValue(),
            TimeToCraft = timeToCraft.Select(value => TimeSpan.FromMilliseconds(value.GetDouble())),
            Disciplines = disciplines.GetValues(missingMemberBehavior),
            Flags = flags.GetValues(missingMemberBehavior),
            Ingredients =
                ingredients.SelectMany(value => ReadIngredient(value, missingMemberBehavior)),
            ChatLink = chatLink.GetValue()
        };
    }

    private static SpearRecipe ReadSpearRecipe(
        JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<int> outputItemId = new("output_item_id");
        RequiredMember<int> outputItemCount = new("output_item_count");
        RequiredMember<int> minRating = new("min_rating");
        RequiredMember<TimeSpan> timeToCraft = new("time_to_craft_ms");
        RequiredMember<CraftingDisciplineName> disciplines = new("disciplines");
        RequiredMember<RecipeFlag> flags = new("flags");
        RequiredMember<Ingredient> ingredients = new("ingredients");
        RequiredMember<int> id = new("id");
        RequiredMember<string> chatLink = new("chat_link");
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("Harpoon"))
                {
                    throw new InvalidOperationException(
                        Strings.InvalidDiscriminator(member.Value.GetString())
                    );
                }
            }
            else if (member.NameEquals(outputItemId.Name))
            {
                outputItemId = outputItemId.From(member.Value);
            }
            else if (member.NameEquals(outputItemCount.Name))
            {
                outputItemCount = outputItemCount.From(member.Value);
            }
            else if (member.NameEquals(minRating.Name))
            {
                minRating = minRating.From(member.Value);
            }
            else if (member.NameEquals(minRating.Name))
            {
                minRating = minRating.From(member.Value);
            }
            else if (member.NameEquals(timeToCraft.Name))
            {
                timeToCraft = timeToCraft.From(member.Value);
            }
            else if (member.NameEquals(disciplines.Name))
            {
                disciplines = disciplines.From(member.Value);
            }
            else if (member.NameEquals(flags.Name))
            {
                flags = flags.From(member.Value);
            }
            else if (member.NameEquals(ingredients.Name))
            {
                ingredients = ingredients.From(member.Value);
            }
            else if (member.NameEquals(id.Name))
            {
                id = id.From(member.Value);
            }
            else if (member.NameEquals(chatLink.Name))
            {
                chatLink = chatLink.From(member.Value);
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new SpearRecipe
        {
            Id = id.GetValue(),
            OutputItemId = outputItemId.GetValue(),
            OutputItemCount = outputItemCount.GetValue(),
            MinRating = minRating.GetValue(),
            TimeToCraft = timeToCraft.Select(value => TimeSpan.FromMilliseconds(value.GetDouble())),
            Disciplines = disciplines.GetValues(missingMemberBehavior),
            Flags = flags.GetValues(missingMemberBehavior),
            Ingredients =
                ingredients.SelectMany(value => ReadIngredient(value, missingMemberBehavior)),
            ChatLink = chatLink.GetValue()
        };
    }

    private static HelmRecipe ReadHelmRecipe(
        JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<int> outputItemId = new("output_item_id");
        RequiredMember<int> outputItemCount = new("output_item_count");
        RequiredMember<int> minRating = new("min_rating");
        RequiredMember<TimeSpan> timeToCraft = new("time_to_craft_ms");
        RequiredMember<CraftingDisciplineName> disciplines = new("disciplines");
        RequiredMember<RecipeFlag> flags = new("flags");
        RequiredMember<Ingredient> ingredients = new("ingredients");
        RequiredMember<int> id = new("id");
        RequiredMember<string> chatLink = new("chat_link");
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("Helm"))
                {
                    throw new InvalidOperationException(
                        Strings.InvalidDiscriminator(member.Value.GetString())
                    );
                }
            }
            else if (member.NameEquals(outputItemId.Name))
            {
                outputItemId = outputItemId.From(member.Value);
            }
            else if (member.NameEquals(outputItemCount.Name))
            {
                outputItemCount = outputItemCount.From(member.Value);
            }
            else if (member.NameEquals(minRating.Name))
            {
                minRating = minRating.From(member.Value);
            }
            else if (member.NameEquals(minRating.Name))
            {
                minRating = minRating.From(member.Value);
            }
            else if (member.NameEquals(timeToCraft.Name))
            {
                timeToCraft = timeToCraft.From(member.Value);
            }
            else if (member.NameEquals(disciplines.Name))
            {
                disciplines = disciplines.From(member.Value);
            }
            else if (member.NameEquals(flags.Name))
            {
                flags = flags.From(member.Value);
            }
            else if (member.NameEquals(ingredients.Name))
            {
                ingredients = ingredients.From(member.Value);
            }
            else if (member.NameEquals(id.Name))
            {
                id = id.From(member.Value);
            }
            else if (member.NameEquals(chatLink.Name))
            {
                chatLink = chatLink.From(member.Value);
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new HelmRecipe
        {
            Id = id.GetValue(),
            OutputItemId = outputItemId.GetValue(),
            OutputItemCount = outputItemCount.GetValue(),
            MinRating = minRating.GetValue(),
            TimeToCraft = timeToCraft.Select(value => TimeSpan.FromMilliseconds(value.GetDouble())),
            Disciplines = disciplines.GetValues(missingMemberBehavior),
            Flags = flags.GetValues(missingMemberBehavior),
            Ingredients =
                ingredients.SelectMany(value => ReadIngredient(value, missingMemberBehavior)),
            ChatLink = chatLink.GetValue()
        };
    }

    private static IngredientCookingRecipe ReadIngredientCookingRecipe(
        JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<int> outputItemId = new("output_item_id");
        RequiredMember<int> outputItemCount = new("output_item_count");
        RequiredMember<int> minRating = new("min_rating");
        RequiredMember<TimeSpan> timeToCraft = new("time_to_craft_ms");
        RequiredMember<CraftingDisciplineName> disciplines = new("disciplines");
        RequiredMember<RecipeFlag> flags = new("flags");
        RequiredMember<Ingredient> ingredients = new("ingredients");
        RequiredMember<int> id = new("id");
        RequiredMember<string> chatLink = new("chat_link");
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("IngredientCooking"))
                {
                    throw new InvalidOperationException(
                        Strings.InvalidDiscriminator(member.Value.GetString())
                    );
                }
            }
            else if (member.NameEquals(outputItemId.Name))
            {
                outputItemId = outputItemId.From(member.Value);
            }
            else if (member.NameEquals(outputItemCount.Name))
            {
                outputItemCount = outputItemCount.From(member.Value);
            }
            else if (member.NameEquals(minRating.Name))
            {
                minRating = minRating.From(member.Value);
            }
            else if (member.NameEquals(minRating.Name))
            {
                minRating = minRating.From(member.Value);
            }
            else if (member.NameEquals(timeToCraft.Name))
            {
                timeToCraft = timeToCraft.From(member.Value);
            }
            else if (member.NameEquals(disciplines.Name))
            {
                disciplines = disciplines.From(member.Value);
            }
            else if (member.NameEquals(flags.Name))
            {
                flags = flags.From(member.Value);
            }
            else if (member.NameEquals(ingredients.Name))
            {
                ingredients = ingredients.From(member.Value);
            }
            else if (member.NameEquals(id.Name))
            {
                id = id.From(member.Value);
            }
            else if (member.NameEquals(chatLink.Name))
            {
                chatLink = chatLink.From(member.Value);
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new IngredientCookingRecipe
        {
            Id = id.GetValue(),
            OutputItemId = outputItemId.GetValue(),
            OutputItemCount = outputItemCount.GetValue(),
            MinRating = minRating.GetValue(),
            TimeToCraft = timeToCraft.Select(value => TimeSpan.FromMilliseconds(value.GetDouble())),
            Disciplines = disciplines.GetValues(missingMemberBehavior),
            Flags = flags.GetValues(missingMemberBehavior),
            Ingredients =
                ingredients.SelectMany(value => ReadIngredient(value, missingMemberBehavior)),
            ChatLink = chatLink.GetValue()
        };
    }

    private static InscriptionRecipe ReadInscriptionRecipe(
        JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<int> outputItemId = new("output_item_id");
        RequiredMember<int> outputItemCount = new("output_item_count");
        RequiredMember<int> minRating = new("min_rating");
        RequiredMember<TimeSpan> timeToCraft = new("time_to_craft_ms");
        RequiredMember<CraftingDisciplineName> disciplines = new("disciplines");
        RequiredMember<RecipeFlag> flags = new("flags");
        RequiredMember<Ingredient> ingredients = new("ingredients");
        RequiredMember<int> id = new("id");
        RequiredMember<string> chatLink = new("chat_link");
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("Inscription"))
                {
                    throw new InvalidOperationException(
                        Strings.InvalidDiscriminator(member.Value.GetString())
                    );
                }
            }
            else if (member.NameEquals(outputItemId.Name))
            {
                outputItemId = outputItemId.From(member.Value);
            }
            else if (member.NameEquals(outputItemCount.Name))
            {
                outputItemCount = outputItemCount.From(member.Value);
            }
            else if (member.NameEquals(minRating.Name))
            {
                minRating = minRating.From(member.Value);
            }
            else if (member.NameEquals(minRating.Name))
            {
                minRating = minRating.From(member.Value);
            }
            else if (member.NameEquals(timeToCraft.Name))
            {
                timeToCraft = timeToCraft.From(member.Value);
            }
            else if (member.NameEquals(disciplines.Name))
            {
                disciplines = disciplines.From(member.Value);
            }
            else if (member.NameEquals(flags.Name))
            {
                flags = flags.From(member.Value);
            }
            else if (member.NameEquals(ingredients.Name))
            {
                ingredients = ingredients.From(member.Value);
            }
            else if (member.NameEquals(id.Name))
            {
                id = id.From(member.Value);
            }
            else if (member.NameEquals(chatLink.Name))
            {
                chatLink = chatLink.From(member.Value);
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new InscriptionRecipe
        {
            Id = id.GetValue(),
            OutputItemId = outputItemId.GetValue(),
            OutputItemCount = outputItemCount.GetValue(),
            MinRating = minRating.GetValue(),
            TimeToCraft = timeToCraft.Select(value => TimeSpan.FromMilliseconds(value.GetDouble())),
            Disciplines = disciplines.GetValues(missingMemberBehavior),
            Flags = flags.GetValues(missingMemberBehavior),
            Ingredients =
                ingredients.SelectMany(value => ReadIngredient(value, missingMemberBehavior)),
            ChatLink = chatLink.GetValue()
        };
    }

    private static InsigniaRecipe ReadInsigniaRecipe(
        JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<int> outputItemId = new("output_item_id");
        RequiredMember<int> outputItemCount = new("output_item_count");
        RequiredMember<int> minRating = new("min_rating");
        RequiredMember<TimeSpan> timeToCraft = new("time_to_craft_ms");
        RequiredMember<CraftingDisciplineName> disciplines = new("disciplines");
        RequiredMember<RecipeFlag> flags = new("flags");
        RequiredMember<Ingredient> ingredients = new("ingredients");
        RequiredMember<int> id = new("id");
        RequiredMember<string> chatLink = new("chat_link");
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("Insignia"))
                {
                    throw new InvalidOperationException(
                        Strings.InvalidDiscriminator(member.Value.GetString())
                    );
                }
            }
            else if (member.NameEquals(outputItemId.Name))
            {
                outputItemId = outputItemId.From(member.Value);
            }
            else if (member.NameEquals(outputItemCount.Name))
            {
                outputItemCount = outputItemCount.From(member.Value);
            }
            else if (member.NameEquals(minRating.Name))
            {
                minRating = minRating.From(member.Value);
            }
            else if (member.NameEquals(minRating.Name))
            {
                minRating = minRating.From(member.Value);
            }
            else if (member.NameEquals(timeToCraft.Name))
            {
                timeToCraft = timeToCraft.From(member.Value);
            }
            else if (member.NameEquals(disciplines.Name))
            {
                disciplines = disciplines.From(member.Value);
            }
            else if (member.NameEquals(flags.Name))
            {
                flags = flags.From(member.Value);
            }
            else if (member.NameEquals(ingredients.Name))
            {
                ingredients = ingredients.From(member.Value);
            }
            else if (member.NameEquals(id.Name))
            {
                id = id.From(member.Value);
            }
            else if (member.NameEquals(chatLink.Name))
            {
                chatLink = chatLink.From(member.Value);
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new InsigniaRecipe
        {
            Id = id.GetValue(),
            OutputItemId = outputItemId.GetValue(),
            OutputItemCount = outputItemCount.GetValue(),
            MinRating = minRating.GetValue(),
            TimeToCraft = timeToCraft.Select(value => TimeSpan.FromMilliseconds(value.GetDouble())),
            Disciplines = disciplines.GetValues(missingMemberBehavior),
            Flags = flags.GetValues(missingMemberBehavior),
            Ingredients =
                ingredients.SelectMany(value => ReadIngredient(value, missingMemberBehavior)),
            ChatLink = chatLink.GetValue()
        };
    }

    private static LegendaryComponentRecipe ReadLegendaryComponentRecipe(
        JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<int> outputItemId = new("output_item_id");
        RequiredMember<int> outputItemCount = new("output_item_count");
        RequiredMember<int> minRating = new("min_rating");
        RequiredMember<TimeSpan> timeToCraft = new("time_to_craft_ms");
        RequiredMember<CraftingDisciplineName> disciplines = new("disciplines");
        RequiredMember<RecipeFlag> flags = new("flags");
        RequiredMember<Ingredient> ingredients = new("ingredients");
        RequiredMember<int> id = new("id");
        RequiredMember<string> chatLink = new("chat_link");
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("LegendaryComponent"))
                {
                    throw new InvalidOperationException(
                        Strings.InvalidDiscriminator(member.Value.GetString())
                    );
                }
            }
            else if (member.NameEquals(outputItemId.Name))
            {
                outputItemId = outputItemId.From(member.Value);
            }
            else if (member.NameEquals(outputItemCount.Name))
            {
                outputItemCount = outputItemCount.From(member.Value);
            }
            else if (member.NameEquals(minRating.Name))
            {
                minRating = minRating.From(member.Value);
            }
            else if (member.NameEquals(minRating.Name))
            {
                minRating = minRating.From(member.Value);
            }
            else if (member.NameEquals(timeToCraft.Name))
            {
                timeToCraft = timeToCraft.From(member.Value);
            }
            else if (member.NameEquals(disciplines.Name))
            {
                disciplines = disciplines.From(member.Value);
            }
            else if (member.NameEquals(flags.Name))
            {
                flags = flags.From(member.Value);
            }
            else if (member.NameEquals(ingredients.Name))
            {
                ingredients = ingredients.From(member.Value);
            }
            else if (member.NameEquals(id.Name))
            {
                id = id.From(member.Value);
            }
            else if (member.NameEquals(chatLink.Name))
            {
                chatLink = chatLink.From(member.Value);
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new LegendaryComponentRecipe
        {
            Id = id.GetValue(),
            OutputItemId = outputItemId.GetValue(),
            OutputItemCount = outputItemCount.GetValue(),
            MinRating = minRating.GetValue(),
            TimeToCraft = timeToCraft.Select(value => TimeSpan.FromMilliseconds(value.GetDouble())),
            Disciplines = disciplines.GetValues(missingMemberBehavior),
            Flags = flags.GetValues(missingMemberBehavior),
            Ingredients =
                ingredients.SelectMany(value => ReadIngredient(value, missingMemberBehavior)),
            ChatLink = chatLink.GetValue()
        };
    }

    private static LeggingsRecipe ReadLeggingsRecipe(
        JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<int> outputItemId = new("output_item_id");
        RequiredMember<int> outputItemCount = new("output_item_count");
        RequiredMember<int> minRating = new("min_rating");
        RequiredMember<TimeSpan> timeToCraft = new("time_to_craft_ms");
        RequiredMember<CraftingDisciplineName> disciplines = new("disciplines");
        RequiredMember<RecipeFlag> flags = new("flags");
        RequiredMember<Ingredient> ingredients = new("ingredients");
        RequiredMember<int> id = new("id");
        RequiredMember<string> chatLink = new("chat_link");
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("Leggings"))
                {
                    throw new InvalidOperationException(
                        Strings.InvalidDiscriminator(member.Value.GetString())
                    );
                }
            }
            else if (member.NameEquals(outputItemId.Name))
            {
                outputItemId = outputItemId.From(member.Value);
            }
            else if (member.NameEquals(outputItemCount.Name))
            {
                outputItemCount = outputItemCount.From(member.Value);
            }
            else if (member.NameEquals(minRating.Name))
            {
                minRating = minRating.From(member.Value);
            }
            else if (member.NameEquals(minRating.Name))
            {
                minRating = minRating.From(member.Value);
            }
            else if (member.NameEquals(timeToCraft.Name))
            {
                timeToCraft = timeToCraft.From(member.Value);
            }
            else if (member.NameEquals(disciplines.Name))
            {
                disciplines = disciplines.From(member.Value);
            }
            else if (member.NameEquals(flags.Name))
            {
                flags = flags.From(member.Value);
            }
            else if (member.NameEquals(ingredients.Name))
            {
                ingredients = ingredients.From(member.Value);
            }
            else if (member.NameEquals(id.Name))
            {
                id = id.From(member.Value);
            }
            else if (member.NameEquals(chatLink.Name))
            {
                chatLink = chatLink.From(member.Value);
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new LeggingsRecipe
        {
            Id = id.GetValue(),
            OutputItemId = outputItemId.GetValue(),
            OutputItemCount = outputItemCount.GetValue(),
            MinRating = minRating.GetValue(),
            TimeToCraft = timeToCraft.Select(value => TimeSpan.FromMilliseconds(value.GetDouble())),
            Disciplines = disciplines.GetValues(missingMemberBehavior),
            Flags = flags.GetValues(missingMemberBehavior),
            Ingredients =
                ingredients.SelectMany(value => ReadIngredient(value, missingMemberBehavior)),
            ChatLink = chatLink.GetValue()
        };
    }

    private static LongbowRecipe ReadLongbowRecipe(
        JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<int> outputItemId = new("output_item_id");
        RequiredMember<int> outputItemCount = new("output_item_count");
        RequiredMember<int> minRating = new("min_rating");
        RequiredMember<TimeSpan> timeToCraft = new("time_to_craft_ms");
        RequiredMember<CraftingDisciplineName> disciplines = new("disciplines");
        RequiredMember<RecipeFlag> flags = new("flags");
        RequiredMember<Ingredient> ingredients = new("ingredients");
        RequiredMember<int> id = new("id");
        RequiredMember<string> chatLink = new("chat_link");
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("LongBow"))
                {
                    throw new InvalidOperationException(
                        Strings.InvalidDiscriminator(member.Value.GetString())
                    );
                }
            }
            else if (member.NameEquals(outputItemId.Name))
            {
                outputItemId = outputItemId.From(member.Value);
            }
            else if (member.NameEquals(outputItemCount.Name))
            {
                outputItemCount = outputItemCount.From(member.Value);
            }
            else if (member.NameEquals(minRating.Name))
            {
                minRating = minRating.From(member.Value);
            }
            else if (member.NameEquals(minRating.Name))
            {
                minRating = minRating.From(member.Value);
            }
            else if (member.NameEquals(timeToCraft.Name))
            {
                timeToCraft = timeToCraft.From(member.Value);
            }
            else if (member.NameEquals(disciplines.Name))
            {
                disciplines = disciplines.From(member.Value);
            }
            else if (member.NameEquals(flags.Name))
            {
                flags = flags.From(member.Value);
            }
            else if (member.NameEquals(ingredients.Name))
            {
                ingredients = ingredients.From(member.Value);
            }
            else if (member.NameEquals(id.Name))
            {
                id = id.From(member.Value);
            }
            else if (member.NameEquals(chatLink.Name))
            {
                chatLink = chatLink.From(member.Value);
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new LongbowRecipe
        {
            Id = id.GetValue(),
            OutputItemId = outputItemId.GetValue(),
            OutputItemCount = outputItemCount.GetValue(),
            MinRating = minRating.GetValue(),
            TimeToCraft = timeToCraft.Select(value => TimeSpan.FromMilliseconds(value.GetDouble())),
            Disciplines = disciplines.GetValues(missingMemberBehavior),
            Flags = flags.GetValues(missingMemberBehavior),
            Ingredients =
                ingredients.SelectMany(value => ReadIngredient(value, missingMemberBehavior)),
            ChatLink = chatLink.GetValue()
        };
    }

    private static MaceRecipe ReadMaceRecipe(
        JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<int> outputItemId = new("output_item_id");
        RequiredMember<int> outputItemCount = new("output_item_count");
        RequiredMember<int> minRating = new("min_rating");
        RequiredMember<TimeSpan> timeToCraft = new("time_to_craft_ms");
        RequiredMember<CraftingDisciplineName> disciplines = new("disciplines");
        RequiredMember<RecipeFlag> flags = new("flags");
        RequiredMember<Ingredient> ingredients = new("ingredients");
        RequiredMember<int> id = new("id");
        RequiredMember<string> chatLink = new("chat_link");
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("Mace"))
                {
                    throw new InvalidOperationException(
                        Strings.InvalidDiscriminator(member.Value.GetString())
                    );
                }
            }
            else if (member.NameEquals(outputItemId.Name))
            {
                outputItemId = outputItemId.From(member.Value);
            }
            else if (member.NameEquals(outputItemCount.Name))
            {
                outputItemCount = outputItemCount.From(member.Value);
            }
            else if (member.NameEquals(minRating.Name))
            {
                minRating = minRating.From(member.Value);
            }
            else if (member.NameEquals(minRating.Name))
            {
                minRating = minRating.From(member.Value);
            }
            else if (member.NameEquals(timeToCraft.Name))
            {
                timeToCraft = timeToCraft.From(member.Value);
            }
            else if (member.NameEquals(disciplines.Name))
            {
                disciplines = disciplines.From(member.Value);
            }
            else if (member.NameEquals(flags.Name))
            {
                flags = flags.From(member.Value);
            }
            else if (member.NameEquals(ingredients.Name))
            {
                ingredients = ingredients.From(member.Value);
            }
            else if (member.NameEquals(id.Name))
            {
                id = id.From(member.Value);
            }
            else if (member.NameEquals(chatLink.Name))
            {
                chatLink = chatLink.From(member.Value);
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new MaceRecipe
        {
            Id = id.GetValue(),
            OutputItemId = outputItemId.GetValue(),
            OutputItemCount = outputItemCount.GetValue(),
            MinRating = minRating.GetValue(),
            TimeToCraft = timeToCraft.Select(value => TimeSpan.FromMilliseconds(value.GetDouble())),
            Disciplines = disciplines.GetValues(missingMemberBehavior),
            Flags = flags.GetValues(missingMemberBehavior),
            Ingredients =
                ingredients.SelectMany(value => ReadIngredient(value, missingMemberBehavior)),
            ChatLink = chatLink.GetValue()
        };
    }

    private static MealRecipe ReadMealRecipe(
        JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<int> outputItemId = new("output_item_id");
        RequiredMember<int> outputItemCount = new("output_item_count");
        RequiredMember<int> minRating = new("min_rating");
        RequiredMember<TimeSpan> timeToCraft = new("time_to_craft_ms");
        RequiredMember<CraftingDisciplineName> disciplines = new("disciplines");
        RequiredMember<RecipeFlag> flags = new("flags");
        RequiredMember<Ingredient> ingredients = new("ingredients");
        RequiredMember<int> id = new("id");
        RequiredMember<string> chatLink = new("chat_link");
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("Meal"))
                {
                    throw new InvalidOperationException(
                        Strings.InvalidDiscriminator(member.Value.GetString())
                    );
                }
            }
            else if (member.NameEquals(outputItemId.Name))
            {
                outputItemId = outputItemId.From(member.Value);
            }
            else if (member.NameEquals(outputItemCount.Name))
            {
                outputItemCount = outputItemCount.From(member.Value);
            }
            else if (member.NameEquals(minRating.Name))
            {
                minRating = minRating.From(member.Value);
            }
            else if (member.NameEquals(minRating.Name))
            {
                minRating = minRating.From(member.Value);
            }
            else if (member.NameEquals(timeToCraft.Name))
            {
                timeToCraft = timeToCraft.From(member.Value);
            }
            else if (member.NameEquals(disciplines.Name))
            {
                disciplines = disciplines.From(member.Value);
            }
            else if (member.NameEquals(flags.Name))
            {
                flags = flags.From(member.Value);
            }
            else if (member.NameEquals(ingredients.Name))
            {
                ingredients = ingredients.From(member.Value);
            }
            else if (member.NameEquals(id.Name))
            {
                id = id.From(member.Value);
            }
            else if (member.NameEquals(chatLink.Name))
            {
                chatLink = chatLink.From(member.Value);
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new MealRecipe
        {
            Id = id.GetValue(),
            OutputItemId = outputItemId.GetValue(),
            OutputItemCount = outputItemCount.GetValue(),
            MinRating = minRating.GetValue(),
            TimeToCraft = timeToCraft.Select(value => TimeSpan.FromMilliseconds(value.GetDouble())),
            Disciplines = disciplines.GetValues(missingMemberBehavior),
            Flags = flags.GetValues(missingMemberBehavior),
            Ingredients =
                ingredients.SelectMany(value => ReadIngredient(value, missingMemberBehavior)),
            ChatLink = chatLink.GetValue()
        };
    }

    private static PistolRecipe ReadPistolRecipe(
        JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<int> outputItemId = new("output_item_id");
        RequiredMember<int> outputItemCount = new("output_item_count");
        RequiredMember<int> minRating = new("min_rating");
        RequiredMember<TimeSpan> timeToCraft = new("time_to_craft_ms");
        RequiredMember<CraftingDisciplineName> disciplines = new("disciplines");
        RequiredMember<RecipeFlag> flags = new("flags");
        RequiredMember<Ingredient> ingredients = new("ingredients");
        RequiredMember<int> id = new("id");
        RequiredMember<string> chatLink = new("chat_link");
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("Pistol"))
                {
                    throw new InvalidOperationException(
                        Strings.InvalidDiscriminator(member.Value.GetString())
                    );
                }
            }
            else if (member.NameEquals(outputItemId.Name))
            {
                outputItemId = outputItemId.From(member.Value);
            }
            else if (member.NameEquals(outputItemCount.Name))
            {
                outputItemCount = outputItemCount.From(member.Value);
            }
            else if (member.NameEquals(minRating.Name))
            {
                minRating = minRating.From(member.Value);
            }
            else if (member.NameEquals(minRating.Name))
            {
                minRating = minRating.From(member.Value);
            }
            else if (member.NameEquals(timeToCraft.Name))
            {
                timeToCraft = timeToCraft.From(member.Value);
            }
            else if (member.NameEquals(disciplines.Name))
            {
                disciplines = disciplines.From(member.Value);
            }
            else if (member.NameEquals(flags.Name))
            {
                flags = flags.From(member.Value);
            }
            else if (member.NameEquals(ingredients.Name))
            {
                ingredients = ingredients.From(member.Value);
            }
            else if (member.NameEquals(id.Name))
            {
                id = id.From(member.Value);
            }
            else if (member.NameEquals(chatLink.Name))
            {
                chatLink = chatLink.From(member.Value);
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new PistolRecipe
        {
            Id = id.GetValue(),
            OutputItemId = outputItemId.GetValue(),
            OutputItemCount = outputItemCount.GetValue(),
            MinRating = minRating.GetValue(),
            TimeToCraft = timeToCraft.Select(value => TimeSpan.FromMilliseconds(value.GetDouble())),
            Disciplines = disciplines.GetValues(missingMemberBehavior),
            Flags = flags.GetValues(missingMemberBehavior),
            Ingredients =
                ingredients.SelectMany(value => ReadIngredient(value, missingMemberBehavior)),
            ChatLink = chatLink.GetValue()
        };
    }

    private static PotionRecipe ReadPotionRecipe(
        JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<int> outputItemId = new("output_item_id");
        RequiredMember<int> outputItemCount = new("output_item_count");
        RequiredMember<int> minRating = new("min_rating");
        RequiredMember<TimeSpan> timeToCraft = new("time_to_craft_ms");
        RequiredMember<CraftingDisciplineName> disciplines = new("disciplines");
        RequiredMember<RecipeFlag> flags = new("flags");
        RequiredMember<Ingredient> ingredients = new("ingredients");
        RequiredMember<int> id = new("id");
        RequiredMember<string> chatLink = new("chat_link");
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("Potion"))
                {
                    throw new InvalidOperationException(
                        Strings.InvalidDiscriminator(member.Value.GetString())
                    );
                }
            }
            else if (member.NameEquals(outputItemId.Name))
            {
                outputItemId = outputItemId.From(member.Value);
            }
            else if (member.NameEquals(outputItemCount.Name))
            {
                outputItemCount = outputItemCount.From(member.Value);
            }
            else if (member.NameEquals(minRating.Name))
            {
                minRating = minRating.From(member.Value);
            }
            else if (member.NameEquals(minRating.Name))
            {
                minRating = minRating.From(member.Value);
            }
            else if (member.NameEquals(timeToCraft.Name))
            {
                timeToCraft = timeToCraft.From(member.Value);
            }
            else if (member.NameEquals(disciplines.Name))
            {
                disciplines = disciplines.From(member.Value);
            }
            else if (member.NameEquals(flags.Name))
            {
                flags = flags.From(member.Value);
            }
            else if (member.NameEquals(ingredients.Name))
            {
                ingredients = ingredients.From(member.Value);
            }
            else if (member.NameEquals(id.Name))
            {
                id = id.From(member.Value);
            }
            else if (member.NameEquals(chatLink.Name))
            {
                chatLink = chatLink.From(member.Value);
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new PotionRecipe
        {
            Id = id.GetValue(),
            OutputItemId = outputItemId.GetValue(),
            OutputItemCount = outputItemCount.GetValue(),
            MinRating = minRating.GetValue(),
            TimeToCraft = timeToCraft.Select(value => TimeSpan.FromMilliseconds(value.GetDouble())),
            Disciplines = disciplines.GetValues(missingMemberBehavior),
            Flags = flags.GetValues(missingMemberBehavior),
            Ingredients =
                ingredients.SelectMany(value => ReadIngredient(value, missingMemberBehavior)),
            ChatLink = chatLink.GetValue()
        };
    }

    private static Ingredient ReadIngredient(
        JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<IngredientKind> type = new("type");
        RequiredMember<int> id = new("id");
        RequiredMember<int> count = new("count");
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(type.Name))
            {
                type = type.From(member.Value);
            }
            else if (member.NameEquals(id.Name))
            {
                id = id.From(member.Value);
            }
            else if (member.NameEquals(count.Name))
            {
                count = count.From(member.Value);
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Ingredient
        {
            Kind = type.GetValue(missingMemberBehavior),
            Id = id.GetValue(),
            Count = count.GetValue()
        };
    }

    private static GuildIngredient ReadGuildIngredient(
        JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<int> upgradeId = new("upgrade_id");
        RequiredMember<int> count = new("count");
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals(upgradeId.Name))
            {
                upgradeId = upgradeId.From(member.Value);
            }
            else if (member.NameEquals(count.Name))
            {
                count = count.From(member.Value);
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new GuildIngredient
        {
            UpgradeId = upgradeId.GetValue(),
            Count = count.GetValue()
        };
    }

    private static RefinementEctoplasmRecipe ReadRefinementEctoplasmRecipe(
        JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<int> outputItemId = new("output_item_id");
        RequiredMember<int> outputItemCount = new("output_item_count");
        RequiredMember<int> minRating = new("min_rating");
        RequiredMember<TimeSpan> timeToCraft = new("time_to_craft_ms");
        RequiredMember<CraftingDisciplineName> disciplines = new("disciplines");
        RequiredMember<RecipeFlag> flags = new("flags");
        RequiredMember<Ingredient> ingredients = new("ingredients");
        RequiredMember<int> id = new("id");
        RequiredMember<string> chatLink = new("chat_link");
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("RefinementEctoplasm"))
                {
                    throw new InvalidOperationException(
                        Strings.InvalidDiscriminator(member.Value.GetString())
                    );
                }
            }
            else if (member.NameEquals(outputItemId.Name))
            {
                outputItemId = outputItemId.From(member.Value);
            }
            else if (member.NameEquals(outputItemCount.Name))
            {
                outputItemCount = outputItemCount.From(member.Value);
            }
            else if (member.NameEquals(minRating.Name))
            {
                minRating = minRating.From(member.Value);
            }
            else if (member.NameEquals(minRating.Name))
            {
                minRating = minRating.From(member.Value);
            }
            else if (member.NameEquals(timeToCraft.Name))
            {
                timeToCraft = timeToCraft.From(member.Value);
            }
            else if (member.NameEquals(disciplines.Name))
            {
                disciplines = disciplines.From(member.Value);
            }
            else if (member.NameEquals(flags.Name))
            {
                flags = flags.From(member.Value);
            }
            else if (member.NameEquals(ingredients.Name))
            {
                ingredients = ingredients.From(member.Value);
            }
            else if (member.NameEquals(id.Name))
            {
                id = id.From(member.Value);
            }
            else if (member.NameEquals(chatLink.Name))
            {
                chatLink = chatLink.From(member.Value);
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new RefinementEctoplasmRecipe
        {
            Id = id.GetValue(),
            OutputItemId = outputItemId.GetValue(),
            OutputItemCount = outputItemCount.GetValue(),
            MinRating = minRating.GetValue(),
            TimeToCraft = timeToCraft.Select(value => TimeSpan.FromMilliseconds(value.GetDouble())),
            Disciplines = disciplines.GetValues(missingMemberBehavior),
            Flags = flags.GetValues(missingMemberBehavior),
            Ingredients =
                ingredients.SelectMany(value => ReadIngredient(value, missingMemberBehavior)),
            ChatLink = chatLink.GetValue()
        };
    }

    private static RefinementObsidianRecipe ReadRefinementObsidianRecipe(
        JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<int> outputItemId = new("output_item_id");
        RequiredMember<int> outputItemCount = new("output_item_count");
        RequiredMember<int> minRating = new("min_rating");
        RequiredMember<TimeSpan> timeToCraft = new("time_to_craft_ms");
        RequiredMember<CraftingDisciplineName> disciplines = new("disciplines");
        RequiredMember<RecipeFlag> flags = new("flags");
        RequiredMember<Ingredient> ingredients = new("ingredients");
        RequiredMember<int> id = new("id");
        RequiredMember<string> chatLink = new("chat_link");
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("RefinementObsidian"))
                {
                    throw new InvalidOperationException(
                        Strings.InvalidDiscriminator(member.Value.GetString())
                    );
                }
            }
            else if (member.NameEquals(outputItemId.Name))
            {
                outputItemId = outputItemId.From(member.Value);
            }
            else if (member.NameEquals(outputItemCount.Name))
            {
                outputItemCount = outputItemCount.From(member.Value);
            }
            else if (member.NameEquals(minRating.Name))
            {
                minRating = minRating.From(member.Value);
            }
            else if (member.NameEquals(minRating.Name))
            {
                minRating = minRating.From(member.Value);
            }
            else if (member.NameEquals(timeToCraft.Name))
            {
                timeToCraft = timeToCraft.From(member.Value);
            }
            else if (member.NameEquals(disciplines.Name))
            {
                disciplines = disciplines.From(member.Value);
            }
            else if (member.NameEquals(flags.Name))
            {
                flags = flags.From(member.Value);
            }
            else if (member.NameEquals(ingredients.Name))
            {
                ingredients = ingredients.From(member.Value);
            }
            else if (member.NameEquals(id.Name))
            {
                id = id.From(member.Value);
            }
            else if (member.NameEquals(chatLink.Name))
            {
                chatLink = chatLink.From(member.Value);
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new RefinementObsidianRecipe
        {
            Id = id.GetValue(),
            OutputItemId = outputItemId.GetValue(),
            OutputItemCount = outputItemCount.GetValue(),
            MinRating = minRating.GetValue(),
            TimeToCraft = timeToCraft.Select(value => TimeSpan.FromMilliseconds(value.GetDouble())),
            Disciplines = disciplines.GetValues(missingMemberBehavior),
            Flags = flags.GetValues(missingMemberBehavior),
            Ingredients =
                ingredients.SelectMany(value => ReadIngredient(value, missingMemberBehavior)),
            ChatLink = chatLink.GetValue()
        };
    }

    private static RefinementRecipe ReadRefinementRecipe(
        JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<int> outputItemId = new("output_item_id");
        RequiredMember<int> outputItemCount = new("output_item_count");
        RequiredMember<int> minRating = new("min_rating");
        RequiredMember<TimeSpan> timeToCraft = new("time_to_craft_ms");
        RequiredMember<CraftingDisciplineName> disciplines = new("disciplines");
        RequiredMember<RecipeFlag> flags = new("flags");
        RequiredMember<Ingredient> ingredients = new("ingredients");
        RequiredMember<int> id = new("id");
        RequiredMember<string> chatLink = new("chat_link");
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("Refinement"))
                {
                    throw new InvalidOperationException(
                        Strings.InvalidDiscriminator(member.Value.GetString())
                    );
                }
            }
            else if (member.NameEquals(outputItemId.Name))
            {
                outputItemId = outputItemId.From(member.Value);
            }
            else if (member.NameEquals(outputItemCount.Name))
            {
                outputItemCount = outputItemCount.From(member.Value);
            }
            else if (member.NameEquals(minRating.Name))
            {
                minRating = minRating.From(member.Value);
            }
            else if (member.NameEquals(minRating.Name))
            {
                minRating = minRating.From(member.Value);
            }
            else if (member.NameEquals(timeToCraft.Name))
            {
                timeToCraft = timeToCraft.From(member.Value);
            }
            else if (member.NameEquals(disciplines.Name))
            {
                disciplines = disciplines.From(member.Value);
            }
            else if (member.NameEquals(flags.Name))
            {
                flags = flags.From(member.Value);
            }
            else if (member.NameEquals(ingredients.Name))
            {
                ingredients = ingredients.From(member.Value);
            }
            else if (member.NameEquals(id.Name))
            {
                id = id.From(member.Value);
            }
            else if (member.NameEquals(chatLink.Name))
            {
                chatLink = chatLink.From(member.Value);
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new RefinementRecipe
        {
            Id = id.GetValue(),
            OutputItemId = outputItemId.GetValue(),
            OutputItemCount = outputItemCount.GetValue(),
            MinRating = minRating.GetValue(),
            TimeToCraft = timeToCraft.Select(value => TimeSpan.FromMilliseconds(value.GetDouble())),
            Disciplines = disciplines.GetValues(missingMemberBehavior),
            Flags = flags.GetValues(missingMemberBehavior),
            Ingredients =
                ingredients.SelectMany(value => ReadIngredient(value, missingMemberBehavior)),
            ChatLink = chatLink.GetValue()
        };
    }

    private static RifleRecipe ReadRifleRecipe(
        JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<int> outputItemId = new("output_item_id");
        RequiredMember<int> outputItemCount = new("output_item_count");
        RequiredMember<int> minRating = new("min_rating");
        RequiredMember<TimeSpan> timeToCraft = new("time_to_craft_ms");
        RequiredMember<CraftingDisciplineName> disciplines = new("disciplines");
        RequiredMember<RecipeFlag> flags = new("flags");
        RequiredMember<Ingredient> ingredients = new("ingredients");
        RequiredMember<int> id = new("id");
        RequiredMember<string> chatLink = new("chat_link");
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("Rifle"))
                {
                    throw new InvalidOperationException(
                        Strings.InvalidDiscriminator(member.Value.GetString())
                    );
                }
            }
            else if (member.NameEquals(outputItemId.Name))
            {
                outputItemId = outputItemId.From(member.Value);
            }
            else if (member.NameEquals(outputItemCount.Name))
            {
                outputItemCount = outputItemCount.From(member.Value);
            }
            else if (member.NameEquals(minRating.Name))
            {
                minRating = minRating.From(member.Value);
            }
            else if (member.NameEquals(minRating.Name))
            {
                minRating = minRating.From(member.Value);
            }
            else if (member.NameEquals(timeToCraft.Name))
            {
                timeToCraft = timeToCraft.From(member.Value);
            }
            else if (member.NameEquals(disciplines.Name))
            {
                disciplines = disciplines.From(member.Value);
            }
            else if (member.NameEquals(flags.Name))
            {
                flags = flags.From(member.Value);
            }
            else if (member.NameEquals(ingredients.Name))
            {
                ingredients = ingredients.From(member.Value);
            }
            else if (member.NameEquals(id.Name))
            {
                id = id.From(member.Value);
            }
            else if (member.NameEquals(chatLink.Name))
            {
                chatLink = chatLink.From(member.Value);
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new RifleRecipe
        {
            Id = id.GetValue(),
            OutputItemId = outputItemId.GetValue(),
            OutputItemCount = outputItemCount.GetValue(),
            MinRating = minRating.GetValue(),
            TimeToCraft = timeToCraft.Select(value => TimeSpan.FromMilliseconds(value.GetDouble())),
            Disciplines = disciplines.GetValues(missingMemberBehavior),
            Flags = flags.GetValues(missingMemberBehavior),
            Ingredients =
                ingredients.SelectMany(value => ReadIngredient(value, missingMemberBehavior)),
            ChatLink = chatLink.GetValue()
        };
    }

    private static RingRecipe ReadRingRecipe(
        JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<int> outputItemId = new("output_item_id");
        RequiredMember<int> outputItemCount = new("output_item_count");
        RequiredMember<int> minRating = new("min_rating");
        RequiredMember<TimeSpan> timeToCraft = new("time_to_craft_ms");
        RequiredMember<CraftingDisciplineName> disciplines = new("disciplines");
        RequiredMember<RecipeFlag> flags = new("flags");
        RequiredMember<Ingredient> ingredients = new("ingredients");
        RequiredMember<int> id = new("id");
        RequiredMember<string> chatLink = new("chat_link");
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("Ring"))
                {
                    throw new InvalidOperationException(
                        Strings.InvalidDiscriminator(member.Value.GetString())
                    );
                }
            }
            else if (member.NameEquals(outputItemId.Name))
            {
                outputItemId = outputItemId.From(member.Value);
            }
            else if (member.NameEquals(outputItemCount.Name))
            {
                outputItemCount = outputItemCount.From(member.Value);
            }
            else if (member.NameEquals(minRating.Name))
            {
                minRating = minRating.From(member.Value);
            }
            else if (member.NameEquals(minRating.Name))
            {
                minRating = minRating.From(member.Value);
            }
            else if (member.NameEquals(timeToCraft.Name))
            {
                timeToCraft = timeToCraft.From(member.Value);
            }
            else if (member.NameEquals(disciplines.Name))
            {
                disciplines = disciplines.From(member.Value);
            }
            else if (member.NameEquals(flags.Name))
            {
                flags = flags.From(member.Value);
            }
            else if (member.NameEquals(ingredients.Name))
            {
                ingredients = ingredients.From(member.Value);
            }
            else if (member.NameEquals(id.Name))
            {
                id = id.From(member.Value);
            }
            else if (member.NameEquals(chatLink.Name))
            {
                chatLink = chatLink.From(member.Value);
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new RingRecipe
        {
            Id = id.GetValue(),
            OutputItemId = outputItemId.GetValue(),
            OutputItemCount = outputItemCount.GetValue(),
            MinRating = minRating.GetValue(),
            TimeToCraft = timeToCraft.Select(value => TimeSpan.FromMilliseconds(value.GetDouble())),
            Disciplines = disciplines.GetValues(missingMemberBehavior),
            Flags = flags.GetValues(missingMemberBehavior),
            Ingredients =
                ingredients.SelectMany(value => ReadIngredient(value, missingMemberBehavior)),
            ChatLink = chatLink.GetValue()
        };
    }

    private static ScepterRecipe ReadScepterRecipe(
        JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<int> outputItemId = new("output_item_id");
        RequiredMember<int> outputItemCount = new("output_item_count");
        RequiredMember<int> minRating = new("min_rating");
        RequiredMember<TimeSpan> timeToCraft = new("time_to_craft_ms");
        RequiredMember<CraftingDisciplineName> disciplines = new("disciplines");
        RequiredMember<RecipeFlag> flags = new("flags");
        RequiredMember<Ingredient> ingredients = new("ingredients");
        RequiredMember<int> id = new("id");
        RequiredMember<string> chatLink = new("chat_link");
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("Scepter"))
                {
                    throw new InvalidOperationException(
                        Strings.InvalidDiscriminator(member.Value.GetString())
                    );
                }
            }
            else if (member.NameEquals(outputItemId.Name))
            {
                outputItemId = outputItemId.From(member.Value);
            }
            else if (member.NameEquals(outputItemCount.Name))
            {
                outputItemCount = outputItemCount.From(member.Value);
            }
            else if (member.NameEquals(minRating.Name))
            {
                minRating = minRating.From(member.Value);
            }
            else if (member.NameEquals(minRating.Name))
            {
                minRating = minRating.From(member.Value);
            }
            else if (member.NameEquals(timeToCraft.Name))
            {
                timeToCraft = timeToCraft.From(member.Value);
            }
            else if (member.NameEquals(disciplines.Name))
            {
                disciplines = disciplines.From(member.Value);
            }
            else if (member.NameEquals(flags.Name))
            {
                flags = flags.From(member.Value);
            }
            else if (member.NameEquals(ingredients.Name))
            {
                ingredients = ingredients.From(member.Value);
            }
            else if (member.NameEquals(id.Name))
            {
                id = id.From(member.Value);
            }
            else if (member.NameEquals(chatLink.Name))
            {
                chatLink = chatLink.From(member.Value);
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new ScepterRecipe
        {
            Id = id.GetValue(),
            OutputItemId = outputItemId.GetValue(),
            OutputItemCount = outputItemCount.GetValue(),
            MinRating = minRating.GetValue(),
            TimeToCraft = timeToCraft.Select(value => TimeSpan.FromMilliseconds(value.GetDouble())),
            Disciplines = disciplines.GetValues(missingMemberBehavior),
            Flags = flags.GetValues(missingMemberBehavior),
            Ingredients =
                ingredients.SelectMany(value => ReadIngredient(value, missingMemberBehavior)),
            ChatLink = chatLink.GetValue()
        };
    }

    private static SeasoningRecipe ReadSeasoningRecipe(
        JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<int> outputItemId = new("output_item_id");
        RequiredMember<int> outputItemCount = new("output_item_count");
        RequiredMember<int> minRating = new("min_rating");
        RequiredMember<TimeSpan> timeToCraft = new("time_to_craft_ms");
        RequiredMember<CraftingDisciplineName> disciplines = new("disciplines");
        RequiredMember<RecipeFlag> flags = new("flags");
        RequiredMember<Ingredient> ingredients = new("ingredients");
        RequiredMember<int> id = new("id");
        RequiredMember<string> chatLink = new("chat_link");
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("Seasoning"))
                {
                    throw new InvalidOperationException(
                        Strings.InvalidDiscriminator(member.Value.GetString())
                    );
                }
            }
            else if (member.NameEquals(outputItemId.Name))
            {
                outputItemId = outputItemId.From(member.Value);
            }
            else if (member.NameEquals(outputItemCount.Name))
            {
                outputItemCount = outputItemCount.From(member.Value);
            }
            else if (member.NameEquals(minRating.Name))
            {
                minRating = minRating.From(member.Value);
            }
            else if (member.NameEquals(minRating.Name))
            {
                minRating = minRating.From(member.Value);
            }
            else if (member.NameEquals(timeToCraft.Name))
            {
                timeToCraft = timeToCraft.From(member.Value);
            }
            else if (member.NameEquals(disciplines.Name))
            {
                disciplines = disciplines.From(member.Value);
            }
            else if (member.NameEquals(flags.Name))
            {
                flags = flags.From(member.Value);
            }
            else if (member.NameEquals(ingredients.Name))
            {
                ingredients = ingredients.From(member.Value);
            }
            else if (member.NameEquals(id.Name))
            {
                id = id.From(member.Value);
            }
            else if (member.NameEquals(chatLink.Name))
            {
                chatLink = chatLink.From(member.Value);
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new SeasoningRecipe
        {
            Id = id.GetValue(),
            OutputItemId = outputItemId.GetValue(),
            OutputItemCount = outputItemCount.GetValue(),
            MinRating = minRating.GetValue(),
            TimeToCraft = timeToCraft.Select(value => TimeSpan.FromMilliseconds(value.GetDouble())),
            Disciplines = disciplines.GetValues(missingMemberBehavior),
            Flags = flags.GetValues(missingMemberBehavior),
            Ingredients =
                ingredients.SelectMany(value => ReadIngredient(value, missingMemberBehavior)),
            ChatLink = chatLink.GetValue()
        };
    }

    private static ShieldRecipe ReadShieldRecipe(
        JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<int> outputItemId = new("output_item_id");
        RequiredMember<int> outputItemCount = new("output_item_count");
        RequiredMember<int> minRating = new("min_rating");
        RequiredMember<TimeSpan> timeToCraft = new("time_to_craft_ms");
        RequiredMember<CraftingDisciplineName> disciplines = new("disciplines");
        RequiredMember<RecipeFlag> flags = new("flags");
        RequiredMember<Ingredient> ingredients = new("ingredients");
        RequiredMember<int> id = new("id");
        RequiredMember<string> chatLink = new("chat_link");
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("Shield"))
                {
                    throw new InvalidOperationException(
                        Strings.InvalidDiscriminator(member.Value.GetString())
                    );
                }
            }
            else if (member.NameEquals(outputItemId.Name))
            {
                outputItemId = outputItemId.From(member.Value);
            }
            else if (member.NameEquals(outputItemCount.Name))
            {
                outputItemCount = outputItemCount.From(member.Value);
            }
            else if (member.NameEquals(minRating.Name))
            {
                minRating = minRating.From(member.Value);
            }
            else if (member.NameEquals(minRating.Name))
            {
                minRating = minRating.From(member.Value);
            }
            else if (member.NameEquals(timeToCraft.Name))
            {
                timeToCraft = timeToCraft.From(member.Value);
            }
            else if (member.NameEquals(disciplines.Name))
            {
                disciplines = disciplines.From(member.Value);
            }
            else if (member.NameEquals(flags.Name))
            {
                flags = flags.From(member.Value);
            }
            else if (member.NameEquals(ingredients.Name))
            {
                ingredients = ingredients.From(member.Value);
            }
            else if (member.NameEquals(id.Name))
            {
                id = id.From(member.Value);
            }
            else if (member.NameEquals(chatLink.Name))
            {
                chatLink = chatLink.From(member.Value);
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new ShieldRecipe
        {
            Id = id.GetValue(),
            OutputItemId = outputItemId.GetValue(),
            OutputItemCount = outputItemCount.GetValue(),
            MinRating = minRating.GetValue(),
            TimeToCraft = timeToCraft.Select(value => TimeSpan.FromMilliseconds(value.GetDouble())),
            Disciplines = disciplines.GetValues(missingMemberBehavior),
            Flags = flags.GetValues(missingMemberBehavior),
            Ingredients =
                ingredients.SelectMany(value => ReadIngredient(value, missingMemberBehavior)),
            ChatLink = chatLink.GetValue()
        };
    }

    private static ShortbowRecipe ReadShortbowRecipe(
        JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<int> outputItemId = new("output_item_id");
        RequiredMember<int> outputItemCount = new("output_item_count");
        RequiredMember<int> minRating = new("min_rating");
        RequiredMember<TimeSpan> timeToCraft = new("time_to_craft_ms");
        RequiredMember<CraftingDisciplineName> disciplines = new("disciplines");
        RequiredMember<RecipeFlag> flags = new("flags");
        RequiredMember<Ingredient> ingredients = new("ingredients");
        RequiredMember<int> id = new("id");
        RequiredMember<string> chatLink = new("chat_link");
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("ShortBow"))
                {
                    throw new InvalidOperationException(
                        Strings.InvalidDiscriminator(member.Value.GetString())
                    );
                }
            }
            else if (member.NameEquals(outputItemId.Name))
            {
                outputItemId = outputItemId.From(member.Value);
            }
            else if (member.NameEquals(outputItemCount.Name))
            {
                outputItemCount = outputItemCount.From(member.Value);
            }
            else if (member.NameEquals(minRating.Name))
            {
                minRating = minRating.From(member.Value);
            }
            else if (member.NameEquals(minRating.Name))
            {
                minRating = minRating.From(member.Value);
            }
            else if (member.NameEquals(timeToCraft.Name))
            {
                timeToCraft = timeToCraft.From(member.Value);
            }
            else if (member.NameEquals(disciplines.Name))
            {
                disciplines = disciplines.From(member.Value);
            }
            else if (member.NameEquals(flags.Name))
            {
                flags = flags.From(member.Value);
            }
            else if (member.NameEquals(ingredients.Name))
            {
                ingredients = ingredients.From(member.Value);
            }
            else if (member.NameEquals(id.Name))
            {
                id = id.From(member.Value);
            }
            else if (member.NameEquals(chatLink.Name))
            {
                chatLink = chatLink.From(member.Value);
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new ShortbowRecipe
        {
            Id = id.GetValue(),
            OutputItemId = outputItemId.GetValue(),
            OutputItemCount = outputItemCount.GetValue(),
            MinRating = minRating.GetValue(),
            TimeToCraft = timeToCraft.Select(value => TimeSpan.FromMilliseconds(value.GetDouble())),
            Disciplines = disciplines.GetValues(missingMemberBehavior),
            Flags = flags.GetValues(missingMemberBehavior),
            Ingredients =
                ingredients.SelectMany(value => ReadIngredient(value, missingMemberBehavior)),
            ChatLink = chatLink.GetValue()
        };
    }

    private static ShouldersRecipe ReadShouldersRecipe(
        JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<int> outputItemId = new("output_item_id");
        RequiredMember<int> outputItemCount = new("output_item_count");
        RequiredMember<int> minRating = new("min_rating");
        RequiredMember<TimeSpan> timeToCraft = new("time_to_craft_ms");
        RequiredMember<CraftingDisciplineName> disciplines = new("disciplines");
        RequiredMember<RecipeFlag> flags = new("flags");
        RequiredMember<Ingredient> ingredients = new("ingredients");
        RequiredMember<int> id = new("id");
        RequiredMember<string> chatLink = new("chat_link");
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("Shoulders"))
                {
                    throw new InvalidOperationException(
                        Strings.InvalidDiscriminator(member.Value.GetString())
                    );
                }
            }
            else if (member.NameEquals(outputItemId.Name))
            {
                outputItemId = outputItemId.From(member.Value);
            }
            else if (member.NameEquals(outputItemCount.Name))
            {
                outputItemCount = outputItemCount.From(member.Value);
            }
            else if (member.NameEquals(minRating.Name))
            {
                minRating = minRating.From(member.Value);
            }
            else if (member.NameEquals(minRating.Name))
            {
                minRating = minRating.From(member.Value);
            }
            else if (member.NameEquals(timeToCraft.Name))
            {
                timeToCraft = timeToCraft.From(member.Value);
            }
            else if (member.NameEquals(disciplines.Name))
            {
                disciplines = disciplines.From(member.Value);
            }
            else if (member.NameEquals(flags.Name))
            {
                flags = flags.From(member.Value);
            }
            else if (member.NameEquals(ingredients.Name))
            {
                ingredients = ingredients.From(member.Value);
            }
            else if (member.NameEquals(id.Name))
            {
                id = id.From(member.Value);
            }
            else if (member.NameEquals(chatLink.Name))
            {
                chatLink = chatLink.From(member.Value);
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new ShouldersRecipe
        {
            Id = id.GetValue(),
            OutputItemId = outputItemId.GetValue(),
            OutputItemCount = outputItemCount.GetValue(),
            MinRating = minRating.GetValue(),
            TimeToCraft = timeToCraft.Select(value => TimeSpan.FromMilliseconds(value.GetDouble())),
            Disciplines = disciplines.GetValues(missingMemberBehavior),
            Flags = flags.GetValues(missingMemberBehavior),
            Ingredients =
                ingredients.SelectMany(value => ReadIngredient(value, missingMemberBehavior)),
            ChatLink = chatLink.GetValue()
        };
    }

    private static SnackRecipe ReadSnackRecipe(
        JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<int> outputItemId = new("output_item_id");
        RequiredMember<int> outputItemCount = new("output_item_count");
        RequiredMember<int> minRating = new("min_rating");
        RequiredMember<TimeSpan> timeToCraft = new("time_to_craft_ms");
        RequiredMember<CraftingDisciplineName> disciplines = new("disciplines");
        RequiredMember<RecipeFlag> flags = new("flags");
        RequiredMember<Ingredient> ingredients = new("ingredients");
        RequiredMember<int> id = new("id");
        RequiredMember<string> chatLink = new("chat_link");
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("Snack"))
                {
                    throw new InvalidOperationException(
                        Strings.InvalidDiscriminator(member.Value.GetString())
                    );
                }
            }
            else if (member.NameEquals(outputItemId.Name))
            {
                outputItemId = outputItemId.From(member.Value);
            }
            else if (member.NameEquals(outputItemCount.Name))
            {
                outputItemCount = outputItemCount.From(member.Value);
            }
            else if (member.NameEquals(minRating.Name))
            {
                minRating = minRating.From(member.Value);
            }
            else if (member.NameEquals(minRating.Name))
            {
                minRating = minRating.From(member.Value);
            }
            else if (member.NameEquals(timeToCraft.Name))
            {
                timeToCraft = timeToCraft.From(member.Value);
            }
            else if (member.NameEquals(disciplines.Name))
            {
                disciplines = disciplines.From(member.Value);
            }
            else if (member.NameEquals(flags.Name))
            {
                flags = flags.From(member.Value);
            }
            else if (member.NameEquals(ingredients.Name))
            {
                ingredients = ingredients.From(member.Value);
            }
            else if (member.NameEquals(id.Name))
            {
                id = id.From(member.Value);
            }
            else if (member.NameEquals(chatLink.Name))
            {
                chatLink = chatLink.From(member.Value);
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new SnackRecipe
        {
            Id = id.GetValue(),
            OutputItemId = outputItemId.GetValue(),
            OutputItemCount = outputItemCount.GetValue(),
            MinRating = minRating.GetValue(),
            TimeToCraft = timeToCraft.Select(value => TimeSpan.FromMilliseconds(value.GetDouble())),
            Disciplines = disciplines.GetValues(missingMemberBehavior),
            Flags = flags.GetValues(missingMemberBehavior),
            Ingredients =
                ingredients.SelectMany(value => ReadIngredient(value, missingMemberBehavior)),
            ChatLink = chatLink.GetValue()
        };
    }

    private static SoupRecipe ReadSoupRecipe(
        JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<int> outputItemId = new("output_item_id");
        RequiredMember<int> outputItemCount = new("output_item_count");
        RequiredMember<int> minRating = new("min_rating");
        RequiredMember<TimeSpan> timeToCraft = new("time_to_craft_ms");
        RequiredMember<CraftingDisciplineName> disciplines = new("disciplines");
        RequiredMember<RecipeFlag> flags = new("flags");
        RequiredMember<Ingredient> ingredients = new("ingredients");
        RequiredMember<int> id = new("id");
        RequiredMember<string> chatLink = new("chat_link");
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("Soup"))
                {
                    throw new InvalidOperationException(
                        Strings.InvalidDiscriminator(member.Value.GetString())
                    );
                }
            }
            else if (member.NameEquals(outputItemId.Name))
            {
                outputItemId = outputItemId.From(member.Value);
            }
            else if (member.NameEquals(outputItemCount.Name))
            {
                outputItemCount = outputItemCount.From(member.Value);
            }
            else if (member.NameEquals(minRating.Name))
            {
                minRating = minRating.From(member.Value);
            }
            else if (member.NameEquals(minRating.Name))
            {
                minRating = minRating.From(member.Value);
            }
            else if (member.NameEquals(timeToCraft.Name))
            {
                timeToCraft = timeToCraft.From(member.Value);
            }
            else if (member.NameEquals(disciplines.Name))
            {
                disciplines = disciplines.From(member.Value);
            }
            else if (member.NameEquals(flags.Name))
            {
                flags = flags.From(member.Value);
            }
            else if (member.NameEquals(ingredients.Name))
            {
                ingredients = ingredients.From(member.Value);
            }
            else if (member.NameEquals(id.Name))
            {
                id = id.From(member.Value);
            }
            else if (member.NameEquals(chatLink.Name))
            {
                chatLink = chatLink.From(member.Value);
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new SoupRecipe
        {
            Id = id.GetValue(),
            OutputItemId = outputItemId.GetValue(),
            OutputItemCount = outputItemCount.GetValue(),
            MinRating = minRating.GetValue(),
            TimeToCraft = timeToCraft.Select(value => TimeSpan.FromMilliseconds(value.GetDouble())),
            Disciplines = disciplines.GetValues(missingMemberBehavior),
            Flags = flags.GetValues(missingMemberBehavior),
            Ingredients =
                ingredients.SelectMany(value => ReadIngredient(value, missingMemberBehavior)),
            ChatLink = chatLink.GetValue()
        };
    }

    private static HarpoonGunRecipe ReadHarpoonGunRecipe(
        JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<int> outputItemId = new("output_item_id");
        RequiredMember<int> outputItemCount = new("output_item_count");
        RequiredMember<int> minRating = new("min_rating");
        RequiredMember<TimeSpan> timeToCraft = new("time_to_craft_ms");
        RequiredMember<CraftingDisciplineName> disciplines = new("disciplines");
        RequiredMember<RecipeFlag> flags = new("flags");
        RequiredMember<Ingredient> ingredients = new("ingredients");
        RequiredMember<int> id = new("id");
        RequiredMember<string> chatLink = new("chat_link");
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("Speargun"))
                {
                    throw new InvalidOperationException(
                        Strings.InvalidDiscriminator(member.Value.GetString())
                    );
                }
            }
            else if (member.NameEquals(outputItemId.Name))
            {
                outputItemId = outputItemId.From(member.Value);
            }
            else if (member.NameEquals(outputItemCount.Name))
            {
                outputItemCount = outputItemCount.From(member.Value);
            }
            else if (member.NameEquals(minRating.Name))
            {
                minRating = minRating.From(member.Value);
            }
            else if (member.NameEquals(minRating.Name))
            {
                minRating = minRating.From(member.Value);
            }
            else if (member.NameEquals(timeToCraft.Name))
            {
                timeToCraft = timeToCraft.From(member.Value);
            }
            else if (member.NameEquals(disciplines.Name))
            {
                disciplines = disciplines.From(member.Value);
            }
            else if (member.NameEquals(flags.Name))
            {
                flags = flags.From(member.Value);
            }
            else if (member.NameEquals(ingredients.Name))
            {
                ingredients = ingredients.From(member.Value);
            }
            else if (member.NameEquals(id.Name))
            {
                id = id.From(member.Value);
            }
            else if (member.NameEquals(chatLink.Name))
            {
                chatLink = chatLink.From(member.Value);
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new HarpoonGunRecipe
        {
            Id = id.GetValue(),
            OutputItemId = outputItemId.GetValue(),
            OutputItemCount = outputItemCount.GetValue(),
            MinRating = minRating.GetValue(),
            TimeToCraft = timeToCraft.Select(value => TimeSpan.FromMilliseconds(value.GetDouble())),
            Disciplines = disciplines.GetValues(missingMemberBehavior),
            Flags = flags.GetValues(missingMemberBehavior),
            Ingredients =
                ingredients.SelectMany(value => ReadIngredient(value, missingMemberBehavior)),
            ChatLink = chatLink.GetValue()
        };
    }

    private static StaffRecipe ReadStaffRecipe(
        JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<int> outputItemId = new("output_item_id");
        RequiredMember<int> outputItemCount = new("output_item_count");
        RequiredMember<int> minRating = new("min_rating");
        RequiredMember<TimeSpan> timeToCraft = new("time_to_craft_ms");
        RequiredMember<CraftingDisciplineName> disciplines = new("disciplines");
        RequiredMember<RecipeFlag> flags = new("flags");
        RequiredMember<Ingredient> ingredients = new("ingredients");
        RequiredMember<int> id = new("id");
        RequiredMember<string> chatLink = new("chat_link");
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("Staff"))
                {
                    throw new InvalidOperationException(
                        Strings.InvalidDiscriminator(member.Value.GetString())
                    );
                }
            }
            else if (member.NameEquals(outputItemId.Name))
            {
                outputItemId = outputItemId.From(member.Value);
            }
            else if (member.NameEquals(outputItemCount.Name))
            {
                outputItemCount = outputItemCount.From(member.Value);
            }
            else if (member.NameEquals(minRating.Name))
            {
                minRating = minRating.From(member.Value);
            }
            else if (member.NameEquals(minRating.Name))
            {
                minRating = minRating.From(member.Value);
            }
            else if (member.NameEquals(timeToCraft.Name))
            {
                timeToCraft = timeToCraft.From(member.Value);
            }
            else if (member.NameEquals(disciplines.Name))
            {
                disciplines = disciplines.From(member.Value);
            }
            else if (member.NameEquals(flags.Name))
            {
                flags = flags.From(member.Value);
            }
            else if (member.NameEquals(ingredients.Name))
            {
                ingredients = ingredients.From(member.Value);
            }
            else if (member.NameEquals(id.Name))
            {
                id = id.From(member.Value);
            }
            else if (member.NameEquals(chatLink.Name))
            {
                chatLink = chatLink.From(member.Value);
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new StaffRecipe
        {
            Id = id.GetValue(),
            OutputItemId = outputItemId.GetValue(),
            OutputItemCount = outputItemCount.GetValue(),
            MinRating = minRating.GetValue(),
            TimeToCraft = timeToCraft.Select(value => TimeSpan.FromMilliseconds(value.GetDouble())),
            Disciplines = disciplines.GetValues(missingMemberBehavior),
            Flags = flags.GetValues(missingMemberBehavior),
            Ingredients =
                ingredients.SelectMany(value => ReadIngredient(value, missingMemberBehavior)),
            ChatLink = chatLink.GetValue()
        };
    }

    private static SwordRecipe ReadSwordRecipe(
        JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<int> outputItemId = new("output_item_id");
        RequiredMember<int> outputItemCount = new("output_item_count");
        RequiredMember<int> minRating = new("min_rating");
        RequiredMember<TimeSpan> timeToCraft = new("time_to_craft_ms");
        RequiredMember<CraftingDisciplineName> disciplines = new("disciplines");
        RequiredMember<RecipeFlag> flags = new("flags");
        RequiredMember<Ingredient> ingredients = new("ingredients");
        RequiredMember<int> id = new("id");
        RequiredMember<string> chatLink = new("chat_link");
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("Sword"))
                {
                    throw new InvalidOperationException(
                        Strings.InvalidDiscriminator(member.Value.GetString())
                    );
                }
            }
            else if (member.NameEquals(outputItemId.Name))
            {
                outputItemId = outputItemId.From(member.Value);
            }
            else if (member.NameEquals(outputItemCount.Name))
            {
                outputItemCount = outputItemCount.From(member.Value);
            }
            else if (member.NameEquals(minRating.Name))
            {
                minRating = minRating.From(member.Value);
            }
            else if (member.NameEquals(minRating.Name))
            {
                minRating = minRating.From(member.Value);
            }
            else if (member.NameEquals(timeToCraft.Name))
            {
                timeToCraft = timeToCraft.From(member.Value);
            }
            else if (member.NameEquals(disciplines.Name))
            {
                disciplines = disciplines.From(member.Value);
            }
            else if (member.NameEquals(flags.Name))
            {
                flags = flags.From(member.Value);
            }
            else if (member.NameEquals(ingredients.Name))
            {
                ingredients = ingredients.From(member.Value);
            }
            else if (member.NameEquals(id.Name))
            {
                id = id.From(member.Value);
            }
            else if (member.NameEquals(chatLink.Name))
            {
                chatLink = chatLink.From(member.Value);
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new SwordRecipe
        {
            Id = id.GetValue(),
            OutputItemId = outputItemId.GetValue(),
            OutputItemCount = outputItemCount.GetValue(),
            MinRating = minRating.GetValue(),
            TimeToCraft = timeToCraft.Select(value => TimeSpan.FromMilliseconds(value.GetDouble())),
            Disciplines = disciplines.GetValues(missingMemberBehavior),
            Flags = flags.GetValues(missingMemberBehavior),
            Ingredients =
                ingredients.SelectMany(value => ReadIngredient(value, missingMemberBehavior)),
            ChatLink = chatLink.GetValue()
        };
    }

    private static TorchRecipe ReadTorchRecipe(
        JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<int> outputItemId = new("output_item_id");
        RequiredMember<int> outputItemCount = new("output_item_count");
        RequiredMember<int> minRating = new("min_rating");
        RequiredMember<TimeSpan> timeToCraft = new("time_to_craft_ms");
        RequiredMember<CraftingDisciplineName> disciplines = new("disciplines");
        RequiredMember<RecipeFlag> flags = new("flags");
        RequiredMember<Ingredient> ingredients = new("ingredients");
        RequiredMember<int> id = new("id");
        RequiredMember<string> chatLink = new("chat_link");
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("Torch"))
                {
                    throw new InvalidOperationException(
                        Strings.InvalidDiscriminator(member.Value.GetString())
                    );
                }
            }
            else if (member.NameEquals(outputItemId.Name))
            {
                outputItemId = outputItemId.From(member.Value);
            }
            else if (member.NameEquals(outputItemCount.Name))
            {
                outputItemCount = outputItemCount.From(member.Value);
            }
            else if (member.NameEquals(minRating.Name))
            {
                minRating = minRating.From(member.Value);
            }
            else if (member.NameEquals(minRating.Name))
            {
                minRating = minRating.From(member.Value);
            }
            else if (member.NameEquals(timeToCraft.Name))
            {
                timeToCraft = timeToCraft.From(member.Value);
            }
            else if (member.NameEquals(disciplines.Name))
            {
                disciplines = disciplines.From(member.Value);
            }
            else if (member.NameEquals(flags.Name))
            {
                flags = flags.From(member.Value);
            }
            else if (member.NameEquals(ingredients.Name))
            {
                ingredients = ingredients.From(member.Value);
            }
            else if (member.NameEquals(id.Name))
            {
                id = id.From(member.Value);
            }
            else if (member.NameEquals(chatLink.Name))
            {
                chatLink = chatLink.From(member.Value);
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new TorchRecipe
        {
            Id = id.GetValue(),
            OutputItemId = outputItemId.GetValue(),
            OutputItemCount = outputItemCount.GetValue(),
            MinRating = minRating.GetValue(),
            TimeToCraft = timeToCraft.Select(value => TimeSpan.FromMilliseconds(value.GetDouble())),
            Disciplines = disciplines.GetValues(missingMemberBehavior),
            Flags = flags.GetValues(missingMemberBehavior),
            Ingredients =
                ingredients.SelectMany(value => ReadIngredient(value, missingMemberBehavior)),
            ChatLink = chatLink.GetValue()
        };
    }

    private static TridentRecipe ReadTridentRecipe(
        JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<int> outputItemId = new("output_item_id");
        RequiredMember<int> outputItemCount = new("output_item_count");
        RequiredMember<int> minRating = new("min_rating");
        RequiredMember<TimeSpan> timeToCraft = new("time_to_craft_ms");
        RequiredMember<CraftingDisciplineName> disciplines = new("disciplines");
        RequiredMember<RecipeFlag> flags = new("flags");
        RequiredMember<Ingredient> ingredients = new("ingredients");
        RequiredMember<int> id = new("id");
        RequiredMember<string> chatLink = new("chat_link");
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("Trident"))
                {
                    throw new InvalidOperationException(
                        Strings.InvalidDiscriminator(member.Value.GetString())
                    );
                }
            }
            else if (member.NameEquals(outputItemId.Name))
            {
                outputItemId = outputItemId.From(member.Value);
            }
            else if (member.NameEquals(outputItemCount.Name))
            {
                outputItemCount = outputItemCount.From(member.Value);
            }
            else if (member.NameEquals(minRating.Name))
            {
                minRating = minRating.From(member.Value);
            }
            else if (member.NameEquals(minRating.Name))
            {
                minRating = minRating.From(member.Value);
            }
            else if (member.NameEquals(timeToCraft.Name))
            {
                timeToCraft = timeToCraft.From(member.Value);
            }
            else if (member.NameEquals(disciplines.Name))
            {
                disciplines = disciplines.From(member.Value);
            }
            else if (member.NameEquals(flags.Name))
            {
                flags = flags.From(member.Value);
            }
            else if (member.NameEquals(ingredients.Name))
            {
                ingredients = ingredients.From(member.Value);
            }
            else if (member.NameEquals(id.Name))
            {
                id = id.From(member.Value);
            }
            else if (member.NameEquals(chatLink.Name))
            {
                chatLink = chatLink.From(member.Value);
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new TridentRecipe
        {
            Id = id.GetValue(),
            OutputItemId = outputItemId.GetValue(),
            OutputItemCount = outputItemCount.GetValue(),
            MinRating = minRating.GetValue(),
            TimeToCraft = timeToCraft.Select(value => TimeSpan.FromMilliseconds(value.GetDouble())),
            Disciplines = disciplines.GetValues(missingMemberBehavior),
            Flags = flags.GetValues(missingMemberBehavior),
            Ingredients =
                ingredients.SelectMany(value => ReadIngredient(value, missingMemberBehavior)),
            ChatLink = chatLink.GetValue()
        };
    }

    private static UpgradeComponentRecipe ReadUpgradeComponentRecipe(
        JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<int> outputItemId = new("output_item_id");
        RequiredMember<int> outputItemCount = new("output_item_count");
        RequiredMember<int> minRating = new("min_rating");
        RequiredMember<TimeSpan> timeToCraft = new("time_to_craft_ms");
        RequiredMember<CraftingDisciplineName> disciplines = new("disciplines");
        RequiredMember<RecipeFlag> flags = new("flags");
        RequiredMember<Ingredient> ingredients = new("ingredients");
        RequiredMember<int> id = new("id");
        RequiredMember<string> chatLink = new("chat_link");
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("UpgradeComponent"))
                {
                    throw new InvalidOperationException(
                        Strings.InvalidDiscriminator(member.Value.GetString())
                    );
                }
            }
            else if (member.NameEquals(outputItemId.Name))
            {
                outputItemId = outputItemId.From(member.Value);
            }
            else if (member.NameEquals(outputItemCount.Name))
            {
                outputItemCount = outputItemCount.From(member.Value);
            }
            else if (member.NameEquals(minRating.Name))
            {
                minRating = minRating.From(member.Value);
            }
            else if (member.NameEquals(minRating.Name))
            {
                minRating = minRating.From(member.Value);
            }
            else if (member.NameEquals(timeToCraft.Name))
            {
                timeToCraft = timeToCraft.From(member.Value);
            }
            else if (member.NameEquals(disciplines.Name))
            {
                disciplines = disciplines.From(member.Value);
            }
            else if (member.NameEquals(flags.Name))
            {
                flags = flags.From(member.Value);
            }
            else if (member.NameEquals(ingredients.Name))
            {
                ingredients = ingredients.From(member.Value);
            }
            else if (member.NameEquals(id.Name))
            {
                id = id.From(member.Value);
            }
            else if (member.NameEquals(chatLink.Name))
            {
                chatLink = chatLink.From(member.Value);
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new UpgradeComponentRecipe
        {
            Id = id.GetValue(),
            OutputItemId = outputItemId.GetValue(),
            OutputItemCount = outputItemCount.GetValue(),
            MinRating = minRating.GetValue(),
            TimeToCraft = timeToCraft.Select(value => TimeSpan.FromMilliseconds(value.GetDouble())),
            Disciplines = disciplines.GetValues(missingMemberBehavior),
            Flags = flags.GetValues(missingMemberBehavior),
            Ingredients =
                ingredients.SelectMany(value => ReadIngredient(value, missingMemberBehavior)),
            ChatLink = chatLink.GetValue()
        };
    }

    private static WarhornRecipe ReadWarhornRecipe(
        JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        RequiredMember<int> outputItemId = new("output_item_id");
        RequiredMember<int> outputItemCount = new("output_item_count");
        RequiredMember<int> minRating = new("min_rating");
        RequiredMember<TimeSpan> timeToCraft = new("time_to_craft_ms");
        RequiredMember<CraftingDisciplineName> disciplines = new("disciplines");
        RequiredMember<RecipeFlag> flags = new("flags");
        RequiredMember<Ingredient> ingredients = new("ingredients");
        RequiredMember<int> id = new("id");
        RequiredMember<string> chatLink = new("chat_link");
        foreach (var member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (!member.Value.ValueEquals("Warhorn"))
                {
                    throw new InvalidOperationException(
                        Strings.InvalidDiscriminator(member.Value.GetString())
                    );
                }
            }
            else if (member.NameEquals(outputItemId.Name))
            {
                outputItemId = outputItemId.From(member.Value);
            }
            else if (member.NameEquals(outputItemCount.Name))
            {
                outputItemCount = outputItemCount.From(member.Value);
            }
            else if (member.NameEquals(minRating.Name))
            {
                minRating = minRating.From(member.Value);
            }
            else if (member.NameEquals(minRating.Name))
            {
                minRating = minRating.From(member.Value);
            }
            else if (member.NameEquals(timeToCraft.Name))
            {
                timeToCraft = timeToCraft.From(member.Value);
            }
            else if (member.NameEquals(disciplines.Name))
            {
                disciplines = disciplines.From(member.Value);
            }
            else if (member.NameEquals(flags.Name))
            {
                flags = flags.From(member.Value);
            }
            else if (member.NameEquals(ingredients.Name))
            {
                ingredients = ingredients.From(member.Value);
            }
            else if (member.NameEquals(id.Name))
            {
                id = id.From(member.Value);
            }
            else if (member.NameEquals(chatLink.Name))
            {
                chatLink = chatLink.From(member.Value);
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new WarhornRecipe
        {
            Id = id.GetValue(),
            OutputItemId = outputItemId.GetValue(),
            OutputItemCount = outputItemCount.GetValue(),
            MinRating = minRating.GetValue(),
            TimeToCraft = timeToCraft.Select(value => TimeSpan.FromMilliseconds(value.GetDouble())),
            Disciplines = disciplines.GetValues(missingMemberBehavior),
            Flags = flags.GetValues(missingMemberBehavior),
            Ingredients =
                ingredients.SelectMany(value => ReadIngredient(value, missingMemberBehavior)),
            ChatLink = chatLink.GetValue()
        };
    }
}

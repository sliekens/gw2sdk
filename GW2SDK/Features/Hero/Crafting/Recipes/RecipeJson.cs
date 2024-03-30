using System.Text.Json;
using GuildWars2.Hero.Crafting.Disciplines;
using GuildWars2.Json;

namespace GuildWars2.Hero.Crafting.Recipes;

internal static class RecipeJson
{
    public static Recipe GetRecipe(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        if (json.TryGetProperty("type", out var discriminator))
        {
            switch (discriminator.GetString())
            {
                case "Amulet":
                    return json.GetAmuletRecipe(missingMemberBehavior);
                case "Axe":
                    return json.GetAxeRecipe(missingMemberBehavior);
                case "Backpack":
                    return json.GetBackpackRecipe(missingMemberBehavior);
                case "Bag":
                    return json.GetBagRecipe(missingMemberBehavior);
                case "Boots":
                    return json.GetBootsRecipe(missingMemberBehavior);
                case "Bulk":
                    return json.GetBulkRecipe(missingMemberBehavior);
                case "Coat":
                    return json.GetCoatRecipe(missingMemberBehavior);
                case "Component":
                    return json.GetComponentRecipe(missingMemberBehavior);
                case "Consumable":
                    return json.GetConsumableRecipe(missingMemberBehavior);
                case "Dagger":
                    return json.GetDaggerRecipe(missingMemberBehavior);
                case "Dessert":
                    return json.GetDessertRecipe(missingMemberBehavior);
                case "Dye":
                    return json.GetDyeRecipe(missingMemberBehavior);
                case "Earring":
                    return json.GetEarringRecipe(missingMemberBehavior);
                case "Feast":
                    return json.GetFeastRecipe(missingMemberBehavior);
                case "Focus":
                    return json.GetFocusRecipe(missingMemberBehavior);
                case "Gloves":
                    return json.GetGlovesRecipe(missingMemberBehavior);
                case "Greatsword":
                    return json.GetGreatswordRecipe(missingMemberBehavior);
                case "GuildConsumable":
                    return json.GetGuildConsumableRecipe(missingMemberBehavior);
                case "GuildConsumableWvw":
                    return json.GetGuildWvwUpgradeRecipe(missingMemberBehavior);
                case "GuildDecoration":
                    return json.GetGuildDecorationRecipe(missingMemberBehavior);
                case "Hammer":
                    return json.GetHammerRecipe(missingMemberBehavior);
                case "Harpoon":
                    return json.GetSpearRecipe(missingMemberBehavior);
                case "Helm":
                    return json.GetHeadgearRecipe(missingMemberBehavior);
                case "IngredientCooking":
                    return json.GetIngredientCookingRecipe(missingMemberBehavior);
                case "Inscription":
                    return json.GetInscriptionRecipe(missingMemberBehavior);
                case "Insignia":
                    return json.GetInsigniaRecipe(missingMemberBehavior);
                case "LegendaryComponent":
                    return json.GetLegendaryComponentRecipe(missingMemberBehavior);
                case "Leggings":
                    return json.GetLeggingsRecipe(missingMemberBehavior);
                case "LongBow":
                    return json.GetLongbowRecipe(missingMemberBehavior);
                case "Mace":
                    return json.GetMaceRecipe(missingMemberBehavior);
                case "Meal":
                    return json.GetMealRecipe(missingMemberBehavior);
                case "Pistol":
                    return json.GetPistolRecipe(missingMemberBehavior);
                case "Potion":
                    return json.GetPotionRecipe(missingMemberBehavior);
                case "Refinement":
                    return json.GetRefinementRecipe(missingMemberBehavior);
                case "RefinementEctoplasm":
                    return json.GetRefinementEctoplasmRecipe(missingMemberBehavior);
                case "RefinementObsidian":
                    return json.GetRefinementObsidianRecipe(missingMemberBehavior);
                case "Rifle":
                    return json.GetRifleRecipe(missingMemberBehavior);
                case "Ring":
                    return json.GetRingRecipe(missingMemberBehavior);
                case "Scepter":
                    return json.GetScepterRecipe(missingMemberBehavior);
                case "Seasoning":
                    return json.GetSeasoningRecipe(missingMemberBehavior);
                case "Shield":
                    return json.GetShieldRecipe(missingMemberBehavior);
                case "ShortBow":
                    return json.GetShortbowRecipe(missingMemberBehavior);
                case "Shoulders":
                    return json.GetShouldersRecipe(missingMemberBehavior);
                case "Snack":
                    return json.GetSnackRecipe(missingMemberBehavior);
                case "Soup":
                    return json.GetSoupRecipe(missingMemberBehavior);
                case "Speargun":
                    return json.GetHarpoonGunRecipe(missingMemberBehavior);
                case "Staff":
                    return json.GetStaffRecipe(missingMemberBehavior);
                case "Sword":
                    return json.GetSwordRecipe(missingMemberBehavior);
                case "Torch":
                    return json.GetTorchRecipe(missingMemberBehavior);
                case "Trident":
                    return json.GetTridentRecipe(missingMemberBehavior);
                case "UpgradeComponent":
                    return json.GetUpgradeComponentRecipe(missingMemberBehavior);
                case "Warhorn":
                    return json.GetWarhornRecipe(missingMemberBehavior);
            }
        }

        RequiredMember outputItemId = "output_item_id";
        RequiredMember outputItemCount = "output_item_count";
        RequiredMember minRating = "min_rating";
        RequiredMember timeToCraft = "time_to_craft_ms";
        RequiredMember disciplines = "disciplines";
        RequiredMember flags = "flags";
        RequiredMember ingredients = "ingredients";
        RequiredMember id = "id";
        RequiredMember chatLink = "chat_link";
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
            else if (outputItemId.Match(member))
            {
                outputItemId = member;
            }
            else if (outputItemCount.Match(member))
            {
                outputItemCount = member;
            }
            else if (minRating.Match(member))
            {
                minRating = member;
            }
            else if (timeToCraft.Match(member))
            {
                timeToCraft = member;
            }
            else if (disciplines.Match(member))
            {
                disciplines = member;
            }
            else if (flags.Match(member))
            {
                flags = member;
            }
            else if (ingredients.Match(member))
            {
                ingredients = member;
            }
            else if (id.Match(member))
            {
                id = member;
            }
            else if (chatLink.Match(member))
            {
                chatLink = member;
            }
            else if (missingMemberBehavior == MissingMemberBehavior.Error)
            {
                throw new InvalidOperationException(Strings.UnexpectedMember(member.Name));
            }
        }

        return new Recipe
        {
            Id = id.Map(value => value.GetInt32()),
            OutputItemId = outputItemId.Map(value => value.GetInt32()),
            OutputItemCount = outputItemCount.Map(value => value.GetInt32()),
            MinRating = minRating.Map(value => value.GetInt32()),
            TimeToCraft = timeToCraft.Map(value => TimeSpan.FromMilliseconds(value.GetDouble())),
            Disciplines =
                disciplines.Map(
                    values =>
                        values.GetList(
                            value => value.GetEnum<CraftingDisciplineName>()
                        )
                ),
            Flags = flags.Map(values => values.GetRecipeFlags()),
            Ingredients =
                ingredients.Map(
                    values => values.GetList(value => value.GetIngredient(missingMemberBehavior))
                ),
            ChatLink = chatLink.Map(value => value.GetStringRequired())
        };
    }
}

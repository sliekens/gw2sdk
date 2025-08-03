using System.Text.Json;

using GuildWars2.Hero.Crafting.Disciplines;
using GuildWars2.Json;

namespace GuildWars2.Hero.Crafting.Recipes;

internal static class RecipeJson
{
    public static Recipe GetRecipe(this in JsonElement json)
    {
        if (json.TryGetProperty("type", out JsonElement discriminator))
        {
            switch (discriminator.GetString())
            {
                case "Amulet":
                    return json.GetAmuletRecipe();
                case "Axe":
                    return json.GetAxeRecipe();
                case "Backpack":
                    return json.GetBackpackRecipe();
                case "Bag":
                    return json.GetBagRecipe();
                case "Boots":
                    return json.GetBootsRecipe();
                case "Bulk":
                    return json.GetBulkRecipe();
                case "Coat":
                    return json.GetCoatRecipe();
                case "Component":
                    return json.GetComponentRecipe();
                case "Consumable":
                    return json.GetConsumableRecipe();
                case "Dagger":
                    return json.GetDaggerRecipe();
                case "Dessert":
                    return json.GetDessertRecipe();
                case "Dye":
                    return json.GetDyeRecipe();
                case "Earring":
                    return json.GetEarringRecipe();
                case "Feast":
                    return json.GetFeastRecipe();
                case "Focus":
                    return json.GetFocusRecipe();
                case "Gloves":
                    return json.GetGlovesRecipe();
                case "Greatsword":
                    return json.GetGreatswordRecipe();
                case "GuildConsumable":
                    return json.GetGuildConsumableRecipe();
                case "GuildConsumableWvw":
                    return json.GetGuildWvwUpgradeRecipe();
                case "GuildDecoration":
                    return json.GetGuildDecorationRecipe();
                case "Hammer":
                    return json.GetHammerRecipe();
                case "Harpoon":
                    return json.GetSpearRecipe();
                case "Helm":
                    return json.GetHeadgearRecipe();
                case "IngredientCooking":
                    return json.GetIngredientCookingRecipe();
                case "Inscription":
                    return json.GetInscriptionRecipe();
                case "Insignia":
                    return json.GetInsigniaRecipe();
                case "LegendaryComponent":
                    return json.GetLegendaryComponentRecipe();
                case "Leggings":
                    return json.GetLeggingsRecipe();
                case "LongBow":
                    return json.GetLongbowRecipe();
                case "Mace":
                    return json.GetMaceRecipe();
                case "Meal":
                    return json.GetMealRecipe();
                case "Pistol":
                    return json.GetPistolRecipe();
                case "Potion":
                    return json.GetPotionRecipe();
                case "Refinement":
                    return json.GetRefinementRecipe();
                case "RefinementEctoplasm":
                    return json.GetRefinementEctoplasmRecipe();
                case "RefinementObsidian":
                    return json.GetRefinementObsidianRecipe();
                case "Rifle":
                    return json.GetRifleRecipe();
                case "Ring":
                    return json.GetRingRecipe();
                case "Scepter":
                    return json.GetScepterRecipe();
                case "Seasoning":
                    return json.GetSeasoningRecipe();
                case "Shield":
                    return json.GetShieldRecipe();
                case "ShortBow":
                    return json.GetShortbowRecipe();
                case "Shoulders":
                    return json.GetShouldersRecipe();
                case "Snack":
                    return json.GetSnackRecipe();
                case "Soup":
                    return json.GetSoupRecipe();
                case "Speargun":
                    return json.GetHarpoonGunRecipe();
                case "Staff":
                    return json.GetStaffRecipe();
                case "Sword":
                    return json.GetSwordRecipe();
                case "Torch":
                    return json.GetTorchRecipe();
                case "Trident":
                    return json.GetTridentRecipe();
                case "UpgradeComponent":
                    return json.GetUpgradeComponentRecipe();
                case "Warhorn":
                    return json.GetWarhornRecipe();
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
        foreach (JsonProperty member in json.EnumerateObject())
        {
            if (member.NameEquals("type"))
            {
                if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
                {
                    ThrowHelper.ThrowUnexpectedDiscriminator(member.Value.GetString());
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
            else if (JsonOptions.MissingMemberBehavior == MissingMemberBehavior.Error)
            {
                ThrowHelper.ThrowUnexpectedMember(member.Name);
            }
        }

        return new Recipe
        {
            Id = id.Map(static (in JsonElement value) => value.GetInt32()),
            OutputItemId = outputItemId.Map(static (in JsonElement value) => value.GetInt32()),
            OutputItemCount = outputItemCount.Map(static (in JsonElement value) => value.GetInt32()),
            MinRating = minRating.Map(static (in JsonElement value) => value.GetInt32()),
            TimeToCraft =
                timeToCraft.Map(static (in JsonElement value) => TimeSpan.FromMilliseconds(value.GetDouble())),
            Disciplines =
                disciplines.Map(static (in JsonElement values) =>
                    values.GetList(static (in JsonElement value) => value.GetEnum<CraftingDisciplineName>())
                ),
            Flags = flags.Map(static (in JsonElement values) => values.GetRecipeFlags()),
            Ingredients =
                ingredients.Map(static (in JsonElement values) =>
                    values.GetList(static (in JsonElement value) => value.GetIngredient())
                ),
            ChatLink = chatLink.Map(static (in JsonElement value) => value.GetStringRequired())
        };
    }
}

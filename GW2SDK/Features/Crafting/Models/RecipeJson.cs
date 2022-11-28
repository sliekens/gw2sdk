using System;
using System.Text.Json;
using GuildWars2.Json;
using JetBrains.Annotations;

namespace GuildWars2.Crafting;

[PublicAPI]
public static class RecipeJson
{
    public static Recipe GetRecipe(
        this JsonElement json,
        MissingMemberBehavior missingMemberBehavior
    )
    {
        switch (json.GetProperty("type").GetString())
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
            case "Food":
                return json.GetFoodRecipe(missingMemberBehavior);
            case "Gloves":
                return json.GetGlovesRecipe(missingMemberBehavior);
            case "Greatsword":
                return json.GetGreatswordRecipe(missingMemberBehavior);
            case "GuildConsumable":
                return json.GetGuildConsumableRecipe(missingMemberBehavior);
            case "GuildConsumableWvw":
                return json.GetGuildConsumableWvwRecipe(missingMemberBehavior);
            case "GuildDecoration":
                return json.GetGuildDecorationRecipe(missingMemberBehavior);
            case "Hammer":
                return json.GetHammerRecipe(missingMemberBehavior);
            case "Harpoon":
                return json.GetSpearRecipe(missingMemberBehavior);
            case "Helm":
                return json.GetHelmRecipe(missingMemberBehavior);
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
                outputItemId.Value = member.Value;
            }
            else if (member.NameEquals(outputItemCount.Name))
            {
                outputItemCount.Value = member.Value;
            }
            else if (member.NameEquals(minRating.Name))
            {
                minRating.Value = member.Value;
            }
            else if (member.NameEquals(minRating.Name))
            {
                minRating.Value = member.Value;
            }
            else if (member.NameEquals(timeToCraft.Name))
            {
                timeToCraft.Value = member.Value;
            }
            else if (member.NameEquals(disciplines.Name))
            {
                disciplines.Value = member.Value;
            }
            else if (member.NameEquals(flags.Name))
            {
                flags.Value = member.Value;
            }
            else if (member.NameEquals(ingredients.Name))
            {
                ingredients.Value = member.Value;
            }
            else if (member.NameEquals(id.Name))
            {
                id.Value = member.Value;
            }
            else if (member.NameEquals(chatLink.Name))
            {
                chatLink.Value = member.Value;
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
                ingredients.SelectMany(value => value.GetIngredient(missingMemberBehavior)),
            ChatLink = chatLink.GetValue()
        };
    }
}

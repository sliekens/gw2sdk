using System.Text.Json;
using System.Text.Json.Serialization;

using GuildWars2.Hero.Crafting.Disciplines;
using GuildWars2.Json;

namespace GuildWars2.Hero.Crafting.Recipes;

internal sealed class RecipeJsonConverter : JsonConverter<Recipe>
{
    public const string DiscriminatorName = "$type";

    public const string DiscriminatorValue = "recipe";

    public override bool CanConvert(Type typeToConvert)
    {
        return typeof(Recipe).IsAssignableFrom(typeToConvert);
    }

    public override Recipe Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options
    )
    {
        using JsonDocument json = JsonDocument.ParseValue(ref reader);
        return Read(json.RootElement);
    }

    public override void Write(Utf8JsonWriter writer, Recipe value, JsonSerializerOptions options)
    {
        Write(writer, value);
    }

    public static Recipe Read(in JsonElement json)
    {
        if (json.TryGetProperty(DiscriminatorName, out JsonElement discriminator))
        {
            switch (discriminator.ToString())
            {
                case AmuletRecipeJsonConverter.DiscriminatorValue:
                    return AmuletRecipeJsonConverter.Read(json);
                case AxeRecipeJsonConverter.DiscriminatorValue:
                    return AxeRecipeJsonConverter.Read(json);
                case BackpackRecipeJsonConverter.DiscriminatorValue:
                    return BackpackRecipeJsonConverter.Read(json);
                case BagRecipeJsonConverter.DiscriminatorValue:
                    return BagRecipeJsonConverter.Read(json);
                case BootsRecipeJsonConverter.DiscriminatorValue:
                    return BootsRecipeJsonConverter.Read(json);
                case BulkRecipeJsonConverter.DiscriminatorValue:
                    return BulkRecipeJsonConverter.Read(json);
                case CoatRecipeJsonConverter.DiscriminatorValue:
                    return CoatRecipeJsonConverter.Read(json);
                case ComponentRecipeJsonConverter.DiscriminatorValue:
                    return ComponentRecipeJsonConverter.Read(json);
                case ConsumableRecipeJsonConverter.DiscriminatorValue:
                    return ConsumableRecipeJsonConverter.Read(json);
                case DaggerRecipeJsonConverter.DiscriminatorValue:
                    return DaggerRecipeJsonConverter.Read(json);
                case DessertRecipeJsonConverter.DiscriminatorValue:
                    return DessertRecipeJsonConverter.Read(json);
                case DyeRecipeJsonConverter.DiscriminatorValue:
                    return DyeRecipeJsonConverter.Read(json);
                case EarringRecipeJsonConverter.DiscriminatorValue:
                    return EarringRecipeJsonConverter.Read(json);
                case FeastRecipeJsonConverter.DiscriminatorValue:
                    return FeastRecipeJsonConverter.Read(json);
                case FocusRecipeJsonConverter.DiscriminatorValue:
                    return FocusRecipeJsonConverter.Read(json);
                case GuildConsumableRecipeJsonConverter.DiscriminatorValue:
                    return GuildConsumableRecipeJsonConverter.Read(json);
                case GuildDecorationRecipeJsonConverter.DiscriminatorValue:
                    return GuildDecorationRecipeJsonConverter.Read(json);
                case GuildWvwUpgradeRecipeJsonConverter.DiscriminatorValue:
                    return GuildWvwUpgradeRecipeJsonConverter.Read(json);
                case GlovesRecipeJsonConverter.DiscriminatorValue:
                    return GlovesRecipeJsonConverter.Read(json);
                case GreatswordRecipeJsonConverter.DiscriminatorValue:
                    return GreatswordRecipeJsonConverter.Read(json);
                case HeadgearRecipeJsonConverter.DiscriminatorValue:
                    return HeadgearRecipeJsonConverter.Read(json);
                case HarpoonGunRecipeJsonConverter.DiscriminatorValue:
                    return HarpoonGunRecipeJsonConverter.Read(json);
                case IngredientCookingRecipeJsonConverter.DiscriminatorValue:
                    return IngredientCookingRecipeJsonConverter.Read(json);
                case InscriptionRecipeJsonConverter.DiscriminatorValue:
                    return InscriptionRecipeJsonConverter.Read(json);
                case InsigniaRecipeJsonConverter.DiscriminatorValue:
                    return InsigniaRecipeJsonConverter.Read(json);
                case LegendaryComponentRecipeJsonConverter.DiscriminatorValue:
                    return LegendaryComponentRecipeJsonConverter.Read(json);
                case LeggingsRecipeJsonConverter.DiscriminatorValue:
                    return LeggingsRecipeJsonConverter.Read(json);
                case HammerRecipeJsonConverter.DiscriminatorValue:
                    return HammerRecipeJsonConverter.Read(json);
                case LongbowRecipeJsonConverter.DiscriminatorValue:
                    return LongbowRecipeJsonConverter.Read(json);
                case MaceRecipeJsonConverter.DiscriminatorValue:
                    return MaceRecipeJsonConverter.Read(json);
                case MealRecipeJsonConverter.DiscriminatorValue:
                    return MealRecipeJsonConverter.Read(json);
                case PistolRecipeJsonConverter.DiscriminatorValue:
                    return PistolRecipeJsonConverter.Read(json);
                case PotionRecipeJsonConverter.DiscriminatorValue:
                    return PotionRecipeJsonConverter.Read(json);
                case RefinementRecipeJsonConverter.DiscriminatorValue:
                    return RefinementRecipeJsonConverter.Read(json);
                case RefinementEctoplasmRecipeJsonConverter.DiscriminatorValue:
                    return RefinementEctoplasmRecipeJsonConverter.Read(json);
                case RefinementObsidianRecipeJsonConverter.DiscriminatorValue:
                    return RefinementObsidianRecipeJsonConverter.Read(json);
                case RifleRecipeJsonConverter.DiscriminatorValue:
                    return RifleRecipeJsonConverter.Read(json);
                case RingRecipeJsonConverter.DiscriminatorValue:
                    return RingRecipeJsonConverter.Read(json);
                case ScepterRecipeJsonConverter.DiscriminatorValue:
                    return ScepterRecipeJsonConverter.Read(json);
                case SeasoningRecipeJsonConverter.DiscriminatorValue:
                    return SeasoningRecipeJsonConverter.Read(json);
                case ShieldRecipeJsonConverter.DiscriminatorValue:
                    return ShieldRecipeJsonConverter.Read(json);
                case ShortbowRecipeJsonConverter.DiscriminatorValue:
                    return ShortbowRecipeJsonConverter.Read(json);
                case ShouldersRecipeJsonConverter.DiscriminatorValue:
                    return ShouldersRecipeJsonConverter.Read(json);
                case SnackRecipeJsonConverter.DiscriminatorValue:
                    return SnackRecipeJsonConverter.Read(json);
                case SoupRecipeJsonConverter.DiscriminatorValue:
                    return SoupRecipeJsonConverter.Read(json);
                case SpearRecipeJsonConverter.DiscriminatorValue:
                    return SpearRecipeJsonConverter.Read(json);
                case StaffRecipeJsonConverter.DiscriminatorValue:
                    return StaffRecipeJsonConverter.Read(json);
                case SwordRecipeJsonConverter.DiscriminatorValue:
                    return SwordRecipeJsonConverter.Read(json);
                case TorchRecipeJsonConverter.DiscriminatorValue:
                    return TorchRecipeJsonConverter.Read(json);
                case TridentRecipeJsonConverter.DiscriminatorValue:
                    return TridentRecipeJsonConverter.Read(json);
                case UpgradeComponentRecipeJsonConverter.DiscriminatorValue:
                    return UpgradeComponentRecipeJsonConverter.Read(json);
                case WarhornRecipeJsonConverter.DiscriminatorValue:
                    return WarhornRecipeJsonConverter.Read(json);
                default:
                    break;
            }
        }

        return new Recipe
        {
            Id = json.GetProperty("id").GetInt32(),
            OutputItemId = json.GetProperty("output_item_id").GetInt32(),
            OutputItemCount = json.GetProperty("output_item_count").GetInt32(),
            MinRating = json.GetProperty("min_rating").GetInt32(),
            TimeToCraft =
                TimeSpan.FromMilliseconds(json.GetProperty("time_to_craft_ms").GetDouble()),
            Disciplines =
                json.GetProperty("disciplines")
                    .GetList(static (in value) => value.GetEnum<CraftingDisciplineName>()),
            Flags = RecipeFlagsJsonConverter.Read(json.GetProperty("flags")),
            Ingredients = json.GetProperty("ingredients").GetList(IngredientJsonConverter.Read),
            ChatLink = json.GetProperty("chat_link").GetStringRequired()
        };
    }

    public static void Write(Utf8JsonWriter writer, Recipe value)
    {
        switch (value)
        {
            case AmuletRecipe amuletRecipe:
                AmuletRecipeJsonConverter.Write(writer, amuletRecipe);
                break;
            case AxeRecipe axeRecipe:
                AxeRecipeJsonConverter.Write(writer, axeRecipe);
                break;
            case BackpackRecipe backpackRecipe:
                BackpackRecipeJsonConverter.Write(writer, backpackRecipe);
                break;
            case BagRecipe bagRecipe:
                BagRecipeJsonConverter.Write(writer, bagRecipe);
                break;
            case BootsRecipe bootsRecipe:
                BootsRecipeJsonConverter.Write(writer, bootsRecipe);
                break;
            case BulkRecipe bulkRecipe:
                BulkRecipeJsonConverter.Write(writer, bulkRecipe);
                break;
            case CoatRecipe coatRecipe:
                CoatRecipeJsonConverter.Write(writer, coatRecipe);
                break;
            case ComponentRecipe componentRecipe:
                ComponentRecipeJsonConverter.Write(writer, componentRecipe);
                break;
            case ConsumableRecipe consumableRecipe:
                ConsumableRecipeJsonConverter.Write(writer, consumableRecipe);
                break;
            case DaggerRecipe daggerRecipe:
                DaggerRecipeJsonConverter.Write(writer, daggerRecipe);
                break;
            case DessertRecipe dessertRecipe:
                DessertRecipeJsonConverter.Write(writer, dessertRecipe);
                break;
            case DyeRecipe dyeRecipe:
                DyeRecipeJsonConverter.Write(writer, dyeRecipe);
                break;
            case EarringRecipe earringRecipe:
                EarringRecipeJsonConverter.Write(writer, earringRecipe);
                break;
            case FeastRecipe feastRecipe:
                FeastRecipeJsonConverter.Write(writer, feastRecipe);
                break;
            case FocusRecipe focusRecipe:
                FocusRecipeJsonConverter.Write(writer, focusRecipe);
                break;
            case GuildConsumableRecipe guildConsumableRecipe:
                GuildConsumableRecipeJsonConverter.Write(writer, guildConsumableRecipe);
                break;
            case GuildDecorationRecipe guildDecorationRecipe:
                GuildDecorationRecipeJsonConverter.Write(writer, guildDecorationRecipe);
                break;
            case GuildWvwUpgradeRecipe guildWvwUpgradeRecipe:
                GuildWvwUpgradeRecipeJsonConverter.Write(writer, guildWvwUpgradeRecipe);
                break;
            case GlovesRecipe glovesRecipe:
                GlovesRecipeJsonConverter.Write(writer, glovesRecipe);
                break;
            case GreatswordRecipe greatswordRecipe:
                GreatswordRecipeJsonConverter.Write(writer, greatswordRecipe);
                break;
            case HeadgearRecipe headgearRecipe:
                HeadgearRecipeJsonConverter.Write(writer, headgearRecipe);
                break;
            case HarpoonGunRecipe harpoonGunRecipe:
                HarpoonGunRecipeJsonConverter.Write(writer, harpoonGunRecipe);
                break;
            case IngredientCookingRecipe ingredientCookingRecipe:
                IngredientCookingRecipeJsonConverter.Write(writer, ingredientCookingRecipe);
                break;
            case InscriptionRecipe inscriptionRecipe:
                InscriptionRecipeJsonConverter.Write(writer, inscriptionRecipe);
                break;
            case InsigniaRecipe insigniaRecipe:
                InsigniaRecipeJsonConverter.Write(writer, insigniaRecipe);
                break;
            case LegendaryComponentRecipe legendaryComponentRecipe:
                LegendaryComponentRecipeJsonConverter.Write(writer, legendaryComponentRecipe);
                break;
            case LeggingsRecipe leggingsRecipe:
                LeggingsRecipeJsonConverter.Write(writer, leggingsRecipe);
                break;
            case HammerRecipe hammerRecipe:
                HammerRecipeJsonConverter.Write(writer, hammerRecipe);
                break;
            case LongbowRecipe longbowRecipe:
                LongbowRecipeJsonConverter.Write(writer, longbowRecipe);
                break;
            case MaceRecipe maceRecipe:
                MaceRecipeJsonConverter.Write(writer, maceRecipe);
                break;
            case MealRecipe mealRecipe:
                MealRecipeJsonConverter.Write(writer, mealRecipe);
                break;
            case PistolRecipe pistolRecipe:
                PistolRecipeJsonConverter.Write(writer, pistolRecipe);
                break;
            case PotionRecipe potionRecipe:
                PotionRecipeJsonConverter.Write(writer, potionRecipe);
                break;
            case RefinementRecipe refinementRecipe:
                RefinementRecipeJsonConverter.Write(writer, refinementRecipe);
                break;
            case RefinementEctoplasmRecipe refinementEctoplasmRecipe:
                RefinementEctoplasmRecipeJsonConverter.Write(writer, refinementEctoplasmRecipe);
                break;
            case RefinementObsidianRecipe refinementObsidianRecipe:
                RefinementObsidianRecipeJsonConverter.Write(writer, refinementObsidianRecipe);
                break;
            case RifleRecipe rifleRecipe:
                RifleRecipeJsonConverter.Write(writer, rifleRecipe);
                break;
            case RingRecipe ringRecipe:
                RingRecipeJsonConverter.Write(writer, ringRecipe);
                break;
            case ScepterRecipe scepterRecipe:
                ScepterRecipeJsonConverter.Write(writer, scepterRecipe);
                break;
            case SeasoningRecipe seasoningRecipe:
                SeasoningRecipeJsonConverter.Write(writer, seasoningRecipe);
                break;
            case ShieldRecipe shieldRecipe:
                ShieldRecipeJsonConverter.Write(writer, shieldRecipe);
                break;
            case ShortbowRecipe shortbowRecipe:
                ShortbowRecipeJsonConverter.Write(writer, shortbowRecipe);
                break;
            case ShouldersRecipe shouldersRecipe:
                ShouldersRecipeJsonConverter.Write(writer, shouldersRecipe);
                break;
            case SnackRecipe snackRecipe:
                SnackRecipeJsonConverter.Write(writer, snackRecipe);
                break;
            case SoupRecipe soupRecipe:
                SoupRecipeJsonConverter.Write(writer, soupRecipe);
                break;
            case SpearRecipe spearRecipe:
                SpearRecipeJsonConverter.Write(writer, spearRecipe);
                break;
            case StaffRecipe staffRecipe:
                StaffRecipeJsonConverter.Write(writer, staffRecipe);
                break;
            case SwordRecipe swordRecipe:
                SwordRecipeJsonConverter.Write(writer, swordRecipe);
                break;
            case TorchRecipe torchRecipe:
                TorchRecipeJsonConverter.Write(writer, torchRecipe);
                break;
            case TridentRecipe tridentRecipe:
                TridentRecipeJsonConverter.Write(writer, tridentRecipe);
                break;
            case UpgradeComponentRecipe upgradeComponentRecipe:
                UpgradeComponentRecipeJsonConverter.Write(writer, upgradeComponentRecipe);
                break;
            case WarhornRecipe warhornRecipe:
                WarhornRecipeJsonConverter.Write(writer, warhornRecipe);
                break;
            default:
                writer.WriteStartObject();
                writer.WriteString(DiscriminatorName, DiscriminatorValue);
                WriteCommonProperties(writer, value);
                writer.WriteEndObject();
                break;
        }
    }

    public static void WriteCommonProperties(Utf8JsonWriter writer, Recipe value)
    {
        writer.WriteNumber("id", value.Id);
        writer.WriteNumber("output_item_id", value.OutputItemId);
        writer.WriteNumber("output_item_count", value.OutputItemCount);
        writer.WriteNumber("min_rating", value.MinRating);
        writer.WriteNumber("time_to_craft_ms", value.TimeToCraft.TotalMilliseconds);
        writer.WriteStartArray("disciplines");
        foreach (Extensible<CraftingDisciplineName> discipline in value.Disciplines)
        {
            writer.WriteStringValue(discipline.ToString());
        }

        writer.WriteEndArray();
        writer.WritePropertyName("flags");
        RecipeFlagsJsonConverter.Write(writer, value.Flags);
        writer.WriteStartArray("ingredients");
        foreach (Ingredient? ingredient in value.Ingredients)
        {
            IngredientJsonConverter.Write(writer, ingredient);
        }

        writer.WriteEndArray();
        writer.WriteString("chat_link", value.ChatLink);
    }
}

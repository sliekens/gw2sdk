using System;
using System.Collections.Generic;
using GW2SDK.Features.Recipes;
using GW2SDK.Infrastructure.Common;
using Newtonsoft.Json.Linq;

namespace GW2SDK.Infrastructure.Recipes
{
    public sealed class RecipeDiscriminatorOptions : DiscriminatorOptions
    {
        public RecipeDiscriminatorOptions()
        {
            Activator = Create;
            Preprocessor = Preprocess;
        }

        public override Type BaseType => typeof(Recipe);

        public override string DiscriminatorFieldName => "type";

        public override bool SerializeDiscriminator => false;

        private void Preprocess(string discriminator, JObject json)
        {
            var @namespace = StringHelper.ToSnakeCase(discriminator);

            // Recipes can have extra fields depending on the type of recipe
            // Anet decided to put those extra fields in a "details" property
            // For us it's much more convenient to flatten the root object before serializing the JSON to CLR objects
            if (json.ContainsKey("details"))
            {
                var details = json.Property("details");
                foreach (var property in ((JObject) details.Value).Properties())
                {
                    // There can be a naming collision that prevents a clean merge
                    // Prefix those duplicate names with the discriminator
                    if (json.ContainsKey(property.Name))
                    {
                        json.Add($"{@namespace}_{property.Name}", property.Value);
                    }
                    else
                    {
                        json.Add(property.Name, property.Value);
                    }
                }

                // Details should now be removed because all its properties have been added to the root object.
                details.Remove();
            }
        }

        public override IEnumerable<(string TypeName, Type Type)> GetDiscriminatedTypes()
        {
            yield return ("Amulet", typeof(AmuletRecipe));
            yield return ("Axe", typeof(AxeRecipe));
            yield return ("Backpack", typeof(BackpackRecipe));
            yield return ("Bag", typeof(BagRecipe));
            yield return ("Boots", typeof(BootsRecipe));
            yield return ("Bulk", typeof(BulkRecipe));
            yield return ("Coat", typeof(CoatRecipe));
            yield return ("Component", typeof(ComponentRecipe));
            yield return ("Consumable", typeof(ConsumableRecipe));
            yield return ("Dagger", typeof(DaggerRecipe));
            yield return ("Dessert", typeof(DessertRecipe));
            yield return ("Dye", typeof(DyeRecipe));
            yield return ("Earring", typeof(EarringRecipe));
            yield return ("Feast", typeof(FeastRecipe));
            yield return ("Focus", typeof(FocusRecipe));
            yield return ("Gloves", typeof(GlovesRecipe));
            yield return ("Greatsword", typeof(GreatswordRecipe));
            yield return ("GuildConsumable", typeof(GuildConsumableRecipe));
            yield return ("GuildConsumableWvw", typeof(GuildConsumableWvwRecipe));
            yield return ("GuildDecoration", typeof(GuildDecorationRecipe));
            yield return ("Hammer", typeof(HammerRecipe));
            yield return ("Harpoon", typeof(HarpoonRecipe));
            yield return ("Helm", typeof(HelmRecipe));
            yield return ("IngredientCooking", typeof(IngredientCookingRecipe));
            yield return ("Inscription", typeof(InscriptionRecipe));
            yield return ("Insignia", typeof(InsigniaRecipe));
            yield return ("LegendaryComponent", typeof(LegendaryComponentRecipe));
            yield return ("Leggings", typeof(LeggingsRecipe));
            yield return ("LongBow", typeof(LongBowRecipe));
            yield return ("Mace", typeof(MaceRecipe));
            yield return ("Meal", typeof(MealRecipe));
            yield return ("Pistol", typeof(PistolRecipe));
            yield return ("Potion", typeof(PotionRecipe));
            yield return ("Refinement", typeof(RefinementRecipe));
            yield return ("RefinementEctoplasm", typeof(RefinementEctoplasmRecipe));
            yield return ("RefinementObsidian", typeof(RefinementObsidianRecipe));
            yield return ("Rifle", typeof(RifleRecipe));
            yield return ("Ring", typeof(RingRecipe));
            yield return ("Scepter", typeof(ScepterRecipe));
            yield return ("Seasoning", typeof(SeasoningRecipe));
            yield return ("Shield", typeof(ShieldRecipe));
            yield return ("ShortBow", typeof(ShortBowRecipe));
            yield return ("Shoulders", typeof(ShouldersRecipe));
            yield return ("Snack", typeof(SnackRecipe));
            yield return ("Soup", typeof(SoupRecipe));
            yield return ("Speargun", typeof(SpeargunRecipe));
            yield return ("Staff", typeof(StaffRecipe));
            yield return ("Sword", typeof(SwordRecipe));
            yield return ("Torch", typeof(TorchRecipe));
            yield return ("Trident", typeof(TridentRecipe));
            yield return ("UpgradeComponent", typeof(UpgradeComponentRecipe));
            yield return ("Warhorn", typeof(WarhornRecipe));
        }

        public object Create(Type objectType)
        {
            if (objectType == typeof(AmuletRecipe)) return new AmuletRecipe();
            if (objectType == typeof(AxeRecipe)) return new AxeRecipe();
            if (objectType == typeof(BackpackRecipe)) return new BackpackRecipe();
            if (objectType == typeof(BagRecipe)) return new BagRecipe();
            if (objectType == typeof(BootsRecipe)) return new BootsRecipe();
            if (objectType == typeof(BulkRecipe)) return new BulkRecipe();
            if (objectType == typeof(CoatRecipe)) return new CoatRecipe();
            if (objectType == typeof(ComponentRecipe)) return new ComponentRecipe();
            if (objectType == typeof(ConsumableRecipe)) return new ConsumableRecipe();
            if (objectType == typeof(DaggerRecipe)) return new DaggerRecipe();
            if (objectType == typeof(DessertRecipe)) return new DessertRecipe();
            if (objectType == typeof(DyeRecipe)) return new DyeRecipe();
            if (objectType == typeof(EarringRecipe)) return new EarringRecipe();
            if (objectType == typeof(FeastRecipe)) return new FeastRecipe();
            if (objectType == typeof(FocusRecipe)) return new FocusRecipe();
            if (objectType == typeof(GlovesRecipe)) return new GlovesRecipe();
            if (objectType == typeof(GreatswordRecipe)) return new GreatswordRecipe();
            if (objectType == typeof(GuildConsumableRecipe)) return new GuildConsumableRecipe();
            if (objectType == typeof(GuildConsumableWvwRecipe)) return new GuildConsumableWvwRecipe();
            if (objectType == typeof(GuildDecorationRecipe)) return new GuildDecorationRecipe();
            if (objectType == typeof(HammerRecipe)) return new HammerRecipe();
            if (objectType == typeof(HarpoonRecipe)) return new HarpoonRecipe();
            if (objectType == typeof(HelmRecipe)) return new HelmRecipe();
            if (objectType == typeof(IngredientCookingRecipe)) return new IngredientCookingRecipe();
            if (objectType == typeof(InscriptionRecipe)) return new InscriptionRecipe();
            if (objectType == typeof(InsigniaRecipe)) return new InsigniaRecipe();
            if (objectType == typeof(LegendaryComponentRecipe)) return new LegendaryComponentRecipe();
            if (objectType == typeof(LeggingsRecipe)) return new LeggingsRecipe();
            if (objectType == typeof(LongBowRecipe)) return new LongBowRecipe();
            if (objectType == typeof(MaceRecipe)) return new MaceRecipe();
            if (objectType == typeof(MealRecipe)) return new MealRecipe();
            if (objectType == typeof(PistolRecipe)) return new PistolRecipe();
            if (objectType == typeof(PotionRecipe)) return new PotionRecipe();
            if (objectType == typeof(RefinementRecipe)) return new RefinementRecipe();
            if (objectType == typeof(RefinementEctoplasmRecipe)) return new RefinementEctoplasmRecipe();
            if (objectType == typeof(RefinementObsidianRecipe)) return new RefinementObsidianRecipe();
            if (objectType == typeof(RifleRecipe)) return new RifleRecipe();
            if (objectType == typeof(RingRecipe)) return new RingRecipe();
            if (objectType == typeof(ScepterRecipe)) return new ScepterRecipe();
            if (objectType == typeof(SeasoningRecipe)) return new SeasoningRecipe();
            if (objectType == typeof(ShieldRecipe)) return new ShieldRecipe();
            if (objectType == typeof(ShortBowRecipe)) return new ShortBowRecipe();
            if (objectType == typeof(ShouldersRecipe)) return new ShouldersRecipe();
            if (objectType == typeof(SnackRecipe)) return new SnackRecipe();
            if (objectType == typeof(SoupRecipe)) return new SoupRecipe();
            if (objectType == typeof(SpeargunRecipe)) return new SpeargunRecipe();
            if (objectType == typeof(StaffRecipe)) return new StaffRecipe();
            if (objectType == typeof(SwordRecipe)) return new SwordRecipe();
            if (objectType == typeof(TorchRecipe)) return new TorchRecipe();
            if (objectType == typeof(TridentRecipe)) return new TridentRecipe();
            if (objectType == typeof(UpgradeComponentRecipe)) return new UpgradeComponentRecipe();
            if (objectType == typeof(WarhornRecipe)) return new WarhornRecipe();
            return new Recipe();
        }
    }
}

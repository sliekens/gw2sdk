using System;
using System.Collections.Generic;
using GW2SDK.Impl.JsonConverters;

namespace GW2SDK.Recipes.Impl
{
    internal sealed class RecipeDiscriminatorOptions : DiscriminatorOptions
    {
        internal override Type BaseType => typeof(Recipe);

        internal override string DiscriminatorFieldName => "type";

        internal override bool SerializeDiscriminator => false;

        internal override IEnumerable<(string TypeName, Type Type)> GetDiscriminatedTypes()
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

        internal override object CreateInstance(Type discriminatedType)
        {
            if (discriminatedType == typeof(AmuletRecipe)) return new AmuletRecipe();
            if (discriminatedType == typeof(AxeRecipe)) return new AxeRecipe();
            if (discriminatedType == typeof(BackpackRecipe)) return new BackpackRecipe();
            if (discriminatedType == typeof(BagRecipe)) return new BagRecipe();
            if (discriminatedType == typeof(BootsRecipe)) return new BootsRecipe();
            if (discriminatedType == typeof(BulkRecipe)) return new BulkRecipe();
            if (discriminatedType == typeof(CoatRecipe)) return new CoatRecipe();
            if (discriminatedType == typeof(ComponentRecipe)) return new ComponentRecipe();
            if (discriminatedType == typeof(ConsumableRecipe)) return new ConsumableRecipe();
            if (discriminatedType == typeof(DaggerRecipe)) return new DaggerRecipe();
            if (discriminatedType == typeof(DessertRecipe)) return new DessertRecipe();
            if (discriminatedType == typeof(DyeRecipe)) return new DyeRecipe();
            if (discriminatedType == typeof(EarringRecipe)) return new EarringRecipe();
            if (discriminatedType == typeof(FeastRecipe)) return new FeastRecipe();
            if (discriminatedType == typeof(FocusRecipe)) return new FocusRecipe();
            if (discriminatedType == typeof(GlovesRecipe)) return new GlovesRecipe();
            if (discriminatedType == typeof(GreatswordRecipe)) return new GreatswordRecipe();
            if (discriminatedType == typeof(GuildConsumableRecipe)) return new GuildConsumableRecipe();
            if (discriminatedType == typeof(GuildConsumableWvwRecipe)) return new GuildConsumableWvwRecipe();
            if (discriminatedType == typeof(GuildDecorationRecipe)) return new GuildDecorationRecipe();
            if (discriminatedType == typeof(HammerRecipe)) return new HammerRecipe();
            if (discriminatedType == typeof(HarpoonRecipe)) return new HarpoonRecipe();
            if (discriminatedType == typeof(HelmRecipe)) return new HelmRecipe();
            if (discriminatedType == typeof(IngredientCookingRecipe)) return new IngredientCookingRecipe();
            if (discriminatedType == typeof(InscriptionRecipe)) return new InscriptionRecipe();
            if (discriminatedType == typeof(InsigniaRecipe)) return new InsigniaRecipe();
            if (discriminatedType == typeof(LegendaryComponentRecipe)) return new LegendaryComponentRecipe();
            if (discriminatedType == typeof(LeggingsRecipe)) return new LeggingsRecipe();
            if (discriminatedType == typeof(LongBowRecipe)) return new LongBowRecipe();
            if (discriminatedType == typeof(MaceRecipe)) return new MaceRecipe();
            if (discriminatedType == typeof(MealRecipe)) return new MealRecipe();
            if (discriminatedType == typeof(PistolRecipe)) return new PistolRecipe();
            if (discriminatedType == typeof(PotionRecipe)) return new PotionRecipe();
            if (discriminatedType == typeof(RefinementRecipe)) return new RefinementRecipe();
            if (discriminatedType == typeof(RefinementEctoplasmRecipe)) return new RefinementEctoplasmRecipe();
            if (discriminatedType == typeof(RefinementObsidianRecipe)) return new RefinementObsidianRecipe();
            if (discriminatedType == typeof(RifleRecipe)) return new RifleRecipe();
            if (discriminatedType == typeof(RingRecipe)) return new RingRecipe();
            if (discriminatedType == typeof(ScepterRecipe)) return new ScepterRecipe();
            if (discriminatedType == typeof(SeasoningRecipe)) return new SeasoningRecipe();
            if (discriminatedType == typeof(ShieldRecipe)) return new ShieldRecipe();
            if (discriminatedType == typeof(ShortBowRecipe)) return new ShortBowRecipe();
            if (discriminatedType == typeof(ShouldersRecipe)) return new ShouldersRecipe();
            if (discriminatedType == typeof(SnackRecipe)) return new SnackRecipe();
            if (discriminatedType == typeof(SoupRecipe)) return new SoupRecipe();
            if (discriminatedType == typeof(SpeargunRecipe)) return new SpeargunRecipe();
            if (discriminatedType == typeof(StaffRecipe)) return new StaffRecipe();
            if (discriminatedType == typeof(SwordRecipe)) return new SwordRecipe();
            if (discriminatedType == typeof(TorchRecipe)) return new TorchRecipe();
            if (discriminatedType == typeof(TridentRecipe)) return new TridentRecipe();
            if (discriminatedType == typeof(UpgradeComponentRecipe)) return new UpgradeComponentRecipe();
            if (discriminatedType == typeof(WarhornRecipe)) return new WarhornRecipe();
            return new Recipe();
        }
    }
}

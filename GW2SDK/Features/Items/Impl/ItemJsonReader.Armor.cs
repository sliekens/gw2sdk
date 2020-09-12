using System;
using System.Text.Json;
using GW2SDK.Enums;
using GW2SDK.Impl.JsonReaders;
using GW2SDK.Impl.JsonReaders.Mappings;

namespace GW2SDK.Items.Impl
{
    public sealed partial class ItemJsonReader
    {
        private static string GetArmorDiscriminator(JsonElement element) =>
            element.GetProperty("details").GetProperty("type").GetString();

        private static void MapArmor(JsonObjectMapping<Armor> armor)
        {
            armor.Discriminate(GetArmorDiscriminator, DiscriminateArmor);
            armor.Map
            (
                "details",
                details =>
                {
                    details.Ignore("type");

                    // All equipment
                    details.Map("attribute_adjustment", to => to.AttributeAdjustment);
                    details.Map("level", to => to.Level);
                    details.Map("infusion_slots", to => to.InfusionSlots, MapInfusionSlot);
                    details.Map
                    (
                        "infix_upgrade",
                        to => to.InfixUpgrade,
                        infixUpgrade => MapInfixUpgrade(infixUpgrade!),
                        MappingSignificance.Optional
                    );
                    details.Map("suffix_item_id", to => to.SuffixItemId);
                    details.Map("secondary_suffix_item_id", to => to.SecondarySuffixItemId);
                    details.Map("stat_choices", to => to.StatChoices, MappingSignificance.Optional);

                    // Armor-specific
                    details.Map("default_skin", to => to.DefaultSkin);
                    details.Map("weight_class", to => to.WeightClass, MapWeightClass);
                    details.Map("defense", to => to.Defense);
                }
            );
        }

        private static void DiscriminateArmor(JsonDiscriminatorMapping<Armor> armor)
        {
            armor.Map<Boots>("Boots");
            armor.Map<Coat>("Coat");
            armor.Map<Gloves>("Gloves");
            armor.Map<Helm>("Helm");
            armor.Map<HelmAquatic>("HelmAquatic");
            armor.Map<Leggings>("Leggings");
            armor.Map<Shoulders>("Shoulders");
        }

        private static WeightClass MapWeightClass
            (in JsonElement element, in JsonPath path) =>
            Enum.Parse<WeightClass>(element.GetString(), false);
    }
}

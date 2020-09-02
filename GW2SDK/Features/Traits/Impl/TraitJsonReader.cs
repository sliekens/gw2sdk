using System;
using System.Text.Json;
using GW2SDK.Impl.JsonReaders;
using GW2SDK.Impl.JsonReaders.Mappings;
using GW2SDK.Traits.Impl.TraitFacts;
using static GW2SDK.Impl.JsonReaders.Mappings.MappingSignificance;

namespace GW2SDK.Traits.Impl
{
    internal sealed class TraitJsonReader : JsonObjectReader<Trait>
    {
        private readonly UnexpectedPropertyBehavior _unexpectedPropertyBehavior;

        private TraitJsonReader(UnexpectedPropertyBehavior unexpectedPropertyBehavior)
        {
            _unexpectedPropertyBehavior = unexpectedPropertyBehavior;
            Configure(MapTrait);
        }

        public static IJsonReader<Trait> Instance { get; } = new TraitJsonReader(UnexpectedPropertyBehavior.Error);

        private void MapTrait(JsonObjectMapping<Trait> trait)
        {
            trait.UnexpectedPropertyBehavior = _unexpectedPropertyBehavior;
            trait.Map("id", to => to.Id);
            trait.Map("tier", to => to.Tier);
            trait.Map("order", to => to.Order);
            trait.Map("name", to => to.Name);
            trait.Map("slot", to => to.Slot, (in JsonElement element, in JsonPath path) => Enum.Parse<TraitSlot>(element.GetString(), false));
            trait.Map("facts", to => to.Facts, MapTraitFact, Optional);
            trait.Map("traited_facts", to => to.TraitedFacts, MapTraitFact, Optional);
            trait.Map("specialization", to => to.SpezializationId);
            trait.Map("icon", to => to.Icon);
            trait.Map("description", to => to.Description, Optional);
            trait.Map("skills", to => to.Skills, MapTraitSkill, Optional);
        }

        private void MapTraitSkill(JsonObjectMapping<TraitSkill> traitSkill)
        {
            traitSkill.UnexpectedPropertyBehavior = _unexpectedPropertyBehavior;
            traitSkill.Map("id", to => to.Id);
            traitSkill.Map("name", to => to.Name);
            traitSkill.Map("facts", to => to.Facts, MapTraitFact);
            traitSkill.Map("traited_facts", to => to.TraitedFacts, MapTraitFact, Optional);
            traitSkill.Map("description", to => to.Description);
            traitSkill.Map("icon", to => to.Icon);
            traitSkill.Map(
                "flags",
                to => to.Flags,
                (in JsonElement element, in JsonPath path) => Enum.Parse<TraitSkillFlag>(element.GetString(), false),
                Optional
            );
            traitSkill.Map(
                "categories",
                to => to.Categories,
                (in JsonElement element, in JsonPath path) => Enum.Parse<SkillCategoryName>(element.GetString(), false),
                Optional
            );
            traitSkill.Map("chat_link", to => to.ChatLink);
        }

        private TraitFact MapTraitFact(in JsonElement element, in JsonPath path)
        {
            if (element.ValueKind != JsonValueKind.Object)
            {
                throw new JsonException("Value is not an object.", path.ToString(), null, null);
            }

            var discriminator = element.TryGetProperty("type", out var type) ? type.GetString() : "";
            return discriminator switch
            {
                "AttributeAdjust" => AttributeAdjustTraitFactJsonReader.Instance.Read(element, path),
                "Buff" => BuffTraitFactJsonReader.Instance.Read(element, path),
                "BuffConversion" => BuffConversionTraitFactJsonReader.Instance.Read(element, path),
                "ComboField" => ComboFieldTraitFactJsonReader.Instance.Read(element, path),
                "ComboFinisher" => ComboFinisherTraitFactJsonReader.Instance.Read(element, path),
                "Damage" => DamageTraitFactJsonReader.Instance.Read(element, path),
                "Distance" => DistanceTraitFactJsonReader.Instance.Read(element, path),
                "NoData" => NoDataTraitFactJsonReader.Instance.Read(element, path),
                "Number" => NumberTraitFactJsonReader.Instance.Read(element, path),
                "Percent" => PercentTraitFactJsonReader.Instance.Read(element, path),
                "PrefixedBuff" => PrefixedBuffTraitFactJsonReader.Instance.Read(element, path),
                "Radius" => RadiusTraitFactJsonReader.Instance.Read(element, path),
                "Range" => RangeTraitFactJsonReader.Instance.Read(element, path),
                "Recharge" => RechargeTraitFactJsonReader.Instance.Read(element, path),
                "StunBreak" => StunBreakTraitFactJsonReader.Instance.Read(element, path),
                "Time" => TimeTraitFactJsonReader.Instance.Read(element, path),
                "Unblockable" => UnblockableTraitFactJsonReader.Instance.Read(element, path),
                // BUG: Life Force Cost trait facts don't have a 'type'
                _ when element.TryGetProperty("percent", out _) => LifeForceCostTraitFactJsonReader.Instance.Read(element, path),
                _ when _unexpectedPropertyBehavior == UnexpectedPropertyBehavior.Ignore => TraitFactJsonReader<TraitFact>.Instance.Read(element, path),
                _ => throw new JsonException($"Could not find a type derived from 'TraitFact' for value '{discriminator}'.", path.ToString(), null, null)
            };

        }
    }
}

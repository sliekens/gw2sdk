using System.Text.Json;
using GW2SDK.Impl.JsonReaders;

namespace GW2SDK.Traits.Impl.TraitFacts
{
    internal sealed class TraitFactDiscriminatingJsonReader : IJsonReader<TraitFact>
    {
        private readonly UnexpectedPropertyBehavior _unexpectedPropertyBehavior;

        public TraitFactDiscriminatingJsonReader(UnexpectedPropertyBehavior unexpectedPropertyBehavior)
        {
            _unexpectedPropertyBehavior = unexpectedPropertyBehavior;
        }

        public TraitFact Read(in JsonElement json)
        {
            if (json.ValueKind != JsonValueKind.Object)
            {
                throw new JsonException("JSON is not an object.");
            }

            var discriminator = json.TryGetProperty("type", out var type) ? type.GetString() : "";
            return discriminator switch
            {
                "AttributeAdjust" => AttributeAdjustTraitFactJsonReader.Instance.Read(json),
                "Buff" => BuffTraitFactJsonReader.Instance.Read(json),
                "BuffConversion" => BuffConversionTraitFactJsonReader.Instance.Read(json),
                "ComboField" => ComboFieldTraitFactJsonReader.Instance.Read(json),
                "ComboFinisher" => ComboFinisherTraitFactJsonReader.Instance.Read(json),
                "Damage" => DamageTraitFactJsonReader.Instance.Read(json),
                "Distance" => DistanceTraitFactJsonReader.Instance.Read(json),
                "NoData" => NoDataTraitFactJsonReader.Instance.Read(json),
                "Number" => NumberTraitFactJsonReader.Instance.Read(json),
                "Percent" => PercentTraitFactJsonReader.Instance.Read(json),
                "PrefixedBuff" => PrefixedBuffTraitFactJsonReader.Instance.Read(json),
                "Radius" => RadiusTraitFactJsonReader.Instance.Read(json),
                "Range" => RangeTraitFactJsonReader.Instance.Read(json),
                "Recharge" => RechargeTraitFactJsonReader.Instance.Read(json),
                "StunBreak" => StunBreakTraitFactJsonReader.Instance.Read(json),
                "Time" => TimeTraitFactJsonReader.Instance.Read(json),
                "Unblockable" => UnblockableTraitFactJsonReader.Instance.Read(json),
                // BUG: Life Force Cost trait facts don't have a 'type'
                _ when json.TryGetProperty("percent", out _) => LifeForceCostTraitFactJsonReader.Instance.Read(json),
                _ when _unexpectedPropertyBehavior == UnexpectedPropertyBehavior.Ignore => TraitFactJsonReader<TraitFact>.Instance.Read(json),
                _ => throw new JsonException($"Could not find a type derived from 'TraitFact' for value '{discriminator}'")
            };

        }

        public bool CanRead(in JsonElement json) => json.ValueKind == JsonValueKind.Object;
    }
}

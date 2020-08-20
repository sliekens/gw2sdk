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

        public TraitFact Read(in JsonElement json, in JsonPath path)
        {
            if (json.ValueKind != JsonValueKind.Object)
            {
                throw new JsonException($"Value at '{path.ToString()}' is not an object.");
            }

            var discriminator = json.TryGetProperty("type", out var type) ? type.GetString() : "";
            return discriminator switch
            {
                "AttributeAdjust" => AttributeAdjustTraitFactJsonReader.Instance.Read(json, path),
                "Buff" => BuffTraitFactJsonReader.Instance.Read(json, path),
                "BuffConversion" => BuffConversionTraitFactJsonReader.Instance.Read(json, path),
                "ComboField" => ComboFieldTraitFactJsonReader.Instance.Read(json, path),
                "ComboFinisher" => ComboFinisherTraitFactJsonReader.Instance.Read(json, path),
                "Damage" => DamageTraitFactJsonReader.Instance.Read(json, path),
                "Distance" => DistanceTraitFactJsonReader.Instance.Read(json, path),
                "NoData" => NoDataTraitFactJsonReader.Instance.Read(json, path),
                "Number" => NumberTraitFactJsonReader.Instance.Read(json, path),
                "Percent" => PercentTraitFactJsonReader.Instance.Read(json, path),
                "PrefixedBuff" => PrefixedBuffTraitFactJsonReader.Instance.Read(json, path),
                "Radius" => RadiusTraitFactJsonReader.Instance.Read(json, path),
                "Range" => RangeTraitFactJsonReader.Instance.Read(json, path),
                "Recharge" => RechargeTraitFactJsonReader.Instance.Read(json, path),
                "StunBreak" => StunBreakTraitFactJsonReader.Instance.Read(json, path),
                "Time" => TimeTraitFactJsonReader.Instance.Read(json, path),
                "Unblockable" => UnblockableTraitFactJsonReader.Instance.Read(json, path),
                // BUG: Life Force Cost trait facts don't have a 'type'
                _ when json.TryGetProperty("percent", out _) => LifeForceCostTraitFactJsonReader.Instance.Read(json, path),
                _ when _unexpectedPropertyBehavior == UnexpectedPropertyBehavior.Ignore => TraitFactJsonReader<TraitFact>.Instance.Read(json, path),
                _ => throw new JsonException($"Could not find a type derived from 'TraitFact' for value '{discriminator}' at '{path.ToString()}'.")
            };

        }

        public bool CanRead(in JsonElement json) => json.ValueKind == JsonValueKind.Object;
    }
}

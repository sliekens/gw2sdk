using System;
using System.Collections.Generic;
using System.Text.Json;
using GW2SDK.Impl.JsonConverters;
using GW2SDK.Impl.JsonReaders;
using JsonValueKind = System.Text.Json.JsonValueKind;

namespace GW2SDK.Items.Impl
{
    public class ItemJsonReader : IJsonReader<Item>
    {
        public Item Read(in JsonElement json)
        {
            if (json.TryGetProperty("type", out var discriminator))
            {
                if (discriminator.ValueEquals("Armor"))
                {
                    if (json.TryGetProperty("details", out var details))
                    {
                        if (json.TryGetProperty("type", out var armorDiscriminator))
                        {
                            if (armorDiscriminator.ValueEquals("Boots"))
                            {
                                return new ArmorJsonReader().Read(json);
                            }
                        }
                    }
                }
            }

            throw new NotSupportedException();
        }

        public bool CanRead(in JsonElement json) => json.ValueKind == JsonValueKind.Object;
    }

    public class ArmorJsonReader : JsonObjectReader<Armor>
    {
        public ArmorJsonReader()
        {
            Map("id", to => to.Id);
            Map("details.defense", to => to.Defense);
        }

    }

    internal sealed class ArmorDiscriminatorOptions : DiscriminatorOptions
    {
        internal override Type BaseType => typeof(Armor);

        internal override string DiscriminatorFieldName => "type";

        internal override bool SerializeDiscriminator => false;

        internal override IEnumerable<(string TypeName, Type Type)> GetDiscriminatedTypes()
        {
            yield return ("Boots", typeof(Boots));
            yield return ("Coat", typeof(Coat));
            yield return ("Gloves", typeof(Gloves));
            yield return ("Helm", typeof(Helm));
            yield return ("HelmAquatic", typeof(HelmAquatic));
            yield return ("Leggings", typeof(Leggings));
            yield return ("Shoulders", typeof(Shoulders));
        }

        internal override object CreateInstance(Type discriminatedType)
        {
            if (discriminatedType == typeof(Boots)) return new Boots();
            if (discriminatedType == typeof(Coat)) return new Coat();
            if (discriminatedType == typeof(Gloves)) return new Gloves();
            if (discriminatedType == typeof(Helm)) return new Helm();
            if (discriminatedType == typeof(HelmAquatic)) return new HelmAquatic();
            if (discriminatedType == typeof(Leggings)) return new Leggings();
            if (discriminatedType == typeof(Shoulders)) return new Shoulders();
            return new Armor();
        }
    }
}

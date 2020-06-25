using System;
using System.Collections.Generic;
using GW2SDK.Impl;
using GW2SDK.Impl.JsonConverters;
using Newtonsoft.Json.Linq;

namespace GW2SDK.Skins.Impl
{
    internal sealed class SkinDiscriminatorOptions : DiscriminatorOptions
    {
        internal override Type BaseType => typeof(Skin);

        internal override string DiscriminatorFieldName => "type";

        internal override bool SerializeDiscriminator => false;

        internal override IEnumerable<(string TypeName, Type Type)> GetDiscriminatedTypes()
        {
            yield return ("Armor", typeof(ArmorSkin));
            yield return ("Back", typeof(BackItemSkin));
            yield return ("Gathering", typeof(GatheringToolSkin));
            yield return ("Weapon", typeof(WeaponSkin));
        }

        internal override void Preprocess(string discriminator, JObject jsonObject)
        {
            var @namespace = StringHelper.ToSnakeCase(discriminator);

            // Skins can have extra fields depending on the type of skin
            // Anet decided to put those extra fields in a "details" property
            // For us it's much more convenient to flatten the root object before serializing the JSON to CLR objects
            if (jsonObject.Property("details")?.Value is JObject details)
            {
                foreach (var property in details.Properties())
                {
                    // There can be a naming collision that prevents a clean merge
                    // Prefix those duplicate names with the discriminator
                    if (jsonObject.ContainsKey(property.Name))
                    {
                        jsonObject.Add($"{@namespace}_{property.Name}", property.Value);
                    }
                    else
                    {
                        jsonObject.Add(property.Name, property.Value);
                    }
                }

                // Details should now be removed because all its properties have been added to the root object.
                jsonObject.Remove("details");
            }
        }

        internal override object CreateInstance(Type discriminatedType)
        {
            if (discriminatedType == typeof(ArmorSkin)) return new ArmorSkin();
            if (discriminatedType == typeof(BackItemSkin)) return new BackItemSkin();
            if (discriminatedType == typeof(GatheringToolSkin)) return new GatheringToolSkin();
            if (discriminatedType == typeof(WeaponSkin)) return new WeaponSkin();
            return new Skin();
        }
    }
}

using System;
using System.Collections.Generic;
using GW2SDK.Impl;
using GW2SDK.Impl.JsonConverters;
using Newtonsoft.Json.Linq;

namespace GW2SDK.Skins.Impl
{
    public sealed class SkinDiscriminatorOptions : DiscriminatorOptions
    {
        public SkinDiscriminatorOptions()
        {
            Activator = Create;
            Preprocessor = Preprocess;
        }

        public override Type BaseType => typeof(Skin);

        public override string DiscriminatorFieldName => "type";

        public override bool SerializeDiscriminator => false;

        private void Preprocess(string discriminator, JObject json)
        {
            var @namespace = StringHelper.ToSnakeCase(discriminator);

            // Skins can have extra fields depending on the type of skin
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
            yield return ("Armor", typeof(ArmorSkin));
            yield return ("Back", typeof(BackItemSkin));
            yield return ("Gathering", typeof(GatheringToolSkin));
            yield return ("Weapon", typeof(WeaponSkin));
        }

        public object Create(Type objectType)
        {
            if (objectType == typeof(ArmorSkin)) return new ArmorSkin();
            if (objectType == typeof(BackItemSkin)) return new BackItemSkin();
            if (objectType == typeof(GatheringToolSkin)) return new GatheringToolSkin();
            if (objectType == typeof(WeaponSkin)) return new WeaponSkin();
            return new Skin();
        }
    }
}

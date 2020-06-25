using System;
using System.Collections.Generic;
using GW2SDK.Impl;
using GW2SDK.Impl.JsonConverters;
using Newtonsoft.Json.Linq;

namespace GW2SDK.Items.Impl
{
    internal sealed class ItemDiscriminatorOptions : DiscriminatorOptions
    {
        internal override Type BaseType => typeof(Item);

        internal override string DiscriminatorFieldName => "type";

        internal override bool SerializeDiscriminator => false;

        internal override IEnumerable<(string TypeName, Type Type)> GetDiscriminatedTypes()
        {
            yield return ("Armor", typeof(Armor));
            yield return ("Back", typeof(BackItem));
            yield return ("Bag", typeof(Bag));
            yield return ("Consumable", typeof(Consumable));
            yield return ("Container", typeof(Container));
            yield return ("CraftingMaterial", typeof(CraftingMaterial));
            yield return ("Gathering", typeof(GatheringTool));
            yield return ("Gizmo", typeof(Gizmo));
            yield return ("Key", typeof(Key));
            yield return ("MiniPet", typeof(Minipet));
            yield return ("Tool", typeof(Tool));
            yield return ("Trinket", typeof(Trinket));
            yield return ("Trophy", typeof(Trophy));
            yield return ("UpgradeComponent", typeof(UpgradeComponent));
            yield return ("Weapon", typeof(Weapon));
        }

        internal override void Preprocess(string discriminator, JObject jsonObject)
        {
            var @namespace = StringHelper.ToSnakeCase(discriminator);

            // Items can have extra fields depending on the type of item (e.g. weapons have max_power, trophies don't)
            // Anet decided to put those extra fields in a "details" property
            // For us it's much more convenient to flatten the root object before serializing the jsonObject to CLR objects
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
            if (discriminatedType == typeof(Armor)) return new Armor();
            if (discriminatedType == typeof(BackItem)) return new BackItem();
            if (discriminatedType == typeof(Bag)) return new Bag();
            if (discriminatedType == typeof(Consumable)) return new Consumable();
            if (discriminatedType == typeof(Container)) return new Container();
            if (discriminatedType == typeof(CraftingMaterial)) return new CraftingMaterial();
            if (discriminatedType == typeof(GatheringTool)) return new GatheringTool();
            if (discriminatedType == typeof(Gizmo)) return new Gizmo();
            if (discriminatedType == typeof(Key)) return new Key();
            if (discriminatedType == typeof(Minipet)) return new Minipet();
            if (discriminatedType == typeof(Tool)) return new Tool();
            if (discriminatedType == typeof(Trinket)) return new Trinket();
            if (discriminatedType == typeof(Trophy)) return new Trophy();
            if (discriminatedType == typeof(UpgradeComponent)) return new UpgradeComponent();
            if (discriminatedType == typeof(Weapon)) return new Weapon();
            return new Item();
        }
    }
}

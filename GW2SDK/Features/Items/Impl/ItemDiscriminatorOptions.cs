using System;
using System.Collections.Generic;
using GW2SDK.Impl;
using GW2SDK.Impl.JsonConverters;
using Newtonsoft.Json.Linq;

namespace GW2SDK.Items.Impl
{
    public sealed class ItemDiscriminatorOptions : DiscriminatorOptions
    {
        public ItemDiscriminatorOptions()
        {
            Activator = Create;
            Preprocessor = Preprocess;
        }

        public override Type BaseType => typeof(Item);

        public override string DiscriminatorFieldName => "type";

        public override bool SerializeDiscriminator => false;

        private void Preprocess(string discriminator, JObject json)
        {
            var @namespace = StringHelper.ToSnakeCase(discriminator);

            // Items can have extra fields depending on the type of item (e.g. weapons have max_power, trophies don't)
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

        public object Create(Type objectType)
        {
            if (objectType == typeof(Armor)) return new Armor();
            if (objectType == typeof(BackItem)) return new BackItem();
            if (objectType == typeof(Bag)) return new Bag();
            if (objectType == typeof(Consumable)) return new Consumable();
            if (objectType == typeof(Container)) return new Container();
            if (objectType == typeof(CraftingMaterial)) return new CraftingMaterial();
            if (objectType == typeof(GatheringTool)) return new GatheringTool();
            if (objectType == typeof(Gizmo)) return new Gizmo();
            if (objectType == typeof(Key)) return new Key();
            if (objectType == typeof(Minipet)) return new Minipet();
            if (objectType == typeof(Tool)) return new Tool();
            if (objectType == typeof(Trinket)) return new Trinket();
            if (objectType == typeof(Trophy)) return new Trophy();
            if (objectType == typeof(UpgradeComponent)) return new UpgradeComponent();
            if (objectType == typeof(Weapon)) return new Weapon();
            return new Item();
        }
    }
}

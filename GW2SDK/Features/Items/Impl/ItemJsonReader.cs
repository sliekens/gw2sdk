using System;
using System.Text.Json;
using GW2SDK.Enums;
using GW2SDK.Impl.JsonReaders;
using GW2SDK.Impl.JsonReaders.Mappings;
using static GW2SDK.Impl.JsonReaders.Mappings.MappingSignificance;

namespace GW2SDK.Items.Impl
{
    public sealed partial class ItemJsonReader : JsonObjectReader<Item>
    {
        public ItemJsonReader()
        {
            Configure(MapItem);
        }

        private static void MapItem(JsonObjectMapping<Item> mapping)
        {
            mapping.Discriminate(GetItemDiscriminator, DiscriminateItem);
            mapping.Ignore("type");
            mapping.Map("id", to => to.Id);
            mapping.Map("name", to => to.Name);
            mapping.Map("description", to => to.Description, Optional);
            mapping.Map("rarity", to => to.Rarity, MapRarity);
            mapping.Map("vendor_value", to => to.VendorValue);
            mapping.Map("game_types", to => to.GameTypes, MapGameType);
            mapping.Map("flags", to => to.Flags, MapItemFlag);
            mapping.Map("restrictions", to => to.Restrictions, MapItemRestriction);
            mapping.Map("chat_link", to => to.Restrictions, MapItemRestriction);
            mapping.Map("icon", to => to.Icon, Optional);
            mapping.Map
            (
                "upgrades_from",
                to => to.UpgradesFrom,
                MapItemUpgrade,
                Optional
            );
            mapping.Map
            (
                "upgrades_into",
                to => to.UpgradesInto,
                MapItemUpgrade,
                Optional
            );
        }

        private static string GetItemDiscriminator(JsonElement element) =>
            element.GetProperty("type").GetString();

        private static void DiscriminateItem(JsonDiscriminatorMapping<Item> item)
        {
            item.Map<Armor>("Armor", MapArmor);
        }

        private static void MapItemUpgrade(JsonObjectMapping<ItemUpgrade> upgrade)
        {
            upgrade.Map("upgrade", to => to.Upgrade, MapUpgradeType);
            upgrade.Map("item_id", to => to.ItemId);
        }

        private static UpgradeType MapUpgradeType(in JsonElement element, in JsonPath path) =>
            Enum.Parse<UpgradeType>(element.GetString(), false);

        private static ItemRestriction MapItemRestriction
            (in JsonElement element, in JsonPath path) =>
            Enum.Parse<ItemRestriction>(element.GetString(), false);

        private static GameType MapGameType(in JsonElement element, in JsonPath path) =>
            Enum.Parse<GameType>(element.GetString(), false);

        private static ItemFlag MapItemFlag(in JsonElement element, in JsonPath path) =>
            Enum.Parse<ItemFlag>(element.GetString(), false);

        private static Rarity MapRarity(in JsonElement element, in JsonPath path) =>
            Enum.Parse<Rarity>(element.GetString(), false);
    }
}

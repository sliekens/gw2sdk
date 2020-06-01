using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace GW2SDK.Tests.Features.Items.Fixtures
{
    public class InMemoryItemDb
    {
        public InMemoryItemDb(IEnumerable<string> objects)
        {
            Items = objects.ToList().AsReadOnly();
        }

        public IReadOnlyCollection<string> Items { get; }

        public IEnumerable<string> GetItemFlags()
        {
            return (
                    from json in Items
                    let jobject = JObject.Parse(json)
                    let entries = jobject.SelectTokens("flags[*]")
                    select entries.Select(token => token.ToString()))
                .SelectMany(values => values)
                .OrderBy(s => s)
                .Distinct();
        }

        public IEnumerable<string> GetItemTypeNames()
        {
            return (
                    from json in Items
                    let jobject = JObject.Parse(json)
                    select jobject.SelectToken("type").ToString()).OrderBy(s => s)
                .Distinct();
        }

        public IEnumerable<string> GetConsumableTypeNames() =>
            (
                from json in Items
                let jobject = JObject.Parse(json)
                where jobject.SelectToken("type").ToString() == "Consumable"
                select jobject.SelectToken("$.details.type").ToString()).OrderBy(s => s)
            .Distinct();

        public IEnumerable<string> GetUnlockerTypeNames() =>
            (
                from json in Items
                let jobject = JObject.Parse(json)
                where jobject.SelectToken("$.type").ToString() == "Consumable"
                where jobject.SelectToken("$.details.type").ToString() == "Unlock"
                select jobject.SelectToken("$.details.unlock_type").ToString()).OrderBy(s => s)
            .Distinct();

        public IEnumerable<string> GetContainerTypeNames() =>
            (
                from json in Items
                let jobject = JObject.Parse(json)
                where jobject.SelectToken("type").ToString() == "Container"
                select jobject.SelectToken("$.details.type").ToString()).OrderBy(s => s)
            .Distinct();

        public IEnumerable<string> GetGizmoTypeNames() =>
            (
                from json in Items
                let jobject = JObject.Parse(json)
                where jobject.SelectToken("type").ToString() == "Gizmo"
                select jobject.SelectToken("$.details.type").ToString()).OrderBy(s => s)
            .Distinct();

        public IEnumerable<string> GetWeaponTypeNames() =>
            (
                from jobject in GetItemsByType("Weapon")
                select jobject.SelectToken("$.details.type").ToString()).OrderBy(s => s)
            .Distinct();

        public IEnumerable<string> GetArmorTypeNames() =>
            (
                from jobject in GetItemsByType("Armor")
                select jobject.SelectToken("$.details.type").ToString()).OrderBy(s => s)
            .Distinct();

        public IEnumerable<string> GetTrinketTypeNames() =>
            (
                from jobject in GetItemsByType("Trinket")
                select jobject.SelectToken("$.details.type").ToString()).OrderBy(s => s)
            .Distinct();

        public IEnumerable<string> GetToolTypeNames() =>
            (
                from jobject in GetItemsByType("Tool")
                select jobject.SelectToken("$.details.type").ToString()).OrderBy(s => s)
            .Distinct();

        public IEnumerable<string> GetUpgradeComponentTypeNames() =>
            (
                from jobject in GetItemsByType("UpgradeComponent")
                select jobject.SelectToken("$.details.type").ToString()).OrderBy(s => s)
            .Distinct();

        public IEnumerable<string> GetGatheringToolTypeNames() =>
            (
                from jobject in GetItemsByType("Gathering")
                select jobject.SelectToken("$.details.type").ToString()).OrderBy(s => s)
            .Distinct();

        public IEnumerable<JObject> GetItemsByType(string typeName) =>
            from json in Items
            let jobject = JObject.Parse(json)
            where jobject.SelectToken("type").ToString() == typeName
            select jobject;

        public IEnumerable<string> GetConsumableItems() => GetItemsByType("Consumable").Select(x => x.ToString());

        public IEnumerable<string> GetConsumableTransmutationItems() =>
        (
            from consumable in GetItemsByType("Consumable")
            where consumable.SelectToken("$.details.type").ToString() == "Transmutation"
            select consumable).Select(x => x.ToString());

        public IEnumerable<string> GetWeaponItems() => GetItemsByType("Weapon").Select(x => x.ToString());

        public IEnumerable<string> GetArmorItems() => GetItemsByType("Armor").Select(x => x.ToString());

        public IEnumerable<string> GetBackItems() => GetItemsByType("Back").Select(x => x.ToString());

        public IEnumerable<string> GetTrophyItems() => GetItemsByType("Tropy").Select(x => x.ToString());

        public IEnumerable<string> GetUpgradeComponentItems() => GetItemsByType("UpgradeComponent").Select(x => x.ToString());

        public IEnumerable<string> GetItemRarities()
        {
            return (
                    from json in Items
                    let jobject = JObject.Parse(json)
                    let entries = jobject.SelectTokens("rarity")
                    select entries.Select(token => token.ToString()))
                .SelectMany(values => values)
                .OrderBy(s => s)
                .Distinct();
        }

        public IEnumerable<string> GetItemGameTypes()
        {
            return (
                    from json in Items
                    let jobject = JObject.Parse(json)
                    let entries = jobject.SelectTokens("game_types[*]")
                    select entries.Select(token => token.ToString()))
                .SelectMany(values => values)
                .OrderBy(s => s)
                .Distinct();
        }

        public IEnumerable<string> GetItemRestrictions()
        {
            return (
                    from json in Items
                    let jobject = JObject.Parse(json)
                    let entries = jobject.SelectTokens("restrictions[*]")
                    select entries.Select(token => token.ToString()))
                .SelectMany(values => values)
                .OrderBy(s => s)
                .Distinct();
        }

        public IEnumerable<string> GetWeaponDamageTypes() =>
            (
                from jobject in GetItemsByType("Weapon")
                select jobject.SelectToken("$.details.damage_type").ToString()).OrderBy(s => s)
            .Distinct();

        public IEnumerable<string> GetInfixUpgradeAttributeNames() =>
            (
                from json in Items
                let jobject = JObject.Parse(json)
                let entries = jobject.SelectTokens("$.details.infix_upgrade.attributes[*].attribute")
                select entries.Select(token => token.ToString()))
            .SelectMany(values => values)
            .OrderBy(s => s)
            .Distinct();

        public IEnumerable<string> GetInfusionSlotFlags() =>
            (
                from json in Items
                let jobject = JObject.Parse(json)
                let entries = jobject.SelectTokens("$.details.infusion_slots[*].flags[*]")
                select entries.Select(token => token.ToString()))
            .SelectMany(values => values)
            .OrderBy(s => s)
            .Distinct();

        public IEnumerable<string> GetArmorWeightClasses() =>
            (
                from jobject in GetItemsByType("Armor")
                select jobject.SelectToken("$.details.weight_class").ToString()).OrderBy(s => s)
            .Distinct();

        public IEnumerable<string> GetUpgradeComponentFlags() =>
            (
                from jobject in GetItemsByType("UpgradeComponent")
                let entries = jobject.SelectTokens("$.details.flags[*]")
                select entries.Select(token => token.ToString()))
            .SelectMany(values => values)
            .OrderBy(s => s)
            .Distinct();

        public IEnumerable<string> GetUpgradeComponentInfusionUpgradeFlags() =>
            (
                from jobject in GetItemsByType("UpgradeComponent")
                let entries = jobject.SelectTokens("$.details.infusion_upgrade_flags[*]")
                select entries.Select(token => token.ToString()))
            .SelectMany(values => values)
            .OrderBy(s => s)
            .Distinct();

        public IEnumerable<string> GetUpgradedItemTypes() =>
            (
                from json in Items
                let jobject = JObject.Parse(json)
                let entries = jobject.SelectTokens("$.upgrades_into[*].upgrade")
                select entries.Select(token => token.ToString()))
            .SelectMany(values => values)
            .OrderBy(s => s)
            .Distinct();
    }
}

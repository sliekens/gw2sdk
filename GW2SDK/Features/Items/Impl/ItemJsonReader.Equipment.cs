using System;
using System.Text.Json;
using GW2SDK.Enums;
using GW2SDK.Impl.JsonReaders;
using GW2SDK.Impl.JsonReaders.Mappings;

namespace GW2SDK.Items.Impl
{
    public sealed partial class ItemJsonReader
    {
        private static void MapInfixUpgrade(JsonObjectMapping<InfixUpgrade> infixUpgrade)
        {
            infixUpgrade.Map("id", to => to.Id);
            infixUpgrade.Map("attributes", to => to.Attributes, MapUpgradeAttribute);
            infixUpgrade.Map("buff", to => to.Buff, buff => MapBuff(buff!));
        }

        private static void MapBuff(JsonObjectMapping<Buff> buff)
        {
            buff.Map("skill_id", to => to.SkillId);
            buff.Map("description", to => to.Description, MappingSignificance.Optional);
        }

        private static void MapUpgradeAttribute
            (JsonObjectMapping<UpgradeAttribute> upgradeAttribute)
        {
            upgradeAttribute.Map("attribute", to => to.Attribute, MapUpgradeAttributeName);
            upgradeAttribute.Map("modifier", to => to.Modifier);
        }

        private static void MapInfusionSlot(JsonObjectMapping<InfusionSlot> infusionSlot)
        {
            infusionSlot.Map("flags", to => to.Flags, MapInfusionSlotFlag);
            infusionSlot.Map("item_id", to => to.ItemId);
        }

        private static InfusionSlotFlag MapInfusionSlotFlag
            (in JsonElement jsonelement, in JsonPath jsonpath) =>
            Enum.Parse<InfusionSlotFlag>(jsonelement.GetString(), false);

        private static UpgradeAttributeName MapUpgradeAttributeName
            (in JsonElement jsonelement, in JsonPath jsonpath) =>
            Enum.Parse<UpgradeAttributeName>(jsonelement.GetString(), false);
    }
}

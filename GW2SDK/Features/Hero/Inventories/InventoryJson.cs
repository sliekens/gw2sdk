﻿using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Hero.Inventories;

internal static class InventoryJson
{
    public static Inventory GetInventory(this JsonElement json)
    {
        return new Inventory { Items = json.GetList(static value => value.GetItemSlot()) };
    }
}

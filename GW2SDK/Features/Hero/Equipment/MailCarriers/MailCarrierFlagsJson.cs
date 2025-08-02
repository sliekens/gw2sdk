﻿using System.Text.Json;

using GuildWars2.Collections;
using GuildWars2.Json;

namespace GuildWars2.Hero.Equipment.MailCarriers;

internal static class MailCarrierFlagsJson
{
    public static MailCarrierFlags GetMailCarrierFlags(this in JsonElement json)
    {
        var @default = false;
        ValueList<string> others = [];
        foreach (var entry in json.EnumerateArray())
        {
            if (entry.ValueEquals("Default"))
            {
                @default = true;
            }
            else
            {
                others.Add(entry.GetStringRequired());
            }
        }

        return new MailCarrierFlags
        {
            Default = @default,
            Other = others
        };
    }
}

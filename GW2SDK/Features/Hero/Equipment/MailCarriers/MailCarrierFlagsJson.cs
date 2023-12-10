using System.Text.Json;
using GuildWars2.Json;

namespace GuildWars2.Hero.Equipment.MailCarriers;

internal static class MailCarrierFlagsJson
{
    public static MailCarrierFlags GetMailCarrierFlags(this JsonElement json)
    {
        var @default = false;
        List<string>? others = null;
        foreach (var entry in json.EnumerateArray())
        {
            if (entry.ValueEquals("Default"))
            {
                @default = true;
            }
            else
            {
                others ??= new List<string>();
                others.Add(entry.GetStringRequired());
            }
        }

        return new MailCarrierFlags
        {
            Default = @default,
            Other = others ?? Empty.ListOfString
        };
    }
}
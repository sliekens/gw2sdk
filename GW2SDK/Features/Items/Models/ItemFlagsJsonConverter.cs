using System.Text.Json;
using System.Text.Json.Serialization;
using GuildWars2.Json;

namespace GuildWars2.Items;

internal class ItemFlagsJsonConverter : JsonConverter<ItemFlags>
{
    public override ItemFlags? Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options
    )
    {
        using var json = JsonDocument.ParseValue(ref reader);
        return Read(json.RootElement);
    }

    public override void Write(
        Utf8JsonWriter writer,
        ItemFlags value,
        JsonSerializerOptions options
    )
    {
        Write(writer, value);
    }

    public static ItemFlags Read(in JsonElement json)
    {
        return new()
        {
            AccountBindOnUse = json.GetProperty("account_bind_on_use").GetBoolean(),
            AccountBound = json.GetProperty("account_bound").GetBoolean(),
            Attuned = json.GetProperty("attuned").GetBoolean(),
            BulkConsume = json.GetProperty("bulk_consume").GetBoolean(),
            DeleteWarning = json.GetProperty("delete_warning").GetBoolean(),
            HideSuffix = json.GetProperty("hide_suffix").GetBoolean(),
            Infused = json.GetProperty("infused").GetBoolean(),
            MonsterOnly = json.GetProperty("monster_only").GetBoolean(),
            NoMysticForge = json.GetProperty("no_mystic_forge").GetBoolean(),
            NoSalvage = json.GetProperty("no_salvage").GetBoolean(),
            NoSell = json.GetProperty("no_sell").GetBoolean(),
            NotUpgradeable = json.GetProperty("not_upgradeable").GetBoolean(),
            NoUnderwater = json.GetProperty("no_underwater").GetBoolean(),
            SoulbindOnUse = json.GetProperty("soulbind_on_use").GetBoolean(),
            Soulbound = json.GetProperty("soulbound").GetBoolean(),
            Tonic = json.GetProperty("tonic").GetBoolean(),
            Unique = json.GetProperty("unique").GetBoolean(),
            Other = json.GetProperty("other").GetList(static (in JsonElement value) => value.GetStringRequired())
        };
    }

    public static void Write(Utf8JsonWriter writer, ItemFlags value)
    {
        writer.WriteStartObject();
        writer.WriteBoolean("account_bind_on_use", value.AccountBindOnUse);
        writer.WriteBoolean("account_bound", value.AccountBound);
        writer.WriteBoolean("attuned", value.Attuned);
        writer.WriteBoolean("bulk_consume", value.BulkConsume);
        writer.WriteBoolean("delete_warning", value.DeleteWarning);
        writer.WriteBoolean("hide_suffix", value.HideSuffix);
        writer.WriteBoolean("infused", value.Infused);
        writer.WriteBoolean("monster_only", value.MonsterOnly);
        writer.WriteBoolean("no_mystic_forge", value.NoMysticForge);
        writer.WriteBoolean("no_salvage", value.NoSalvage);
        writer.WriteBoolean("no_sell", value.NoSell);
        writer.WriteBoolean("not_upgradeable", value.NotUpgradeable);
        writer.WriteBoolean("no_underwater", value.NoUnderwater);
        writer.WriteBoolean("soulbind_on_use", value.SoulbindOnUse);
        writer.WriteBoolean("soulbound", value.Soulbound);
        writer.WriteBoolean("tonic", value.Tonic);
        writer.WriteBoolean("unique", value.Unique);
        writer.WriteStartArray("other");
        foreach (var other in value.Other)
        {
            writer.WriteStringValue(other);
        }

        writer.WriteEndArray();
        writer.WriteEndObject();
    }
}

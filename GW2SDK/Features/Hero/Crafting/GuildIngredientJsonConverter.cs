﻿using System.Text.Json;
using System.Text.Json.Serialization;
using GuildWars2.Json;

namespace GuildWars2.Hero.Crafting;

internal sealed class GuildIngredientJsonConverter : JsonConverter<GuildIngredient>
{
    public override GuildIngredient Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using var json = JsonDocument.ParseValue(ref reader);
        return Read(json.RootElement);
    }

    public override void Write(Utf8JsonWriter writer, GuildIngredient value, JsonSerializerOptions options)
    {
        Write(writer, value);
    }

    public static GuildIngredient Read(JsonElement json)
    {
        return new GuildIngredient
        {
            UpgradeId = json.GetProperty("upgrade_id").GetInt32(),
            Count = json.GetProperty("count").GetInt32()
        };
    }

    public static void Write(Utf8JsonWriter writer, GuildIngredient value)
    {
        writer.WriteStartObject();
        writer.WriteNumber("upgrade_id", value.UpgradeId);
        writer.WriteNumber("count", value.Count);
        writer.WriteEndObject();
    }
}

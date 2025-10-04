using System.Text.Json;
using System.Text.Json.Serialization;

using GuildWars2.Json;

namespace GuildWars2.Items;

internal sealed class UpgradeComponentFlagsJsonConverter : JsonConverter<UpgradeComponentFlags>
{
    public override UpgradeComponentFlags? Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options
    )
    {
        using JsonDocument json = JsonDocument.ParseValue(ref reader);
        return Read(json.RootElement);
    }

    public override void Write(
        Utf8JsonWriter writer,
        UpgradeComponentFlags value,
        JsonSerializerOptions options
    )
    {
        Write(writer, value);
    }

    public static UpgradeComponentFlags Read(in JsonElement json)
    {
        return new()
        {
            HeavyArmor = json.GetProperty("heavy_armor").GetBoolean(),
            MediumArmor = json.GetProperty("medium_armor").GetBoolean(),
            LightArmor = json.GetProperty("light_armor").GetBoolean(),
            Axe = json.GetProperty("axe").GetBoolean(),
            Dagger = json.GetProperty("dagger").GetBoolean(),
            Focus = json.GetProperty("focus").GetBoolean(),
            Greatsword = json.GetProperty("greatsword").GetBoolean(),
            Hammer = json.GetProperty("hammer").GetBoolean(),
            HarpoonGun = json.GetProperty("harpoon_gun").GetBoolean(),
            LongBow = json.GetProperty("long_bow").GetBoolean(),
            Mace = json.GetProperty("mace").GetBoolean(),
            Pistol = json.GetProperty("pistol").GetBoolean(),
            Rifle = json.GetProperty("rifle").GetBoolean(),
            Scepter = json.GetProperty("scepter").GetBoolean(),
            Shield = json.GetProperty("shield").GetBoolean(),
            ShortBow = json.GetProperty("short_bow").GetBoolean(),
            Spear = json.GetProperty("spear").GetBoolean(),
            Staff = json.GetProperty("staff").GetBoolean(),
            Sword = json.GetProperty("sword").GetBoolean(),
            Torch = json.GetProperty("torch").GetBoolean(),
            Trident = json.GetProperty("trident").GetBoolean(),
            Trinket = json.GetProperty("trinket").GetBoolean(),
            Warhorn = json.GetProperty("warhorn").GetBoolean(),
            Other = json.GetProperty("other").GetList(static (in value) => value.GetStringRequired())
        };
    }

    public static void Write(Utf8JsonWriter writer, UpgradeComponentFlags value)
    {
        writer.WriteStartObject();
        writer.WriteBoolean("heavy_armor", value.HeavyArmor);
        writer.WriteBoolean("medium_armor", value.MediumArmor);
        writer.WriteBoolean("light_armor", value.LightArmor);
        writer.WriteBoolean("axe", value.Axe);
        writer.WriteBoolean("dagger", value.Dagger);
        writer.WriteBoolean("focus", value.Focus);
        writer.WriteBoolean("greatsword", value.Greatsword);
        writer.WriteBoolean("hammer", value.Hammer);
        writer.WriteBoolean("harpoon_gun", value.HarpoonGun);
        writer.WriteBoolean("long_bow", value.LongBow);
        writer.WriteBoolean("mace", value.Mace);
        writer.WriteBoolean("pistol", value.Pistol);
        writer.WriteBoolean("rifle", value.Rifle);
        writer.WriteBoolean("scepter", value.Scepter);
        writer.WriteBoolean("shield", value.Shield);
        writer.WriteBoolean("short_bow", value.ShortBow);
        writer.WriteBoolean("spear", value.Spear);
        writer.WriteBoolean("staff", value.Staff);
        writer.WriteBoolean("sword", value.Sword);
        writer.WriteBoolean("torch", value.Torch);
        writer.WriteBoolean("trident", value.Trident);
        writer.WriteBoolean("trinket", value.Trinket);
        writer.WriteBoolean("warhorn", value.Warhorn);
        writer.WriteStartArray("other");
        foreach (string other in value.Other)
        {
            writer.WriteStringValue(other);
        }

        writer.WriteEndArray();
        writer.WriteEndObject();
    }
}

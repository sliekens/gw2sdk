using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Equipment;

/// <summary>Information about a dye slot of an equipment piece.</summary>
[DataTransferObject]
[JsonConverter(typeof(DyeSlotJsonConverter))]
public sealed record DyeSlot
{
    /// <summary>The dye color ID which can be used to retrieve the color information.</summary>
    public required int ColorId { get; init; }

    /// <summary>The material of the dye slot. Each dye has a different RGB value depending on the material.</summary>
    public required Extensible<Material> Material { get; init; }
}

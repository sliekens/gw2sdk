using System.Text.Json.Serialization;

namespace GuildWars2.Items;

/// <summary>Information about a trident.</summary>
[JsonConverter(typeof(GuildWars2.ModelJsonConverterFactory))]
public sealed record Trident : Weapon
{
    /// <inheritdoc />
    public override bool TwoHanded => true;
}

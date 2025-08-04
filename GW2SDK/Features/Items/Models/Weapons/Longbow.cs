using System.Text.Json.Serialization;

namespace GuildWars2.Items;

/// <summary>Information about a longbow.</summary>
[JsonConverter(typeof(LongbowJsonConverter))]
public sealed record Longbow : Weapon
{
    /// <inheritdoc />
    public override bool TwoHanded => true;
}

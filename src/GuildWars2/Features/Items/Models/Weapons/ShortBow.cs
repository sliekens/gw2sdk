using System.Text.Json.Serialization;

namespace GuildWars2.Items;

/// <summary>Information about a short bow.</summary>
[JsonConverter(typeof(ShortBowJsonConverter))]
public sealed record ShortBow : Weapon
{
    /// <inheritdoc />
    public override bool TwoHanded => true;
}

using System.Text.Json.Serialization;

namespace GuildWars2.Items;

/// <summary>Information about a staff.</summary>
[JsonConverter(typeof(StaffJsonConverter))]
public sealed record Staff : Weapon
{
    /// <inheritdoc />
    public override bool TwoHanded => true;
}

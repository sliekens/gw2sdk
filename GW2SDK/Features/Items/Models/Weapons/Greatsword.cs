using System.Text.Json.Serialization;

namespace GuildWars2.Items;

/// <summary>Information about a greatsword.</summary>
[PublicAPI]
[JsonConverter(typeof(GreatswordJsonConverter))]
public sealed record Greatsword : Weapon
{
    /// <inheritdoc />
    public override bool TwoHanded => true;
}

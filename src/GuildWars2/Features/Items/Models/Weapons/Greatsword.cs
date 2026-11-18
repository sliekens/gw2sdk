using System.Text.Json.Serialization;

namespace GuildWars2.Items;

/// <summary>Information about a greatsword.</summary>
[JsonConverter(typeof(GuildWars2.ModelJsonConverterFactory))]
public sealed record Greatsword : Weapon
{
    /// <inheritdoc />
    public override bool TwoHanded => true;
}

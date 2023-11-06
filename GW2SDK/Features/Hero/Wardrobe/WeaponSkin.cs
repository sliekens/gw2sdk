namespace GuildWars2.Hero.Wardrobe;

[PublicAPI]
[Inheritable]
public record WeaponSkin : Skin
{
    public required DamageType DamageType { get; init; }
}

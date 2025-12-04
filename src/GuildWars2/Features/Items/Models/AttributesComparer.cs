using GuildWars2.Hero;

namespace GuildWars2.Items;

internal sealed class
    AttributesComparer : EqualityComparer<KeyValuePair<Extensible<AttributeName>, int>>
{
    private AttributesComparer()
    {
    }

    public static EqualityComparer<KeyValuePair<Extensible<AttributeName>, int>> Instance { get; } =
        new AttributesComparer();

    public override bool Equals(
        KeyValuePair<Extensible<AttributeName>, int> x,
        KeyValuePair<Extensible<AttributeName>, int> y
    )
    {
        return x.Key == y.Key && x.Value == y.Value;
    }

    public override int GetHashCode(KeyValuePair<Extensible<AttributeName>, int> obj)
    {
        return HashCode.Combine(obj.Key, obj.Value);
    }
}

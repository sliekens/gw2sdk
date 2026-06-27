using GuildWars2.Hero.Equipment.Wardrobe;

namespace GuildWars2.Tests.Features.Hero.Equipment.Wardrobe;

internal sealed class EquipmentSkinComparer : EqualityComparer<EquipmentSkin>
{
    private EquipmentSkinComparer()
    {
    }

    public static EquipmentSkinComparer Instance { get; } = new();

    public override bool Equals(EquipmentSkin? x, EquipmentSkin? y)
    {
        if (ReferenceEquals(x, y))
        {
            return true;
        }

        if (x is null || y is null)
        {
            return false;
        }

        return x.Id == y.Id
            && x.Name == y.Name
            && x.Description == y.Description
            && EqualityComparer<SkinFlags>.Default.Equals(x.Flags, y.Flags)
            && SequenceEqual(x.Races, y.Races)
            && x.Rarity == y.Rarity
            && EqualityComparer<Uri?>.Default.Equals(x.IconUrl, y.IconUrl);
    }

    public override int GetHashCode(EquipmentSkin obj)
    {
        HashCode hash = new();
        hash.Add(obj.Id);
        hash.Add(obj.Name);
        hash.Add(obj.Description);
        hash.Add(obj.Flags);
        hash.Add(GetHashCode(obj.Races));
        hash.Add(obj.Rarity);
        hash.Add(obj.IconUrl);
        return hash.ToHashCode();
    }

    private static bool SequenceEqual<T>(IEnumerable<T> left, IEnumerable<T> right)
    {
        using IEnumerator<T> leftEnumerator = left.GetEnumerator();
        using IEnumerator<T> rightEnumerator = right.GetEnumerator();

        while (true)
        {
            bool leftHasValue = leftEnumerator.MoveNext();
            bool rightHasValue = rightEnumerator.MoveNext();

            if (leftHasValue != rightHasValue)
            {
                return false;
            }

            if (!leftHasValue)
            {
                return true;
            }

            if (!EqualityComparer<T>.Default.Equals(leftEnumerator.Current, rightEnumerator.Current))
            {
                return false;
            }
        }
    }

    private static int GetHashCode<T>(IEnumerable<T> values)
    {
        HashCode hash = new();
        foreach (T value in values)
        {
            hash.Add(value);
        }

        return hash.ToHashCode();
    }
}

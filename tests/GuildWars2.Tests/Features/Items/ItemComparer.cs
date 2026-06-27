using GuildWars2.Items;

namespace GuildWars2.Tests.Features.Items;

internal sealed class ItemComparer : EqualityComparer<Item>
{
    private ItemComparer()
    {
    }

    public static ItemComparer Instance { get; } = new();

    public override bool Equals(Item? x, Item? y)
    {
        if (ReferenceEquals(x, y))
        {
            return true;
        }

        if (x is null || y is null)
        {
            return false;
        }

        if (x.GetType() != y.GetType())
        {
            return false;
        }

        return x.Id == y.Id
            && x.Name == y.Name
            && x.Description == y.Description
            && x.Level == y.Level
            && x.Rarity == y.Rarity
            && x.VendorValue == y.VendorValue
            && SequenceEqual(x.GameTypes, y.GameTypes)
            && EqualityComparer<ItemFlags>.Default.Equals(x.Flags, y.Flags)
            && EqualityComparer<ItemRestriction>.Default.Equals(x.Restrictions, y.Restrictions)
            && x.ChatLink == y.ChatLink
            && EqualityComparer<Uri?>.Default.Equals(x.IconUrl, y.IconUrl);
    }

    public override int GetHashCode(Item obj)
    {
        HashCode hash = new();
        hash.Add(obj.GetType());
        hash.Add(obj.Id);
        hash.Add(obj.Name);
        hash.Add(obj.Description);
        hash.Add(obj.Level);
        hash.Add(obj.Rarity);
        hash.Add(obj.VendorValue);
        hash.Add(GetHashCode(obj.GameTypes));
        hash.Add(obj.Flags);
        hash.Add(obj.Restrictions);
        hash.Add(obj.ChatLink);
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

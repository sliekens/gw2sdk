using GuildWars2.Hero.Crafting.Recipes;

namespace GuildWars2.Tests.Features.Hero.Crafting.Recipes;

internal sealed class RecipeComparer : EqualityComparer<Recipe>
{
    private RecipeComparer()
    {
    }

    public static RecipeComparer Instance { get; } = new();

    public override bool Equals(Recipe? x, Recipe? y)
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
            && x.OutputItemId == y.OutputItemId
            && x.OutputItemCount == y.OutputItemCount
            && x.MinRating == y.MinRating
            && x.TimeToCraft == y.TimeToCraft
            && SequenceEqual(x.Disciplines, y.Disciplines)
            && EqualityComparer<RecipeFlags>.Default.Equals(x.Flags, y.Flags)
            && SequenceEqual(x.Ingredients, y.Ingredients)
            && x.ChatLink == y.ChatLink;
    }

    public override int GetHashCode(Recipe obj)
    {
        HashCode hash = new();
        hash.Add(obj.Id);
        hash.Add(obj.OutputItemId);
        hash.Add(obj.OutputItemCount);
        hash.Add(obj.MinRating);
        hash.Add(obj.TimeToCraft);
        hash.Add(GetHashCode(obj.Disciplines));
        hash.Add(obj.Flags);
        hash.Add(GetHashCode(obj.Ingredients));
        hash.Add(obj.ChatLink);
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

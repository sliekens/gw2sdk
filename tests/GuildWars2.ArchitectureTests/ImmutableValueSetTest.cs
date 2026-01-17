using GuildWars2.Collections;

namespace GuildWars2.ArchitectureTests;

public class ImmutableValueSetTest
{
    private sealed record SampleRecord(ImmutableValueSet<int> Numbers);

    [Test]
    public async Task Empty_set_has_zero_count()
    {
        ImmutableValueSet<int> set = [];

        await Assert.That(set.Count).IsEqualTo(0);
    }

    [Test]
    public async Task Sets_with_same_elements_are_equal()
    {
        ImmutableValueSet<int> set1 = new([1, 2, 3]);
        ImmutableValueSet<int> set2 = new([1, 2, 3]);

        await Assert.That(set1.Equals(set2)).IsTrue();
        await Assert.That(set1 == set2).IsTrue();
        await Assert.That(set1 != set2).IsFalse();
    }

    [Test]
    public async Task Sets_with_same_elements_in_different_order_are_equal()
    {
        ImmutableValueSet<int> set1 = new([1, 2, 3]);
        ImmutableValueSet<int> set2 = new([3, 1, 2]);

        await Assert.That(set1.Equals(set2)).IsTrue();
        await Assert.That(set1 == set2).IsTrue();
    }

    [Test]
    public async Task Sets_with_different_elements_are_not_equal()
    {
        ImmutableValueSet<int> set1 = new([1, 2, 3]);
        ImmutableValueSet<int> set2 = new([1, 2, 4]);

        await Assert.That(set1.Equals(set2)).IsFalse();
        await Assert.That(set1 == set2).IsFalse();
        await Assert.That(set1 != set2).IsTrue();
    }

    [Test]
    public async Task Sets_with_different_counts_are_not_equal()
    {
        ImmutableValueSet<int> set1 = new([1, 2, 3]);
        ImmutableValueSet<int> set2 = new([1, 2]);

        await Assert.That(set1.Equals(set2)).IsFalse();
        await Assert.That(set1 == set2).IsFalse();
    }

    [Test]
    public async Task Same_reference_is_equal()
    {
        ImmutableValueSet<int> set = new([1, 2, 3]);

        await Assert.That(set.Equals(set)).IsTrue();
#pragma warning disable CS1718 // Comparison made to same variable
        await Assert.That(set == set).IsTrue();
#pragma warning restore CS1718
    }

    [Test]
    public async Task Null_is_not_equal()
    {
        ImmutableValueSet<int> set = new([1, 2, 3]);

#pragma warning disable CA1508 // Intentionally testing null comparison
        await Assert.That(set.Equals(null)).IsFalse();
        await Assert.That(set == null).IsFalse();
        await Assert.That(null == set).IsFalse();
#pragma warning restore CA1508
    }

    [Test]
    public async Task Both_null_are_equal()
    {
        ImmutableValueSet<int>? left = null;
        ImmutableValueSet<int>? right = null;

#pragma warning disable CA1508 // Intentionally testing null comparison
        await Assert.That(left == right).IsTrue();
#pragma warning restore CA1508
    }

    [Test]
    public async Task Equal_sets_have_same_hash_code()
    {
        ImmutableValueSet<int> set1 = new([1, 2, 3]);
        ImmutableValueSet<int> set2 = new([1, 2, 3]);

        await Assert.That(set1.GetHashCode()).IsEqualTo(set2.GetHashCode());
    }

    [Test]
    public async Task Equal_sets_with_different_order_have_same_hash_code()
    {
        ImmutableValueSet<int> set1 = new([1, 2, 3]);
        ImmutableValueSet<int> set2 = new([3, 1, 2]);

        await Assert.That(set1.GetHashCode()).IsEqualTo(set2.GetHashCode());
    }

    [Test]
    public async Task Different_sets_have_different_hash_codes()
    {
        ImmutableValueSet<int> set1 = new([1, 2, 3]);
        ImmutableValueSet<int> set2 = new([1, 2, 4]);

        await Assert.That(set1.GetHashCode()).IsNotEqualTo(set2.GetHashCode());
    }

    [Test]
    public async Task Records_with_equal_sets_are_equal()
    {
        ImmutableValueSet<int> set1 = new([1, 2, 3]);
        ImmutableValueSet<int> set2 = new([1, 2, 3]);

        SampleRecord left = new(set1);
        SampleRecord right = new(set2);

        await Assert.That(left.Numbers.Equals(right.Numbers)).IsTrue();
        await Assert.That(left == right).IsTrue();
    }

    [Test]
    public async Task Records_with_different_sets_are_not_equal()
    {
        ImmutableValueSet<int> set1 = new([1, 2, 3]);
        ImmutableValueSet<int> set2 = new([4, 5, 6]);

        SampleRecord left = new(set1);
        SampleRecord right = new(set2);

        await Assert.That(left.Numbers.Equals(right.Numbers)).IsFalse();
        await Assert.That(left == right).IsFalse();
    }

    [Test]
    public async Task Add_returns_new_set_with_item()
    {
        ImmutableValueSet<int> original = new([1, 2]);
        ImmutableValueSet<int> modified = original.Add(3);

        await Assert.That(original.Count).IsEqualTo(2);
        await Assert.That(modified.Count).IsEqualTo(3);
        await Assert.That(modified.Contains(3)).IsTrue();
    }

    [Test]
    public async Task Add_existing_item_returns_same_set()
    {
        ImmutableValueSet<int> original = new([1, 2, 3]);
        ImmutableValueSet<int> modified = original.Add(2);

        await Assert.That(ReferenceEquals(original, modified)).IsTrue();
    }

    [Test]
    public async Task Remove_returns_new_set_without_item()
    {
        ImmutableValueSet<int> original = new([1, 2, 3]);
        ImmutableValueSet<int> modified = original.Remove(2);

        await Assert.That(original.Count).IsEqualTo(3);
        await Assert.That(modified.Count).IsEqualTo(2);
        await Assert.That(modified.Contains(2)).IsFalse();
    }

    [Test]
    public async Task Remove_nonexistent_item_returns_same_set()
    {
        ImmutableValueSet<int> original = new([1, 2, 3]);
        ImmutableValueSet<int> modified = original.Remove(99);

        await Assert.That(ReferenceEquals(original, modified)).IsTrue();
    }

    [Test]
    public async Task Clear_returns_empty_set()
    {
        ImmutableValueSet<int> original = new([1, 2, 3]);
        ImmutableValueSet<int> cleared = original.Clear();

        await Assert.That(original.Count).IsEqualTo(3);
        await Assert.That(cleared.Count).IsEqualTo(0);
        await Assert.That(ReferenceEquals(cleared, ImmutableValueSet<int>.Empty)).IsTrue();
    }

    [Test]
    public async Task Union_combines_sets()
    {
        ImmutableValueSet<int> set1 = new([1, 2]);
        ImmutableValueSet<int> set2 = new([2, 3]);
        ImmutableValueSet<int> result = set1.Union(set2);

        await Assert.That(result.Count).IsEqualTo(3);
        await Assert.That(result.Contains(1)).IsTrue();
        await Assert.That(result.Contains(2)).IsTrue();
        await Assert.That(result.Contains(3)).IsTrue();
    }

    [Test]
    public async Task Intersect_returns_common_elements()
    {
        ImmutableValueSet<int> set1 = new([1, 2, 3]);
        ImmutableValueSet<int> set2 = new([2, 3, 4]);
        ImmutableValueSet<int> result = set1.Intersect(set2);

        await Assert.That(result.Count).IsEqualTo(2);
        await Assert.That(result.Contains(2)).IsTrue();
        await Assert.That(result.Contains(3)).IsTrue();
    }

    [Test]
    public async Task Except_returns_difference()
    {
        ImmutableValueSet<int> set1 = new([1, 2, 3]);
        ImmutableValueSet<int> set2 = new([2, 3, 4]);
        ImmutableValueSet<int> result = set1.Except(set2);

        await Assert.That(result.Count).IsEqualTo(1);
        await Assert.That(result.Contains(1)).IsTrue();
    }

    [Test]
    public async Task IsSubsetOf_returns_true_for_subset()
    {
        ImmutableValueSet<int> set1 = new([1, 2]);
        ImmutableValueSet<int> set2 = new([1, 2, 3]);

        await Assert.That(set1.IsSubsetOf(set2)).IsTrue();
        await Assert.That(set2.IsSubsetOf(set1)).IsFalse();
    }

    [Test]
    public async Task IsSupersetOf_returns_true_for_superset()
    {
        ImmutableValueSet<int> set1 = new([1, 2, 3]);
        ImmutableValueSet<int> set2 = new([1, 2]);

        await Assert.That(set1.IsSupersetOf(set2)).IsTrue();
        await Assert.That(set2.IsSupersetOf(set1)).IsFalse();
    }

    [Test]
    public async Task Overlaps_returns_true_when_sets_share_elements()
    {
        ImmutableValueSet<int> set1 = new([1, 2]);
        ImmutableValueSet<int> set2 = new([2, 3]);
        ImmutableValueSet<int> set3 = new([4, 5]);

        await Assert.That(set1.Overlaps(set2)).IsTrue();
        await Assert.That(set1.Overlaps(set3)).IsFalse();
    }

    [Test]
    public async Task SetEquals_returns_true_for_equal_sets()
    {
        ImmutableValueSet<int> set1 = new([1, 2, 3]);
        int[] array = [3, 2, 1];

        await Assert.That(set1.SetEquals(array)).IsTrue();
    }

    [Test]
    public async Task Contains_returns_true_for_existing_item()
    {
        ImmutableValueSet<int> set = new([1, 2, 3]);

        await Assert.That(set.Contains(2)).IsTrue();
        await Assert.That(set.Contains(99)).IsFalse();
    }

    [Test]
    public async Task Can_enumerate_items()
    {
        ImmutableValueSet<int> set = new([1, 2, 3]);
        List<int> result = [];

        foreach (int item in set)
        {
            result.Add(item);
        }

        await Assert.That(result).Count().IsEqualTo(3);
    }

    [Test]
    public async Task Collection_expression_creates_set_with_elements()
    {
        ImmutableValueSet<int> set = [1, 2, 3];

        await Assert.That(set.Count).IsEqualTo(3);
        await Assert.That(set.Contains(1)).IsTrue();
        await Assert.That(set.Contains(2)).IsTrue();
        await Assert.That(set.Contains(3)).IsTrue();
    }

    [Test]
    public async Task Collection_expression_with_spread_creates_set_with_elements()
    {
        int[] source = [1, 2, 3];
        ImmutableValueSet<int> set = [.. source];

        await Assert.That(set.Count).IsEqualTo(3);
        await Assert.That(set.Contains(1)).IsTrue();
        await Assert.That(set.Contains(2)).IsTrue();
        await Assert.That(set.Contains(3)).IsTrue();
    }

    [Test]
    public async Task Collection_expression_with_spread_from_list_creates_set_with_elements()
    {
        List<int> source = [4, 5, 6];
        ImmutableValueSet<int> set = [.. source];

        await Assert.That(set.Count).IsEqualTo(3);
        await Assert.That(set.Contains(4)).IsTrue();
        await Assert.That(set.Contains(5)).IsTrue();
        await Assert.That(set.Contains(6)).IsTrue();
    }

    [Test]
    public async Task Collection_expression_deduplicates_elements()
    {
        ImmutableValueSet<int> set = [1, 2, 2, 3, 3, 3];

        await Assert.That(set.Count).IsEqualTo(3);
    }

    [Test]
    public async Task Collection_expression_created_set_equals_constructor_created_set()
    {
        ImmutableValueSet<int> fromExpression = [1, 2, 3];
        ImmutableValueSet<int> fromConstructor = new([1, 2, 3]);

        await Assert.That(fromExpression).IsEqualTo(fromConstructor);
    }
}

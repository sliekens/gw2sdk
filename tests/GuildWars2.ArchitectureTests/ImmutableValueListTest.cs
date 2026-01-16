using GuildWars2.Collections;

namespace GuildWars2.ArchitectureTests;

public class ImmutableValueListTest
{
    private sealed record SampleRecord(ImmutableValueList<int> Numbers);

    [Test]
    public async Task Empty_list_has_zero_count()
    {
        ImmutableValueList<int> list = [];

        await Assert.That(list.Count).IsEqualTo(0);
    }

    [Test]
    public async Task Lists_with_same_elements_are_equal()
    {
        ImmutableValueList<int> list1 = new([1, 2, 3]);
        ImmutableValueList<int> list2 = new([1, 2, 3]);

        await Assert.That(list1.Equals(list2)).IsTrue();
        await Assert.That(list1 == list2).IsTrue();
        await Assert.That(list1 != list2).IsFalse();
    }

    [Test]
    public async Task Lists_with_different_elements_are_not_equal()
    {
        ImmutableValueList<int> list1 = new([1, 2, 3]);
        ImmutableValueList<int> list2 = new([1, 2, 4]);

        await Assert.That(list1.Equals(list2)).IsFalse();
        await Assert.That(list1 == list2).IsFalse();
        await Assert.That(list1 != list2).IsTrue();
    }

    [Test]
    public async Task Lists_with_different_counts_are_not_equal()
    {
        ImmutableValueList<int> list1 = new([1, 2, 3]);
        ImmutableValueList<int> list2 = new([1, 2]);

        await Assert.That(list1.Equals(list2)).IsFalse();
        await Assert.That(list1 == list2).IsFalse();
    }

    [Test]
    public async Task Same_reference_is_equal()
    {
        ImmutableValueList<int> list = new([1, 2, 3]);

        await Assert.That(list.Equals(list)).IsTrue();
#pragma warning disable CS1718 // Comparison made to same variable
        await Assert.That(list == list).IsTrue();
#pragma warning restore CS1718
    }

    [Test]
    public async Task Null_is_not_equal()
    {
        ImmutableValueList<int> list = new([1, 2, 3]);

#pragma warning disable CA1508 // Intentionally testing null comparison
        await Assert.That(list.Equals(null)).IsFalse();
        await Assert.That(list == null).IsFalse();
        await Assert.That(null == list).IsFalse();
#pragma warning restore CA1508
    }

    [Test]
    public async Task Both_null_are_equal()
    {
        ImmutableValueList<int>? left = null;
        ImmutableValueList<int>? right = null;

#pragma warning disable CA1508 // Intentionally testing null comparison
        await Assert.That(left == right).IsTrue();
#pragma warning restore CA1508
    }

    [Test]
    public async Task Equal_lists_have_same_hash_code()
    {
        ImmutableValueList<int> list1 = new([1, 2, 3]);
        ImmutableValueList<int> list2 = new([1, 2, 3]);

        await Assert.That(list1.GetHashCode()).IsEqualTo(list2.GetHashCode());
    }

    [Test]
    public async Task Different_lists_have_different_hash_codes()
    {
        ImmutableValueList<int> list1 = new([1, 2, 3]);
        ImmutableValueList<int> list2 = new([1, 2, 4]);

        await Assert.That(list1.GetHashCode()).IsNotEqualTo(list2.GetHashCode());
    }

    [Test]
    public async Task Records_with_equal_lists_are_equal()
    {
        ImmutableValueList<int> list1 = new([1, 2, 3]);
        ImmutableValueList<int> list2 = new([1, 2, 3]);

        SampleRecord left = new(list1);
        SampleRecord right = new(list2);

        await Assert.That(left.Numbers.Equals(right.Numbers)).IsTrue();
        await Assert.That(left == right).IsTrue();
    }

    [Test]
    public async Task Records_with_different_lists_are_not_equal()
    {
        ImmutableValueList<int> list1 = new([1, 2, 3]);
        ImmutableValueList<int> list2 = new([4, 5, 6]);

        SampleRecord left = new(list1);
        SampleRecord right = new(list2);

        await Assert.That(left.Numbers.Equals(right.Numbers)).IsFalse();
        await Assert.That(left == right).IsFalse();
    }

    [Test]
    public async Task Add_returns_new_list_with_item()
    {
        ImmutableValueList<int> original = new([1, 2]);
        ImmutableValueList<int> modified = original.Add(3);

        await Assert.That(original.Count).IsEqualTo(2);
        await Assert.That(modified.Count).IsEqualTo(3);
        await Assert.That(modified[2]).IsEqualTo(3);
    }

    [Test]
    public async Task AddRange_returns_new_list_with_items()
    {
        ImmutableValueList<int> original = new([1]);
        ImmutableValueList<int> modified = original.AddRange([2, 3]);

        await Assert.That(original.Count).IsEqualTo(1);
        await Assert.That(modified.Count).IsEqualTo(3);
    }

    [Test]
    public async Task Insert_returns_new_list_with_item_at_index()
    {
        ImmutableValueList<int> original = new([1, 3]);
        ImmutableValueList<int> modified = original.Insert(1, 2);

        await Assert.That(original.Count).IsEqualTo(2);
        await Assert.That(modified.Count).IsEqualTo(3);
        await Assert.That(modified[1]).IsEqualTo(2);
    }

    [Test]
    public async Task RemoveAt_returns_new_list_without_item()
    {
        ImmutableValueList<int> original = new([1, 2, 3]);
        ImmutableValueList<int> modified = original.RemoveAt(1);

        await Assert.That(original.Count).IsEqualTo(3);
        await Assert.That(modified.Count).IsEqualTo(2);
        await Assert.That(modified[0]).IsEqualTo(1);
        await Assert.That(modified[1]).IsEqualTo(3);
    }

    [Test]
    public async Task Remove_returns_new_list_without_first_occurrence()
    {
        ImmutableValueList<int> original = new([1, 2, 2, 3]);
        ImmutableValueList<int> modified = original.Remove(2, null);

        await Assert.That(original.Count).IsEqualTo(4);
        await Assert.That(modified.Count).IsEqualTo(3);
    }

    [Test]
    public async Task Clear_returns_empty_list()
    {
        ImmutableValueList<int> original = new([1, 2, 3]);
        ImmutableValueList<int> cleared = original.Clear();

        await Assert.That(original.Count).IsEqualTo(3);
        await Assert.That(cleared.Count).IsEqualTo(0);
        await Assert.That(ReferenceEquals(cleared, ImmutableValueList<int>.Empty)).IsTrue();
    }

    [Test]
    public async Task Can_enumerate_items()
    {
        ImmutableValueList<int> list = new([1, 2, 3]);
        List<int> result = [];

        foreach (int item in list)
        {
            result.Add(item);
        }

        await Assert.That(result).IsEquivalentTo([1, 2, 3]);
    }

    [Test]
    public async Task Indexer_returns_correct_item()
    {
        ImmutableValueList<string> list = new(["a", "b", "c"]);

        await Assert.That(list[0]).IsEqualTo("a");
        await Assert.That(list[1]).IsEqualTo("b");
        await Assert.That(list[2]).IsEqualTo("c");
    }
}

using GuildWars2.Collections;

namespace GuildWars2.ArchitectureTests;

public class ImmutableValueArrayTest
{
    private sealed record SampleRecord(ImmutableValueArray<int> Numbers);

    [Test]
    public async Task Empty_array_has_zero_length()
    {
        ImmutableValueArray<int> array = [];

        await Assert.That(array.Length).IsEqualTo(0);
    }

    [Test]
    public async Task Arrays_with_same_elements_are_equal()
    {
        ImmutableValueArray<int> array1 = new([1, 2, 3]);
        ImmutableValueArray<int> array2 = new([1, 2, 3]);

        await Assert.That(array1.Equals(array2)).IsTrue();
        await Assert.That(array1 == array2).IsTrue();
        await Assert.That(array1 != array2).IsFalse();
    }

    [Test]
    public async Task Arrays_with_different_elements_are_not_equal()
    {
        ImmutableValueArray<int> array1 = new([1, 2, 3]);
        ImmutableValueArray<int> array2 = new([1, 2, 4]);

        await Assert.That(array1.Equals(array2)).IsFalse();
        await Assert.That(array1 == array2).IsFalse();
        await Assert.That(array1 != array2).IsTrue();
    }

    [Test]
    public async Task Arrays_with_same_elements_in_different_order_are_not_equal()
    {
        ImmutableValueArray<int> array1 = new([1, 2, 3]);
        ImmutableValueArray<int> array2 = new([3, 2, 1]);

        await Assert.That(array1.Equals(array2)).IsFalse();
        await Assert.That(array1 == array2).IsFalse();
    }

    [Test]
    public async Task Arrays_with_different_lengths_are_not_equal()
    {
        ImmutableValueArray<int> array1 = new([1, 2, 3]);
        ImmutableValueArray<int> array2 = new([1, 2]);

        await Assert.That(array1.Equals(array2)).IsFalse();
        await Assert.That(array1 == array2).IsFalse();
    }

    [Test]
    public async Task Same_reference_is_equal()
    {
        ImmutableValueArray<int> array = new([1, 2, 3]);

        await Assert.That(array.Equals(array)).IsTrue();
#pragma warning disable CS1718 // Comparison made to same variable
        await Assert.That(array == array).IsTrue();
#pragma warning restore CS1718
    }

    [Test]
    public async Task Null_is_not_equal()
    {
        ImmutableValueArray<int> array = new([1, 2, 3]);

#pragma warning disable CA1508 // Intentionally testing null comparison
        await Assert.That(array.Equals(null)).IsFalse();
        await Assert.That(array == null).IsFalse();
        await Assert.That(null == array).IsFalse();
#pragma warning restore CA1508
    }

    [Test]
    public async Task Both_null_are_equal()
    {
        ImmutableValueArray<int>? left = null;
        ImmutableValueArray<int>? right = null;

#pragma warning disable CA1508 // Intentionally testing null comparison
        await Assert.That(left == right).IsTrue();
#pragma warning restore CA1508
    }

    [Test]
    public async Task Equal_arrays_have_same_hash_code()
    {
        ImmutableValueArray<int> array1 = new([1, 2, 3]);
        ImmutableValueArray<int> array2 = new([1, 2, 3]);

        await Assert.That(array1.GetHashCode()).IsEqualTo(array2.GetHashCode());
    }

    [Test]
    public async Task Different_arrays_have_different_hash_codes()
    {
        ImmutableValueArray<int> array1 = new([1, 2, 3]);
        ImmutableValueArray<int> array2 = new([1, 2, 4]);

        await Assert.That(array1.GetHashCode()).IsNotEqualTo(array2.GetHashCode());
    }

    [Test]
    public async Task Records_with_equal_arrays_are_equal()
    {
        ImmutableValueArray<int> array1 = new([1, 2, 3]);
        ImmutableValueArray<int> array2 = new([1, 2, 3]);

        SampleRecord left = new(array1);
        SampleRecord right = new(array2);

        await Assert.That(left.Numbers.Equals(right.Numbers)).IsTrue();
        await Assert.That(left == right).IsTrue();
    }

    [Test]
    public async Task Records_with_different_arrays_are_not_equal()
    {
        ImmutableValueArray<int> array1 = new([1, 2, 3]);
        ImmutableValueArray<int> array2 = new([4, 5, 6]);

        SampleRecord left = new(array1);
        SampleRecord right = new(array2);

        await Assert.That(left.Numbers.Equals(right.Numbers)).IsFalse();
        await Assert.That(left == right).IsFalse();
    }

    [Test]
    public async Task Add_returns_new_array_with_item()
    {
        ImmutableValueArray<int> original = new([1, 2]);
        ImmutableValueArray<int> modified = original.Add(3);

        await Assert.That(original.Length).IsEqualTo(2);
        await Assert.That(modified.Length).IsEqualTo(3);
        await Assert.That(modified[2]).IsEqualTo(3);
    }

    [Test]
    public async Task AddRange_returns_new_array_with_items()
    {
        ImmutableValueArray<int> original = new([1]);
        ImmutableValueArray<int> modified = original.AddRange([2, 3]);

        await Assert.That(original.Length).IsEqualTo(1);
        await Assert.That(modified.Length).IsEqualTo(3);
    }

    [Test]
    public async Task Insert_returns_new_array_with_item_at_index()
    {
        ImmutableValueArray<int> original = new([1, 3]);
        ImmutableValueArray<int> modified = original.Insert(1, 2);

        await Assert.That(original.Length).IsEqualTo(2);
        await Assert.That(modified.Length).IsEqualTo(3);
        await Assert.That(modified[1]).IsEqualTo(2);
    }

    [Test]
    public async Task RemoveAt_returns_new_array_without_item()
    {
        ImmutableValueArray<int> original = new([1, 2, 3]);
        ImmutableValueArray<int> modified = original.RemoveAt(1);

        await Assert.That(original.Length).IsEqualTo(3);
        await Assert.That(modified.Length).IsEqualTo(2);
        await Assert.That(modified[0]).IsEqualTo(1);
        await Assert.That(modified[1]).IsEqualTo(3);
    }

    [Test]
    public async Task Remove_returns_new_array_without_first_occurrence()
    {
        ImmutableValueArray<int> original = new([1, 2, 2, 3]);
        ImmutableValueArray<int> modified = original.Remove(2, null);

        await Assert.That(original.Length).IsEqualTo(4);
        await Assert.That(modified.Length).IsEqualTo(3);
    }

    [Test]
    public async Task Remove_nonexistent_item_returns_same_array()
    {
        ImmutableValueArray<int> original = new([1, 2, 3]);
        ImmutableValueArray<int> modified = original.Remove(99, null);

        await Assert.That(ReferenceEquals(original, modified)).IsTrue();
    }

    [Test]
    public async Task SetItem_returns_new_array_with_replaced_element()
    {
        ImmutableValueArray<int> original = new([1, 2, 3]);
        ImmutableValueArray<int> modified = original.SetItem(1, 99);

        await Assert.That(original[1]).IsEqualTo(2);
        await Assert.That(modified[1]).IsEqualTo(99);
    }

    [Test]
    public async Task Clear_returns_empty_array()
    {
        ImmutableValueArray<int> original = new([1, 2, 3]);
        ImmutableValueArray<int> cleared = original.Clear();

        await Assert.That(original.Length).IsEqualTo(3);
        await Assert.That(cleared.Length).IsEqualTo(0);
        await Assert.That(ReferenceEquals(cleared, ImmutableValueArray<int>.Empty)).IsTrue();
    }

    [Test]
    public async Task Contains_returns_true_for_existing_item()
    {
        ImmutableValueArray<int> array = new([1, 2, 3]);

        await Assert.That(array.Contains(2)).IsTrue();
        await Assert.That(array.Contains(99)).IsFalse();
    }

    [Test]
    public async Task IndexOf_returns_index_of_item()
    {
        ImmutableValueArray<int> array = new([1, 2, 3]);

        await Assert.That(array.IndexOf(2, 0, array.Count, null)).IsEqualTo(1);
        await Assert.That(array.IndexOf(99, 0, array.Count, null)).IsEqualTo(-1);
    }

    [Test]
    public async Task Indexer_returns_correct_item()
    {
        ImmutableValueArray<string> array = new(["a", "b", "c"]);

        await Assert.That(array[0]).IsEqualTo("a");
        await Assert.That(array[1]).IsEqualTo("b");
        await Assert.That(array[2]).IsEqualTo("c");
    }

    [Test]
    public async Task Can_enumerate_items()
    {
        ImmutableValueArray<int> array = new([1, 2, 3]);
        List<int> result = [];

        foreach (int item in array)
        {
            result.Add(item);
        }

        await Assert.That(result).IsEquivalentTo([1, 2, 3]);
    }

#if NET
    [Test]
    public async Task AsSpan_returns_span_of_elements()
    {
        ImmutableValueArray<int> array = new([1, 2, 3]);
        ReadOnlySpan<int> span = array.AsSpan();

        // Cannot use await with spans, so extract values first
        int length = span.Length;
        int first = span[0];
        int second = span[1];
        int third = span[2];

        await Assert.That(length).IsEqualTo(3);
        await Assert.That(first).IsEqualTo(1);
        await Assert.That(second).IsEqualTo(2);
        await Assert.That(third).IsEqualTo(3);
    }

    [Test]
    public async Task AsMemory_returns_memory_of_elements()
    {
        ImmutableValueArray<int> array = new([1, 2, 3]);
        ReadOnlyMemory<int> memory = array.AsMemory();

        await Assert.That(memory.Length).IsEqualTo(3);
    }

    [Test]
    public async Task Can_construct_from_span()
    {
        ReadOnlySpan<int> span = [1, 2, 3];
        ImmutableValueArray<int> array = new(span);

        await Assert.That(array.Length).IsEqualTo(3);
        await Assert.That(array[0]).IsEqualTo(1);
        await Assert.That(array[1]).IsEqualTo(2);
        await Assert.That(array[2]).IsEqualTo(3);
    }
#endif

    [Test]
    public async Task Count_property_returns_length()
    {
        ImmutableValueArray<int> array = new([1, 2, 3]);
        ImmutableValueArray<int> collection = array;

        await Assert.That(collection.Count).IsEqualTo(3);
    }
}

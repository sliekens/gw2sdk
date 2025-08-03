using System.Runtime.CompilerServices;

namespace GuildWars2;

/// <summary>Miscellaneous extension methods.</summary>
[PublicAPI]
public static class Extensions
{
    /// <summary>Returns a new task that converts the <see cref="HashSet{T}" /> from the original task to a
    /// <see cref="Dictionary{TKey,TValue}" />.</summary>
    /// <typeparam name="TKey">The type of the key in the dictionary.</typeparam>
    /// <typeparam name="TValue">The type of the value in the dictionary.</typeparam>
    /// <param name="instance">The task.</param>
    /// <param name="keySelector">The function to select the key from the value.</param>
    /// <returns>A new task.</returns>
    public static async Task<(Dictionary<TKey, TValue> Value, MessageContext Context)>
        AsDictionary<TKey, TValue>(
            this Task<(HashSet<TValue> Value, MessageContext Context)> instance,
            Func<TValue, TKey> keySelector
        ) where TKey : notnull
    {
        ThrowHelper.ThrowIfNull(instance);
        (HashSet<TValue> value, MessageContext context) = await instance.ConfigureAwait(false);
        return (value.ToDictionary(keySelector), context);
    }

    /// <summary>Returns a new task that only returns the value of the original task, discarding the message context.</summary>
    /// <typeparam name="T">The type of the value returned by the original task.</typeparam>
    /// <param name="task">The original task, which returns a tuple of the value and also the message context.</param>
    /// <returns>A new task that returns only the value.</returns>
    public static async Task<T> ValueOnly<T>(this Task<(T, MessageContext)> task)
    {
        ThrowHelper.ThrowIfNull(task);
        (T value, _) = await task.ConfigureAwait(false);
        return value;
    }

    /// <summary>Returns a new IAsyncEnumerable that only returns the value of the original IAsyncEnumerable, discarding the
    /// message context.</summary>
    /// <typeparam name="T">The type of the value returned by the original IAsyncEnumerable.</typeparam>
    /// <param name="source">The original IAsyncEnumerable, which returns a tuple of the value and also the message context.</param>
    /// <param name="cancellationToken">A token to cancel the enumeration.</param>
    /// <returns>A new IAsyncEnumerable that returns only the value.</returns>
    public static async IAsyncEnumerable<T> ValueOnly<T>(
        this IAsyncEnumerable<(T, MessageContext)> source,
        [EnumeratorCancellation] CancellationToken cancellationToken = default
    )
    {
        await foreach ((T value, MessageContext _) in source.WithCancellation(cancellationToken)
            .ConfigureAwait(false))
        {
            yield return value;
        }
    }
}

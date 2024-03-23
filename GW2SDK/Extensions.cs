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
    internal static async Task<(Dictionary<TKey, TValue> Value, MessageContext Context)>
        AsDictionary<TKey, TValue>(
            this Task<(HashSet<TValue> Value, MessageContext Context)> instance,
            Func<TValue, TKey> keySelector
        ) where TKey : notnull
    {
        var (value, context) = await instance.ConfigureAwait(false);
        return (value.ToDictionary(keySelector), context);
    }

    /// <summary>Returns a new task that only returns the value of the original task, discarding the message context.</summary>
    /// <typeparam name="T">The type of the value returned by the original task.</typeparam>
    /// <param name="task">The original task, which returns a tuple of the value and also the message context.</param>
    /// <returns>A new task that returns only the value.</returns>
    public static Task<T> ValueOnly<T>(this Task<(T, MessageContext)> task) =>
        task.ContinueWith(t => t.Result.Item1);
}

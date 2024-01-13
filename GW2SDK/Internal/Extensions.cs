namespace GuildWars2;

/// <summary>Miscellaneous extensions 🤷‍♂️.</summary>
internal static class Extensions
{
    /// <summary>Converts a <see cref="HashSet{T}" /> to a <see cref="Dictionary{TKey,TValue}" />.</summary>
    /// <typeparam name="TKey">The type of the key in the dictionary.</typeparam>
    /// <typeparam name="TValue">The type of the value in the dictionary.</typeparam>
    /// <param name="instance">The task.</param> 
    /// <param name="keySelector">The function to select the key from the value.</param>
    /// <returns>A new task.</returns>
    internal static async Task<(Dictionary<TKey, TValue> Value, MessageContext Context)> AsDictionary<TKey, TValue>(
        this Task<(HashSet<TValue> Value, MessageContext Context)> instance,
        Func<TValue, TKey> keySelector
    ) where TKey : notnull
    {
        var (value, context) = await instance.ConfigureAwait(false);
        return (value.ToDictionary(keySelector), context);
    }
}

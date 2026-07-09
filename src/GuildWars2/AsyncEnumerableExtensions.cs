using System.Runtime.CompilerServices;

namespace GuildWars2;

/// <summary>Extension methods for <see cref="IAsyncEnumerable{T}" />.</summary>
public static class AsyncEnumerableExtensions
{
    /// <summary>Returns elements from an async sequence until the specified cancellation token is triggered, then gracefully completes the sequence without throwing an exception.</summary>
    /// <typeparam name="T">The type of the elements in the source sequence.</typeparam>
    /// <param name="source">The source async enumerable sequence.</param>
    /// <param name="cancellationToken">The cancellation token that terminates the sequence when triggered.</param>
    /// <returns>An async enumerable that yields elements from the source until cancellation is requested.</returns>
    public static async IAsyncEnumerable<T> TakeUntil<T>(
        this IAsyncEnumerable<T> source,
        [EnumeratorCancellation] CancellationToken cancellationToken
    )
    {
        ThrowHelper.ThrowIfNull(source);

        await foreach (T item in source.ConfigureAwait(false))
        {
            if (cancellationToken.IsCancellationRequested)
            {
                yield break;
            }

            yield return item;
        }
    }
}

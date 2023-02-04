using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GuildWars2.Tasks;

internal static class InterleavedTask
{
    /// <summary>Reorders tasks in order of their completion.</summary>
    /// <typeparam name="T">The type of the results produced by the input tasks.</typeparam>
    /// <param name="inputTasks">The tasks to reorder.</param>
    /// <returns>A stream of the values produced by the input tasks in the order of their completion.</returns>
    public static async IAsyncEnumerable<T> OrderByCompletion<T>(
        this IReadOnlyCollection<Task<T>> inputTasks
    )
    {
        foreach (var bucket in Interleaved(inputTasks))
        {
            var completedTask = await bucket.ConfigureAwait(false);
            yield return await completedTask.ConfigureAwait(false);
        }
    }

    private static Task<Task<T>>[] Interleaved<T>(IReadOnlyCollection<Task<T>> inputTasks)
    {
        // Found on https://devblogs.microsoft.com/pfxteam/processing-tasks-as-they-complete/
        // Thanks Stephen Toub!
        var buckets = new TaskCompletionSource<Task<T>>[inputTasks.Count];
        var results = new Task<Task<T>>[buckets.Length];
        for (var i = 0; i < buckets.Length; i++)
        {
            buckets[i] = new TaskCompletionSource<Task<T>>();
            results[i] = buckets[i].Task;
        }

        var nextTaskIndex = -1;
        foreach (var inputTask in inputTasks)
        {
            inputTask.ContinueWith(
                Continuation,
                CancellationToken.None,
                TaskContinuationOptions.ExecuteSynchronously,
                TaskScheduler.Default
            );
        }

        void Continuation(Task<T> completed)
        {
            var bucket = buckets[Interlocked.Increment(ref nextTaskIndex)];
            bucket.TrySetResult(completed);
        }

        return results;
    }
}

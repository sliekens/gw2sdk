namespace GuildWars2.Tasks;

internal static class InterleavedTask
{
    internal static Task<Task<T>>[] Interleave<T>(this IEnumerable<Task<T>> tasks)
    {
        // Found on https://devblogs.microsoft.com/pfxteam/processing-tasks-as-they-complete/
        // Thanks Stephen Toub!
        var inputTasks = tasks.ToList();
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

namespace GuildWars2.Tasks;

internal static class InterleavedTask
{
    internal static Task<Task<T>>[] Interleave<T>(this IEnumerable<Task<T>> tasks)
    {
        // Found on https://devblogs.microsoft.com/pfxteam/processing-tasks-as-they-complete/
        // Thanks Stephen Toub!
        List<Task<T>> inputTasks = tasks.ToList();
        TaskCompletionSource<Task<T>>[] buckets = new TaskCompletionSource<Task<T>>[inputTasks.Count];
        Task<Task<T>>[] results = new Task<Task<T>>[buckets.Length];
        for (int i = 0; i < buckets.Length; i++)
        {
            buckets[i] = new TaskCompletionSource<Task<T>>();
            results[i] = buckets[i].Task;
        }

        int nextTaskIndex = -1;
        foreach (Task<T> inputTask in inputTasks)
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
            TaskCompletionSource<Task<T>> bucket = buckets[Interlocked.Increment(ref nextTaskIndex)];
            bucket.TrySetResult(completed);
        }

        return results;
    }
}

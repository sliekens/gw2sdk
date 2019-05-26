using Xunit;

// TODO: fix concurrency issues with API caches
// -> The process cannot access the file '[...].json' because it is being used by another process.
[assembly: CollectionBehavior(DisableTestParallelization = true)]

using System.Collections.Concurrent;

namespace GuildWars2.Tests.TestInfrastructure;

public sealed class CompositeDisposable : IDisposable
{
    private readonly ConcurrentBag<IDisposable> disposables = [];

    private bool disposed;

    public void Add(IDisposable disposable)
    {
        ArgumentNullException.ThrowIfNull(disposable);
        if (disposed)
        {
            disposable.Dispose();
        }
        else
        {
            disposables.Add(disposable);
        }
    }

    public T Add<T>(T disposable) where T : IDisposable
    {
        Add(disposable);
        return disposable;
    }

    public void Dispose()
    {
        if (disposed)
        {
            return;
        }

        disposed = true;
        foreach (IDisposable disposable in disposables)
        {
            disposable.Dispose();
        }

        while (disposables.TryTake(out IDisposable? _))
        {
            // Drain remaining references; ConcurrentBag may contain items added concurrently.
        }
    }
}

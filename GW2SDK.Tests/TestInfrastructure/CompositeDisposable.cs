using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GW2SDK.Tests.TestInfrastructure;

internal sealed class CompositeDisposable : IAsyncDisposable, IDisposable
{
    private readonly List<IAsyncDisposable> asyncDisposables = new();

    private readonly List<IDisposable> disposables = new();

    private bool disposed;

    public async ValueTask DisposeAsync()
    {
        if (disposed) return;
        foreach (var asyncDisposable in asyncDisposables)
        {
            await asyncDisposable.DisposeAsync().ConfigureAwait(false);
        }

        foreach (var disposable in disposables)
        {
            if (disposable is IAsyncDisposable asyncDisposable)
            {
                await asyncDisposable.DisposeAsync().ConfigureAwait(false);
            }
            else
            {
                disposable.Dispose();
            }
        }

        Dispose(false);
    }

    public void Dispose()
    {
        if (disposed) return;
        Dispose(true);
    }

    public void Add(IDisposable disposable)
    {
        if (disposed)
        {
            throw new ObjectDisposedException(
                "CompositeDisposable cannot be reused once disposed. Create a new instance."
            );
        }

        disposables.Add(disposable);
    }

    public void Add(IAsyncDisposable disposable)
    {
        if (disposed)
        {
            throw new ObjectDisposedException(
                "CompositeDisposable cannot be reused once disposed. Create a new instance."
            );
        }

        asyncDisposables.Add(disposable);
    }

    private void Dispose(bool disposing)
    {
        if (disposing)
        {
            foreach (var disposable in disposables)
            {
                disposable.Dispose();
            }

            foreach (var asyncDisposable in asyncDisposables)
            {
                (asyncDisposable as IDisposable)?.Dispose();
            }
        }

        disposed = true;
        disposables.Clear();
        asyncDisposables.Clear();
    }
}

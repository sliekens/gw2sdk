using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GW2SDK.Tests.TestInfrastructure
{
    internal sealed class CompositeDisposable : IAsyncDisposable, IDisposable
    {
        private bool _disposed;

        private readonly List<IDisposable> _disposables = new();

        private readonly List<IAsyncDisposable> _asyncDisposables = new();

        public void Add(IDisposable disposable)
        {
            if (_disposed)
            {
                throw new ObjectDisposedException("CompositeDisposable cannot be reused once disposed. Create a new instance.");
            }

            _disposables.Add(disposable);
        }

        public void Add(IAsyncDisposable disposable)
        {
            if (_disposed)
            {
                throw new ObjectDisposedException("CompositeDisposable cannot be reused once disposed. Create a new instance.");
            }

            _asyncDisposables.Add(disposable);
        }

        public void Dispose()
        {
            if (_disposed) return;
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        public async ValueTask DisposeAsync()
        {
            if (_disposed) return;
            foreach (var asyncDisposable in _asyncDisposables)
            {
                await asyncDisposable.DisposeAsync().ConfigureAwait(false);
            }

            foreach (var disposable in _disposables)
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

            Dispose(disposing: false);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                foreach (var disposable in _disposables)
                {
                    disposable.Dispose();
                }

                foreach (var asyncDisposable in _asyncDisposables)
                {
                    (asyncDisposable as IDisposable)?.Dispose();
                }
            }

            _disposed = true;
            _disposables.Clear();
            _asyncDisposables.Clear();
        }
    }
}

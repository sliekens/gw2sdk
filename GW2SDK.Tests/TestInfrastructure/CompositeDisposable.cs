using System;
using System.Collections.Generic;

namespace GW2SDK.Tests.TestInfrastructure
{
    internal class CompositeDisposable : IDisposable
    {
        private readonly List<IDisposable> _disposables = new();

        public void Dispose()
        {
            foreach (var disposable in _disposables)
            {
                disposable.Dispose();
            }
        }

        public void Add(IDisposable disposable) => _disposables.Add(disposable);
    }
}
using System;
using System.Collections;
using System.Collections.Generic;

namespace GW2SDK
{
    internal sealed class DataTransferSet<T> : IDataTransferSet<T>
    {
        private readonly ICollectionContext _context;

        private readonly IReadOnlySet<T> _inner;

        public DataTransferSet(IReadOnlySet<T> inner, ICollectionContext context)
        {
            _inner = inner ?? throw new ArgumentNullException(nameof(inner));
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public int ResultTotal => _context.ResultTotal;

        public int ResultCount => _context.ResultCount;

        public IEnumerator<T> GetEnumerator() => _inner.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable) _inner).GetEnumerator();

        public int Count => _inner.Count;

        public bool Contains(T item) => _inner.Contains(item);

        public bool IsProperSubsetOf(IEnumerable<T> other) => _inner.IsProperSubsetOf(other);

        public bool IsProperSupersetOf(IEnumerable<T> other) => _inner.IsProperSupersetOf(other);

        public bool IsSubsetOf(IEnumerable<T> other) => _inner.IsSubsetOf(other);

        public bool IsSupersetOf(IEnumerable<T> other) => _inner.IsSupersetOf(other);

        public bool Overlaps(IEnumerable<T> other) => _inner.Overlaps(other);

        public bool SetEquals(IEnumerable<T> other) => _inner.SetEquals(other);
    }
}

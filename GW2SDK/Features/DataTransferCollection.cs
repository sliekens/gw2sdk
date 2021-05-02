using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace GW2SDK
{
    internal sealed class DataTransferCollection<T> : ReadOnlyCollection<T>, IDataTransferCollection<T>
    {
        private readonly ICollectionContext _context;

        internal DataTransferCollection(IList<T> list, ICollectionContext context)
            : base(list)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public int ResultTotal => _context.ResultTotal;

        public int ResultCount => _context.ResultCount;
    }
}

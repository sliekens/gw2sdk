using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace GW2SDK.Impl
{
    public sealed class DataTransferList<T> : ReadOnlyCollection<T>, IDataTransferList<T>
    {
        private readonly IListContext _context;

        public DataTransferList(IList<T> list, IListContext context)
            : base(list)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public int ResultTotal => _context.ResultTotal;

        public int ResultCount => _context.ResultCount;
    }
}

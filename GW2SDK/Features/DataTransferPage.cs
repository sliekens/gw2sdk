using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace GW2SDK
{
    internal sealed class DataTransferPage<T> : ReadOnlyCollection<T>, IDataTransferPage<T>
    {
        private readonly IPageContext _context;

        internal DataTransferPage(IList<T> list, IPageContext context)
            : base(list)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public int ResultTotal => _context.ResultTotal;

        public int ResultCount => _context.ResultCount;

        public ContinuationToken? Previous => _context.Previous;

        public ContinuationToken? Next => _context.Next;

        public ContinuationToken First => _context.First;

        public ContinuationToken Self => _context.Self;

        public ContinuationToken Last => _context.Last;

        public int PageTotal => _context.PageTotal;

        public int PageSize => _context.PageSize;
    }
}

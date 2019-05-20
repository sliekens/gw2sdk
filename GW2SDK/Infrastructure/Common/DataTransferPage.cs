using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using GW2SDK.Features.Common;

namespace GW2SDK.Infrastructure.Common
{
    public sealed class DataTransferPage<T> : ReadOnlyCollection<T>, IDataTransferPage<T>
    {
        private readonly IPageContext _context;

        public DataTransferPage([NotNull] IList<T> list, [NotNull] IPageContext context) : base(list)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public int ResultTotal => _context.ResultTotal;

        public int ResultCount => _context.ResultCount;

        public int PageTotal => _context.PageTotal;

        public int PageSize => _context.PageSize;
    }
}

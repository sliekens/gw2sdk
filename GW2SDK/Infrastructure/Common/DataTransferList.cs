﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using GW2SDK.Features.Common;

namespace GW2SDK.Infrastructure.Common
{
    public sealed class DataTransferList<T> : ReadOnlyCollection<T>, IDataTransferList<T>
    {
        private readonly IListContext _context;

        public DataTransferList([NotNull] IList<T> list, [NotNull] IListContext context)
            : base(list)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public int ResultTotal => _context.ResultTotal;

        public int ResultCount => _context.ResultCount;
    }
}

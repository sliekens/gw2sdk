using System.Collections.Generic;
using System.Collections.ObjectModel;
using GW2SDK.Infrastructure;

namespace GW2SDK.Features.Common.Infrastructure
{
    public sealed class DataTransferList<T> : ReadOnlyCollection<T>, IDataTransferList<T>
    {
        public DataTransferList([NotNull] IList<T> list) : base(list)
        {
        }

        public IListMetaData MetaData { get; internal set; }
    }
}

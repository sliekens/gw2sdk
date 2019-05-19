using System.Collections.Generic;
using System.Collections.ObjectModel;
using GW2SDK.Infrastructure;

namespace GW2SDK.Features.Common.Infrastructure
{
    public sealed class DataTransferPagedList<T> : ReadOnlyCollection<T>, IDataTransferPagedList<T>
    {
        public DataTransferPagedList([NotNull] IList<T> list) : base(list)
        {
        }

        public IPagedListMetaData MetaData { get; internal set; }

        IListMetaData IDataTransferList<T>.MetaData => MetaData;
    }
}
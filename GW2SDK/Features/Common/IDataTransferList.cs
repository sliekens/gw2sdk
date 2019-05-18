using System.Collections.Generic;

namespace GW2SDK.Features.Common
{
    public interface IDataTransferList<out T> : IReadOnlyList<T>
    {
        IListMetaData MetaData { get; }
    }
}

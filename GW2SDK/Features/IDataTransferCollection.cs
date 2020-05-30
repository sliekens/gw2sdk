using System.Collections.Generic;
using GW2SDK.Annotations;

namespace GW2SDK
{
    [PublicAPI]
    public interface IDataTransferCollection<out T> : IReadOnlyCollection<T>, ICollectionContext
    {
    }
}

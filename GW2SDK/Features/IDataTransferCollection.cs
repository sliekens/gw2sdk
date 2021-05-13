using System.Collections.Generic;
using JetBrains.Annotations;

namespace GW2SDK
{
    [PublicAPI]
    public interface IDataTransferCollection<out T> : IReadOnlyCollection<T>, ICollectionContext
    {
    }
}

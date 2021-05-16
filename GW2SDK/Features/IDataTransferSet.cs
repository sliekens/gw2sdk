using System.Collections.Generic;
using JetBrains.Annotations;

namespace GW2SDK
{
    [PublicAPI]
    public interface IDataTransferSet<T> : IReadOnlySet<T>, ICollectionContext
    {
    }
}

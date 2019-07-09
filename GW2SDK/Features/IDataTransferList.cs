using System.Collections.Generic;
using GW2SDK.Annotations;

namespace GW2SDK
{
    [PublicAPI]
    public interface IDataTransferList<out T> : IReadOnlyList<T>, IListContext
    {
    }
}

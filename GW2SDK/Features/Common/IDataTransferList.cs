using System.Collections.Generic;
using GW2SDK.Infrastructure;

namespace GW2SDK.Features.Common
{
    [PublicAPI]
    public interface IDataTransferList<out T> : IReadOnlyList<T>, IListContext
    {
    }
}

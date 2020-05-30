using System.Collections.Generic;
using GW2SDK.Annotations;

namespace GW2SDK
{
    [PublicAPI]
    public interface IDataTransferPage<out T> : IReadOnlyCollection<T>, IPageContext
    {
    }
}

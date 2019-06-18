using System.Collections.Generic;
using GW2SDK.Infrastructure;

namespace GW2SDK.Features.Common
{
    [PublicAPI]
    public interface IDataTransferPage<out T> : IReadOnlyList<T>, IPageContext
    {
    }
}

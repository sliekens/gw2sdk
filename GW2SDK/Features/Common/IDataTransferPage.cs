using System.Collections.Generic;

namespace GW2SDK.Features.Common
{
    public interface IDataTransferPage<out T> : IReadOnlyList<T>, IPageContext
    {
    }
}

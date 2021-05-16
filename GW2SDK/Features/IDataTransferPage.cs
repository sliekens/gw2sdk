using System.Collections.Generic;
using JetBrains.Annotations;

namespace GW2SDK
{
    [PublicAPI]
    public interface IDataTransferPage<T> : IReadOnlySet<T>, IPageContext
    {
    }
}

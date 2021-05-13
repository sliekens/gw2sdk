using System.Collections.Generic;
using JetBrains.Annotations;

namespace GW2SDK
{
    [PublicAPI]
    public interface IDataTransferPage<out T> : IReadOnlyCollection<T>, IPageContext
    {
    }
}

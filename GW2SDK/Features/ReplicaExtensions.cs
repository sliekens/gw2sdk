using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace GW2SDK
{
    [PublicAPI]
    public static class ReplicaExtensions
    {
        public static IEnumerable<T> Values<T>(this IEnumerable<IReplica<T>> instance)
        {
            return instance.Where(replica => replica.HasValue)
                .Select(replica => replica.Value!);
        }
    }
}

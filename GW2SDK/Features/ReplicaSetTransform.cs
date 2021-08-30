using System.Collections.Generic;

namespace GW2SDK
{
    /// <summary>Used internally by the <see cref="SplitQuery{TKey,TRecord}" />.</summary>
    internal static class ReplicaSetTransform
    {
        internal static IEnumerable<IReplica<T>> Explode<T>(this IReplicaSet<T> set)
        {
            if (!set.HasValues)
            {
                yield break;
            }

            foreach (var record in set.Values)
            {
                yield return new Replica<T>(set.Date, true, record, set.Expires, set.LastModified);
            }
        }
    }
}

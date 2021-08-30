using System;
using System.Collections.Generic;

namespace GW2SDK
{
    internal sealed class ReplicaPage<T> : IReplicaPage<T>
    {
        public ReplicaPage(
            DateTimeOffset date,
            bool hasValues,
#if NET
            IReadOnlySet<T>? value = default,
#else
            IReadOnlyCollection<T>? value = default,
#endif
            IPageContext? context = null,
            DateTimeOffset? expires = null,
            DateTimeOffset? lastModified = null
        )
        {
            Date = date;
            if (hasValues)
            {
                Values = value ?? throw new ArgumentNullException(nameof(value));
                Context = context ?? throw new ArgumentNullException(nameof(context));
                HasValues = true;
                Expires = expires;
                LastModified = lastModified;
            }
        }
#if NET
        public IReadOnlySet<T>? Values { get; }

        public IPageContext? Context { get; }
#else
        public IReadOnlyCollection<T> Values { get; } = default!;

        public IPageContext Context { get; } = default!;
#endif

        public DateTimeOffset Date { get; }

        public DateTimeOffset? Expires { get; }

        public DateTimeOffset? LastModified { get; }

        public bool HasValues { get; }
    }
}

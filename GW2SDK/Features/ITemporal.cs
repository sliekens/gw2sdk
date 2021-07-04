using System;
using JetBrains.Annotations;

namespace GW2SDK
{
    [PublicAPI]
    public interface ITemporal
    {
        /// <summary>Gets the date and time that indicate when the current object was retrieved from the primary data source. This
        /// is strictly informative and cannot be used to track changes to the object. Use <see cref="LastModified" /> to track changes.</summary>
        public DateTimeOffset? Update { get; }

        /// <summary>Gets the date and time that indicate when an update should be requested.</summary>
        public DateTimeOffset? Expires { get; }

        /// <summary>Gets the date and time that indicate when the current object was changed.</summary>
        public DateTimeOffset? LastModified { get; }
    }
}

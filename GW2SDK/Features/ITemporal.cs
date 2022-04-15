using System;
using JetBrains.Annotations;

namespace GW2SDK;

[PublicAPI]
public interface ITemporal
{
    /// <summary>Gets the date and time that indicate when the current object was retrieved from the source of truth. This is
    /// strictly informative and does not represent the real age of the object. It can be used to calculate the age of a cache
    /// entry, but not for change tracking. Use <see cref="LastModified" /> to track changes. Use <see cref="Expires" /> to
    /// calculate the freshness of the object.</summary>
    public DateTimeOffset Date { get; }

    /// <summary>Gets the date and time that indicate when an update should be requested.</summary>
    public DateTimeOffset? Expires { get; }

    /// <summary>Gets the date and time that indicate when the current object was changed.</summary>
    public DateTimeOffset? LastModified { get; }
}

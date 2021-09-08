using System.ComponentModel;
using JetBrains.Annotations;

namespace GW2SDK.Http.Caching
{
    [PublicAPI]
    [DefaultValue(Public)]
    public enum CachingBehavior
    {
        /// <summary>The default behavior, instructs the cache to store only responses that can be reused by more than one user.
        /// This behavior is safe for all application types, but it works best for applications that accomodate multiple users,
        /// e.g. server applications, or even desktop applications that support user switching.</summary>
        Public,

        /// <summary>Unsafe, instructs the cache to also store personal information. This is only suitable for applications that
        /// are dedicated to a single user, e.g. a cache that is bundled with apps installed on the user's personal device.</summary>
        Private
    }
}

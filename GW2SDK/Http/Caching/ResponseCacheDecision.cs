using System.ComponentModel;
using JetBrains.Annotations;

namespace GW2SDK.Http.Caching
{
    [PublicAPI]
    [DefaultValue(Miss)]
    public enum ResponseCacheDecision
    {
        /// <summary>Indicates that no stored response can be reused for a request.</summary>
        Miss,

        /// <summary>Indicates that a stored response can be reused for a request.</summary>
        Hit,

        /// <summary>Indicates that a stored response was found but must be validated with the origin server.</summary>
        Validate,

        /// <summary>Indicates that a stored response past its freshness lifetime can be reused (either explicitly or because the origin server is unreachable).</summary>
        Stale
    }
}

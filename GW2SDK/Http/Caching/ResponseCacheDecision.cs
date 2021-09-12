using System.ComponentModel;
using JetBrains.Annotations;

namespace GW2SDK.Http.Caching
{
    [PublicAPI]
    [DefaultValue(Miss)]
    public enum ResponseCacheDecision
    {
        /// <summary>Indicates that the origin must be contacted for a request.</summary>
        Miss,

        /// <summary>Indicates that a stored response can be reused for a request.</summary>
        Fresh,

        /// <summary>Indicates that a stored response exists for a request, but it must be validated with the origin.</summary>
        MustValidate,

        /// <summary>Indicates that a stale response can be reused for a request without validating.</summary>
        AllowStale
    }
}

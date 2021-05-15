using JetBrains.Annotations;

namespace GW2SDK
{
    /// <summary>A token is usually obtained as part of the result of another resource request. It can contain the URI of
    /// related resources or the URI of the original resource itself.</summary>
    /// <remarks>Not meant to be used directly.</remarks>
    [PublicAPI]
    public sealed record ContinuationToken(string Token);
}

using System;
using System.Net.Http;
using JetBrains.Annotations;

namespace GW2SDK
{
    /// <summary>A GET request for a given continuation token.</summary>
    /// <remarks>Not meant to be used directly.</remarks>
    [PublicAPI]
    public sealed class ContinuationRequest
    {
        public ContinuationRequest(ContinuationToken continuation)
        {
            if (continuation is null or { IsEmpty: true })
            {
                throw new ArgumentException("Continuation token cannot be empty.", nameof(continuation));
            }

            Continuation = continuation;
        }

        public ContinuationToken Continuation { get; }

        public static implicit operator HttpRequestMessage(ContinuationRequest r)
        {
            var location = new Uri(r.Continuation.Token, UriKind.Relative);
            return new HttpRequestMessage(HttpMethod.Get, location);
        }
    }
}

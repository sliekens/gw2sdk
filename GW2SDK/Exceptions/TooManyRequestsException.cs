using System;
using GW2SDK.Annotations;

namespace GW2SDK
{
    [PublicAPI]
    public sealed class TooManyRequestsException : InvalidOperationException
    {
        public TooManyRequestsException()
        {
        }

        public TooManyRequestsException(string? message)
            : base(message)
        {
        }

        public TooManyRequestsException(string? message, Exception? innerException)
            : base(message, innerException)
        {
        }
    }
}

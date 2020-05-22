using System;
using GW2SDK.Annotations;

namespace GW2SDK.Exceptions
{
    [PublicAPI]
    public sealed class UnauthorizedOperationException : InvalidOperationException
    {
        public UnauthorizedOperationException()
        {
        }

        public UnauthorizedOperationException(string? message)
            : base(message)
        {
        }

        public UnauthorizedOperationException(string? message, Exception? innerException)
            : base(message, innerException)
        {
        }
    }
}

using System;
using JetBrains.Annotations;

namespace GW2SDK.Http
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

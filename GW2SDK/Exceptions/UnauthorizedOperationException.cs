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

        public UnauthorizedOperationException([CanBeNull] string message)
            : base(message)
        {
        }

        public UnauthorizedOperationException([CanBeNull] string message, [CanBeNull] Exception innerException)
            : base(message, innerException)
        {
        }
    }
}

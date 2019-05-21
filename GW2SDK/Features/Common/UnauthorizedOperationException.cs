using System;
using GW2SDK.Infrastructure;

namespace GW2SDK.Features.Common
{
    public sealed class UnauthorizedOperationException : InvalidOperationException
    {
        public UnauthorizedOperationException()
        {
        }

        public UnauthorizedOperationException([CanBeNull] string message) : base(message)
        {
        }

        public UnauthorizedOperationException([CanBeNull] string message, [CanBeNull] Exception innerException) : base(message, innerException)
        {
        }
    }
}

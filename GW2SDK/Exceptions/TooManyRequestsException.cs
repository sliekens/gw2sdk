using System;
using GW2SDK.Annotations;

namespace GW2SDK.Exceptions
{
    [PublicAPI]
    public sealed class TooManyRequestsException : InvalidOperationException
    {
        public TooManyRequestsException()
        {
        }

        public TooManyRequestsException([CanBeNull] string message)
            : base(message)
        {
        }

        public TooManyRequestsException([CanBeNull] string message, [CanBeNull] Exception innerException)
            : base(message, innerException)
        {
        }
    }
}

using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

using GuildWars2.Http;

namespace GuildWars2;

[StackTraceHidden]
internal static class ThrowHelper
{
    [DoesNotReturn]
    internal static void ThrowInvalidOperationException(string? message)
    {
        throw new InvalidOperationException(message);
    }

    [DoesNotReturn]
    internal static void ThrowUnexpectedMember(string memberName)
    {
        ThrowInvalidOperationException($"Unexpected member '{memberName}'.");
    }

    [DoesNotReturn]
    internal static void ThrowUnexpectedArrayLength(int length)
    {
        ThrowInvalidOperationException($"Unexpected array length [{length}].");
    }

    [DoesNotReturn]
    internal static void ThrowUnexpectedDiscriminator(string? discriminatorValue)
    {
        ThrowInvalidOperationException($"Unexpected discriminator value '{discriminatorValue}'.");
    }

    [DoesNotReturn]
    internal static void ThrowInvalidDiscriminator(string? discriminatorValue)
    {
        ThrowInvalidOperationException($"Invalid discriminator value '{discriminatorValue}'.");
    }

    [DoesNotReturn]
    internal static void ThrowArgumentNull(string? paramName)
    {
        throw new ArgumentNullException(paramName);
    }

    public static void ThrowIfNull(
        [System.Diagnostics.CodeAnalysis.NotNull] object? argument,
        [CallerArgumentExpression(nameof(argument))] string? paramName = null
    )
    {
        if (argument is null)
        {
            ThrowArgumentNull(paramName);
        }
    }

    [DoesNotReturn]
    internal static void ThrowBadArgument(string? message, string? paramName)
    {
        throw new ArgumentException(message, paramName);
    }

    internal static void ThrowArgumentOutOfRange(
        string? message,
        object argument,
        [CallerArgumentExpression(nameof(argument))] string? paramName = null
    )
    {
        throw new ArgumentOutOfRangeException(paramName, argument, message);
    }

    internal static void ThrowInvalidFormat(string? message)
    {
        throw new FormatException(message);
    }

    [DoesNotReturn]
    internal static void ThrowObjectDisposed(object? instance)
    {
        throw new ObjectDisposedException(instance?.GetType().FullName);
    }

    [DoesNotReturn]
    public static void ThrowBadResponse(string? message, HttpStatusCode? statusCode)
    {
#if NET
        throw new BadResponseException(message, null, statusCode);
#else
        throw new BadResponseException(message);
#endif
    }

    [DoesNotReturn]
    public static void ThrowBadResponse(
        string? message,
        Exception? innerException,
        HttpStatusCode? statusCode
    )
    {
#if NET
        throw new BadResponseException(message, innerException, statusCode);
#else
        throw new BadResponseException(message, innerException);
#endif
    }
}

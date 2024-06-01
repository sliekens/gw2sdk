using System.Diagnostics.CodeAnalysis;

namespace GuildWars2;

internal static class ThrowHelper
{
    [DoesNotReturn]
    internal static void ThrowUnexpectedMember(string memberName) =>
        throw new InvalidOperationException($"Unexpected member '{memberName}'.");

    [DoesNotReturn]
    internal static void ThrowUnexpectedArrayLength(int length) =>
        throw new InvalidOperationException($"Unexpected array length [{length}].");

    [DoesNotReturn]
    internal static void ThrowUnexpectedDiscriminator(string? discriminatorValue) =>
        throw new InvalidOperationException(
            $"Unexpected discriminator value '{discriminatorValue}'."
        );

    [DoesNotReturn]
    internal static void ThrowInvalidDiscriminator(string? discriminatorValue) =>
        throw new InvalidOperationException($"Invalid discriminator value '{discriminatorValue}'.");
}

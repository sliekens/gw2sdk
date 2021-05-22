namespace GW2SDK
{
    /// <summary>Utility for formatting commonly used strings, e.g. Exception messages.</summary>
    internal static class Strings
    {
        internal static string UnexpectedMember(string memberName) => $"Unexpected member '{memberName}'.";

        internal static string UnexpectedDiscriminator(string? discriminatorValue) => $"Unexpected discriminator value '{discriminatorValue}'.";

        internal static string InvalidDiscriminator(string? discriminatorValue) => $"Invalid discriminator value '{discriminatorValue}'.";
    }
}

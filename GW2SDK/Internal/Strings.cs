namespace GuildWars2;

/// <summary>Utility for formatting commonly used strings, e.g. Exception messages.</summary>
internal static class Strings
{
    internal static string UnexpectedMember(string memberName) =>
        $"Unexpected member '{memberName}'.";

    internal static string UnexpectedEnum(string memberName) =>
        $"Unexpected enum member '{memberName}'.";

    internal static string UnexpectedArrayLength(int length) =>
        $"Unexpected array length [{length}].";

    internal static string UnexpectedDiscriminator(string? discriminatorValue) =>
        $"Unexpected discriminator value '{discriminatorValue}'.";

    internal static string InvalidDiscriminator(string? discriminatorValue) =>
        $"Invalid discriminator value '{discriminatorValue}'.";

    internal static string ToCsv(this IEnumerable<string> values) => string.Join(",", values);
}

using System;
using System.Collections.Generic;

namespace GuildWars2;

/// <summary>Utility for formatting commonly used strings, e.g. Exception messages.</summary>
internal static class Strings
{
    internal static string UnexpectedMember(string memberName) =>
        $"Unexpected member '{memberName}'.";

    internal static string UnexpectedDiscriminator(string? discriminatorValue) =>
        $"Unexpected discriminator value '{discriminatorValue}'.";

    internal static string InvalidDiscriminator(string? discriminatorValue) =>
        $"Invalid discriminator value '{discriminatorValue}'.";

    internal static int GetDeterministicHashCode(this string str)
    {
        // Yoinked from a blog post, thanks
        // https://andrewlock.net/why-is-string-gethashcode-different-each-time-i-run-my-program-in-net-core/
        unchecked
        {
            var hash1 = (5381 << 16) + 5381;
            var hash2 = hash1;

            for (var i = 0; i < str.Length; i += 2)
            {
                hash1 = ((hash1 << 5) + hash1) ^ str[i];
                if (i == str.Length - 1)
                {
                    break;
                }

                hash2 = ((hash2 << 5) + hash2) ^ str[i + 1];
            }

            return hash1 + (hash2 * 1566083941);
        }
    }

    internal static string UrlEncoded(this string value) => Uri.EscapeDataString(value);

    internal static string ToCsv(this IEnumerable<string> values) => string.Join(",", values);
}

using System.Globalization;
using System.Text.RegularExpressions;

namespace GuildWars2;

/// <summary>Represents a language.</summary>
[PublicAPI]
public sealed class Language
{
    private static readonly Regex Alpha2Pattern = new(
        "^[a-z]{2}$",
        RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Compiled
    );

    /// <summary>Represents the English language.</summary>
    public static readonly Language English = new("en");

    /// <summary>Represents the Spanish language.</summary>
    public static readonly Language Spanish = new("es");

    /// <summary>Represents the German language.</summary>
    public static readonly Language German = new("de");

    /// <summary>Represents the French language.</summary>
    public static readonly Language French = new("fr");

    /// <summary>Represents the Chinese language.</summary>
    public static readonly Language Chinese = new("zh");

    /// <summary>Initializes a new instance of the <see cref="Language" /> class with the specified alpha-2 code.</summary>
    /// <param name="alpha2Code">The alpha-2 code representing the language.</param>
    /// <exception cref="ArgumentException">Thrown when <paramref name="alpha2Code" /> is null, empty, or whitespace, or when
    /// it is not a valid two-letter code.</exception>
    public Language(string alpha2Code)
    {
        if (string.IsNullOrWhiteSpace(alpha2Code))
        {
            ThrowHelper.ThrowBadArgument("Value cannot be null or whitespace.", nameof(alpha2Code));
        }

        if (!Alpha2Pattern.IsMatch(alpha2Code))
        {
            ThrowHelper.ThrowBadArgument("Value must be a two-letter code.", nameof(alpha2Code));
        }

        Alpha2Code = alpha2Code.ToLowerInvariant();
    }

    /// <summary>Gets the language associated with the current <see cref="CultureInfo" />.</summary>
    public static Language CurrentUICulture =>
        new(CultureInfo.CurrentUICulture.TwoLetterISOLanguageName);

    /// <summary>Gets the alpha-2 code representing the language.</summary>
    public string Alpha2Code { get; }

    /// <summary>Gets the <see cref="CultureInfo" /> associated with the current language.</summary>
    public CultureInfo CultureInfo => CultureInfo.GetCultureInfo(Alpha2Code);

    /// <summary>Gets the string representation of the language.</summary>
    /// <returns>The alpha-2 code representing the language.</returns>
    public override string ToString()
    {
        return Alpha2Code;
    }
}

﻿using System.Globalization;
using System.Text.RegularExpressions;

namespace GuildWars2;

[PublicAPI]
public sealed class Language
{
    private static readonly Regex Alpha2Pattern = new(
        "^[a-z]{2}$",
        RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Compiled
    );

    public static readonly Language English = new("en");

    public static readonly Language Spanish = new("es");

    public static readonly Language German = new("de");

    public static readonly Language French = new("fr");

    public static readonly Language Chinese = new("zh");

    public Language(string alpha2Code)
    {
        if (string.IsNullOrWhiteSpace(alpha2Code))
        {
            throw new ArgumentException("Value cannot be null or whitespace.", nameof(alpha2Code));
        }

        if (!Alpha2Pattern.IsMatch(alpha2Code))
        {
            throw new ArgumentException("Value must be a two-letter code.", nameof(alpha2Code));
        }

        Alpha2Code = alpha2Code.ToLowerInvariant();
    }

    public static Language CurrentUICulture =>
        new(CultureInfo.CurrentUICulture.TwoLetterISOLanguageName);

    public string Alpha2Code { get; }

    public CultureInfo CultureInfo => CultureInfo.GetCultureInfo(Alpha2Code);

    public override string ToString() => Alpha2Code;
}
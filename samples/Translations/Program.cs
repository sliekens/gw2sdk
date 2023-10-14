using System.Globalization;
using GuildWars2;

using var httpClient = new HttpClient();
var gw2 = new Gw2Client(httpClient);

// For demonstration, pretend the user has selected German as the preferred language
CultureInfo.CurrentUICulture = CultureInfo.GetCultureInfo("de");

// Typically you want to configure the user's preferred language (CurrentUICulture) as the default
httpClient.DefaultRequestHeaders.AcceptLanguage.Add(
    new System.Net.Http.Headers.StringWithQualityHeaderValue(Language.CurrentUICulture.Alpha2Code)
);

var mounts = await gw2.Mounts.GetMounts();

Console.WriteLine("Preferred language");
foreach (var mount in mounts.Value)
{
    Console.WriteLine("* {0}", mount.Name);
}

Console.WriteLine();

// Alternatively you can specify a language per request
// See the default set of supported languages
HashSet<Language> languages = new()
{
    Language.English,
    Language.German,
    Language.French,
    Language.Spanish,
    Language.Chinese
};

foreach (var language in languages)
{
    // Localizable endpoints accept an optional language parameter
    // When omitted, the default language is used (as specified in Accept-Language, or English if not specified)
    mounts = await gw2.Mounts.GetMounts(language);

    Console.WriteLine(language.CultureInfo.EnglishName);
    foreach (var mount in mounts.Value)
    {
        Console.WriteLine("* {0}", mount.Name);
    }

    Console.WriteLine();
}

// Finally let's see what happens when an unsupported language is used
var unsupportedLanguage = new Language("ko");
mounts = await gw2.Mounts.GetMounts(unsupportedLanguage);

Console.WriteLine(unsupportedLanguage.CultureInfo.EnglishName + " (unsupported)");
foreach (var mount in mounts.Value)
{
    Console.WriteLine("* {0}", mount.Name);
}

Console.WriteLine();

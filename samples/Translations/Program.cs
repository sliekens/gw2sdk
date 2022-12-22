using System.Globalization;
using System.Text;
using GuildWars2;
using GuildWars2.Mounts;

Console.OutputEncoding = Encoding.UTF8;

using var httpClient = new HttpClient();
var gw2 = new Gw2Client(httpClient);

// The default set of supported languages
var languages = new HashSet<Language>
{
    Language.English,
    Language.German,
    Language.French,
    Language.Spanish,
    Language.Chinese
};

// If your application uses CultureInfo.CurrentUICulture for translations, you might want to use Language.CurrentUICulture
CultureInfo.CurrentUICulture = CultureInfo.GetCultureInfo("ko");
languages.Add(Language.CurrentUICulture);

// Loop over the languages, show names of Mounts in each language
foreach (var language in languages)
{
    Console.WriteLine(CultureInfo.GetCultureInfo(language.Alpha2Code).EnglishName);

    // Pass the Language object when fetching the data
    // If you don't pass any Language, or if the language is not supported, English is used
    // So you will see the English names for Korean ("ko") because the server does not support Korean
    foreach (var mount in (HashSet<Mount>)await gw2.Mounts.GetMounts(language))
    {
        Console.WriteLine("* {0}", mount.Name);
    }

    Console.WriteLine();
}

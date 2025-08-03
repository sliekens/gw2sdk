﻿using System.Globalization;

using GuildWars2;
using GuildWars2.Hero.Equipment.Mounts;

// For demonstration, pretend the user has selected German as the preferred language
CultureInfo.CurrentUICulture = CultureInfo.GetCultureInfo("de");

using HttpClient httpClient = new();
Gw2Client gw2 = new(httpClient);

// Use the preferred language by passing 'Language.CurrentUICulture'
HashSet<Mount> mounts = await gw2.Hero.Equipment.Mounts.GetMounts(Language.CurrentUICulture).ValueOnly();
PrintMountNames("CurrentUICulture (German)", mounts);

// Alternatively you can force a specific language
mounts = await gw2.Hero.Equipment.Mounts.GetMounts(Language.English).ValueOnly();
PrintMountNames("English", mounts);

mounts = await gw2.Hero.Equipment.Mounts.GetMounts(Language.German).ValueOnly();
PrintMountNames("German", mounts);

mounts = await gw2.Hero.Equipment.Mounts.GetMounts(Language.French).ValueOnly();
PrintMountNames("French", mounts);

mounts = await gw2.Hero.Equipment.Mounts.GetMounts(Language.Spanish).ValueOnly();
PrintMountNames("Spanish", mounts);

mounts = await gw2.Hero.Equipment.Mounts.GetMounts(Language.Chinese).ValueOnly();
PrintMountNames("Chinese", mounts);

// Finally, let's see what happens when an unsupported language is used:
CultureInfo.CurrentUICulture = CultureInfo.GetCultureInfo("ja");
mounts = await gw2.Hero.Equipment.Mounts.GetMounts(Language.CurrentUICulture).ValueOnly();
PrintMountNames(Language.CurrentUICulture.CultureInfo.EnglishName, mounts);

static void PrintMountNames(string language, IEnumerable<Mount> mounts)
{
    Console.WriteLine(language);
    foreach (Mount mount in mounts)
    {
        Console.WriteLine("* {0}", mount.Name);
    }

    Console.WriteLine();
}

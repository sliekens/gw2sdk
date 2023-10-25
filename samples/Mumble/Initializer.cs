using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;

namespace BasicUsage;

static class Initializer
{
    [ModuleInitializer]
    internal static void Initialize()
    {
        Console.OutputEncoding = Encoding.UTF8;
        CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("en");
        CultureInfo.CurrentUICulture = CultureInfo.GetCultureInfo("en");
    }
}

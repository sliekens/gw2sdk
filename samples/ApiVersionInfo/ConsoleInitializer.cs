using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;

namespace ApiVersionInfo;

public static class ConsoleInitializer
{
    [ModuleInitializer]
    internal static void Run()
    {
        Console.OutputEncoding = Encoding.UTF8;
        CultureInfo.CurrentCulture = CultureInfo.GetCultureInfo("en");
        CultureInfo.CurrentUICulture = CultureInfo.GetCultureInfo("en");
    }
}

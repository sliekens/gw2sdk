using Spectre.Console;

namespace ApiVersionInfo;

internal sealed class RouteOptions
{
    public bool ShowDisabled { get; init; }

    public bool ShowAuthorized { get; init; }

    public static RouteOptions Prompt()
    {
        return new RouteOptions
        {
            ShowDisabled = AnsiConsole.Confirm("Show disabled routes?", false),
            ShowAuthorized = AnsiConsole.Confirm("Show routes that require an account?")
        };
    }
}

using System;
using Microsoft.Extensions.Logging;
using Spectre.Console;

namespace MostVersatileMaterials.Logging;

public class AnsiConsoleLogger : ILogger
{
    public void Log<TState>(
        LogLevel logLevel,
        EventId eventId,
        TState state,
        Exception? exception,
        Func<TState, Exception?, string> formatter
    )
    {
        if (!IsEnabled(logLevel))
        {
            return;
        }

        var message = formatter(state, null);

        AnsiConsole.Markup($"[grey][[{DateTime.Now:HH:mm:ss} {eventId}]][/] {message}");
        if (exception != null)
        {
            AnsiConsole.WriteException(exception);
        }
    }

    public bool IsEnabled(LogLevel logLevel) => true;

    public IDisposable? BeginScope<TState>(TState state) where TState : notnull => null;
}

public class AnsiConsoleLoggerProvider : ILoggerProvider
{
    public ILogger CreateLogger(string categoryName) => new AnsiConsoleLogger();

    public void Dispose()
    {
    }
}

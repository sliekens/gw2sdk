using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace GuildWars2;

[StackTraceHidden]
internal static class Ensure
{
    internal static T NotNull<T>(
        [System.Diagnostics.CodeAnalysis.NotNull] T? argument,
        [CallerArgumentExpression("argument")] string? paramName = null
    ) where T : class
    {
        ThrowHelper.ThrowIfNull(argument, paramName);
        return argument;
    }
}

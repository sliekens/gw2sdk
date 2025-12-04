using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace GuildWars2.Tests.TestInfrastructure;

internal static class ArgumentNullExceptionExtensions
{
    extension(ArgumentNullException)
    {
        public static void ThrowIfNull([NotNull] object? argument, [CallerMemberName] string? argumentName = null)
        {
            if (argument is null)
            {
                throw new ArgumentNullException(argumentName);
            }
        }
    }
}

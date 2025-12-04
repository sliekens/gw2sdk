namespace GuildWars2.Tests.TestInfrastructure;

internal static class StreamReaderExtensions
{
#if !NET
    public static Task<string> ReadToEndAsync(this StreamReader instance, CancellationToken cancellationToken)
    {
        return cancellationToken.IsCancellationRequested
            ? Task.FromCanceled<string>(cancellationToken)
            : instance.ReadToEndAsync();
    }
#endif
}

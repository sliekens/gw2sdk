namespace GuildWars2.Tests.TestInfrastructure;

internal static class HttpContentExtensions
{
#if !NET
    /// <summary>Provides an overload of <see cref="LoadIntoBufferAsync"/> that accepts a <see cref="CancellationToken"/>.</summary>
    /// <param name="instance">The HTTP content instance to read from.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <remarks>
    /// This overload is provided for compatibility with older versions of .NET where the native method does not accept a <see cref="CancellationToken"/>.
    /// </remarks>
    public static Task LoadIntoBufferAsync(
        this HttpContent instance,
        CancellationToken cancellationToken)
    {

        return cancellationToken.IsCancellationRequested
            ? Task.FromCanceled(cancellationToken)
            : instance.LoadIntoBufferAsync();
    }

    /// <summary>Provides an overload of <see cref="ReadAsStreamAsync"/> that accepts a <see cref="CancellationToken"/>.</summary>
    /// <param name="instance">The HTTP content instance to read from.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <remarks>
    /// This overload is provided for compatibility with older versions of .NET where the native method does not accept a <see cref="CancellationToken"/>.
    /// </remarks>
    public static Task<Stream> ReadAsStreamAsync(
        this HttpContent instance,
        CancellationToken cancellationToken
    )
    {
        return cancellationToken.IsCancellationRequested
            ? Task.FromCanceled<Stream>(cancellationToken)
            : instance.ReadAsStreamAsync();
    }

    /// <summary>Provides an overload of <see cref="ReadAsStringAsync"/> that accepts a <see cref="CancellationToken"/>.</summary>
    /// <param name="instance">The HTTP content instance to read from.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <remarks>
    /// This overload is provided for compatibility with older versions of .NET where the native method does not accept a <see cref="CancellationToken"/>.
    /// </remarks>
    public static Task<string> ReadAsStringAsync(
        this HttpContent instance,
        CancellationToken cancellationToken
    )
    {
        return cancellationToken.IsCancellationRequested
            ? Task.FromCanceled<string>(cancellationToken)
            : instance.ReadAsStringAsync();
    }
#endif
}

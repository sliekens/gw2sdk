using System.Diagnostics;

using GuildWars2;
using GuildWars2.Collections;
using GuildWars2.Hero.Builds;

using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace MemoryCacheUsage;

internal sealed class Program
{
    public static async Task Main(string[] args)
    {
        HostApplicationBuilder appBuilder = Host.CreateApplicationBuilder(args);

        // IMemoryCache is registered as a singleton, so all consumers share the same entries
        appBuilder.Services.AddMemoryCache();
        appBuilder.Services.AddHttpClient<Gw2Client>();
        appBuilder.Services.AddSingleton<CachedTraits>();

        // Keep the request log out of the way, so the timings below are easy to read
        appBuilder.Logging.AddFilter("System.Net.Http.HttpClient", LogLevel.Warning);

        IHost app = appBuilder.Build();

        CachedTraits traits = app.Services.GetRequiredService<CachedTraits>();

        // The first call fetches every trait in one request, the second call is served from memory
        IImmutableValueSet<Trait> firstCall = await Measure("Cold", () => traits.GetTraits())
            .ConfigureAwait(false);
        await Measure("Warm", () => traits.GetTraits()).ConfigureAwait(false);

        // Single traits come out of the cached set without another request
        Trait trait = await traits.GetTraitById(firstCall.Min(static trait => trait.Id))
            .ConfigureAwait(false);
        Console.WriteLine($"Trait {trait.Id} is '{trait.Name}'");

        static async Task<IImmutableValueSet<Trait>> Measure(
            string label,
            Func<Task<IImmutableValueSet<Trait>>> getTraits
        )
        {
            long before = Stopwatch.GetTimestamp();
            IImmutableValueSet<Trait> traits = await getTraits().ConfigureAwait(false);
            TimeSpan elapsed = Stopwatch.GetElapsedTime(before);

            Console.WriteLine($"{label,-5}: {traits.Count} traits in {elapsed.TotalMilliseconds:F0}ms");
            return traits;
        }
    }
}

// Caches v2/traits, which only changes when the game is updated
//
// Usage:
//  services.AddMemoryCache();
//  services.AddHttpClient<Gw2Client>();
//  services.AddSingleton<CachedTraits>();
internal sealed class CachedTraits(Gw2Client gw2, IMemoryCache cache)
{
    /// <summary>Retrieves all traits, cached until the API says the response is stale.</summary>
    public async Task<IImmutableValueSet<Trait>> GetTraits(
        Language? language = null,
        CancellationToken cancellationToken = default
    )
    {
        // Trait names and descriptions are localized, so the language is part of the cache key
        string cacheKey = $"v2/traits?lang={language?.Alpha2Code ?? "en"}";

        IImmutableValueSet<Trait>? traits = await cache.GetOrCreateAsync(
                cacheKey,
                async entry =>
                {
                    (IImmutableValueSet<Trait> value, MessageContext context) = await gw2.Hero.Builds
                        .GetTraits(language, cancellationToken: cancellationToken)
                        .ConfigureAwait(false);

                    // MessageContext exposes the Expires and Last-Modified response headers
                    entry.AbsoluteExpiration = context.Expires ?? context.Date.AddHours(1);
                    return value;
                }
            )
            .ConfigureAwait(false);

        // The factory above never returns null, so neither does GetOrCreateAsync
        return traits!;
    }

    /// <summary>Retrieves a trait by its ID from the cached set of all traits.</summary>
    public async Task<Trait> GetTraitById(
        int traitId,
        Language? language = null,
        CancellationToken cancellationToken = default
    )
    {
        IImmutableValueSet<Trait> traits = await GetTraits(language, cancellationToken)
            .ConfigureAwait(false);

        return traits.Single(trait => trait.Id == traitId);
    }
}

using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Commerce.Listings;

// ReSharper disable once ClassNeverInstantiated.Global
public class OrderBookFixture
{
    public IReadOnlyCollection<string> ItemPrices { get; } = FlatFileReader.Read("Data/listings.json.gz").ToList().AsReadOnly();
}

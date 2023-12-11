using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Commerce.Prices;

// ReSharper disable once ClassNeverInstantiated.Global
public class ItemPriceFixture
{
    public IReadOnlyCollection<string> ItemPrices { get; } =
        FlatFileReader.Read("Data/prices.json.gz").ToList().AsReadOnly();
}

using System.Collections.Generic;
using System.Linq;

using GW2SDK.Tests.TestInfrastructure;

namespace GW2SDK.Tests.Features.Commerce.Listings;

// ReSharper disable once ClassNeverInstantiated.Global
public class OrderBookFixture
{
    public OrderBookFixture()
    {
        ItemPrices = FlatFileReader.Read("Data/listings.json.gz")
            .ToList()
            .AsReadOnly();
    }

    public IReadOnlyCollection<string> ItemPrices { get; }
}
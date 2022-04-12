using System.Collections.Generic;
using System.Linq;

using GW2SDK.Tests.TestInfrastructure;

namespace GW2SDK.Tests.Features.Commerce.Prices;

// ReSharper disable once ClassNeverInstantiated.Global
public class ItemPriceFixture
{
    public ItemPriceFixture()
    {
        ItemPrices = FlatFileReader.Read("Data/prices.json.gz")
            .ToList()
            .AsReadOnly();
    }

    public IReadOnlyCollection<string> ItemPrices { get; }
}
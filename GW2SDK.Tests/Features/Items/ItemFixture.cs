using System.Collections.Generic;
using System.Linq;
using GW2SDK.Tests.TestInfrastructure;

namespace GW2SDK.Tests.Features.Items;

// ReSharper disable once ClassNeverInstantiated.Global
public class ItemFixture
{
    public ItemFixture()
    {
        Items = FlatFileReader.Read("Data/items.json.gz").ToList().AsReadOnly();
    }

    public IReadOnlyCollection<string> Items { get; }
}

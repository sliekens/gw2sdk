using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Items;

// ReSharper disable once ClassNeverInstantiated.Global
public class ItemFixture
{
    public ItemFixture()
    {
        Items = FlatFileReader.Read("Data/items.json.gz").ToList().AsReadOnly();
    }

    public IReadOnlyCollection<string> Items { get; }
}

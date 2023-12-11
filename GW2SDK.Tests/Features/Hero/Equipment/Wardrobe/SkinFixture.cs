using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Equipment.Wardrobe;

public class SkinFixture
{
    public IReadOnlyCollection<string> Skins { get; } =
        FlatFileReader.Read("Data/skins.json.gz").ToList().AsReadOnly();
}

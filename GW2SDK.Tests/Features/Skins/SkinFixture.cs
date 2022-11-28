using System.Collections.Generic;
using System.Linq;
using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Skins;

public class SkinFixture
{
    public SkinFixture()
    {
        Skins = FlatFileReader.Read("Data/skins.json.gz").ToList().AsReadOnly();
    }

    public IReadOnlyCollection<string> Skins { get; }
}

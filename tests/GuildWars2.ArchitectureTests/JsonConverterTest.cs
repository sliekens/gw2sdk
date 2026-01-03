using System.Text.Json.Serialization;

using GuildWars2.Tests.Common;

namespace GuildWars2.ArchitectureTests;

[ClassDataSource<AssemblyFixture>(Shared = SharedType.PerTestSession)]
public class JsonConverterTest(AssemblyFixture fixture)
{
    [Test]
    public async Task AllEnumsShouldHaveJsonConverterAttribute()
    {
        IEnumerable<Type> enumTypes = fixture.ExportedEnums.Where(t => t is { IsPublic: true, Namespace: not null } && t.Namespace.StartsWith("GuildWars2", StringComparison.Ordinal));
        using (Assert.Multiple())
        {
            foreach (Type enumType in enumTypes)
            {
                bool hasJsonConverterAttribute = enumType.GetCustomAttributes(typeof(JsonConverterAttribute), false).Length != 0;
                await Assert.That(hasJsonConverterAttribute).IsTrue().Because($"Enum {enumType.Name} does not have a JsonConverterAttribute.");
            }
        }
    }
}

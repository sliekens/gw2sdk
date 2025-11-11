using System.Text.Json.Serialization;

using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.PatternsAndPractices;

[ClassDataSource<AssemblyFixture>(Shared = SharedType.PerTestSession)]
public class JsonConverterTest(AssemblyFixture fixture)
{
    [Test]
    public void AllEnumsShouldHaveJsonConverterAttribute()
    {
        // Get all enum types in the assembly
        IEnumerable<Type> enumTypes = fixture.Assembly.GetTypes().Where(t => t is { IsEnum: true, IsPublic: true, Namespace: not null } && t.Namespace.StartsWith("GuildWars2", StringComparison.Ordinal));
        Assert.All(enumTypes, enumType =>
        {
            bool hasJsonConverterAttribute = enumType.GetCustomAttributes(typeof(JsonConverterAttribute), false).Length != 0;
            Assert.True(hasJsonConverterAttribute, $"Enum {enumType.Name} does not have a JsonConverterAttribute.");
        });
    }
}

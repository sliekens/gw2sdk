using System.Text.Json.Serialization;
using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.PatternsAndPractices;

public class JsonConverterTest(AssemblyFixture fixture) : IClassFixture<AssemblyFixture>
{
    [Fact]
    public void AllEnumsShouldHaveJsonConverterAttribute()
    {
        // Get all enum types in the assembly
        var enumTypes = fixture.Assembly.GetTypes()
            .Where(t => t is { IsEnum: true, IsPublic: true, Namespace: not null } && t.Namespace.StartsWith("GuildWars2"));

        Assert.All(enumTypes,
            enumType =>
            {
                var hasJsonConverterAttribute =
                    enumType.GetCustomAttributes(typeof(JsonConverterAttribute), false).Any();
                Assert.True(
                    hasJsonConverterAttribute,
                    $"Enum {enumType.Name} does not have a JsonConverterAttribute."
                );
            });
    }
}

using System.Text.Json;

using GuildWars2.Tests.Common;

namespace GuildWars2.ArchitectureTests;

[ClassDataSource<AssemblyFixture>(Shared = SharedType.PerTestSession)]
public class DataTransferJsonTest(AssemblyFixture fixture)
{
    // Could use a better name
    // The premise is that every JSON conversion should be an extension method for JsonElement
    // e.g. JsonDocument.RootElement.GetSomeRecord()
    [Test]
    public async Task JsonElement_conversions_are_extensions()
    {
        List<AssemblyFixture.JsonReaderMethod> staticMethods =
            [.. fixture.JsonElementReaderMethods];
        Assert.All(fixture.DataTransferObjects, dto =>
        {
            List<AssemblyFixture.JsonReaderMethod> matches = [.. staticMethods.Where(m => m.ReturnType == dto)];
            Assert.All(matches, m =>
            {
                Assert.Equal("Get" + dto.Name, m.Name);
                Assert.Equal(dto.Name + "Json", m.DeclaringType.Name);
                Assert.True(m.IsDeclaringTypeNotPublic, $"{m.Name} must be internal.");
                Assert.True(m.IsExtensionMethod, $"{m.Name} must be an extension method.");
                // Parameter type recorded without ref modifier; ensure it's JsonElement
                Assert.Equal(typeof(JsonElement), m.FirstParameterType);
                Assert.Equal(dto.Namespace, m.Namespace);
            });
        });
    }
}

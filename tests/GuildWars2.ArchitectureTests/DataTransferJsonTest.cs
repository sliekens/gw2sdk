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
        using (Assert.Multiple())
        {
            foreach (Type dto in fixture.DataTransferObjects)
            {
                List<AssemblyFixture.JsonReaderMethod> matches = [.. staticMethods.Where(m => m.ReturnType == dto)];
                using (Assert.Multiple())
                {
                    foreach (AssemblyFixture.JsonReaderMethod m in matches)
                    {
                        await Assert.That(m.Name).IsEqualTo("Get" + dto.Name);
                        await Assert.That(m.DeclaringType.Name).IsEqualTo(dto.Name + "Json");
                        await Assert.That(m.IsDeclaringTypeNotPublic).IsTrue().Because($"{m.Name} must be internal.");
                        await Assert.That(m.IsExtensionMethod).IsTrue().Because($"{m.Name} must be an extension method.");
                        // Parameter type recorded without ref modifier; ensure it's JsonElement
                        await Assert.That(m.FirstParameterType).IsEqualTo(typeof(JsonElement));
                        await Assert.That(m.Namespace).IsEqualTo(dto.Namespace);
                    }
                }
            }
        }
    }
}

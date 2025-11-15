// Removed unused reflection using after switching to generated metadata.

using GuildWars2.Tests.Common;

namespace GuildWars2.ArchitectureTests;

[ClassDataSource<AssemblyFixture>(Shared = SharedType.PerTestSession)]
public class ImmutableDataTest(AssemblyFixture fixture)
{
    [Test]
    public void Data_objects_are_immutable()
    {
        Assert.All(fixture.DataTransferObjectProperties, actual =>
        {
            Assert.True(!actual.HasSetter || actual.IsInitOnly, $"{actual.DeclaringType}.{actual.Name} must be read-only or init-only.");
        });
    }
}

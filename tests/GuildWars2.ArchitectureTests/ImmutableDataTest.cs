// Removed unused reflection using after switching to generated metadata.

using GuildWars2.Tests.Common;

namespace GuildWars2.ArchitectureTests;

[ClassDataSource<AssemblyFixture>(Shared = SharedType.PerTestSession)]
public class ImmutableDataTest(AssemblyFixture fixture)
{
    [Test]
    public async Task Data_objects_are_immutable()
    {
        using (Assert.Multiple())
        {
            foreach (AssemblyFixture.DtProperty actual in fixture.DataTransferObjectProperties)
            {
                bool compliant = !actual.HasSetter || actual.IsInitOnly;
                await Assert.That(compliant).IsTrue()
                    .Because($"{actual.DeclaringType}.{actual.Name} must be read-only or init-only.");

                if (actual.IsCollection)
                {
                    await Assert.That(actual.IsImmutableCollection).IsTrue()
                        .Because($"{actual.DeclaringType}.{actual.Name} must use an immutable collection interface (e.g., IImmutableValueList<T>, IImmutableValueSet<T>, IImmutableValueDictionary<TKey, TValue>).");
                }
            }
        }
    }
}

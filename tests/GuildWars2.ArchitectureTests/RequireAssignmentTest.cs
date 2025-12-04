// Removed unused reflection using after switching to generated metadata.

using GuildWars2.Tests.Common;

namespace GuildWars2.ArchitectureTests;

[ClassDataSource<AssemblyFixture>(Shared = SharedType.PerTestSession)]
public class RequireAssignmentTest(AssemblyFixture fixture)
{
    [Test]
    public async Task Data_transfer_object_members_are_required()
    {
        using (Assert.Multiple())
        {
            foreach (AssemblyFixture.DtProperty actual in fixture.DataTransferObjectProperties)
            {
                bool compliant = actual.IsObsolete || !actual.HasSetter || actual.HasRequiredMemberAttribute;
                await Assert.That(compliant).IsTrue()
                    .Because($"{actual.DeclaringType}.{actual.Name} must be read-only or read-write and marked as 'required'.");
            }
        }
    }
}

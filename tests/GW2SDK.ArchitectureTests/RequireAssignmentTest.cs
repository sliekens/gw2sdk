// Removed unused reflection using after switching to generated metadata.

using GuildWars2.Tests.Common;

namespace GuildWars2.ArchitectureTests;

[ClassDataSource<AssemblyFixture>(Shared = SharedType.PerTestSession)]
public class RequireAssignmentTest(AssemblyFixture fixture)
{
    [Test]
    public void Data_transfer_object_members_are_required()
    {
        Assert.All(fixture.DataTransferObjectProperties, actual =>
        {
            bool compliant = actual.IsObsolete || !actual.HasSetter || actual.HasRequiredMemberAttribute;
            Assert.True(compliant, $"{actual.DeclaringType}.{actual.Name} must be read-only or read-write and marked as 'required'.");
        });
    }
}

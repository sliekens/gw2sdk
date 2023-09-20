using System.Reflection;
using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.PatternsAndPractices;

public class RequireAssignmentTest : IClassFixture<AssemblyFixture>
{
    public RequireAssignmentTest(AssemblyFixture fixture)
    {
        this.fixture = fixture;
    }

    private readonly AssemblyFixture fixture;

    [Fact]
    public void Data_transfer_object_members_are_required()
    {
        Assert.All(
            fixture.DataTransferObjects.SelectMany(type => type.GetProperties()),
            actual =>
            {
                Assert.True(
                    IsRequired(actual),
                    $"{actual.DeclaringType?.Name}.{actual.Name} must be read-only or read-write and marked as 'required'."
                );
            }
        );

        static bool IsRequired(PropertyInfo actual)
        {
            return !actual.CanWrite
                || actual.CustomAttributes.Any(
                    annotation => annotation.AttributeType.Name == "RequiredMemberAttribute"
                );
        }
    }
}

using System.Reflection;

using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.PatternsAndPractices;

public class RequireAssignmentTest(AssemblyFixture fixture) : IClassFixture<AssemblyFixture>
{
    [Fact]
    public void Data_transfer_object_members_are_required()
    {
        Assert.All(
            fixture.DataTransferObjects.SelectMany(type => type.GetProperties()),
            actual =>
            {
                Assert.True(
                    IsCompliant(actual),
                    $"{actual.DeclaringType?.Name}.{actual.Name} must be read-only or read-write and marked as 'required'."
                );
            }
        );

        static bool IsCompliant(PropertyInfo actual)
        {
            if (actual.CustomAttributes.Any(annotation =>
                    annotation.AttributeType == typeof(ObsoleteAttribute)
                ))
            {
                return true;
            }

            return !actual.CanWrite
                || actual.CustomAttributes.Any(annotation =>
                    annotation.AttributeType.Name == "RequiredMemberAttribute"
                );
        }
    }
}

using System.Linq;
using System.Reflection;
using GW2SDK.Tests.TestInfrastructure;
using Xunit;

namespace GW2SDK.Tests.PatternsAndPractices;

public class ImmutableDataTest : IClassFixture<AssemblyFixture>
{
    public ImmutableDataTest(AssemblyFixture fixture)
    {
        this.fixture = fixture;
    }

    private readonly AssemblyFixture fixture;

    [Fact]
    public void Data_objects_are_immutable()
    {
        Assert.All(
            fixture.DataTransferObjects.SelectMany(type => type.GetProperties()),
            actual =>
            {
                Assert.True(IsReadOnly(actual), $"{actual.DeclaringType?.Name}.{actual.Name} must be read only.");
            }
        );

        static bool IsReadOnly(PropertyInfo actual)
        {
            return !actual.CanWrite
                || actual.SetMethod!.ReturnParameter!.GetRequiredCustomModifiers()
                    .Any(modReq => modReq.Name == "IsExternalInit");
        }
    }
}

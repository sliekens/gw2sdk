using System.Reflection;
using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.PatternsAndPractices;

public class DesignedForInheritanceTest(AssemblyFixture fixture) : IClassFixture<AssemblyFixture>
{
    [Fact]
    public void Every_exported_class_is_designed_for_inheritance_or_sealed()
    {
        /*
         * The goal of this test is to ensure that all unsealed types are designed for inheritance.
         */
        var classes = fixture.Assembly.ExportedTypes.Where(type => type.IsClass).ToList();
        Assert.All(
            classes,
            type =>
            {
                if (type.IsAbstract)
                {
                    return;
                }

                if (type.IsSealed)
                {
                    return;
                }

                if (type.GetCustomAttributes()
                    .Any(att => att.GetType().Name == "InheritableAttribute"))
                {
                    return;
                }

                throw new ApplicationException(
                    $"Type '{type}' is not abstract nor sealed, check if it needs to be abstract or sealed or marked as [Inheritable]."
                );
            }
        );
    }

    [Fact]
    public void Every_exported_class_with_InheritableAttribute_has_a_subtype()
    {
        var classes = fixture.Assembly.ExportedTypes.Where(type => type.IsClass).ToList();
        var inheritableClasses = classes.Where(
                type => type.GetCustomAttributes()
                    .Any(att => att.GetType().Name == "InheritableAttribute")
            )
            .ToList();
        Assert.All(
            inheritableClasses,
            type =>
            {
                var subtypes = classes.Where(subtype => subtype.IsSubclassOf(type)).ToList();
                if (subtypes.Count == 0)
                {
                    throw new ApplicationException(
                        $"Type '{type}' is marked as [Inheritable] but has no subtypes."
                    );
                }
            }
        );
    }
}

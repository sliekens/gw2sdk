using System.Reflection;

using GuildWars2.Tests.Common;

namespace GuildWars2.ArchitectureTests;

[ClassDataSource<AssemblyFixture>(Shared = SharedType.PerTestSession)]
public class DesignedForInheritanceTest(AssemblyFixture fixture)
{
    [Test]
    public void Every_exported_class_is_designed_for_inheritance_or_sealed()
    {
        /*
         * The goal of this test is to ensure that all unsealed types are designed for inheritance.
         */
        IEnumerable<Type> classes = fixture.ExportedClasses;
        Assert.All(classes, type =>
        {
            if (type.IsAbstract)
            {
                return;
            }

            if (type.IsSealed)
            {
                return;
            }

            if (type.GetCustomAttributes().Any(att => att.GetType().Name == "InheritableAttribute"))
            {
                return;
            }

            throw new InvalidOperationException($"Type '{type}' is not abstract nor sealed, check if it needs to be abstract or sealed or marked as [Inheritable].");
        });
    }

    [Test]
    public void Every_exported_class_with_InheritableAttribute_has_a_subtype()
    {
        IEnumerable<Type> classes = fixture.ExportedClasses;
        IEnumerable<Type> inheritableClasses = fixture.InheritableClasses;
        Assert.All(inheritableClasses, type =>
        {
            List<Type> subtypes = [.. classes.Where(subtype => subtype.IsSubclassOf(type))];
            if (subtypes.Count == 0)
            {
                throw new InvalidOperationException($"Type '{type}' is marked as [Inheritable] but has no subtypes.");
            }
        });
    }
}

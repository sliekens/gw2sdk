using System.Reflection;

using GuildWars2.Tests.Common;

namespace GuildWars2.ArchitectureTests;

[ClassDataSource<AssemblyFixture>(Shared = SharedType.PerTestSession)]
public class DesignedForInheritanceTest(AssemblyFixture fixture)
{
    [Test]
    public async Task Every_exported_class_is_designed_for_inheritance_or_sealed()
    {
        /*
         * The goal of this test is to ensure that all unsealed types are designed for inheritance.
         */
        IEnumerable<Type> classes = fixture.ExportedClasses;
        using (Assert.Multiple())
        {
            foreach (Type type in classes)
            {
                if (type.IsAbstract)
                {
                    continue;
                }

                if (type.IsSealed)
                {
                    continue;
                }

                if (type.GetCustomAttributes().Any(att => att.GetType().Name == "InheritableAttribute"))
                {
                    continue;
                }

                Assert.Fail($"Type '{type}' is not abstract nor sealed, check if it needs to be abstract or sealed or marked as [Inheritable].");
            }
        }
    }

    [Test]
    public async Task Every_exported_class_with_InheritableAttribute_has_a_subtype()
    {
        IEnumerable<Type> classes = fixture.ExportedClasses;
        IEnumerable<Type> inheritableClasses = fixture.InheritableClasses;
        using (Assert.Multiple())
        {
            foreach (Type type in inheritableClasses)
            {
                List<Type> subtypes = [.. classes.Where(subtype => subtype.IsSubclassOf(type))];
                if (subtypes.Count == 0)
                {
                    Assert.Fail($"Type '{type}' is marked as [Inheritable] but has no subtypes.");
                }
            }
        }
    }
}

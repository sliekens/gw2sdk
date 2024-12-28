using System.Reflection;

namespace GuildWars2.Tests.TestInfrastructure;

// ReSharper disable once ClassNeverInstantiated.Global
public class AssemblyFixture
{
    public Assembly Assembly { get; } = Assembly.Load("GW2SDK");

    public IEnumerable<Type> DataTransferObjects =>
        Assembly.DefinedTypes.Where(type => WithAttribute(type, "DataTransferObjectAttribute"));

    private static bool WithAttribute(MemberInfo type, string attributeName)
    {
        return type.GetCustomAttributes()
            .Any(attribute => attribute.GetType().Name == attributeName);
    }
}

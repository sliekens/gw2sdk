using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace GuildWars2.Tests.TestInfrastructure;

// ReSharper disable once ClassNeverInstantiated.Global
public class AssemblyFixture
{
    public AssemblyFixture()
    {
        Assembly = Assembly.Load("GW2SDK");
    }

    public Assembly Assembly { get; }

    public IEnumerable<Type> DataTransferObjects =>
        Assembly.DefinedTypes.Where(type => WithAttribute(type, "DataTransferObjectAttribute"));

    private static bool WithAttribute(MemberInfo type, string attributeName) =>
        type.GetCustomAttributes().Any(attribute => attribute.GetType().Name == attributeName);
}

using System.Reflection;

namespace GuildWars2.Tests.TestInfrastructure;

public partial class AssemblyFixture
{
    public Assembly Assembly { get; } = Assembly.Load("GW2SDK");
}

using System.Reflection;

namespace GW2SDK.Tests.TestInfrastructure
{
    public class AssemblyFixture
    {
        public AssemblyFixture()
        {
            Assembly = Assembly.Load("GW2SDK");
        }

        public Assembly Assembly { get; }
    }
}

using System.Reflection;

namespace GW2SDK.Tests.TestInfrastructure
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class AssemblyFixture
    {
        public AssemblyFixture()
        {
            Assembly = Assembly.Load("GW2SDK");
        }

        public Assembly Assembly { get; }
    }
}

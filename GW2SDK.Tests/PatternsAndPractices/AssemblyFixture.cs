using System.Reflection;

namespace GW2SDK.Tests.PatternsAndPractices
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

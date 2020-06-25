using System;
using System.Linq;
using System.Reflection;
using GW2SDK.Annotations;
using GW2SDK.Tests.TestInfrastructure;
using Xunit;

namespace GW2SDK.Tests.PatternsAndPractices
{
    public class DesignedForInheritanceTest : IClassFixture<AssemblyFixture>
    {
        public DesignedForInheritanceTest(AssemblyFixture fixture)
        {
            _fixture = fixture;
        }

        private readonly AssemblyFixture _fixture;

        [Fact]
        public void Every_exported_class_is_designed_for_inheritance_or_sealed()
        {
            var classes = _fixture.Assembly.ExportedTypes.Where(type => type.IsClass).ToList();
            foreach (var type in classes)
            {
                if (type.IsAbstract)
                {
                    continue;
                }

                if (type.IsSealed)
                {
                    continue;
                }

                if (type.GetCustomAttribute<InheritableAttribute>() is object)
                {
                    continue;
                }

                throw new ApplicationException($"Type '{type}' is public but not abstract, seal it or mark it as [Inheritable].");
            }
        }
    }
}

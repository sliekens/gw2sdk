using System;
using System.Linq;
using System.Reflection;
using GW2SDK.Infrastructure;
using Xunit;

namespace GW2SDK.Tests.PatternsAndPractices
{
    public class DesignedForInheritanceTest : IClassFixture<AssemblyFixture>
    {
        private readonly AssemblyFixture _fixture;

        public DesignedForInheritanceTest(AssemblyFixture fixture)
        {
            _fixture = fixture;
        }


        [Fact]
        public void EveryExportedClass_ShouldBeInheritableOrSealed()
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

                throw new ApplicationException(
                    $"Type '{type}' is public but not abstract, seal it or mark it as [Inheritable].");
            }
        }
    }
}
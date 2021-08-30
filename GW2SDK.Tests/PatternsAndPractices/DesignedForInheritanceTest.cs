using System;
using System.Linq;
using System.Reflection;
using GW2SDK.Tests.TestInfrastructure;
using Xunit;

namespace GW2SDK.Tests.PatternsAndPractices
{
    public class DesignedForInheritanceTest : IClassFixture<AssemblyFixture>
    {
        public DesignedForInheritanceTest(AssemblyFixture fixture)
        {
            this.fixture = fixture;
        }

        private readonly AssemblyFixture fixture;

        [Fact]
        public void Every_exported_class_is_designed_for_inheritance_or_sealed()
        {
            /*
             * The goal of this test is to ensure that all unsealed types are designed for inheritance.
             */
            var classes = fixture.Assembly.ExportedTypes.Where(type => type.IsClass).ToList();
            Assert.All(classes,
                type =>
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

                    throw new ApplicationException($"Type '{type}' is public but not abstract, seal it or mark it as [Inheritable].");
                });
        }
    }
}

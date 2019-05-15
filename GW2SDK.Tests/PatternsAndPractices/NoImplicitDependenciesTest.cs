using System;
using System.Linq;
using System.Reflection;
using GW2SDK.Infrastructure;
using Xunit;

namespace GW2SDK.Tests.PatternsAndPractices
{
    public class NoImplicitDependenciesTest : IClassFixture<AssemblyFixture>
    {
        public NoImplicitDependenciesTest(AssemblyFixture fixture)
        {
            _fixture = fixture;
        }

        private readonly AssemblyFixture _fixture;

        [Fact]
        public void EveryConstructor_ShouldBeExplicitAboutNullability()
        {
            var classes = _fixture.Assembly.GetTypes().Where(type => type.IsClass).ToList();
            foreach (var @class in classes)
            foreach (var constructorInfo in @class.GetConstructors())
            foreach (var parameterInfo in constructorInfo.GetParameters())
            {
                if (parameterInfo.ParameterType.IsValueType)
                {
                    continue;
                }

                Assert.True(parameterInfo.GetCustomAttribute<NotNullAttribute>() is object
                            || parameterInfo.GetCustomAttribute<CanBeNullAttribute>() is object,
                    $"Mark parameter '{parameterInfo}' as [NotNull] or [CanBeNull] in constructors of type '{@class}'.");
            }
        }
    }
}
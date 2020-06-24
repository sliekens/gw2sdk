using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using GW2SDK.Tests.TestInfrastructure;
using Xunit;

namespace GW2SDK.Tests.PatternsAndPractices
{
    public class SensibleDefaultsTest : IClassFixture<AssemblyFixture>
    {
        public SensibleDefaultsTest(AssemblyFixture fixture)
        {
            _fixture = fixture;
        }

        private readonly AssemblyFixture _fixture;

        [Fact]
        public void Every_default_enum_member_is_intentional()
        {
            /*
             * The goal of this test is to ensure that all enums have a sensible default value, or no default value.
             * This avoids arbitrary defaults like in System.DayOfWeek.
             *
             * DayOfWeek day = default;
             * => Sunday
             *
             */
            var enums = _fixture.Assembly.ExportedTypes.Where(type => type.IsEnum).ToList();
            Assert.All(enums,
                type =>
                {
                    if (Enum.IsDefined(type, 0))
                    {
                        var annotation = type.GetCustomAttribute<DefaultValueAttribute>();
                        if (annotation is null)
                        {
                            throw new ApplicationException($"Enum '{type}' has an implicit default value, change its value or mark it as [DefaultValue].");
                        }

                        if (annotation.Value is null || annotation.Value.GetType() != type)
                        {
                            throw new ApplicationException($"Enum '{type}' has a [DefaultValue] with an invalid type, use the enum's type.");
                        }

                        if (!Enum.IsDefined(type, annotation.Value))
                        {
                            throw new ApplicationException($"Enum '{type}' has a [DefaultValue] that does not exist, adjust or remove the attribute.");
                        }
                    }
                });
        }
    }
}

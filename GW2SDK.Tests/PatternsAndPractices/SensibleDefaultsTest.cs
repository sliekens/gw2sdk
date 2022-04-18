using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using GW2SDK.Tests.TestInfrastructure;
using Xunit;

namespace GW2SDK.Tests.PatternsAndPractices;

public class SensibleDefaultsTest : IClassFixture<AssemblyFixture>
{
    public SensibleDefaultsTest(AssemblyFixture fixture)
    {
        this.fixture = fixture;
    }

    private readonly AssemblyFixture fixture;

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
        var enums = fixture.Assembly.ExportedTypes.Where(type => type.IsEnum).ToList();
        Assert.All(
            enums,
            type =>
            {
                if (HasDefaultMember(type))
                {
                    var annotation = type.GetCustomAttribute<DefaultValueAttribute>();
                    if (annotation is null)
                    {
                        throw new ApplicationException(
                            $"Enum '{type}' has an implicit default value, change its value or mark it as [DefaultValue]."
                            );
                    }

                    if (annotation.Value is null || annotation.Value.GetType() != type)
                    {
                        throw new ApplicationException(
                            $"Enum '{type}' has a [DefaultValue] with an invalid type, use the enum's type."
                            );
                    }

                    if (!Enum.IsDefined(type, annotation.Value))
                    {
                        throw new ApplicationException(
                            $"Enum '{type}' has a [DefaultValue] that does not exist, adjust or remove the attribute."
                            );
                    }
                }
            }
            );

        static bool HasDefaultMember(Type enumType)
        {
            var underlyingType = Enum.GetUnderlyingType(enumType);
            if (underlyingType == typeof(int))
            {
                return Enum.IsDefined(enumType, 0);
            }

            if (underlyingType == typeof(uint))
            {
                return Enum.IsDefined(enumType, uint.MinValue);
            }

            throw new NotSupportedException("Enum type not supported");
        }
    }
}

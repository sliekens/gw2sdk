using System.ComponentModel;
using System.Reflection;

using GuildWars2.Tests.Common;

namespace GuildWars2.ArchitectureTests;

[ClassDataSource<AssemblyFixture>(Shared = SharedType.PerTestSession)]
public class SensibleDefaultsTest(AssemblyFixture fixture)
{
    [Test]
    public async Task Every_default_enum_member_is_intentional()
    {
        /*
         * The goal of this test is to ensure that all enums have a sensible default value, or no default value.
         * This avoids arbitrary defaults like in System.DayOfWeek.
         *
         * DayOfWeek day = default;
         * => Sunday
         *
         */
        IEnumerable<Type> enums = fixture.ExportedEnums;
        using (Assert.Multiple())
        {
            foreach (Type type in enums)
            {
                if (HasDefaultMember(type))
                {
                    DefaultValueAttribute? annotation = type.GetCustomAttribute<DefaultValueAttribute>();
                    if (annotation is null)
                    {
                        Assert.Fail($"Enum '{type}' has an implicit default value, change its value or mark it as [DefaultValue].");
                    }
                    else if (annotation.Value is null || annotation.Value.GetType() != type)
                    {
                        Assert.Fail($"Enum '{type}' has a [DefaultValue] with an invalid type, use the enum's type.");
                    }
                    else if (!Enum.IsDefined(type, annotation.Value))
                    {
                        Assert.Fail($"Enum '{type}' has a [DefaultValue] that does not exist, adjust or remove the attribute.");
                    }
                }
            }
        }

        static bool HasDefaultMember(Type enumType)
        {
            Type underlyingType = Enum.GetUnderlyingType(enumType);
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

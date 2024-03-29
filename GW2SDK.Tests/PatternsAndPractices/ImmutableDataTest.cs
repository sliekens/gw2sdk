﻿using System.Reflection;
using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.PatternsAndPractices;

public class ImmutableDataTest(AssemblyFixture fixture) : IClassFixture<AssemblyFixture>
{
    [Fact]
    public void Data_objects_are_immutable()
    {
        Assert.All(
            fixture.DataTransferObjects.SelectMany(type => type.GetProperties()),
            actual =>
            {
                Assert.True(
                    IsReadOnly(actual),
                    $"{actual.DeclaringType?.Name}.{actual.Name} must be read-only or init-only."
                );
            }
        );

        static bool IsReadOnly(PropertyInfo actual)
        {
            return !actual.CanWrite
                || actual.SetMethod!.ReturnParameter!.GetRequiredCustomModifiers()
                    .Any(modReq => modReq.Name == "IsExternalInit");
        }
    }
}
